﻿namespace FMMLEditor7
{
	partial class SettingDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpageFMPSetting = new System.Windows.Forms.TabPage();
			this.btnOpenDialogFMC7 = new System.Windows.Forms.Button();
			this.textboxFMC7Path = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOpenDialogFMP7 = new System.Windows.Forms.Button();
			this.textboxFMP7Path = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabpageEditorSetting = new System.Windows.Forms.TabPage();
			this.buttonSelectFont = new System.Windows.Forms.Button();
			this.labelEditorFontSample = new System.Windows.Forms.Label();
			this.nudTextWrapWidth = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.checkAutoTextWrap = new System.Windows.Forms.CheckBox();
			this.checkTextWrap = new System.Windows.Forms.CheckBox();
			this.tabpageAbout = new System.Windows.Forms.TabPage();
			this.linkLabelAOSoft = new System.Windows.Forms.LinkLabel();
			this.linkLabelAzuki = new System.Windows.Forms.LinkLabel();
			this.linkLabelFMP = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.labelCopyright = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.checkProcessStartFMP7 = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabpageFMPSetting.SuspendLayout();
			this.tabpageEditorSetting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTextWrapWidth)).BeginInit();
			this.tabpageAbout.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabpageFMPSetting);
			this.tabControl1.Controls.Add(this.tabpageEditorSetting);
			this.tabControl1.Controls.Add(this.tabpageAbout);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(430, 326);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpageFMPSetting
			// 
			this.tabpageFMPSetting.Controls.Add(this.label8);
			this.tabpageFMPSetting.Controls.Add(this.checkProcessStartFMP7);
			this.tabpageFMPSetting.Controls.Add(this.btnOpenDialogFMC7);
			this.tabpageFMPSetting.Controls.Add(this.textboxFMC7Path);
			this.tabpageFMPSetting.Controls.Add(this.label2);
			this.tabpageFMPSetting.Controls.Add(this.btnOpenDialogFMP7);
			this.tabpageFMPSetting.Controls.Add(this.textboxFMP7Path);
			this.tabpageFMPSetting.Controls.Add(this.label1);
			this.tabpageFMPSetting.Location = new System.Drawing.Point(4, 22);
			this.tabpageFMPSetting.Name = "tabpageFMPSetting";
			this.tabpageFMPSetting.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageFMPSetting.Size = new System.Drawing.Size(422, 300);
			this.tabpageFMPSetting.TabIndex = 0;
			this.tabpageFMPSetting.Text = "FMP / FMC";
			this.tabpageFMPSetting.UseVisualStyleBackColor = true;
			// 
			// btnOpenDialogFMC7
			// 
			this.btnOpenDialogFMC7.Location = new System.Drawing.Point(364, 108);
			this.btnOpenDialogFMC7.Name = "btnOpenDialogFMC7";
			this.btnOpenDialogFMC7.Size = new System.Drawing.Size(36, 23);
			this.btnOpenDialogFMC7.TabIndex = 5;
			this.btnOpenDialogFMC7.Text = "...";
			this.btnOpenDialogFMC7.UseVisualStyleBackColor = true;
			this.btnOpenDialogFMC7.Click += new System.EventHandler(this.btnOpenDialogFMC7_Click);
			// 
			// textboxFMC7Path
			// 
			this.textboxFMC7Path.Location = new System.Drawing.Point(21, 110);
			this.textboxFMC7Path.Name = "textboxFMC7Path";
			this.textboxFMC7Path.Size = new System.Drawing.Size(337, 19);
			this.textboxFMC7Path.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(169, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "FMC7 コンパイラモジュールへのパス";
			// 
			// btnOpenDialogFMP7
			// 
			this.btnOpenDialogFMP7.Location = new System.Drawing.Point(364, 54);
			this.btnOpenDialogFMP7.Name = "btnOpenDialogFMP7";
			this.btnOpenDialogFMP7.Size = new System.Drawing.Size(36, 23);
			this.btnOpenDialogFMP7.TabIndex = 2;
			this.btnOpenDialogFMP7.Text = "...";
			this.btnOpenDialogFMP7.UseVisualStyleBackColor = true;
			this.btnOpenDialogFMP7.Click += new System.EventHandler(this.btnOpenDialogFMP7_Click);
			// 
			// textboxFMP7Path
			// 
			this.textboxFMP7Path.Location = new System.Drawing.Point(21, 56);
			this.textboxFMP7Path.Name = "textboxFMP7Path";
			this.textboxFMP7Path.Size = new System.Drawing.Size(337, 19);
			this.textboxFMP7Path.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "FMP7 本体へのパス";
			// 
			// tabpageEditorSetting
			// 
			this.tabpageEditorSetting.Controls.Add(this.buttonSelectFont);
			this.tabpageEditorSetting.Controls.Add(this.labelEditorFontSample);
			this.tabpageEditorSetting.Controls.Add(this.nudTextWrapWidth);
			this.tabpageEditorSetting.Controls.Add(this.label3);
			this.tabpageEditorSetting.Controls.Add(this.checkAutoTextWrap);
			this.tabpageEditorSetting.Controls.Add(this.checkTextWrap);
			this.tabpageEditorSetting.Location = new System.Drawing.Point(4, 22);
			this.tabpageEditorSetting.Name = "tabpageEditorSetting";
			this.tabpageEditorSetting.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageEditorSetting.Size = new System.Drawing.Size(422, 300);
			this.tabpageEditorSetting.TabIndex = 1;
			this.tabpageEditorSetting.Text = "編集";
			this.tabpageEditorSetting.UseVisualStyleBackColor = true;
			// 
			// buttonSelectFont
			// 
			this.buttonSelectFont.Location = new System.Drawing.Point(28, 206);
			this.buttonSelectFont.Name = "buttonSelectFont";
			this.buttonSelectFont.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectFont.TabIndex = 5;
			this.buttonSelectFont.Text = "フォント選択";
			this.buttonSelectFont.UseVisualStyleBackColor = true;
			this.buttonSelectFont.Click += new System.EventHandler(this.buttonSelectFont_Click);
			// 
			// labelEditorFontSample
			// 
			this.labelEditorFontSample.BackColor = System.Drawing.SystemColors.Menu;
			this.labelEditorFontSample.Location = new System.Drawing.Point(26, 105);
			this.labelEditorFontSample.Name = "labelEditorFontSample";
			this.labelEditorFontSample.Size = new System.Drawing.Size(370, 80);
			this.labelEditorFontSample.TabIndex = 4;
			this.labelEditorFontSample.Text = "フォント ABC 123";
			this.labelEditorFontSample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// nudTextWrapWidth
			// 
			this.nudTextWrapWidth.Location = new System.Drawing.Point(230, 27);
			this.nudTextWrapWidth.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.nudTextWrapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudTextWrapWidth.Name = "nudTextWrapWidth";
			this.nudTextWrapWidth.Size = new System.Drawing.Size(49, 19);
			this.nudTextWrapWidth.TabIndex = 3;
			this.nudTextWrapWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(195, 29);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "桁数";
			// 
			// checkAutoTextWrap
			// 
			this.checkAutoTextWrap.AutoSize = true;
			this.checkAutoTextWrap.Location = new System.Drawing.Point(28, 65);
			this.checkAutoTextWrap.Name = "checkAutoTextWrap";
			this.checkAutoTextWrap.Size = new System.Drawing.Size(225, 16);
			this.checkAutoTextWrap.TabIndex = 1;
			this.checkAutoTextWrap.Text = "テキストの折り返しをウィンドウ幅に合わせる";
			this.checkAutoTextWrap.UseVisualStyleBackColor = true;
			// 
			// checkTextWrap
			// 
			this.checkTextWrap.AutoSize = true;
			this.checkTextWrap.Location = new System.Drawing.Point(28, 28);
			this.checkTextWrap.Name = "checkTextWrap";
			this.checkTextWrap.Size = new System.Drawing.Size(135, 16);
			this.checkTextWrap.TabIndex = 0;
			this.checkTextWrap.Text = "テキスト表示を折り返す";
			this.checkTextWrap.UseVisualStyleBackColor = true;
			// 
			// tabpageAbout
			// 
			this.tabpageAbout.Controls.Add(this.linkLabelAOSoft);
			this.tabpageAbout.Controls.Add(this.linkLabelAzuki);
			this.tabpageAbout.Controls.Add(this.linkLabelFMP);
			this.tabpageAbout.Controls.Add(this.label7);
			this.tabpageAbout.Controls.Add(this.label6);
			this.tabpageAbout.Controls.Add(this.label5);
			this.tabpageAbout.Controls.Add(this.labelCopyright);
			this.tabpageAbout.Controls.Add(this.labelVersion);
			this.tabpageAbout.Controls.Add(this.label4);
			this.tabpageAbout.Location = new System.Drawing.Point(4, 22);
			this.tabpageAbout.Name = "tabpageAbout";
			this.tabpageAbout.Size = new System.Drawing.Size(422, 300);
			this.tabpageAbout.TabIndex = 2;
			this.tabpageAbout.Text = "このアプリケーションについて";
			this.tabpageAbout.UseVisualStyleBackColor = true;
			// 
			// linkLabelAOSoft
			// 
			this.linkLabelAOSoft.AutoSize = true;
			this.linkLabelAOSoft.Location = new System.Drawing.Point(126, 142);
			this.linkLabelAOSoft.Name = "linkLabelAOSoft";
			this.linkLabelAOSoft.Size = new System.Drawing.Size(56, 12);
			this.linkLabelAOSoft.TabIndex = 7;
			this.linkLabelAOSoft.TabStop = true;
			this.linkLabelAOSoft.Text = "linkLabel1";
			this.linkLabelAOSoft.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAOSoft_LinkClicked);
			// 
			// linkLabelAzuki
			// 
			this.linkLabelAzuki.AutoSize = true;
			this.linkLabelAzuki.Location = new System.Drawing.Point(124, 270);
			this.linkLabelAzuki.Name = "linkLabelAzuki";
			this.linkLabelAzuki.Size = new System.Drawing.Size(59, 12);
			this.linkLabelAzuki.TabIndex = 6;
			this.linkLabelAzuki.TabStop = true;
			this.linkLabelAzuki.Text = "Azuki URL";
			this.linkLabelAzuki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAzuki_LinkClicked);
			// 
			// linkLabelFMP
			// 
			this.linkLabelFMP.AutoSize = true;
			this.linkLabelFMP.Location = new System.Drawing.Point(124, 224);
			this.linkLabelFMP.Name = "linkLabelFMP";
			this.linkLabelFMP.Size = new System.Drawing.Size(54, 12);
			this.linkLabelFMP.TabIndex = 5;
			this.linkLabelFMP.TabStop = true;
			this.linkLabelFMP.Text = "FMP URL";
			this.linkLabelFMP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFMP_LinkClicked);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label7.Location = new System.Drawing.Point(95, 248);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(163, 15);
			this.label7.TabIndex = 4;
			this.label7.Text = "Azuki Text Editor Engine";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(95, 200);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 15);
			this.label6.TabIndex = 3;
			this.label6.Text = "Guu and TeamFMP";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(77, 174);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Special Thanks";
			// 
			// labelCopyright
			// 
			this.labelCopyright.AutoSize = true;
			this.labelCopyright.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelCopyright.Location = new System.Drawing.Point(74, 118);
			this.labelCopyright.Name = "labelCopyright";
			this.labelCopyright.Size = new System.Drawing.Size(67, 15);
			this.labelCopyright.TabIndex = 0;
			this.labelCopyright.Text = "Copyrgiht";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.labelVersion.Location = new System.Drawing.Point(74, 86);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(58, 15);
			this.labelVersion.TabIndex = 1;
			this.labelVersion.Text = "Version.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(26, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(330, 50);
			this.label4.TabIndex = 0;
			this.label4.Text = "FMP7 MML Editor";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(367, 344);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(286, 344);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// fontDialog1
			// 
			this.fontDialog1.AllowVerticalFonts = false;
			this.fontDialog1.ShowEffects = false;
			// 
			// checkProcessStartFMP7
			// 
			this.checkProcessStartFMP7.AutoSize = true;
			this.checkProcessStartFMP7.Location = new System.Drawing.Point(21, 159);
			this.checkProcessStartFMP7.Name = "checkProcessStartFMP7";
			this.checkProcessStartFMP7.Size = new System.Drawing.Size(378, 16);
			this.checkProcessStartFMP7.TabIndex = 6;
			this.checkProcessStartFMP7.Text = "\"MMLコンパイルと再生\"を実行時、FMP7が起動していなかったら起動する。";
			this.checkProcessStartFMP7.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(19, 193);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(364, 12);
			this.label8.TabIndex = 7;
			this.label8.Text = "※通常の再生、停止コントロールはFMP7が起動している時のみ機能します。";
			// 
			// SettingDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(454, 376);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "設定";
			this.Shown += new System.EventHandler(this.SettingDialog_Shown);
			this.tabControl1.ResumeLayout(false);
			this.tabpageFMPSetting.ResumeLayout(false);
			this.tabpageFMPSetting.PerformLayout();
			this.tabpageEditorSetting.ResumeLayout(false);
			this.tabpageEditorSetting.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTextWrapWidth)).EndInit();
			this.tabpageAbout.ResumeLayout(false);
			this.tabpageAbout.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabpageFMPSetting;
		private System.Windows.Forms.TabPage tabpageEditorSetting;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpenDialogFMP7;
		private System.Windows.Forms.TextBox textboxFMP7Path;
		private System.Windows.Forms.Button btnOpenDialogFMC7;
		private System.Windows.Forms.TextBox textboxFMC7Path;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.CheckBox checkAutoTextWrap;
		private System.Windows.Forms.CheckBox checkTextWrap;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudTextWrapWidth;
		private System.Windows.Forms.Label labelEditorFontSample;
		private System.Windows.Forms.Button buttonSelectFont;
		private System.Windows.Forms.TabPage tabpageAbout;
		private System.Windows.Forms.Label labelCopyright;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel linkLabelAzuki;
		private System.Windows.Forms.LinkLabel linkLabelFMP;
		private System.Windows.Forms.LinkLabel linkLabelAOSoft;
		private System.Windows.Forms.CheckBox checkProcessStartFMP7;
		private System.Windows.Forms.Label label8;
	}
}