using System;
using System.Collections.Generic;
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
		private FMPCompiler _compilerFMC7 = new FMPCompiler();
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

		public FMCResult Compile(string mmlPath, bool compileAndPlay)
		{
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
