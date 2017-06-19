using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{


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
			var ci = MMLAnalyzer.Analyze(mmlPath);
			switch (ci.Info.CompilerType)
			{
				case CompilerType.FMP7:
					{
						if (_compilerFMC7.IsInitialized == false)
						{
							try
							{
								InitializeFMC7();
							}
							catch (Exception e)
							{
								throw new Exception(
									string.Format(
										MMLEditorResource.Error_LoadCompilerModule,
										e.Message));
							}

							InitializeFMC7();
						}

						return new CompileResult(
							_compilerFMC7.Compile(mmlPath, compileAndPlay),
							ci.CompiledFilePath,
							null);
					}

				case CompilerType.FMPv4:
					{
						return StartCompilerProcess(ci, compileAndPlay, true);
					}

				case CompilerType.PMD:
					{
						return StartCompilerProcess(ci, compileAndPlay, false);
					}
			}

			return new CompileResult(new FMC7Result(FMC7Status.ErrorNoData, null), null, null);
		}

		private CompileResult StartCompilerProcess(MMLAnalyzer ci, bool compileAndPlay, bool redirectStderr)
		{
			CheckExePath(_setting.MSDOSPlayerPath);

			string compilerExe = null;
			switch (ci.Info.CompilerType)
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
			psi.Arguments = string.Format("\"{0}\" \"{1}\"", compilerExe, ci.MMLFilePath);
			psi.WorkingDirectory = Path.GetDirectoryName(ci.MMLFilePath);
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = !redirectStderr;
			psi.RedirectStandardError = redirectStderr;

			using (var p = Process.Start(psi))
			{
				var stdout = (redirectStderr ? p.StandardError : p.StandardOutput).ReadToEnd()?.Trim();
				p.WaitForExit();

				//	compileAndPlay の場合は ErrorPlay を返すことにより
				//	呼び出し元で再生開始処理を行わせる。
				return new CompileResult(
					new FMC7Result(
						p.ExitCode == 0 ?
							compileAndPlay ? FMC7Status.ErrorPlay : FMC7Status.Success :
							FMC7Status.ErrorCompile,
						GetFMC7InfoFromErrorString(ci, stdout)),
					ci.CompiledFilePath,
					stdout.Trim());
			}
		}

		private List<FMC7Info> GetFMC7InfoFromErrorString(MMLAnalyzer analyzer, string msgstr)
		{
			if (msgstr == null)
			{
				return null;
			}

			var fileName = Path.GetFileName(analyzer.MMLFilePath);
			var ret = new List<FMC7Info>();

			if (analyzer.FMPMML != null &&
				analyzer.Info.MMLFileExtType == MMLFileExtType.FMPv4_mzi &&
				analyzer.FMPMML.PPZPCMFile != null &&
				analyzer.FMPMML.UseOPNASpec &&
				analyzer.FMPMML.UseFM3Extend == false)
			{
				var log = new FMC7Log();
				log.FileName = fileName;
				log.Kind = FMC7LogKind.Warning;
				log.Message = MMLEditorResource.Warning_OPNAPPZ;
				ret.Add(new FMC7Info(log));
			}

			var lines = msgstr.Split(
				new string[]{ Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);
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

				ret.Add(new FMC7Info(log));
			}

			return ret;
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


	}
}
