using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FMP.FMP7;

namespace FMMLEditor7
{
	/// <summary>
	/// 早送り制御
	/// </summary>
	internal class FastForward : IDisposable
	{
		internal struct PlayCountInfo
		{
			public uint Count;
			public uint CountNow;
		}

		private Thread _thread;
		private FMPWork _work;

		private bool _threadloop;
		private EventWaitHandle _eventWait;
		private EventWaitHandle _eventComplete;

		private bool _fastfoward;
		private bool _playing;
		private PlayCountInfo _fastfowardCurrent;
		private int _fastforwardStartTickCount;
		private byte[] _fastfowardMaskFlags;

		private static byte[] _allmaskon;

		static FastForward()
		{
			_allmaskon = new byte[FMPGlobalWork.MaxPart];
			for (int i = 0; i < _allmaskon.Length; i++)
			{
				_allmaskon[i] = 1;
			}
		}

		public FastForward(FMPWork work)
		{
			_work = work;

			_fastfowardMaskFlags = new byte[FMPGlobalWork.MaxPart];

			_eventWait = new ManualResetEvent(false);
			_eventComplete = new ManualResetEvent(false);
			_threadloop = true;

			_fastfoward = false;

			_thread = new Thread(ThreadMain);
			_thread.Start();
		}

		public void Dispose()
		{
			_threadloop = false;
			if (_eventWait != null)
			{
				if (_thread != null)
				{
					_eventWait.Set();
					_thread.Join();
				}

				_eventWait.Close();
			}

			if (_eventComplete != null)
			{
				_eventComplete.Close();
			}
		}

		public void Start()
		{
			try
			{
				if (_fastfoward)
				{
					return;
				}


				var work = _work.GetGlobalWork();
				//FMPControl.MusicPause();

				_playing = (work.Status & FMPStat.Play) != 0;

				if (_playing == false)
				{
					return;
				}

				_fastfowardCurrent.Count = work.Count;
				_fastfowardCurrent.CountNow = work.CountNow;
				_fastforwardStartTickCount = Environment.TickCount;

				unsafe
				{
					for (int i = 0; i < _fastfowardMaskFlags.Length; i++)
					{
						_fastfowardMaskFlags[i] = work.Mask[i];
					}
				}
				FMPControl.SetMask(_allmaskon);

				Thread.Sleep(200);

				_eventWait.Set();
				_fastfoward = true;
			}
			catch
			{
				_fastfoward = false;
			}
		}

		public void Stop()
		{
			if (_fastfoward)
			{
				try
				{
					_eventWait.Reset();
					_eventComplete.WaitOne();

					FMPControl.SetMask(_fastfowardMaskFlags);

					if (_playing)
					{
						//FMPControl.MusicPause();
					}
				}
				catch
				{
				}

				_fastfoward = false;
			}
		}

		private void ThreadMain()
		{
			while (_threadloop)
			{
				_eventWait.WaitOne();
				if (_threadloop == false)
				{
					break;
				}

				_eventComplete.Reset();

				try
				{
					FMPControl.SetSeek(Current.CountNow);
				}
				catch
				{
				}
				_eventComplete.Set();
			}
		}

		public PlayCountInfo Current
		{
			get
			{
				if (_fastfoward)
				{
					var current = _fastfowardCurrent;
					int pos = Environment.TickCount - _fastforwardStartTickCount;
					if (pos < 0)
					{
						pos += int.MaxValue;
					}

					current.CountNow += (uint)(pos);
					if (current.CountNow >
						current.Count)
					{
						current.CountNow -= _fastfowardCurrent.Count;
					}

					return current;
				}

				unsafe
				{
					var ret = new PlayCountInfo();
					bool opened = false;
					try
					{
						_work.Open();
						opened = true;
						var w = _work.GlobalWorkPointer;

						ret.Count = w->Count;
						ret.CountNow = w->CountNow;

						return ret;
					}
					finally
					{
						if (opened)
						{
							_work.Close();
						}
					}
				}
			}
		}
	}
}
