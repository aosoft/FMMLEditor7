using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FMP.FMP7;

namespace FMPMMLEditor7
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

		private Thread m_thread;
		private FMPWork m_work;

		private bool m_threadloop;
		private EventWaitHandle m_eventWait;
		private EventWaitHandle m_eventComplete;

		private bool m_fastfoward;
		private bool m_playing;
		private PlayCountInfo m_fastfowardCurrent;
		private int m_fastforwardStartTickCount;
		private byte[] m_fastfowardMaskFlags;

		private static byte[] m_allmaskon;

		static FastForward()
		{
			m_allmaskon = new byte[FMPGlobalWork.MaxPart];
			for (int i = 0; i < m_allmaskon.Length; i++)
			{
				m_allmaskon[i] = 1;
			}
		}

		public FastForward(FMPWork work)
		{
			m_work = work;

			m_fastfowardMaskFlags = new byte[FMPGlobalWork.MaxPart];

			m_eventWait = new ManualResetEvent(false);
			m_eventComplete = new ManualResetEvent(false);
			m_threadloop = true;

			m_fastfoward = false;

			m_thread = new Thread(ThreadMain);
			m_thread.Start();
		}

		public void Dispose()
		{
			m_threadloop = false;
			if (m_eventWait != null)
			{
				if (m_thread != null)
				{
					m_eventWait.Set();
					m_thread.Join();
				}

				m_eventWait.Close();
			}

			if (m_eventComplete != null)
			{
				m_eventComplete.Close();
			}
		}

		public void Start()
		{
			try
			{
				if (m_fastfoward)
				{
					return;
				}


				var work = m_work.GetGlobalWork();
				//FMPControl.MusicPause();

				m_playing = (work.Status & FMPStat.Play) != 0;

				if (m_playing == false)
				{
					return;
				}

				m_fastfowardCurrent.Count = work.Count;
				m_fastfowardCurrent.CountNow = work.CountNow;
				m_fastforwardStartTickCount = Environment.TickCount;

				unsafe
				{
					for (int i = 0; i < m_fastfowardMaskFlags.Length; i++)
					{
						m_fastfowardMaskFlags[i] = work.Mask[i];
					}
				}
				FMPControl.SetMask(m_allmaskon);

				Thread.Sleep(200);

				m_eventWait.Set();
				m_fastfoward = true;
			}
			catch
			{
				m_fastfoward = false;
			}
		}

		public void Stop()
		{
			if (m_fastfoward)
			{
				try
				{
					m_eventWait.Reset();
					m_eventComplete.WaitOne();

					FMPControl.SetMask(m_fastfowardMaskFlags);

					if (m_playing)
					{
						//FMPControl.MusicPause();
					}
				}
				catch
				{
				}

				m_fastfoward = false;
			}
		}

		private void ThreadMain()
		{
			while (m_threadloop)
			{
				m_eventWait.WaitOne();
				if (m_threadloop == false)
				{
					break;
				}

				m_eventComplete.Reset();

				try
				{
					FMPControl.SetSeek(Current.CountNow);
				}
				catch
				{
				}
				m_eventComplete.Set();
			}
		}

		public PlayCountInfo Current
		{
			get
			{
				if (m_fastfoward)
				{
					var current = m_fastfowardCurrent;
					int pos = Environment.TickCount - m_fastforwardStartTickCount;
					if (pos < 0)
					{
						pos += int.MaxValue;
					}

					current.CountNow += (uint)(pos);
					if (current.CountNow >
						current.Count)
					{
						current.CountNow -= m_fastfowardCurrent.Count;
					}

					return current;
				}

				unsafe
				{
					var ret = new PlayCountInfo();
					bool opened = false;
					try
					{
						m_work.Open();
						opened = true;
						var w = m_work.GlobalWorkPointer;

						ret.Count = w->Count;
						ret.CountNow = w->CountNow;

						return ret;
					}
					finally
					{
						if (opened)
						{
							m_work.Close();
						}
					}
				}
			}
		}
	}
}
