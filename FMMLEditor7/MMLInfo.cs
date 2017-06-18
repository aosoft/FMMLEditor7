using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	struct MMLInfo
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

		public MMLInfo(CompilerType compilerType, CompiledFileType compiledFileType)
		{
			CompilerType = compilerType;
			CompiledFileType = compiledFileType;
		}

		static public MMLInfo GetMMLInfo(string mmlPath)
		{
			var ret = new MMLInfo();

			switch (System.IO.Path.GetExtension(mmlPath).ToLower())
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
			return ret;
		}
	}
}
