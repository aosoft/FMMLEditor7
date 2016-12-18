using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FMMLEditor7
{
	[Flags]
	enum LoadLibraryFlags : uint
	{
		None = 0,
		DontResolveDllReferences = 0x1,
		LoadLibraryAsDatafile = 0x2,
		LoadWithAlteredSearchPath = 0x8,
		IgnoreCodeAuthorizationLevel = 0x10,
		LoadLibraryAsDatafileExclusive = 0x40,
		LoadLibraryAsImageResource = 0x20
	}

	class Kernel32Wrapper
	{
		private const string m_dllName = "kernel32.dll";

		[DllImport(m_dllName, EntryPoint = "LoadLibraryExW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = false)]
		public static extern IntPtr LoadLibraryEx(string fileName, IntPtr reserved, LoadLibraryFlags flag);

		[DllImport(m_dllName, SetLastError = true)]
		public static extern bool FreeLibrary(IntPtr module);

		[DllImport(m_dllName, CharSet = CharSet.Ansi, ExactSpelling = true)]
		public static extern IntPtr GetProcAddress(IntPtr module, string procName);

		public static TDelegate GetUnmangedFunc<TDelegate>(IntPtr module, string procName)
			where TDelegate : class
		{
			IntPtr p = GetProcAddress(module, procName);

			if (p == IntPtr.Zero)
			{
				throw new ArgumentException();
			}

			var ret = Marshal.GetDelegateForFunctionPointer(p, typeof(TDelegate)) as TDelegate;
			if (ret == null)
			{
				throw new ArgumentException();
			}
			return ret;
		}
	}
}
