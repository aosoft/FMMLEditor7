using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMLEditor7
{
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

		public void InitializeFMC7()
		{
			_compilerFMC7.FinalizeFMC();
			_compilerFMC7.InitializeFMC(_setting.FMC7Path);
		}


		public bool IsInitializedFMC7
		{
			get
			{
				return _compilerFMC7.IsInitialized;
			}
		}
	}
}
