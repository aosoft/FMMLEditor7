using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using FMP.FMP7;
using System.IO;

namespace FMMLEditor7
{
	[Flags]
	public enum FMCCompileFlag
	{
		None = 0,
		OnlyPrepro = 0x00000001,	// 展開チェックのみ
		OnlyCompile = 0x00000002,	// コンパイルチェックのみ
		PlayAfter = 0x00000100,	// コンパイル成功後自動演奏
	}

	public enum FMCStatus
	{
		Success = 0,		// 正常終了
		ErrorCompile = 1,		// コンパイルエラー
		ErrorFileOpen = 2,		// ファイルオープンエラー
		ErrorReadFile = 3,		// 読み込みエラー
		ErrorWriteFile = 4,		// 書き込みエラー
		ErrorDeleteFile = 5,		// 削除エラー
		ErrorNoData = 6,		// 有効なデータなし
		ErrorPlay = 7,		// データ演奏エラー
	}

	public enum FMCKind
	{
		File = 0,		// ファイル情報
		Part = 1,		// パート情報
		Log = 2,		// コンパイルログ
		Info = 3,		// インフォメーション
	}

	public enum FMCType
	{
		OPNA = 0,		// OPNA音源
		SSG = 1,		// SSG音源
		PCM = 2,		// PCM音源
		OPM = 3,		// OPM音源
	}

	public enum FMCLogKind
	{
		Error = 0,		// エラー
		Warning = 1,		// ワーニング
		Info = 2,		// 情報
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMCLog
	{
		public FMCLogKind Kind;							// ログ識別
		
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Message;						// エラーメッセージ

		public int Line;								// エラー行
		public int Col;								// エラーカラム
		
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
		public string Part;							// パート名

		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;						// エラーファイル名

		[MarshalAs(UnmanagedType.LPWStr)]
		public string AliasName;						// エイリアス名

		[MarshalAs(UnmanagedType.LPWStr)]
		public string MML;							// 該当MML
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMCPartInfo
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
		public string Part;							// パート名

		public FMCType Type;							// 音源タイプ
		public int Total;								// 総クロック
		public int Loop;								// ループクロック
		public int Bytes;								// 出力byte数
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMCFileInfo
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;						// エラーファイル名

		public int PartTotal;						// 総パート数
		public int PartOPNA;						// OPNAパート数
		public int PartSSG;							// SSGパート数
		public int PartPCM;							// PCMパート数
		public int ToneFM4;							// FM音色数
		public int WavePCM;							// WAVE数
		public int Envelop;							// エンベロープ数
		public int PartOPM;							// OPMパート数 (added 7.08d)
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMCStandardInfo
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Message;						// エラーファイル名
	}

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate FMCStatus CompileDelegate(
		[MarshalAs(UnmanagedType.LPWStr)] string inputFile,
		[MarshalAs(UnmanagedType.LPWStr)] string outputFile,
		FMCCompileFlag flags);

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate void FreeDelegate();

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate IntPtr GetInfoDelegate(int no);

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate int GetInfoNumDelegate();

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate void GetVersionDelegate(ref int system, ref int major, ref int minor);	

	public class FMCInfo
	{
		public FMCKind Kind
		{
			get;
			private set;
		}

		public FMCLog Log
		{
			get;
			private set;
		}

		public FMCFileInfo File
		{
			get;
			private set;
		}

		public FMCPartInfo Part
		{
			get;
			private set;
		}

		public FMCStandardInfo Info
		{
			get;
			private set;
		}

		internal FMCInfo(FMCLog info)
		{
			Kind = FMCKind.Log;
			Log = info;
		}

		internal FMCInfo(FMCFileInfo info)
		{
			Kind = FMCKind.File;
			File = info;
		}

		internal FMCInfo(FMCPartInfo info)
		{
			Kind = FMCKind.Part;
			Part = info;
		}

		internal FMCInfo(FMCStandardInfo info)
		{
			Kind = FMCKind.Info;
			Info = info;
		}
	}

	class FMCResult : IEnumerable<FMCInfo>
	{
		private FMCInfo[] _infos;

		public FMCStatus Result
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

		public int Count
		{
			get
			{
				return _infos.Length;
			}
		}

		public FMCInfo this[int index]
		{
			get
			{
				return _infos[index];
			}
		}

		internal FMCResult(FMCStatus result, FMCInfo[] infos, string compiledFilePath, string consoleOut = null)
		{
			Result = result;
			CompiledFilePath = compiledFilePath;
			ConsoleOut = consoleOut;
			_infos = infos;
		}

		public IEnumerator<FMCInfo> GetEnumerator()
		{
			return (_infos as IEnumerable<FMCInfo>).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _infos.GetEnumerator();
		}
	}

	class FMPCompiler : IDisposable
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResult
		{
			public FMCKind Kind;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultLog
		{
			public FMCKind Kind;
			public FMCLog Log;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultFile
		{
			public FMCKind Kind;
			public FMCFileInfo File;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultPart
		{
			public FMCKind Kind;
			public FMCPartInfo Part;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultStandard
		{
			public FMCKind Kind;
			public FMCStandardInfo Info;
		}


		private IntPtr _fmcdll = IntPtr.Zero;
		private string _fmcdllpath = null;

		private CompileDelegate _funcCompile = null;
		private FreeDelegate _funcFree = null;
		private GetInfoDelegate _funcGetInfo = null;
		private GetInfoNumDelegate _funcGetInfoNum = null;
		private GetVersionDelegate _funcGetVersion = null;

		private static readonly string FMCVer = "7.08f";
		private const int FMCVerSys = 7;
		private const int FMCVerMajor = 8;
		private const char FMCVerMinor = 'f';

		public void InitializeFMC(string fmcdllPath)
		{
			FinalizeFMC();

			IntPtr fmcdll = Kernel32Wrapper.LoadLibraryEx(
				fmcdllPath,
				IntPtr.Zero,
				LoadLibraryFlags.LoadWithAlteredSearchPath);
			if (fmcdll == IntPtr.Zero)
			{
				Marshal.ThrowExceptionForHR(
					Marshal.GetHRForLastWin32Error());
			}

			_fmcdll = fmcdll;
			_fmcdllpath = fmcdllPath;

			try
			{
				_funcCompile = Kernel32Wrapper.GetUnmangedFunc<CompileDelegate>(
					fmcdll, "Compile");
				_funcFree = Kernel32Wrapper.GetUnmangedFunc<FreeDelegate>(
					fmcdll, "Free");
				_funcGetInfo = Kernel32Wrapper.GetUnmangedFunc<GetInfoDelegate>(
					fmcdll, "GetInfo");
				_funcGetInfoNum = Kernel32Wrapper.GetUnmangedFunc<GetInfoNumDelegate>(
					fmcdll, "GetInfoNum");
				_funcGetVersion = Kernel32Wrapper.GetUnmangedFunc<GetVersionDelegate>(
					fmcdll, "GetVer");

				var ver = GetFMCVersion();

				if (ver.System < FMCVerSys ||
					(ver.System == FMCVerSys && ver.Major < FMCVerMajor) ||
					(ver.System == FMCVerSys && ver.Major == FMCVerMajor && ver.MinorChar < FMCVerMinor))
				{
					//	FMC の必要バージョンを満たしていない
					throw new Exception(
						string.Format(
							MMLEditorResource.Error_FMCVersion,
							FMCVer));
				}
			}
			catch(Exception e)
			{
				FinalizeFMC();
				if (e is ArgumentException)
				{
					throw new Exception(
						string.Format(
							MMLEditorResource.Error_FMCModule,
							FMCVer));
				}
				throw;
			}
		}

		public void FinalizeFMC()
		{
			if (_fmcdll != IntPtr.Zero)
			{
				Kernel32Wrapper.FreeLibrary(_fmcdll);
			}
			_fmcdll = IntPtr.Zero;
			_fmcdllpath = null;
			_funcCompile = null;
			_funcFree = null;
			_funcGetInfo = null;
			_funcGetInfoNum = null;
			_funcGetVersion = null;
		}

		public FMPVersion GetFMCVersion()
		{
			if (IsInitialized == false)
			{
				throw new InvalidOperationException();
			}

			int system = 0;
			int major = 0;
			int minor = 0;

			_funcGetVersion(ref system, ref major, ref minor);

			return new FMPVersion(system, major, minor);
		}

		public FMCResult Compile(string srcfile, bool compileAndPlay)
		{
			if (IsInitialized == false)
			{
				throw new InvalidOperationException();
			}

			var status = _funcCompile(
				srcfile, null,
				compileAndPlay ? FMCCompileFlag.PlayAfter : FMCCompileFlag.None);

			try
			{
				int count = _funcGetInfoNum();

				var infos = new FMCInfo[count];

				for (int i = 0; i < count; i++)
				{
					var pinfo = _funcGetInfo(i);
					
					var baseinfo =
						(NativeCompileResult)Marshal.PtrToStructure(pinfo, typeof(NativeCompileResult));

					switch (baseinfo.Kind)
					{
						case FMCKind.Log:
							{
								var r = (NativeCompileResultLog)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultLog));

								infos[i] = new FMCInfo(r.Log);
							}
							break;

						case FMCKind.File:
							{
								var r = (NativeCompileResultFile)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultFile));

								infos[i] = new FMCInfo(r.File);
							}
							break;

						case FMCKind.Part:
							{
								var r = (NativeCompileResultPart)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultPart));

								infos[i] = new FMCInfo(r.Part);
							}
							break;

						case FMCKind.Info:
							{
								var r = (NativeCompileResultStandard)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultStandard));

								infos[i] = new FMCInfo(r.Info);
							}
							break;
					}
				}

				string path = null;
				if (status == FMCStatus.Success || status == FMCStatus.ErrorPlay)
				{
					path =
						string.Format("{0}{1}{2}.owi",
							Path.GetDirectoryName(srcfile),
							Path.DirectorySeparatorChar,
							Path.GetFileNameWithoutExtension(srcfile));
				}
				return new FMCResult(status, infos, path);
			}
			finally
			{
				_funcFree();
			}
		}

		public void Dispose()
		{
			FinalizeFMC();
		}


		public bool IsInitialized
		{
			get
			{
				return _fmcdll != IntPtr.Zero;
			}
		}

		public string DllPath
		{
			get
			{
				return _fmcdllpath;
			}
		}
	}
}
