﻿using System;
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


		public void InitializeFMC7()
		{
			_compilerFMC7.FinalizeFMC();
			_compilerFMC7.InitializeFMC(_setting.FMC7Path);
		}

		public FMC7Result Compile(string mmlPath, bool compileAndPlay)
		{
			var ci = GetCompilerType(mmlPath);
			switch (ci.CompilerType)
			{
				case CompilerType.Unknown:
					{
						return new FMC7Result(FMC7Status.ErrorNoData, null, null);
					}

				case CompilerType.FMP7:
					{
						return _compilerFMC7.Compile(mmlPath, compileAndPlay);
					}
					break;

				case CompilerType.FMPv4:
					{
					}
					break;
			}

			return null;
		}

		private FMC7Result StartCompilerProcess(CompilerType ct, string mmlPath)
		{
			if (string.IsNullOrEmpty(_setting.MSDOSPlayerPath))
			{
				return new FMC7Result(FMC7Status.ErrorCompile, null, null);
			}

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
						return new FMC7Result(FMC7Status.ErrorCompile, null, null);
					}
			}

			var psi = new ProcessStartInfo();
			psi.FileName = _setting.MSDOSPlayerPath;
			psi.Arguments = string.Format("\"{0}\" \"{1}\"", compilerExe, mmlPath);
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.RedirectStandardOutput = true;

			using (var p = Process.Start(psi))
			{
				string s = p.StandardOutput.ReadToEnd();
			}

			return new FMC7Result(FMC7Status.ErrorCompile, null, null);
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
