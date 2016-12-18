namespace FMPMMLEditor7
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menuitemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemNewFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitemQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemCompileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemMMLCompileAndPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemMMLCompile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitemMoveToTextEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemMovieToCompileResult = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemMoveToCompileErrorReport = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemTool = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.menuitemStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuitemSetting = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpageCompileResult = new System.Windows.Forms.TabPage();
			this.listviewCompileResult = new System.Windows.Forms.ListView();
			this.colheadResultPart = new System.Windows.Forms.ColumnHeader();
			this.colheadResultSoundUnitType = new System.Windows.Forms.ColumnHeader();
			this.colheadResultTotalClock = new System.Windows.Forms.ColumnHeader();
			this.colheadResultLoopClock = new System.Windows.Forms.ColumnHeader();
			this.colheadResultBytes = new System.Windows.Forms.ColumnHeader();
			this.tabpageCompileErrorReport = new System.Windows.Forms.TabPage();
			this.listviewCompileErrorReport = new System.Windows.Forms.ListView();
			this.colheadErrorLogKind = new System.Windows.Forms.ColumnHeader();
			this.colheadErrorPart = new System.Windows.Forms.ColumnHeader();
			this.colheadErrorLineNumber = new System.Windows.Forms.ColumnHeader();
			this.colheadErrorMessage = new System.Windows.Forms.ColumnHeader();
			this.colheadErrorAlias = new System.Windows.Forms.ColumnHeader();
			this.colheadErrorMML = new System.Windows.Forms.ColumnHeader();
			this.tabpageCompileFileInfo = new System.Windows.Forms.TabPage();
			this.listviewFileInfo = new System.Windows.Forms.ListView();
			this.colheadFileInfoTitle = new System.Windows.Forms.ColumnHeader();
			this.colheadData = new System.Windows.Forms.ColumnHeader();
			this.tabpageCompileMessages = new System.Windows.Forms.TabPage();
			this.textboxMessages = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.labelCursorPosition = new System.Windows.Forms.ToolStripStatusLabel();
			this.progressPlayPosition = new System.Windows.Forms.ToolStripProgressBar();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabpageCompileResult.SuspendLayout();
			this.tabpageCompileErrorReport.SuspendLayout();
			this.tabpageCompileFileInfo.SuspendLayout();
			this.tabpageCompileMessages.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemFile,
            this.menuitemCompileMenu,
            this.menuitemTool});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(688, 26);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menuitemFile
			// 
			this.menuitemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemNewFile,
            this.menuitemOpen,
            this.menuitemSave,
            this.menuitemSaveAs,
            this.toolStripSeparator1,
            this.menuitemQuit});
			this.menuitemFile.Name = "menuitemFile";
			this.menuitemFile.Size = new System.Drawing.Size(85, 22);
			this.menuitemFile.Text = "ファイル(&F)";
			// 
			// menuitemNewFile
			// 
			this.menuitemNewFile.Name = "menuitemNewFile";
			this.menuitemNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.menuitemNewFile.Size = new System.Drawing.Size(274, 22);
			this.menuitemNewFile.Text = "新規作成(&N)";
			this.menuitemNewFile.Click += new System.EventHandler(this.menuitemNewFile_Click);
			// 
			// menuitemOpen
			// 
			this.menuitemOpen.Name = "menuitemOpen";
			this.menuitemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.menuitemOpen.Size = new System.Drawing.Size(274, 22);
			this.menuitemOpen.Text = "ファイルを開く(&O)";
			this.menuitemOpen.Click += new System.EventHandler(this.menuitemOpen_Click);
			// 
			// menuitemSave
			// 
			this.menuitemSave.Name = "menuitemSave";
			this.menuitemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.menuitemSave.Size = new System.Drawing.Size(274, 22);
			this.menuitemSave.Text = "ファイルを保存する(&S)";
			this.menuitemSave.Click += new System.EventHandler(this.menuitemSave_Click);
			// 
			// menuitemSaveAs
			// 
			this.menuitemSaveAs.Name = "menuitemSaveAs";
			this.menuitemSaveAs.Size = new System.Drawing.Size(274, 22);
			this.menuitemSaveAs.Text = "ファイルに名前をつけて保存する(&A)";
			this.menuitemSaveAs.Click += new System.EventHandler(this.menuitemSaveAs_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(271, 6);
			// 
			// menuitemQuit
			// 
			this.menuitemQuit.Name = "menuitemQuit";
			this.menuitemQuit.Size = new System.Drawing.Size(274, 22);
			this.menuitemQuit.Text = "終了する(&X)";
			this.menuitemQuit.Click += new System.EventHandler(this.menuitemQuit_Click);
			// 
			// menuitemCompileMenu
			// 
			this.menuitemCompileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemMMLCompileAndPlay,
            this.menuitemMMLCompile,
            this.toolStripSeparator3,
            this.menuitemMoveToTextEditor,
            this.menuitemMovieToCompileResult,
            this.menuitemMoveToCompileErrorReport});
			this.menuitemCompileMenu.Name = "menuitemCompileMenu";
			this.menuitemCompileMenu.Size = new System.Drawing.Size(80, 22);
			this.menuitemCompileMenu.Text = "コンパイル";
			// 
			// menuitemMMLCompileAndPlay
			// 
			this.menuitemMMLCompileAndPlay.Name = "menuitemMMLCompileAndPlay";
			this.menuitemMMLCompileAndPlay.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.menuitemMMLCompileAndPlay.Size = new System.Drawing.Size(221, 22);
			this.menuitemMMLCompileAndPlay.Text = "MMLコンパイルと再生";
			this.menuitemMMLCompileAndPlay.Click += new System.EventHandler(this.menuitemMMLCompileAndPlay_Click);
			// 
			// menuitemMMLCompile
			// 
			this.menuitemMMLCompile.Name = "menuitemMMLCompile";
			this.menuitemMMLCompile.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.menuitemMMLCompile.Size = new System.Drawing.Size(221, 22);
			this.menuitemMMLCompile.Text = "MMLコンパイル";
			this.menuitemMMLCompile.Click += new System.EventHandler(this.menuitemMMLCompile_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(218, 6);
			// 
			// menuitemMoveToTextEditor
			// 
			this.menuitemMoveToTextEditor.Name = "menuitemMoveToTextEditor";
			this.menuitemMoveToTextEditor.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.menuitemMoveToTextEditor.Size = new System.Drawing.Size(221, 22);
			this.menuitemMoveToTextEditor.Text = "MMLソースへ移動";
			this.menuitemMoveToTextEditor.Click += new System.EventHandler(this.menuitemMoveToTextEditor_Click);
			// 
			// menuitemMovieToCompileResult
			// 
			this.menuitemMovieToCompileResult.Name = "menuitemMovieToCompileResult";
			this.menuitemMovieToCompileResult.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.menuitemMovieToCompileResult.Size = new System.Drawing.Size(221, 22);
			this.menuitemMovieToCompileResult.Text = "コンパイル結果へ移動";
			this.menuitemMovieToCompileResult.Click += new System.EventHandler(this.menuitemMovieToCompileResult_Click);
			// 
			// menuitemMoveToCompileErrorReport
			// 
			this.menuitemMoveToCompileErrorReport.Name = "menuitemMoveToCompileErrorReport";
			this.menuitemMoveToCompileErrorReport.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.menuitemMoveToCompileErrorReport.Size = new System.Drawing.Size(221, 22);
			this.menuitemMoveToCompileErrorReport.Text = "エラーリストへ移動";
			this.menuitemMoveToCompileErrorReport.Click += new System.EventHandler(this.menuitemMoveToCompileErrorReport_Click);
			// 
			// menuitemTool
			// 
			this.menuitemTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemPlay,
            this.menuitemStop,
            this.toolStripSeparator2,
            this.menuitemSetting});
			this.menuitemTool.Name = "menuitemTool";
			this.menuitemTool.Size = new System.Drawing.Size(74, 22);
			this.menuitemTool.Text = "ツール(&T)";
			// 
			// menuitemPlay
			// 
			this.menuitemPlay.Name = "menuitemPlay";
			this.menuitemPlay.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.menuitemPlay.Size = new System.Drawing.Size(206, 22);
			this.menuitemPlay.Text = "再生開始／一時停止";
			this.menuitemPlay.Click += new System.EventHandler(this.menuitemPlay_Click);
			// 
			// menuitemStop
			// 
			this.menuitemStop.Name = "menuitemStop";
			this.menuitemStop.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.menuitemStop.Size = new System.Drawing.Size(206, 22);
			this.menuitemStop.Text = "再生停止";
			this.menuitemStop.Click += new System.EventHandler(this.menuitemStop_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(203, 6);
			// 
			// menuitemSetting
			// 
			this.menuitemSetting.Name = "menuitemSetting";
			this.menuitemSetting.Size = new System.Drawing.Size(206, 22);
			this.menuitemSetting.Text = "設定";
			this.menuitemSetting.Click += new System.EventHandler(this.menuitemSetting_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 26);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(688, 515);
			this.splitContainer1.SplitterDistance = 385;
			this.splitContainer1.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabpageCompileResult);
			this.tabControl1.Controls.Add(this.tabpageCompileErrorReport);
			this.tabControl1.Controls.Add(this.tabpageCompileFileInfo);
			this.tabControl1.Controls.Add(this.tabpageCompileMessages);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(688, 126);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpageCompileResult
			// 
			this.tabpageCompileResult.Controls.Add(this.listviewCompileResult);
			this.tabpageCompileResult.Location = new System.Drawing.Point(4, 4);
			this.tabpageCompileResult.Name = "tabpageCompileResult";
			this.tabpageCompileResult.Size = new System.Drawing.Size(680, 100);
			this.tabpageCompileResult.TabIndex = 1;
			this.tabpageCompileResult.Text = "コンパイル結果";
			this.tabpageCompileResult.UseVisualStyleBackColor = true;
			// 
			// listviewCompileResult
			// 
			this.listviewCompileResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colheadResultPart,
            this.colheadResultSoundUnitType,
            this.colheadResultTotalClock,
            this.colheadResultLoopClock,
            this.colheadResultBytes});
			this.listviewCompileResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewCompileResult.FullRowSelect = true;
			this.listviewCompileResult.Location = new System.Drawing.Point(0, 0);
			this.listviewCompileResult.MultiSelect = false;
			this.listviewCompileResult.Name = "listviewCompileResult";
			this.listviewCompileResult.Size = new System.Drawing.Size(680, 100);
			this.listviewCompileResult.TabIndex = 0;
			this.listviewCompileResult.UseCompatibleStateImageBehavior = false;
			this.listviewCompileResult.View = System.Windows.Forms.View.Details;
			// 
			// colheadResultPart
			// 
			this.colheadResultPart.Text = "パート";
			this.colheadResultPart.Width = 77;
			// 
			// colheadResultSoundUnitType
			// 
			this.colheadResultSoundUnitType.Text = "音源";
			this.colheadResultSoundUnitType.Width = 90;
			// 
			// colheadResultTotalClock
			// 
			this.colheadResultTotalClock.Text = "総クロック数";
			this.colheadResultTotalClock.Width = 120;
			// 
			// colheadResultLoopClock
			// 
			this.colheadResultLoopClock.Text = "ループクロック数";
			this.colheadResultLoopClock.Width = 120;
			// 
			// colheadResultBytes
			// 
			this.colheadResultBytes.Text = "出力バイト数";
			this.colheadResultBytes.Width = 120;
			// 
			// tabpageCompileErrorReport
			// 
			this.tabpageCompileErrorReport.Controls.Add(this.listviewCompileErrorReport);
			this.tabpageCompileErrorReport.Location = new System.Drawing.Point(4, 4);
			this.tabpageCompileErrorReport.Name = "tabpageCompileErrorReport";
			this.tabpageCompileErrorReport.Padding = new System.Windows.Forms.Padding(3);
			this.tabpageCompileErrorReport.Size = new System.Drawing.Size(680, 100);
			this.tabpageCompileErrorReport.TabIndex = 0;
			this.tabpageCompileErrorReport.Text = "エラー";
			this.tabpageCompileErrorReport.UseVisualStyleBackColor = true;
			// 
			// listviewCompileErrorReport
			// 
			this.listviewCompileErrorReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colheadErrorLogKind,
            this.colheadErrorPart,
            this.colheadErrorLineNumber,
            this.colheadErrorMessage,
            this.colheadErrorAlias,
            this.colheadErrorMML});
			this.listviewCompileErrorReport.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewCompileErrorReport.FullRowSelect = true;
			this.listviewCompileErrorReport.Location = new System.Drawing.Point(3, 3);
			this.listviewCompileErrorReport.MultiSelect = false;
			this.listviewCompileErrorReport.Name = "listviewCompileErrorReport";
			this.listviewCompileErrorReport.Size = new System.Drawing.Size(674, 94);
			this.listviewCompileErrorReport.TabIndex = 0;
			this.listviewCompileErrorReport.UseCompatibleStateImageBehavior = false;
			this.listviewCompileErrorReport.View = System.Windows.Forms.View.Details;
			this.listviewCompileErrorReport.DoubleClick += new System.EventHandler(this.listviewCompileErrorReport_DoubleClick);
			this.listviewCompileErrorReport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listviewCompileErrorReport_KeyPress);
			// 
			// colheadErrorLogKind
			// 
			this.colheadErrorLogKind.Text = "種別";
			this.colheadErrorLogKind.Width = 93;
			// 
			// colheadErrorPart
			// 
			this.colheadErrorPart.Text = "パート";
			this.colheadErrorPart.Width = 77;
			// 
			// colheadErrorLineNumber
			// 
			this.colheadErrorLineNumber.Text = "行番号";
			this.colheadErrorLineNumber.Width = 68;
			// 
			// colheadErrorMessage
			// 
			this.colheadErrorMessage.Text = "内容";
			this.colheadErrorMessage.Width = 279;
			// 
			// colheadErrorAlias
			// 
			this.colheadErrorAlias.Text = "エイリアス";
			// 
			// colheadErrorMML
			// 
			this.colheadErrorMML.Text = "MML";
			// 
			// tabpageCompileFileInfo
			// 
			this.tabpageCompileFileInfo.Controls.Add(this.listviewFileInfo);
			this.tabpageCompileFileInfo.Location = new System.Drawing.Point(4, 4);
			this.tabpageCompileFileInfo.Name = "tabpageCompileFileInfo";
			this.tabpageCompileFileInfo.Size = new System.Drawing.Size(680, 100);
			this.tabpageCompileFileInfo.TabIndex = 2;
			this.tabpageCompileFileInfo.Text = "ファイル情報";
			this.tabpageCompileFileInfo.UseVisualStyleBackColor = true;
			// 
			// listviewFileInfo
			// 
			this.listviewFileInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colheadFileInfoTitle,
            this.colheadData});
			this.listviewFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listviewFileInfo.FullRowSelect = true;
			this.listviewFileInfo.Location = new System.Drawing.Point(0, 0);
			this.listviewFileInfo.MultiSelect = false;
			this.listviewFileInfo.Name = "listviewFileInfo";
			this.listviewFileInfo.Size = new System.Drawing.Size(680, 100);
			this.listviewFileInfo.TabIndex = 1;
			this.listviewFileInfo.UseCompatibleStateImageBehavior = false;
			this.listviewFileInfo.View = System.Windows.Forms.View.Details;
			// 
			// colheadFileInfoTitle
			// 
			this.colheadFileInfoTitle.Text = "項目";
			this.colheadFileInfoTitle.Width = 117;
			// 
			// colheadData
			// 
			this.colheadData.Text = "内容";
			this.colheadData.Width = 400;
			// 
			// tabpageCompileMessages
			// 
			this.tabpageCompileMessages.Controls.Add(this.textboxMessages);
			this.tabpageCompileMessages.Location = new System.Drawing.Point(4, 4);
			this.tabpageCompileMessages.Name = "tabpageCompileMessages";
			this.tabpageCompileMessages.Size = new System.Drawing.Size(680, 100);
			this.tabpageCompileMessages.TabIndex = 3;
			this.tabpageCompileMessages.Text = "メッセージ";
			this.tabpageCompileMessages.UseVisualStyleBackColor = true;
			// 
			// textboxMessages
			// 
			this.textboxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textboxMessages.Location = new System.Drawing.Point(0, 0);
			this.textboxMessages.Multiline = true;
			this.textboxMessages.Name = "textboxMessages";
			this.textboxMessages.ReadOnly = true;
			this.textboxMessages.Size = new System.Drawing.Size(680, 100);
			this.textboxMessages.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelCursorPosition,
            this.progressPlayPosition});
			this.statusStrip1.Location = new System.Drawing.Point(0, 543);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(688, 23);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// labelCursorPosition
			// 
			this.labelCursorPosition.Name = "labelCursorPosition";
			this.labelCursorPosition.Size = new System.Drawing.Size(551, 18);
			this.labelCursorPosition.Spring = true;
			this.labelCursorPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// progressPlayPosition
			// 
			this.progressPlayPosition.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.progressPlayPosition.AutoSize = false;
			this.progressPlayPosition.Name = "progressPlayPosition";
			this.progressPlayPosition.Size = new System.Drawing.Size(120, 17);
			this.progressPlayPosition.Step = 1;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(688, 566);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabpageCompileResult.ResumeLayout(false);
			this.tabpageCompileErrorReport.ResumeLayout(false);
			this.tabpageCompileFileInfo.ResumeLayout(false);
			this.tabpageCompileMessages.ResumeLayout(false);
			this.tabpageCompileMessages.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem menuitemFile;
		private System.Windows.Forms.ToolStripMenuItem menuitemCompileMenu;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem menuitemNewFile;
		private System.Windows.Forms.ToolStripMenuItem menuitemOpen;
		private System.Windows.Forms.ToolStripMenuItem menuitemSave;
		private System.Windows.Forms.ToolStripMenuItem menuitemTool;
		private System.Windows.Forms.ToolStripMenuItem menuitemSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem menuitemQuit;
		private System.Windows.Forms.ToolStripMenuItem menuitemMMLCompileAndPlay;
		private System.Windows.Forms.ToolStripMenuItem menuitemMMLCompile;
		private System.Windows.Forms.ToolStripMenuItem menuitemPlay;
		private System.Windows.Forms.ToolStripMenuItem menuitemStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menuitemSetting;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabpageCompileErrorReport;
		private System.Windows.Forms.ListView listviewCompileErrorReport;
		private System.Windows.Forms.ColumnHeader colheadErrorLineNumber;
		private System.Windows.Forms.ColumnHeader colheadErrorLogKind;
		private System.Windows.Forms.ColumnHeader colheadErrorMessage;
		private System.Windows.Forms.ColumnHeader colheadErrorPart;
		private System.Windows.Forms.TabPage tabpageCompileResult;
		private System.Windows.Forms.ListView listviewCompileResult;
		private System.Windows.Forms.ColumnHeader colheadErrorAlias;
		private System.Windows.Forms.ColumnHeader colheadErrorMML;
		private System.Windows.Forms.ColumnHeader colheadResultPart;
		private System.Windows.Forms.ColumnHeader colheadResultSoundUnitType;
		private System.Windows.Forms.ColumnHeader colheadResultTotalClock;
		private System.Windows.Forms.ColumnHeader colheadResultLoopClock;
		private System.Windows.Forms.ColumnHeader colheadResultBytes;
		private System.Windows.Forms.TabPage tabpageCompileFileInfo;
		private System.Windows.Forms.TabPage tabpageCompileMessages;
		private System.Windows.Forms.ListView listviewFileInfo;
		private System.Windows.Forms.ColumnHeader colheadFileInfoTitle;
		private System.Windows.Forms.ColumnHeader colheadData;
		private System.Windows.Forms.TextBox textboxMessages;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem menuitemMovieToCompileResult;
		private System.Windows.Forms.ToolStripMenuItem menuitemMoveToCompileErrorReport;
		private System.Windows.Forms.ToolStripMenuItem menuitemMoveToTextEditor;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar progressPlayPosition;
		private System.Windows.Forms.ToolStripStatusLabel labelCursorPosition;
		private System.Windows.Forms.Timer timer1;
	}
}

