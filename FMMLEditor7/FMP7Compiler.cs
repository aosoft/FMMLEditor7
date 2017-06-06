using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using FMP.FMP7;
using System.IO;

namespace FMMLEditor7
{
	[Flags]
	public enum FMC7CompileFlag
	{
		None = 0,
		OnlyPrepro = 0x00000001,	// 展開チェックのみ
		OnlyCompile = 0x00000002,	// コンパイルチェックのみ
		PlayAfter = 0x00000100,	// コンパイル成功後自動演奏
	}

	public enum FMC7Status
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

	public enum FMC7Kind
	{
		File = 0,		// ファイル情報
		Part = 1,		// パート情報
		Log = 2,		// コンパイルログ
		Info = 3,		// インフォメーション
	}

	public enum FMC7Type
	{
		OPNA = 0,		// OPNA音源
		SSG = 1,		// SSG音源
		PCM = 2,		// PCM音源
		OPM = 3,		// OPM音源
	}

	public enum FMC7LogKind
	{
		Error = 0,		// エラー
		Warning = 1,		// ワーニング
		Info = 2,		// 情報
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMC7Log
	{
		public FMC7LogKind Kind;							// ログ識別
		
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
	public struct FMC7PartInfo
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
		public string Part;							// パート名

		public FMC7Type Type;							// 音源タイプ
		public int Total;								// 総クロック
		public int Loop;								// ループクロック
		public int Bytes;								// 出力byte数
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	public struct FMC7FileInfo
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
	public struct FMC7StandardInfo
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Message;						// エラーファイル名
	}

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate FMC7Status CompileDelegate(
		[MarshalAs(UnmanagedType.LPWStr)] string inputFile,
		[MarshalAs(UnmanagedType.LPWStr)] string outputFile,
		FMC7CompileFlag flags);

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate void FreeDelegate();

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate IntPtr GetInfoDelegate(int no);

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate int GetInfoNumDelegate();

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate void GetVersionDelegate(ref int system, ref int major, ref int minor);	

	public class FMC7Info
	{
		public FMC7Kind Kind
		{
			get;
			private set;
		}

		public FMC7Log Log
		{
			get;
			private set;
		}

		public FMC7FileInfo File
		{
			get;
			private set;
		}

		public FMC7PartInfo Part
		{
			get;
			private set;
		}

		public FMC7StandardInfo Info
		{
			get;
			private set;
		}

		internal FMC7Info(FMC7Log info)
		{
			Kind = FMC7Kind.Log;
			Log = info;
		}

		internal FMC7Info(FMC7FileInfo info)
		{
			Kind = FMC7Kind.File;
			File = info;
		}

		internal FMC7Info(FMC7PartInfo info)
		{
			Kind = FMC7Kind.Part;
			Part = info;
		}

		internal FMC7Info(FMC7StandardInfo info)
		{
			Kind = FMC7Kind.Info;
			Info = info;
		}
	}

	class FMC7Result : IEnumerable<FMC7Info>
	{
		private FMC7Info[] _infos;

		public FMC7Status Result
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

		public FMC7Info this[int index]
		{
			get
			{
				return _infos[index];
			}
		}

		internal FMC7Result(FMC7Status result, FMC7Info[] infos)
		{
			Result = result;
			_infos = infos;
		}

		public IEnumerator<FMC7Info> GetEnumerator()
		{
			return (_infos as IEnumerable<FMC7Info>).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _infos.GetEnumerator();
		}
	}

	class FMP7Compiler : IDisposable
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResult
		{
			public FMC7Kind Kind;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultLog
		{
			public FMC7Kind Kind;
			public FMC7Log Log;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultFile
		{
			public FMC7Kind Kind;
			public FMC7FileInfo File;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultPart
		{
			public FMC7Kind Kind;
			public FMC7PartInfo Part;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
		private struct NativeCompileResultStandard
		{
			public FMC7Kind Kind;
			public FMC7StandardInfo Info;
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

		public FMC7Result Compile(string srcfile, bool compileAndPlay)
		{
			if (IsInitialized == false)
			{
				throw new InvalidOperationException();
			}

			var status = _funcCompile(
				srcfile, null,
				compileAndPlay ? FMC7CompileFlag.PlayAfter : FMC7CompileFlag.None);

			try
			{
				int count = _funcGetInfoNum();

				var infos = new FMC7Info[count];

				for (int i = 0; i < count; i++)
				{
					var pinfo = _funcGetInfo(i);
					
					var baseinfo =
						(NativeCompileResult)Marshal.PtrToStructure(pinfo, typeof(NativeCompileResult));

					switch (baseinfo.Kind)
					{
						case FMC7Kind.Log:
							{
								var r = (NativeCompileResultLog)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultLog));

								infos[i] = new FMC7Info(r.Log);
							}
							break;

						case FMC7Kind.File:
							{
								var r = (NativeCompileResultFile)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultFile));

								infos[i] = new FMC7Info(r.File);
							}
							break;

						case FMC7Kind.Part:
							{
								var r = (NativeCompileResultPart)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultPart));

								infos[i] = new FMC7Info(r.Part);
							}
							break;

						case FMC7Kind.Info:
							{
								var r = (NativeCompileResultStandard)Marshal.PtrToStructure(
									pinfo, typeof(NativeCompileResultStandard));

								infos[i] = new FMC7Info(r.Info);
							}
							break;
					}
				}

				return new FMC7Result(status, infos);
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
