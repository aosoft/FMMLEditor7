using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	enum CompilerType : int
	{
		Unknown = 0,
		FMP7,
		FMPv4,
		PMD
	}

	enum CompiledFileType : int
	{
		Unknown = 0,
		FMP7_owi,
		FMPv4_opi,
		FMPv4_ovi,
		FMPv4_ozi,
		PMD_m
	}

	struct CompileInfo
	{
		public CompilerType CompilerType
		{
			get;
			private set;
		}

		public CompiledFileType CompiledFileType
		{
			get;
			private set;
		}

		public CompileInfo(CompilerType compilerType, CompiledFileType compiledFileType)
		{
			CompilerType = compilerType;
			CompiledFileType = compiledFileType;
		}
	}

	struct CompileResult
	{
		public CompileResult(FMC7Result r, string compiledFilePath, string consoleOut)
		{
			FMC7Result = r;
			CompiledFilePath = compiledFilePath;
			ConsoleOut = consoleOut;
		}

		public FMC7Result FMC7Result
		{
			get;
			private set;
		}

		/// <summary>
		/// コンパイルされた曲データバイナリのファイルパス
		/// </summary>
		public string CompiledFilePath
		{
			get;
			private set;
		}

		/// <summary>
		/// コンソール出力そのもの
		/// FMC7 では設定されない。
		/// </summary>
		public string ConsoleOut
		{
			get;
			private set;
		}
	}

	class Compiler : IDisposable
	{
		private FMP7Compiler _compilerFMC7 = new FMP7Compiler();
		private Settings _setting = null;

		public Compiler(Settings setting)
		{
			_setting = setting;
		}

		public void Dispose()
		{
			_compilerFMC7?.Dispose();
			_compilerFMC7 = null;
			_setting = null;
		}

		public bool IsInitializedFMC7
		{
			get
			{
				return _compilerFMC7.IsInitialized;
			}
		}

		public string DllPathFMC7
		{
			get
			{
				return _compilerFMC7.DllPath;
			}
		}

		public void InitializeFMC7()
		{
			_compilerFMC7.FinalizeFMC();
			_compilerFMC7.InitializeFMC(_setting.FMC7Path);
		}

		public CompileResult Compile(string mmlPath, bool compileAndPlay)
		{
			var ci = GetCompilerType(mmlPath);
			switch (ci.CompilerType)
			{
				case CompilerType.FMP7:
					{
						if (_compilerFMC7.IsInitialized == false)
						{
							throw new Exception(MMLEditorResource.Error_NotInitializedCompiler);
						}

						return new CompileResult(
							_compilerFMC7.Compile(mmlPath, compileAndPlay),
							GetCompiledFilePath(mmlPath),
							null);
					}

				case CompilerType.FMPv4:
				case CompilerType.PMD:
					{
						return StartCompilerProcess(ci.CompilerType, mmlPath, compileAndPlay);
					}
			}

			return new CompileResult(new FMC7Result(FMC7Status.ErrorNoData, null), null, null);
		}

		private CompileResult StartCompilerProcess(CompilerType ct, string mmlPath, bool compileAndPlay)
		{
			CheckExePath(_setting.MSDOSPlayerPath);

			string compilerExe = null;
			switch (ct)
			{
				case CompilerType.FMPv4:
					{
						compilerExe = _setting.FMCPath;
					}
					break;

				case CompilerType.PMD:
					{
						compilerExe = _setting.MCPath;
					}
					break;

				default:
					{
						throw new Exception(MMLEditorResource.Error_NotSettingCompilerExe);
					}
			}

			CheckExePath(compilerExe);

			var psi = new ProcessStartInfo();
			psi.FileName = _setting.MSDOSPlayerPath;
			psi.Arguments = string.Format("\"{0}\" \"{1}\"", compilerExe, mmlPath);
			psi.WorkingDirectory = Path.GetDirectoryName(mmlPath);
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;

			using (var p = Process.Start(psi))
			{
				var stdout = p.StandardOutput.ReadToEnd()?.Trim();
				var stderr = p.StandardError.ReadToEnd()?.Trim();
				p.WaitForExit();

				//	compileAndPlay の場合は ErrorPlay を返すことにより
				//	呼び出し元で再生開始処理を行わせる。
				return new CompileResult(
					new FMC7Result(
						p.ExitCode == 0 ?
							compileAndPlay ? FMC7Status.ErrorPlay : FMC7Status.Success :
							FMC7Status.ErrorCompile,
						GetFMC7InfoFromErrorString(
							mmlPath,
							ct == CompilerType.FMPv4 ? stderr : stdout)),
					GetCompiledFilePath(mmlPath),
					string.Format("{0}{2}{2}{1}", stderr, stdout, Environment.NewLine).Trim());
			}
		}

		private FMC7Info[] GetFMC7InfoFromErrorString(string mmlPath, string msgstr)
		{
			if (msgstr == null)
			{
				return null;
			}
			var lines = msgstr.Split(
				new string[]{ Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);
			var fileName = Path.GetFileName(mmlPath);
			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i]?.Trim();
				if (string.IsNullOrEmpty(line))
				{
					continue;
				}

				if (line.IndexOf(fileName, StringComparison.OrdinalIgnoreCase) < 0)
				{
					continue;
				}

				if ((i + 1) == lines.Length)
				{
					break;
				}

				var index1 = line.IndexOf('(') + 1;
				var index2 = line.IndexOf(')');
				if (index1 < 0 || index2 < 0 || index1 > index2)
				{
					break;
				}

				int linenumber;
				var ls = line.Substring(index1, index2 - index1);
				if (int.TryParse(ls, out linenumber) == false)
				{
					break;
				}

				var log = new FMC7Log();
				log.Kind = FMC7LogKind.Error;
				log.FileName = fileName;
				log.Line = linenumber;
				log.Message = lines[i + 1];

				return new FMC7Info[] { new FMC7Info(log) };
			}

			return null;
		}


		static public CompileInfo GetCompilerType(string mmlPath)
		{
			try
			{
				switch (Path.GetExtension(mmlPath).ToLower())
				{
					case ".mwi":
						{
							return new CompileInfo(CompilerType.FMP7, CompiledFileType.FMP7_owi);
						}

					case ".mpi":
						{
							return new CompileInfo(CompilerType.FMPv4, CompiledFileType.FMPv4_opi);
						}

					case ".mvi":
						{
							return new CompileInfo(CompilerType.FMPv4, CompiledFileType.FMPv4_ovi);
						}

					case ".mzi":
						{
							return new CompileInfo(CompilerType.FMPv4, CompiledFileType.FMPv4_ozi);
						}

					case ".mml":
						{
							return new CompileInfo(CompilerType.PMD, CompiledFileType.PMD_m);
						}
				}
			}
			catch
			{
			}
			return new CompileInfo(CompilerType.Unknown, CompiledFileType.Unknown);
		}

		static private void CheckExePath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new Exception(MMLEditorResource.Error_NotSettingCompilerExe);
			}

			if (File.Exists(path) == false)
			{
				throw new Exception(string.Format(MMLEditorResource.Error_NotExists, path));
			}
		}

		static public string GetCompiledFilePath(string mmlPath)
		{
			try
			{
				var basepath =
					Path.Combine(
						Path.GetDirectoryName(mmlPath),
						Path.GetFileNameWithoutExtension(mmlPath));

				switch (GetCompilerType(mmlPath).CompiledFileType)
				{
					case CompiledFileType.FMP7_owi:
						{
							return basepath + ".owi";
						}

					case CompiledFileType.FMPv4_opi:
						{
							return basepath + ".opi";
						}

					case CompiledFileType.FMPv4_ovi:
						{
							return basepath + ".ovi";
						}

					case CompiledFileType.FMPv4_ozi:
						{
							return basepath + ".ozi";
						}

					case CompiledFileType.PMD_m:
						{
							//	ソースを読み込み、中の #Filename をチェックする

							var path = GetPMDFilenameOptionValue(mmlPath);
							if (path != null)
							{
								return path;
							}

							return basepath + ".m";
						}
				}
			}
			catch
			{
			}
			return null;
		}

		static private string GetPMDFilenameOptionValue(string pmdMmlPath)
		{
			try
			{
				string filename = null;
				using (var sr = new StreamReader(pmdMmlPath))
				{
					while (true)
					{
						var l = sr.ReadLine();
						if (l == null)
						{
							break;
						}
						var splits = l.Split(' ', '\t');

						int count = splits.Length - 1;
						for (int i = 0; i < count; i++)
						{
							if (splits[i] != null &&
								splits[i].Equals("#Filename", StringComparison.OrdinalIgnoreCase))
							{
								filename = splits[i + 1];
								break;
							}
						}
					}
				}

				if (string.IsNullOrEmpty(filename) == false)
				{
					if (filename[0] == '.')
					{
						//	指定が拡張子のみ
						return
							Path.Combine(
								Path.GetDirectoryName(pmdMmlPath),
								Path.GetFileNameWithoutExtension(pmdMmlPath) + filename);
					}
					else
					{
						//	指定がファイル名
						return
							Path.Combine(
								Path.GetDirectoryName(pmdMmlPath),
								filename);
					}
				}
			}
			catch
			{
			}

			return null;
		}
	}
}
