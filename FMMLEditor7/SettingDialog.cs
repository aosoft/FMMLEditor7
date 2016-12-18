using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace FMPMMLEditor7
{
	public partial class SettingDialog : Form
	{
		private Settings m_setting;
		private Font m_font;
		private Font m_deffont;

		public SettingDialog(Settings setting)
		{
			InitializeComponent();
			m_setting = setting;
			m_deffont = labelEditorFontSample.Font;

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
			labelEditorFontSample.Font = m_deffont;
			/*
			if (m_font != null)
			{
				m_font.Dispose();
				m_font = null;
			}
			 */
			m_font = null;
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
			textboxFMP7Path.Text = m_setting.FMP7Path;
			textboxFMC7Path.Text = m_setting.FMC7Path;
			checkProcessStartFMP7.Checked = m_setting.ProcessStartFMP7;
			m_font =
				new Font(
					m_setting.EditorFontName,
					m_setting.EditorFontSize,
					m_setting.EditorFontStyle);
			labelEditorFontSample.Font = m_font;

			nudTextWrapWidth.Value = m_setting.EditorTextWidth;
			checkTextWrap.Checked = m_setting.EditorTextWrap;
			checkAutoTextWrap.Checked = m_setting.EditorAutoTextWrap;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			m_setting.FMP7Path = textboxFMP7Path.Text;
			m_setting.FMC7Path = textboxFMC7Path.Text;
			m_setting.ProcessStartFMP7 = checkProcessStartFMP7.Checked;
			m_setting.EditorFontName = m_font.FontFamily.Name;
			m_setting.EditorFontSize = m_font.Size;
			m_setting.EditorFontStyle = m_font.Style;
			m_setting.EditorTextWidth = (int)nudTextWrapWidth.Value;
			m_setting.EditorTextWrap = checkTextWrap.Checked;
			m_setting.EditorAutoTextWrap = checkAutoTextWrap.Checked;

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

		private void buttonSelectFont_Click(object sender, EventArgs e)
		{
			fontDialog1.Font = m_font;
			if (fontDialog1.ShowDialog() == DialogResult.OK)
			{
				labelEditorFontSample.Font = fontDialog1.Font;
				if (m_font != null)
				{
					m_font.Dispose();
				}
				m_font = fontDialog1.Font;
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
