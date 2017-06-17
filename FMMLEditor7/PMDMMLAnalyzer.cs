using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	class PMDMMLAnalyzer
	{
		public string CompiledFileName
		{
			get;
			set;
		}

		static public PMDMMLAnalyzer Analyze(string mmlPath)
		{
			var ret = new PMDMMLAnalyzer();

			string filename = null;
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
					ret.CompiledFileName =
						Path.Combine(
							Path.GetDirectoryName(mmlPath),
							Path.GetFileNameWithoutExtension(mmlPath) + filename);
				}
				else
				{
					//	指定がファイル名
					ret.CompiledFileName =
						Path.Combine(
							Path.GetDirectoryName(mmlPath),
							filename);
				}
			}

			return ret;
		}
	}
}
