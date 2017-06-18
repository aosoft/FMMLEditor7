using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	class MMLAnalyzer
	{
		public MMLInfo Info
		{
			get;
			private set;
		}

		public string MMLFilePath
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

		private MMLAnalyzer()
		{
		}

		static public MMLAnalyzer Analyze(string mmlPath)
		{
			var ret = new MMLAnalyzer();

			ret.MMLFilePath = mmlPath;
			var basepath =
				Path.Combine(
					Path.GetDirectoryName(mmlPath),
					Path.GetFileNameWithoutExtension(mmlPath));

			//	CompilerType / FlieExtType
			ret.Info = MMLInfo.GetMMLInfo(mmlPath);
			switch (ret.Info.CompilerType)
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
							ret.Info = new MMLInfo(
								ret.Info.CompilerType, ret.Info.MMLFileExtType, CompiledFileExtType.FMPv4_ozi);
						}

						switch (ret.Info.CompiledFileExtType)
						{
							case CompiledFileExtType.FMPv4_opi:
								{
									ret.CompiledFilePath = basepath + ".opi";
								}
								break;

							case CompiledFileExtType.FMPv4_ovi:
								{
									ret.CompiledFilePath = basepath + ".ovi";
								}
								break;

							case CompiledFileExtType.FMPv4_ozi:
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
