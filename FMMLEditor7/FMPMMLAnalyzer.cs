using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	class FMPMMLAnalyzer
	{
		public string ADPCMPCMFile
		{
			get;
			private set;
		}

		public string PPZPCMFile
		{
			get;
			private set;
		}

		public bool UseOPNASpec
		{
			get;
			private set;
		}

		public bool UseFM3Extend
		{
			get;
			private set;
		}

		static public FMPMMLAnalyzer Analyze(string mmlPath)
		{
			var ret = new FMPMMLAnalyzer();

			using (var sr = new StreamReader(mmlPath))
			{
				while (true)
				{
					var l = sr.ReadLine();
					if (l == null)
					{
						break;
					}

					var splits = l.Trim().Split(' ', '\t');
					if (splits.Length < 2 ||
						splits[0][0] != '\'')
					{
						continue;
					}

					foreach (var c in splits[0])
					{
						switch (c)
						{
							case '$':
								{
									ret.PPZPCMFile = splits.Length == 2 ? splits[1] : null;
								}
								break;

							case '#':
								{
									ret.ADPCMPCMFile = splits.Length == 2 ? splits[1] : null;
								}
								break;

							case 'G':
							case 'H':
							case 'I':
							case 'J':
							case 'K':
								{
									ret.UseOPNASpec = true;
								}
								break;

							case 'X':
							case 'Y':
							case 'Z':
								{
									ret.UseFM3Extend = true;
								}
								break;
						}
					}
				}
			}

			return ret;
		}
	}
}
