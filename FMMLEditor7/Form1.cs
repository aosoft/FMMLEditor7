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
		private Settings _setting = new Settings();
		private FMPCompiler _compiler = new FMPCompiler();
		private FMPWork _work = new FMPWork();
		private FastForward _fastforward;

		private AzukiControl _editor = null;
		
		private string _mmlFileName = null;
		private SettingDialog _settingDialog = null;

		public Form1()
		{
			InitializeComponent();

			_settingDialog = new SettingDialog(_setting);

			MMLFileName = null;

			_editor = new AzukiControl();
			_editor.ShowsHRuler = true;
			_editor.Resize += (_s, _e) =>
				{
					UpdateEditorState();
				};

			_editor.KeyDown += editor_KeyDown;
			_editor.KeyUp += editor_KeyUp;
			_editor.LostFocus += editor_LostFocus;

			openFileDialog1.Filter = MMLEditorResource.FileFilter_MML;
			saveFileDialog1.Filter = MMLEditorResource.FileFilter_MML;

			splitContainer1.Panel1.Controls.Add(_editor);
			_editor.Dock = DockStyle.Fill;

			_fastforward = new FastForward(_work);
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
			if (_setting.EditorTextWrap)
			{
				_editor.ViewType = Sgry.Azuki.ViewType.WrappedProportional;
				if (_setting.EditorAutoTextWrap)
				{
					_editor.ViewWidth = ClientSize.Width;
				}
				else
				{
					_editor.ViewWidth =
						_editor.View.LineNumAreaWidth +
						_setting.EditorTextWidth *
						_editor.View.HRulerUnitWidth;
				}
			}
			else
			{
				_editor.ViewType = Sgry.Azuki.ViewType.Proportional;
			}
		}

		private void UpdateEditorFont()
		{
			var oldfont = _editor.Font;
			var newfont = new Font(
				_setting.EditorFontName,
				_setting.EditorFontSize,
				_setting.EditorFontStyle);
			_editor.Font = newfont;
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
							string.Format(MMLEditorResource.Message_CheckOverwriteFile, fileName),
							MMLEditorResource.AppName,
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Exclamation) != DialogResult.OK)
						{
							return false;
						}
					}
				}

				sw = new StreamWriter(fileName, false, Encoding.GetEncoding("shift_jis"));
				sw.Write(_editor.Document.Text);
				MMLFileName = fileName;
				_editor.Document.IsDirty = false;
				UpdateRecentFilesMenu(fileName);
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
					_editor.Document = new Document();
					_editor.Document.Text = sr.ReadToEnd();
					_editor.Document.ClearHistory();

					MMLFileName = fileName;
					_editor.Document.IsDirty = false;
					UpdateRecentFilesMenu(fileName);

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
			if (_editor.Document.IsDirty)
			{
				switch (MessageBox.Show(
					MMLEditorResource.Message_SaveFile,
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

		private void UpdateRecentFilesMenu(string newFile)
		{
			_setting.UpdateRecentFiles(newFile);
			UpdateRecentFilesMenu();
		}

		private void UpdateRecentFilesMenu()
		{
			menuitemRecentFiles.DropDownItems.Clear();
			if (_setting.RecentFiles.Count > 0)
			{
				menuitemRecentFiles.Enabled = true;
				foreach (var item in _setting.RecentFiles)
				{
					var menuitem = new ToolStripMenuItem();
					menuitem.Text = Path.GetFileName(item);
					menuitem.ToolTipText = item;
					menuitem.Click += (s, e) =>
					{
						Open(item);
					};
					menuitemRecentFiles.DropDownItems.Add(menuitem);
				}

				menuitemRecentFiles.DropDownItems.Add(new ToolStripSeparator());

				{
					var menuitem = new ToolStripMenuItem();
					menuitem.Text = MMLEditorResource.MenuItem_ClearRecentFiles;
					menuitem.Click += (s, e) =>
					{
						_setting.RecentFiles.Clear();
						UpdateRecentFilesMenu();
					};
					menuitemRecentFiles.DropDownItems.Add(menuitem);
				}
			}
			else
			{
				menuitemRecentFiles.Enabled = false;
			}
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
				if (_setting.FMC7Path != _compiler.DllPath)
				{
					_compiler.FinalizeFMC();
					_compiler.InitializeFMC(_setting.FMC7Path);

					var version = _compiler.GetFMCVersion();
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

			_editor.Focus();
		}

		private void Compile(bool compileAndPlay)
		{
			if (_compiler.IsInitialized)
			{
				if (Save())
				{
					var r = _compiler.Compile(_mmlFileName, compileAndPlay);
					UpdateCompileResult(r);

					if (r.Result == FMCStatus.ErrorPlay &&
						compileAndPlay &&
						_setting.ProcessStartFMP7 &&
						FMPControl.CheckAvailableFMP() == false)
					{
						try
						{
							string arg =
								string.Format("\"{0}{1}{2}.owi\"",
									Path.GetDirectoryName(_mmlFileName),
									Path.DirectorySeparatorChar,
									Path.GetFileNameWithoutExtension(_mmlFileName));
							Process.Start(_setting.FMP7Path, arg);

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

		private readonly string[]  _lineparse = new string[] { "\r\n", "\n", "\r" }; 

		private void JumpToErrorMML()
		{
			if (listviewCompileErrorReport.SelectedItems.Count < 1)
			{
				return;
			}

			var item = listviewCompileErrorReport.SelectedItems[0];
			int line = int.Parse(item.SubItems[2].Text) - 1;

			var linetext = _editor.Document.GetLineContent(line);
			var mml = item.SubItems[5].Text;

			var mmllines = mml.Split(_lineparse, StringSplitOptions.None);

			if (mmllines.Length > 1)
			{
				//	複数行にまたぐMMLの場合
				_editor.Document.SetSelection(
					_editor.Document.GetCharIndexFromLineColumnIndex(line, 0),
					_editor.Document.GetCharIndexFromLineColumnIndex(line + mmllines.Length, 0) - 1);
			}
			else
			{
				int col = linetext.IndexOf(mml);
				if (col < 0)
				{
					_editor.Document.SetCaretIndex(line, 0);
				}
				else
				{
					int anchor =
						_editor.Document.GetCharIndexFromLineColumnIndex(
							line, col);
					_editor.Document.SetSelection(anchor, anchor + mml.Length);
				}
			}
			_editor.ScrollToCaret();
			_editor.Focus();
		}

		/*-------------------------------------------------------------------
			プロパティ
		-------------------------------------------------------------------*/

		private string MMLFileName
		{
			get
			{
				return _mmlFileName;
			}

			set
			{
				string name = value;
				if (name == null)
				{
					name = MMLEditorResource.NoName;
				}

				Text = string.Format("{0} - {1}", name, MMLEditorResource.AppName);

				_mmlFileName = value;
			}
		}

		/*-------------------------------------------------------------------
			イベント
		-------------------------------------------------------------------*/

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				_setting.Load();
				UpdateRecentFilesMenu();
			}
			catch
			{
			}

			try
			{
				if (_setting.AvailableWindowPosInfo)
				{
					SetBounds(
						_setting.WindowLeft, _setting.WindowTop,
						_setting.WindowWidth, _setting.WindowHeight);

					WindowState = _setting.WindowState;
					splitContainer1.SplitterDistance = _setting.EditorHeight;
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

				_setting.AvailableWindowPosInfo = true;
				Rectangle rect;
				if (WindowState != FormWindowState.Normal)
				{
					rect = RestoreBounds;
				}
				else
				{
					rect = Bounds;
				}
				_setting.WindowLeft = rect.X;
				_setting.WindowTop = rect.Y;
				_setting.WindowWidth = rect.Width;
				_setting.WindowHeight = rect.Height;
				_setting.WindowState = WindowState;
				_setting.EditorHeight = splitContainer1.SplitterDistance;
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
				_setting.Save();
			}
			catch
			{
			}
			finally
			{
				if (_fastforward != null)
				{
					_fastforward.Dispose();
				}
				if (_compiler != null)
				{
					_compiler.Dispose();
				}
				if (_work != null)
				{
					_work.Dispose();
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
				_fastforward.Start();
			}
			else
			{
				//FastPlay = false;
				_fastforward.Stop();
			}
		}

		private void editor_KeyUp(object sender, KeyEventArgs e)
		{
			editor_KeyDown(sender, e);
		}

		private void editor_LostFocus(object sender, EventArgs e)
		{
			//FastPlay = false;
			_fastforward.Stop();
		}

		private void menuitemNewFile_Click(object sender, EventArgs e)
		{
			try
			{
				if (CheckUpdateAndSave())
				{
					_editor.Document = new Document();
					MMLFileName = null;
					_editor.Document.IsDirty = false;
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
				_work.Open();
				try
				{
					var gwork = _work.GlobalWorkPointer;
					pstat = (*gwork).Status & (FMPStat.Play | FMPStat.Pause);
				}
				finally
				{
					_work.Close();
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
			if (_settingDialog.ShowDialog(this) == DialogResult.OK)
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
			_editor.Focus();
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
				var current = _fastforward.Current;

				progressPlayPosition.Maximum = (int)current.Count;
				progressPlayPosition.Value = (int)current.CountNow;
			}
			catch
			{
			}
		}
	}
}
