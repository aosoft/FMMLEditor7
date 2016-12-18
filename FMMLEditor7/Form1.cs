using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Sgry.Azuki.WinForms;
using Sgry.Azuki;
using FMP.FMP7;

namespace FMMLEditor7
{
	public partial class Form1 : Form
	{
		private Settings m_setting = new Settings();
		private FMPCompiler m_compiler = new FMPCompiler();
		private FMPWork m_work = new FMPWork();
		private FastForward m_fastforward;

		private AzukiControl m_editor = null;
		
		private string m_mmlFileName = null;
		private SettingDialog m_settingDialog = null;

		public Form1()
		{
			InitializeComponent();

			m_settingDialog = new SettingDialog(m_setting);

			MMLFileName = null;

			m_editor = new AzukiControl();
			m_editor.ShowsHRuler = true;
			m_editor.Resize += (_s, _e) =>
				{
					UpdateEditorState();
				};

			m_editor.KeyDown += editor_KeyDown;
			m_editor.KeyUp += editor_KeyUp;
			m_editor.LostFocus += editor_LostFocus;

			openFileDialog1.Filter = MMLEditorResource.FileFilter_MML;
			saveFileDialog1.Filter = MMLEditorResource.FileFilter_MML;

			splitContainer1.Panel1.Controls.Add(m_editor);
			m_editor.Dock = DockStyle.Fill;

			m_fastforward = new FastForward(m_work);
		}

		/*-------------------------------------------------------------------
			ユーティリティ
		-------------------------------------------------------------------*/

		private void ShowErrorDialog(string msg)
		{
			MessageBox.Show(
				msg,
				MMLEditorResource.AppName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
		}

		private void UpdateEditorState()
		{
			if (m_setting.EditorTextWrap)
			{
				m_editor.ViewType = Sgry.Azuki.ViewType.WrappedProportional;
				if (m_setting.EditorAutoTextWrap)
				{
					m_editor.ViewWidth = ClientSize.Width;
				}
				else
				{
					m_editor.ViewWidth =
						m_editor.View.LineNumAreaWidth +
						m_setting.EditorTextWidth *
						m_editor.View.HRulerUnitWidth;
				}
			}
			else
			{
				m_editor.ViewType = Sgry.Azuki.ViewType.Proportional;
			}
		}

		private void UpdateEditorFont()
		{
			var oldfont = m_editor.Font;
			var newfont = new Font(
				m_setting.EditorFontName,
				m_setting.EditorFontSize,
				m_setting.EditorFontStyle);
			m_editor.Font = newfont;
			/*
			if (oldfont != null)
			{
				oldfont.Dispose();
			}
			 */
		}

		private bool SaveMML(string fileName, bool checkFileExists)
		{
			StreamWriter sw = null;
			try
			{

				if (checkFileExists)
				{
					if (File.Exists(fileName))
					{
						if (MessageBox.Show(
							string.Format("'{0}' はすでに存在します。上書きしてよろしいですか？", fileName),
							MMLEditorResource.AppName,
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Exclamation) != DialogResult.OK)
						{
							return false;
						}
					}
				}

				sw = new StreamWriter(fileName, false, Encoding.GetEncoding("shift_jis"));
				sw.Write(m_editor.Document.Text);
				MMLFileName = fileName;
				m_editor.Document.IsDirty = false;
				return true;
			}
			finally
			{
				if (sw != null)
				{
					sw.Close();
				}
			}
		}

		private bool Open(string fileName)
		{
			if (CheckUpdateAndSave())
			{
				StreamReader sr = null;
				try
				{
					sr = new StreamReader(fileName, Encoding.GetEncoding("shift_jis"));
					m_editor.Document = new Document();
					m_editor.Document.Text = sr.ReadToEnd();
					m_editor.Document.ClearHistory();

					MMLFileName = fileName;
					m_editor.Document.IsDirty = false;

					return true;
				}
				finally
				{
					if (sr != null)
					{
						sr.Close();
					}
				}
			}
			return false;
		}

		private bool Save()
		{
			if (MMLFileName == null)
			{
				return SaveAs();
			}
			else
			{
				return SaveMML(MMLFileName, false);
			}
		}

		private bool SaveAs()
		{
			saveFileDialog1.FileName = MMLFileName;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				return SaveMML(saveFileDialog1.FileName, true);
			}
			return false;
		}

		private bool CheckUpdateAndSave()
		{
			if (m_editor.Document.IsDirty)
			{
				switch (MessageBox.Show(
					"編集中のファイルが保存されていません。保存しますか？",
					MMLEditorResource.AppName,
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning))
				{
					case DialogResult.Yes:
						{
							if (Save() == false)
							{
								return false;
							}
						}
						break;

					case DialogResult.Cancel:
						{
							return false;
						}
				}
			}
			return true;
		}

		private bool CheckSupportMMLExt(string path)
		{
			string ext = Path.GetExtension(path).ToLower();
			if (ext == ".mwi")
			{
				return true;
			}
			return false;
		}

		private void CheckCompilerDllSettingAndUpdate()
		{
			try
			{
				if (m_setting.FMC7Path != m_compiler.DllPath)
				{
					m_compiler.FinalizeFMC();
					m_compiler.InitializeFMC(m_setting.FMC7Path);

					var version = m_compiler.GetFMCVersion();
				}
			}
			catch (Exception e)
			{
				ShowErrorDialog(
					string.Format(
						MMLEditorResource.Error_LoadCompilerModule,
						e.Message));
			}
		}

		private void UpdateCompileResult(FMCResult result)
		{
			listviewCompileResult.Items.Clear();
			listviewCompileErrorReport.Items.Clear();
			listviewFileInfo.Items.Clear();
			textboxMessages.Text = "";

			var msgs = new StringBuilder();

			foreach (var info in result)
			{
				switch (info.Kind)
				{
					case FMCKind.Part:
						{
							var item = new ListViewItem(info.Part.Part);
							item.SubItems.Add(info.Part.Type.ToString());
							item.SubItems.Add(info.Part.Total.ToString());
							item.SubItems.Add(info.Part.Loop.ToString());
							item.SubItems.Add(info.Part.Bytes.ToString());

							listviewCompileResult.Items.Add(item);
						}
						break;

					case FMCKind.Log:
						{
							var item = new ListViewItem(info.Log.Kind.ToString());
							item.SubItems.Add(info.Log.Part);
							item.SubItems.Add(info.Log.Line.ToString());
							item.SubItems.Add(info.Log.Message);
							item.SubItems.Add(info.Log.AliasName);
							item.SubItems.Add(info.Log.MML.Trim());

							listviewCompileErrorReport.Items.Add(item);
						}
						break;

					case FMCKind.Info:
						{
							msgs.AppendLine(info.Info.Message);
						}
						break;

					case FMCKind.File:
						{
							ListViewItem lvi = null;

							lvi = new ListViewItem(MMLEditorResource.FileInfo_FileName);
							lvi.SubItems.Add(info.File.FileName);
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_PartTotal);
							lvi.SubItems.Add(info.File.PartTotal.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_PartOPNA);
							lvi.SubItems.Add(info.File.PartOPNA.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_PartOPM);
							lvi.SubItems.Add(info.File.PartOPM.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_PartSSG);
							lvi.SubItems.Add(info.File.PartSSG.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_PartPCM);
							lvi.SubItems.Add(info.File.PartPCM.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_ToneFM4);
							lvi.SubItems.Add(info.File.ToneFM4.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_WavePCM);
							lvi.SubItems.Add(info.File.WavePCM.ToString());
							listviewFileInfo.Items.Add(lvi);

							lvi = new ListViewItem(MMLEditorResource.FileInfo_Envelop);
							lvi.SubItems.Add(info.File.Envelop.ToString());
							listviewFileInfo.Items.Add(lvi);
						}
						break;
				}
			}

			textboxMessages.Text = msgs.ToString();

			switch (result.Result)
			{
				case FMCStatus.Success:
					{
						tabControl1.SelectedIndex = 0;
					}
					break;

				case FMCStatus.ErrorCompile:
					{
						tabControl1.SelectedIndex = 1;
					}
					break;
			}

			m_editor.Focus();
		}

		private void Compile(bool compileAndPlay)
		{
			if (m_compiler.IsInitialized)
			{
				if (Save())
				{
					var r = m_compiler.Compile(m_mmlFileName, compileAndPlay);
					UpdateCompileResult(r);

					if (r.Result == FMCStatus.Success &&
						compileAndPlay &&
						m_setting.ProcessStartFMP7 &&
						FMPControl.CheckAvailableFMP() == false)
					{
						try
						{
							string arg =
								string.Format("\"{0}{1}{2}.owi\"",
									Path.GetDirectoryName(m_mmlFileName),
									Path.DirectorySeparatorChar,
									Path.GetFileNameWithoutExtension(m_mmlFileName));
							Process.Start(m_setting.FMP7Path, arg);

							for (int i = 0; i < 20; i++)
							{
								if (FMPControl.CheckAvailableFMP())
								{
									Activate();
									return;
								}
								System.Threading.Thread.Sleep(100);
							}

							ShowErrorDialog(
								MMLEditorResource.Error_FMPNotAvailable);
						}
						catch
						{
							throw new Exception(MMLEditorResource.Error_FailedStartupFMP);
						}
					}
				}
			}
			else
			{
				ShowErrorDialog(
					MMLEditorResource.Error_NotInitializedCompiler);
			}
		}

		private readonly string[]  m_lineparse = new string[] { "\r\n", "\n", "\r" }; 

		private void JumpToErrorMML()
		{
			if (listviewCompileErrorReport.SelectedItems.Count < 1)
			{
				return;
			}

			var item = listviewCompileErrorReport.SelectedItems[0];
			int line = int.Parse(item.SubItems[2].Text) - 1;

			var linetext = m_editor.Document.GetLineContent(line);
			var mml = item.SubItems[5].Text;

			var mmllines = mml.Split(m_lineparse, StringSplitOptions.None);

			if (mmllines.Length > 1)
			{
				//	複数行にまたぐMMLの場合
				m_editor.Document.SetSelection(
					m_editor.Document.GetCharIndexFromLineColumnIndex(line, 0),
					m_editor.Document.GetCharIndexFromLineColumnIndex(line + mmllines.Length, 0) - 1);
			}
			else
			{
				int col = linetext.IndexOf(mml);
				if (col < 0)
				{
					m_editor.Document.SetCaretIndex(line, 0);
				}
				else
				{
					int anchor =
						m_editor.Document.GetCharIndexFromLineColumnIndex(
							line, col);
					m_editor.Document.SetSelection(anchor, anchor + mml.Length);
				}
			}
			m_editor.ScrollToCaret();
			m_editor.Focus();
		}

		/*-------------------------------------------------------------------
			プロパティ
		-------------------------------------------------------------------*/

		private string MMLFileName
		{
			get
			{
				return m_mmlFileName;
			}

			set
			{
				string name = value;
				if (name == null)
				{
					name = MMLEditorResource.NoName;
				}

				Text = string.Format("{0} - {1}", name, MMLEditorResource.AppName);

				m_mmlFileName = value;
			}
		}

		/*-------------------------------------------------------------------
			イベント
		-------------------------------------------------------------------*/

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				m_setting.Load();
			}
			catch
			{
			}

			try
			{
				if (m_setting.AvailableWindowPosInfo)
				{
					SetBounds(
						m_setting.WindowLeft, m_setting.WindowTop,
						m_setting.WindowWidth, m_setting.WindowHeight);

					WindowState = m_setting.WindowState;
					splitContainer1.SplitterDistance = m_setting.EditorHeight;
				}

				UpdateEditorFont();
				UpdateEditorState();

				CheckCompilerDllSettingAndUpdate();

				timer1.Start();
			}
			catch
			{
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				e.Cancel = CheckUpdateAndSave() ? false : true;
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
				return;
			}

			try
			{
				//
				//	閉じる前にウィンドウの状態を控える
				//

				m_setting.AvailableWindowPosInfo = true;
				Rectangle rect;
				if (WindowState != FormWindowState.Normal)
				{
					rect = RestoreBounds;
				}
				else
				{
					rect = Bounds;
				}
				m_setting.WindowLeft = rect.X;
				m_setting.WindowTop = rect.Y;
				m_setting.WindowWidth = rect.Width;
				m_setting.WindowHeight = rect.Height;
				m_setting.WindowState = WindowState;
				m_setting.EditorHeight = splitContainer1.SplitterDistance;
			}
			catch
			{
			}

			timer1.Stop();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				m_setting.Save();
			}
			catch
			{
			}
			finally
			{
				if (m_fastforward != null)
				{
					m_fastforward.Dispose();
				}
				if (m_compiler != null)
				{
					m_compiler.Dispose();
				}
				if (m_work != null)
				{
					m_work.Dispose();
				}
			}
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					foreach (string fileName in
						(string[])e.Data.GetData(DataFormats.FileDrop))
					{
						if (CheckSupportMMLExt(fileName))
						{
							e.Effect = DragDropEffects.All;
							return;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			e.Effect = DragDropEffects.None;
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					foreach (string fileName in
						(string[])e.Data.GetData(DataFormats.FileDrop))
					{
						if (CheckSupportMMLExt(fileName))
						{
							Open(fileName);
							return;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void editor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Alt && e.Control)
			{
				//FastPlay = true;
				m_fastforward.Start();
			}
			else
			{
				//FastPlay = false;
				m_fastforward.Stop();
			}
		}

		private void editor_KeyUp(object sender, KeyEventArgs e)
		{
			editor_KeyDown(sender, e);
		}

		private void editor_LostFocus(object sender, EventArgs e)
		{
			//FastPlay = false;
			m_fastforward.Stop();
		}

		private void menuitemNewFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (CheckUpdateAndSave())
				{
					m_editor.Document = new Document();
					MMLFileName = null;
					m_editor.Document.IsDirty = false;
				}
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}

		}

		private void menuitemOpen_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = MMLFileName;
			try
			{
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					Open(openFileDialog1.FileName);
				}
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}


		private void menuitemSave_Click(object sender, EventArgs e)
		{
			try
			{
				Save();
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemSaveAs_Click(object sender, EventArgs e)
		{
			try
			{
				SaveAs();
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemQuit_Click(object sender, EventArgs e)
		{
			Close();
		}

		unsafe private void menuitemPlay_Click(object sender, EventArgs e)
		{
			try
			{
				FMPStat pstat;
				m_work.Open();
				try
				{
					var gwork = m_work.GlobalWorkPointer;
					pstat = (*gwork).Status & (FMPStat.Play | FMPStat.Pause);
				}
				finally
				{
					m_work.Close();
				}

				if (pstat == FMPStat.None)
				{
					FMPControl.MusicPlay();
				}
				else
				{
					FMPControl.MusicPause();
				}
			}
			catch (Exception ex)
			{
				var ex2 = ex as FMPException;
				if (ex2 != null && ex2.Error == FMPError.FMPNotFound)
				{
					return;
				}

				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemStop_Click(object sender, EventArgs e)
		{
			try
			{
				FMPControl.MusicStop();
			}
			catch (Exception ex)
			{
				var ex2 = ex as FMPException;
				if (ex2 != null && ex2.Error == FMPError.FMPNotFound)
				{
					return;
				}

				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemSetting_Click(object sender, EventArgs e)
		{
			if (m_settingDialog.ShowDialog(this) == DialogResult.OK)
			{
				UpdateEditorFont();
				UpdateEditorState();
				CheckCompilerDllSettingAndUpdate();
			}
		}

		private void menuitemMMLCompileAndPlay_Click(object sender, EventArgs e)
		{
			try
			{
				Compile(true);
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemMMLCompile_Click(object sender, EventArgs e)
		{
			try
			{
				Compile(false);
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void menuitemMoveToTextEditor_Click(object sender, EventArgs e)
		{
			m_editor.Focus();
		}

		private void menuitemMovieToCompileResult_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedIndex = 0;
			listviewCompileResult.Focus();
		}

		private void menuitemMoveToCompileErrorReport_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedIndex = 1;
			listviewCompileErrorReport.Focus();
		}

		private void listviewCompileErrorReport_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				JumpToErrorMML();
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		private void listviewCompileErrorReport_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				JumpToErrorMML();
			}
			catch (Exception ex)
			{
				ShowErrorDialog(ex.Message);
			}
		}

		unsafe private void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				var current = m_fastforward.Current;

				progressPlayPosition.Maximum = (int)current.Count;
				progressPlayPosition.Value = (int)current.CountNow;
			}
			catch
			{
			}
		}
	}
}
