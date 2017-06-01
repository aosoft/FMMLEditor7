using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace FMMLEditor7
{
	public partial class SettingDialog : Form
	{
		private Settings _setting;
		private Font _font;
		private Font _deffont;

		public SettingDialog(Settings setting)
		{
			InitializeComponent();
			_setting = setting;
			_deffont = labelEditorFontSample.Font;

			var assem = Assembly.GetExecutingAssembly();
			var assemName = assem.GetName();
			var copyright = Attribute.GetCustomAttribute(
				assem, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;

			labelVersion.Text =
				string.Format("Version.{0}", assemName.Version);
			labelCopyright.Text = copyright.Copyright;

			linkLabelFMP.Text = MMLEditorResource.URL_FMP;
			linkLabelAzuki.Text = MMLEditorResource.URL_Azuki;
			linkLabelAOSoft.Text = MMLEditorResource.URL_AOSoft;
		}

		/*-------------------------------------------------------------------
			ユーティリティ
		-------------------------------------------------------------------*/

		private void EndDialog(DialogResult result)
		{
			labelEditorFontSample.Font = _deffont;
			/*
			if (m_font != null)
			{
				m_font.Dispose();
				m_font = null;
			}
			 */
			_font = null;
			DialogResult = result;
		}

		/*-------------------------------------------------------------------
			プロパティ
		-------------------------------------------------------------------*/

		/*-------------------------------------------------------------------
			イベント
		-------------------------------------------------------------------*/

		private void SettingDialog_Shown(object sender, EventArgs e)
		{
			textboxFMP7Path.Text = _setting.FMP7Path;
			textboxFMC7Path.Text = _setting.FMC7Path;
			textboxMSDOSPlayerPath.Text = _setting.MSDOSPlayerPath;
			textboxFMCPath.Text = _setting.FMCPath;
			textboxMCPath.Text = _setting.MCPath;
			checkProcessStartFMP7.Checked = _setting.ProcessStartFMP7;
			_font =
				new Font(
					_setting.EditorFontName,
					_setting.EditorFontSize,
					_setting.EditorFontStyle);
			labelEditorFontSample.Font = _font;

			nudTextWrapWidth.Value = _setting.EditorTextWidth;
			checkTextWrap.Checked = _setting.EditorTextWrap;
			checkAutoTextWrap.Checked = _setting.EditorAutoTextWrap;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_setting.FMP7Path = textboxFMP7Path.Text;
			_setting.FMC7Path = textboxFMC7Path.Text;
			_setting.MSDOSPlayerPath = textboxMSDOSPlayerPath.Text;
			_setting.FMCPath = textboxFMCPath.Text;
			_setting.MCPath = textboxMCPath.Text;
			_setting.ProcessStartFMP7 = checkProcessStartFMP7.Checked;
			_setting.EditorFontName = _font.FontFamily.Name;
			_setting.EditorFontSize = _font.Size;
			_setting.EditorFontStyle = _font.Style;
			_setting.EditorTextWidth = (int)nudTextWrapWidth.Value;
			_setting.EditorTextWrap = checkTextWrap.Checked;
			_setting.EditorAutoTextWrap = checkAutoTextWrap.Checked;

			EndDialog(DialogResult.OK);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			EndDialog(DialogResult.Cancel);
		}

		private void btnOpenDialogFMP7_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = MMLEditorResource.FileFilter_FMP7EXE;
			openFileDialog1.FileName = textboxFMP7Path.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textboxFMP7Path.Text = openFileDialog1.FileName;
			}
		}

		private void btnOpenDialogFMC7_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = MMLEditorResource.FileFilter_FMC7DLL;
			openFileDialog1.FileName = textboxFMC7Path.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textboxFMC7Path.Text = openFileDialog1.FileName;
			}
		}

		private void btnOpenDialogMSDOSPlayer_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = MMLEditorResource.FileFilter_MSDOSPlayerEXE;
			openFileDialog1.FileName = textboxMSDOSPlayerPath.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textboxMSDOSPlayerPath.Text = openFileDialog1.FileName;
			}
		}

		private void btnOpenDialogFMC_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = MMLEditorResource.FileFilter_FMCEXE;
			openFileDialog1.FileName = textboxFMCPath.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textboxFMCPath.Text = openFileDialog1.FileName;
			}
		}

		private void btnOpenDialogMC_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = MMLEditorResource.FileFilter_MCEXE;
			openFileDialog1.FileName = textboxMCPath.Text;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textboxMCPath.Text = openFileDialog1.FileName;
			}
		}

		private void buttonSelectFont_Click(object sender, EventArgs e)
		{
			fontDialog1.Font = _font;
			if (fontDialog1.ShowDialog() == DialogResult.OK)
			{
				labelEditorFontSample.Font = fontDialog1.Font;
				if (_font != null)
				{
					_font.Dispose();
				}
				_font = fontDialog1.Font;
			}
		}

		private void linkLabelFMP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(MMLEditorResource.URL_FMP);
		}

		private void linkLabelAzuki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(MMLEditorResource.URL_Azuki);
		}

		private void linkLabelAOSoft_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(MMLEditorResource.URL_AOSoft);
		}
	}
}
