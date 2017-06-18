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

	class CompileInfo
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

		public string MmlFilePath
		{
			get;
			private set;
		}

		public string CompiledFilePath
		{
			get;
			private set;
		}

		public FMPMMLAnalyzer FMPMML
		{
			get;
			private set;
		}

		public PMDMMLAnalyzer PMDMML
		{
			get;
			private set;
		}

		private CompileInfo()
		{
		}

		static public CompileInfo Analyze(string mmlPath)
		{
			var ret = new CompileInfo();

			ret.MmlFilePath = mmlPath;
			var basepath =
				Path.Combine(
					Path.GetDirectoryName(mmlPath),
					Path.GetFileNameWithoutExtension(mmlPath));

			//	CompilerType / CompiledFileType
			switch (Path.GetExtension(mmlPath).ToLower())
			{
				case ".mwi":
					{
						ret.CompilerType = CompilerType.FMP7;
						ret.CompiledFileType = CompiledFileType.FMP7_owi;
					}
					break;

				case ".mpi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.CompiledFileType = CompiledFileType.FMPv4_opi;
					}
					break;

				case ".mvi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.CompiledFileType = CompiledFileType.FMPv4_ovi;
					}
					break;

				case ".mzi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.CompiledFileType = CompiledFileType.FMPv4_ozi;
					}
					break;

				case ".mml":
					{
						ret.CompilerType = CompilerType.PMD;
						ret.CompiledFileType = CompiledFileType.PMD_m;
					}
					break;

				default:
					{
						ret.CompilerType = CompilerType.Unknown;
						ret.CompiledFileType = CompiledFileType.Unknown;
					}
					break;
			}

			switch (ret.CompilerType)
			{
				case CompilerType.FMP7:
					{
						ret.CompiledFilePath = basepath + ".owi";
					}
					break;

				case CompilerType.FMPv4:
					{
						ret.FMPMML = FMPMMLAnalyzer.Analyze(mmlPath);
						if (ret.FMPMML.PPZPCMFile != null)
						{
							ret.CompiledFileType = CompiledFileType.FMPv4_ozi;
						}

						switch (ret.CompiledFileType)
						{
							case CompiledFileType.FMPv4_opi:
								{
									ret.CompiledFilePath = basepath + ".opi";
								}
								break;

							case CompiledFileType.FMPv4_ovi:
								{
									ret.CompiledFilePath = basepath + ".ovi";
								}
								break;

							case CompiledFileType.FMPv4_ozi:
								{
									ret.CompiledFilePath = basepath + ".ozi";
								}
								break;
						}
					}
					break;

				case CompilerType.PMD:
					{
						ret.PMDMML = PMDMMLAnalyzer.Analyze(mmlPath);

						ret.CompiledFilePath = ret.PMDMML.CompiledFileName;
						if (ret.CompiledFilePath == null)
						{
							ret.CompiledFilePath = basepath + ".m";
						}
					}
					break;

			}

			return ret;
		}


	}
}
