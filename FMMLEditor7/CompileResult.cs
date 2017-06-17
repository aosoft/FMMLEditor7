using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
	struct CompileResult
	{
		public CompileResult(FMC7Result r, string compiledFilePath, string consoleOut)
		{
			FMC7Result = r;
			CompiledFilePath = compiledFilePath;
			ConsoleOut = consoleOut;
		}

		public FMC7Result FMC7Result
		{
			get;
			private set;
		}

		/// <summary>
		/// コンパイルされた曲データバイナリのファイルパス
		/// </summary>
		public string CompiledFilePath
		{
			get;
			private set;
		}

		/// <summary>
		/// コンソール出力そのもの
		/// FMC7 では設定されない。
		/// </summary>
		public string ConsoleOut
		{
			get;
			private set;
		}
	}
}
