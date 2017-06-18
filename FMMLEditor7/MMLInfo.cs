using System;
using System.Collections.Generic;
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

	enum MMLFileExtType : int
	{
		Unknown = 0,
		FMP7_mwi,
		FMPv4_mpi,
		FMPv4_mvi,
		FMPv4_mzi,
		PMD_mml
	}

	enum CompiledFileExtType : int
	{
		Unknown = 0,
		FMP7_owi,
		FMPv4_opi,
		FMPv4_ovi,
		FMPv4_ozi,
		PMD_m
	}

	struct MMLInfo
	{
		public CompilerType CompilerType
		{
			get;
			private set;
		}

		public MMLFileExtType MMLFileExtType
		{
			get;
			private set;
		}

		public CompiledFileExtType CompiledFileExtType
		{
			get;
			private set;
		}

		public MMLInfo(
			CompilerType compilerType,
			MMLFileExtType mmlfileExtType,
			CompiledFileExtType compiledFileExtType)
		{
			CompilerType = compilerType;
			MMLFileExtType = mmlfileExtType;
			CompiledFileExtType = compiledFileExtType;
		}

		static public MMLInfo GetMMLInfo(string mmlPath)
		{
			var ret = new MMLInfo();

			switch (System.IO.Path.GetExtension(mmlPath).ToLower())
			{
				case ".mwi":
					{
						ret.CompilerType = CompilerType.FMP7;
						ret.MMLFileExtType = MMLFileExtType.FMP7_mwi;
						ret.CompiledFileExtType = CompiledFileExtType.FMP7_owi;
					}
					break;

				case ".mpi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.MMLFileExtType = MMLFileExtType.FMPv4_mpi;
						ret.CompiledFileExtType = CompiledFileExtType.FMPv4_opi;
					}
					break;

				case ".mvi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.MMLFileExtType = MMLFileExtType.FMPv4_mvi;
						ret.CompiledFileExtType = CompiledFileExtType.FMPv4_ovi;
					}
					break;

				case ".mzi":
					{
						ret.CompilerType = CompilerType.FMPv4;
						ret.MMLFileExtType = MMLFileExtType.FMPv4_mzi;
						ret.CompiledFileExtType = CompiledFileExtType.FMPv4_ozi;
					}
					break;

				case ".mml":
					{
						ret.CompilerType = CompilerType.PMD;
						ret.MMLFileExtType = MMLFileExtType.PMD_mml;
						ret.CompiledFileExtType = CompiledFileExtType.PMD_m;
					}
					break;

				default:
					{
						ret.CompilerType = CompilerType.Unknown;
						ret.MMLFileExtType = MMLFileExtType.Unknown;
						ret.CompiledFileExtType = CompiledFileExtType.Unknown;
					}
					break;
			}
			return ret;
		}
	}
}
