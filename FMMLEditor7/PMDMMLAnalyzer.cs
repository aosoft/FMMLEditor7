using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	struct PMDMMLAnalyzer
	{
		public string CompiledFileName
		{
			get;
			set;
		}

		static public PMDMMLAnalyzer Analyze(string mmlPath)
		{
			var ret = new PMDMMLAnalyzer();

			using (var sr = new StreamReader(mmlPath))
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
							ret.CompiledFileName = splits[i + 1];
							break;
						}
					}
				}
			}

			return ret;
		}
	}
}
