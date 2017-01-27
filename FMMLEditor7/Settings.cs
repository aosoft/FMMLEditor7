using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;
using System.Drawing;


namespace FMMLEditor7
{
	/// <summary>
	/// 設定管理クラス
	/// </summary>
	public class Settings
	{
		private string _settingFilePath;

		public string FMP7Path
		{
			get;
			set;
		}

		public string FMC7Path
		{
			get;
			set;
		}

		public bool ProcessStartFMP7
		{
			get;
			set;
		}

		public string EditorFontName
		{
			get;
			set;
		}

		public float EditorFontSize
		{
			get;
			set;
		}

		public FontStyle EditorFontStyle
		{
			get;
			set;
		}

		public int EditorTextWidth
		{
			get;
			set;
		}

		public bool EditorTextWrap
		{
			get;
			set;
		}

		public bool EditorAutoTextWrap
		{
			get;
			set;
		}

		public bool AvailableWindowPosInfo
		{
			get;
			set;
		}

		public int WindowLeft
		{
			get;
			set;
		}

		public int WindowTop
		{
			get;
			set;
		}

		public int WindowWidth
		{
			get;
			set;
		}

		public int WindowHeight
		{
			get;
			set;
		}

		public int EditorHeight
		{
			get;
			set;
		}

		public FormWindowState WindowState
		{
			get;
			set;
		}


		public Settings()
		{
			_settingFilePath =
				Path.Combine(
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					MMLEditorResource.SettingFile);

			AvailableWindowPosInfo = false;

			var appdir = Path.GetDirectoryName(
				System.Reflection.Assembly.GetExecutingAssembly().Location);

			FMP7Path = Path.Combine(appdir, MMLEditorResource.FMP7ProgramName);
			FMC7Path = Path.Combine(appdir, MMLEditorResource.FMC7DllName);
			ProcessStartFMP7 = true;

			EditorFontName = MMLEditorResource.DefaultFont_FontFamiliyName;
			EditorFontSize =
				float.Parse(MMLEditorResource.DefaultFont_FontSize);

			EditorTextWidth = int.Parse(MMLEditorResource.DefaultEditor_TextWidth);
			EditorTextWrap = true;
			EditorAutoTextWrap = true;
			EditorFontStyle = FontStyle.Regular;
		}

		private string GetNodeValue(XPathNavigator xnav, string path, string defvalue)
		{
			var xnav2 = xnav.SelectSingleNode(path);
			if (xnav2 == null)
			{
				return defvalue;
			}
			return xnav2.Value;
		}

		public void Load()
		{
			var doc = new XPathDocument(_settingFilePath);
			var xnav = doc.CreateNavigator();

			var xnavBase = xnav.SelectSingleNode("/Setting");

			if (xnavBase != null)
			{
				var xnavFMP = xnavBase.SelectSingleNode("FMP");
				if (xnavFMP != null)
				{
					FMP7Path = GetNodeValue(xnavFMP, "FMP7Path", FMP7Path);
					FMC7Path = GetNodeValue(xnavFMP, "FMC7Path", FMC7Path);
					ProcessStartFMP7 =
						bool.Parse(
							GetNodeValue(
								xnavFMP,
								"ProcessStartFMP7",
								ProcessStartFMP7.ToString()));
				}

				var xnavEditor = xnavBase.SelectSingleNode("Editor");
				if (xnavEditor != null)
				{
					EditorFontName =
						GetNodeValue(
							xnavEditor,
							"FontName",
							EditorFontName);
					EditorFontSize =
						float.Parse(
							GetNodeValue(
								xnavEditor,
								"FontSize",
								EditorFontSize.ToString()));
					EditorFontStyle =
						(FontStyle)Enum.Parse(
							typeof(FontStyle),
							GetNodeValue(
								xnavEditor,
								"FontStyle",
								EditorFontStyle.ToString()));
					EditorTextWrap =
						bool.Parse(
							GetNodeValue(
								xnavEditor,
								"TextWrap",
								EditorTextWrap.ToString()));
					EditorTextWidth =
						int.Parse(
							GetNodeValue(
								xnavEditor,
								"TextWidth",
								EditorTextWidth.ToString()));
					EditorAutoTextWrap =
						bool.Parse(
							GetNodeValue(
								xnavEditor,
								"AutoTextWrap",
								EditorAutoTextWrap.ToString()));
				}

				AvailableWindowPosInfo = false;
				var xnavWindowStateBlock = xnavBase.SelectSingleNode("WindowInfo");
				if (xnavWindowStateBlock != null)
				{
					var xnavWindowLeft = xnavWindowStateBlock.SelectSingleNode("Left");
					var xnavWindowTop = xnavWindowStateBlock.SelectSingleNode("Top");
					var xnavWindowWidth = xnavWindowStateBlock.SelectSingleNode("Width");
					var xnavWindowHeight = xnavWindowStateBlock.SelectSingleNode("Height");
					var xnavWindowState = xnavWindowStateBlock.SelectSingleNode("WindowState");
					var xnavEditorHeight = xnavWindowStateBlock.SelectSingleNode("EditorHeight");

					if (xnavWindowLeft != null &&
						xnavWindowTop != null &&
						xnavWindowWidth != null &&
						xnavWindowHeight != null &&
						xnavWindowState != null &&
						xnavEditorHeight != null)
					{
						WindowLeft = int.Parse(xnavWindowLeft.Value.ToString());
						WindowTop = int.Parse(xnavWindowTop.Value.ToString());
						WindowWidth = int.Parse(xnavWindowWidth.Value.ToString());
						WindowHeight = int.Parse(xnavWindowHeight.Value.ToString());
						WindowState =
							(FormWindowState)Enum.Parse(typeof(FormWindowState), xnavWindowState.ToString());
						EditorHeight = int.Parse(xnavEditorHeight.Value.ToString());
						AvailableWindowPosInfo = true;
					}
				}
			}
		}

		public void Save()
		{
			XmlWriterSettings xmlws = new XmlWriterSettings();
			xmlws.Indent = true;
			xmlws.IndentChars = "  ";
			xmlws.Encoding = Encoding.UTF8;

			string dir = Path.GetDirectoryName(Path.GetFullPath(_settingFilePath));
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}

			using (XmlWriter w = XmlWriter.Create(_settingFilePath, xmlws))
			{
				w.WriteStartDocument();
				w.WriteStartElement("Setting");
				{
					w.WriteStartElement("FMP");
					{
						w.WriteElementString("FMP7Path", FMP7Path);
						w.WriteElementString("FMC7Path", FMC7Path);
						w.WriteElementString("ProcessStartFMP7", ProcessStartFMP7.ToString());
					}
					w.WriteEndElement();

					w.WriteStartElement("Editor");
					{
						w.WriteElementString("FontName", EditorFontName);
						w.WriteElementString("FontSize", EditorFontSize.ToString());
						w.WriteElementString("FontStyle", EditorFontStyle.ToString());
						w.WriteElementString("TextWrap", EditorTextWrap.ToString());
						w.WriteElementString("TextWidth", EditorTextWidth.ToString());
						w.WriteElementString("AutoTextWrap", EditorAutoTextWrap.ToString());
					}
					w.WriteEndElement();

					if (AvailableWindowPosInfo)
					{
						w.WriteStartElement("WindowInfo");
						{
							w.WriteElementString("Left", WindowLeft.ToString());
							w.WriteElementString("Top", WindowTop.ToString());
							w.WriteElementString("Width", WindowWidth.ToString());
							w.WriteElementString("Height", WindowHeight.ToString());
							w.WriteElementString("WindowState", WindowState.ToString());
							w.WriteElementString("EditorHeight", EditorHeight.ToString());
						}
						w.WriteEndElement();
					}
				}
				w.WriteEndElement();
				w.WriteEndDocument();
			}
		}

	}
}
