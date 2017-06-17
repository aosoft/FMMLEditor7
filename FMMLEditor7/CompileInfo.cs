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
}
