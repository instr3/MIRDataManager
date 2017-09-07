namespace MIRDataManager
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxScoreFilter = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoplay = new System.Windows.Forms.CheckBox();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.checkBoxExportMusic = new System.Windows.Forms.CheckBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonRename = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonDeleteUncreatedFolder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listViewNotCreated = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonRescan = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer2 = new AxWMPLib.AxWindowsMediaPlayer();
            this.checkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.labelHint = new System.Windows.Forms.Label();
            this.checkBoxAutoScan = new System.Windows.Forms.CheckBox();
            this.linkLabelAutoDownload = new System.Windows.Forms.LinkLabel();
            this.linkLabelManualDownload = new System.Windows.Forms.LinkLabel();
            this.groupBoxDownloadManager = new System.Windows.Forms.GroupBox();
            this.buttonDeleteOsuMap = new System.Windows.Forms.Button();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.buttonOMAExport = new System.Windows.Forms.Button();
            this.buttonExportMusic = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer2)).BeginInit();
            this.groupBoxDownloadManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBoxScoreFilter);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 76);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "评分";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(54, 18);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(101, 21);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "关键词";
            // 
            // comboBoxScoreFilter
            // 
            this.comboBoxScoreFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScoreFilter.FormattingEnabled = true;
            this.comboBoxScoreFilter.Items.AddRange(new object[] {
            "至少0",
            "至少1",
            "至少2",
            "至少3",
            "至少4",
            "至少5",
            "至少6",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxScoreFilter.Location = new System.Drawing.Point(54, 45);
            this.comboBoxScoreFilter.Name = "comboBoxScoreFilter";
            this.comboBoxScoreFilter.Size = new System.Drawing.Size(101, 20);
            this.comboBoxScoreFilter.TabIndex = 0;
            this.comboBoxScoreFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxScoreFilter_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxAutoplay);
            this.groupBox2.Controls.Add(this.buttonSelectAll);
            this.groupBox2.Controls.Add(this.checkBoxExportMusic);
            this.groupBox2.Controls.Add(this.buttonExport);
            this.groupBox2.Controls.Add(this.buttonDelete);
            this.groupBox2.Controls.Add(this.buttonRefresh);
            this.groupBox2.Controls.Add(this.buttonNew);
            this.groupBox2.Controls.Add(this.buttonRename);
            this.groupBox2.Location = new System.Drawing.Point(13, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 147);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作";
            // 
            // checkBoxAutoplay
            // 
            this.checkBoxAutoplay.AutoSize = true;
            this.checkBoxAutoplay.Checked = true;
            this.checkBoxAutoplay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoplay.Location = new System.Drawing.Point(79, 82);
            this.checkBoxAutoplay.Name = "checkBoxAutoplay";
            this.checkBoxAutoplay.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoplay.TabIndex = 8;
            this.checkBoxAutoplay.Text = "自动试听";
            this.checkBoxAutoplay.UseVisualStyleBackColor = true;
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(79, 49);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(64, 23);
            this.buttonSelectAll.TabIndex = 7;
            this.buttonSelectAll.Text = "全选";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // checkBoxExportMusic
            // 
            this.checkBoxExportMusic.AutoSize = true;
            this.checkBoxExportMusic.Location = new System.Drawing.Point(79, 111);
            this.checkBoxExportMusic.Name = "checkBoxExportMusic";
            this.checkBoxExportMusic.Size = new System.Drawing.Size(72, 16);
            this.checkBoxExportMusic.TabIndex = 6;
            this.checkBoxExportMusic.Text = "导出音乐";
            this.checkBoxExportMusic.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(9, 107);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(64, 23);
            this.buttonExport.TabIndex = 5;
            this.buttonExport.Text = "导出选中";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(9, 78);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(64, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "删除选中";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(9, 49);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(64, 23);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(9, 20);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(64, 23);
            this.buttonNew.TabIndex = 2;
            this.buttonNew.Text = "新建";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(80, 20);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(64, 23);
            this.buttonRename.TabIndex = 1;
            this.buttonRename.Text = "重命名";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(180, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 462);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(909, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "已标记曲目";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(5, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(898, 427);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyUp);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件";
            this.columnHeader1.Width = 400;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标题";
            this.columnHeader2.Width = 170;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "标记人";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "标记时间";
            this.columnHeader4.Width = 165;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "评分";
            this.columnHeader5.Width = 50;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.buttonDeleteUncreatedFolder);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.listViewNotCreated);
            this.tabPage2.Controls.Add(this.buttonRescan);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(909, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "未创建曲目";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(232, 407);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "搞2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonDeleteUncreatedFolder
            // 
            this.buttonDeleteUncreatedFolder.Location = new System.Drawing.Point(80, 407);
            this.buttonDeleteUncreatedFolder.Name = "buttonDeleteUncreatedFolder";
            this.buttonDeleteUncreatedFolder.Size = new System.Drawing.Size(68, 23);
            this.buttonDeleteUncreatedFolder.TabIndex = 12;
            this.buttonDeleteUncreatedFolder.Text = "删除目录";
            this.buttonDeleteUncreatedFolder.UseVisualStyleBackColor = true;
            this.buttonDeleteUncreatedFolder.Click += new System.EventHandler(this.buttonDeleteUncreatedFolder_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "搞";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listViewNotCreated
            // 
            this.listViewNotCreated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader9});
            this.listViewNotCreated.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewNotCreated.HideSelection = false;
            this.listViewNotCreated.Location = new System.Drawing.Point(6, 6);
            this.listViewNotCreated.Name = "listViewNotCreated";
            this.listViewNotCreated.Size = new System.Drawing.Size(897, 395);
            this.listViewNotCreated.TabIndex = 11;
            this.listViewNotCreated.UseCompatibleStateImageBehavior = false;
            this.listViewNotCreated.View = System.Windows.Forms.View.Details;
            this.listViewNotCreated.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            this.listViewNotCreated.DoubleClick += new System.EventHandler(this.listViewNotCreated_DoubleClick);
            this.listViewNotCreated.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewNotCreated_KeyUp);
            this.listViewNotCreated.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewNotCreated_MouseClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "文件夹";
            this.columnHeader6.Width = 700;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "创建时间";
            this.columnHeader9.Width = 165;
            // 
            // buttonRescan
            // 
            this.buttonRescan.Location = new System.Drawing.Point(6, 407);
            this.buttonRescan.Name = "buttonRescan";
            this.buttonRescan.Size = new System.Drawing.Size(68, 23);
            this.buttonRescan.TabIndex = 9;
            this.buttonRescan.Text = "重新扫描";
            this.buttonRescan.UseVisualStyleBackColor = true;
            this.buttonRescan.Click += new System.EventHandler(this.buttonRescan_Click);
            // 
            // axWindowsMediaPlayer2
            // 
            this.axWindowsMediaPlayer2.Enabled = true;
            this.axWindowsMediaPlayer2.Location = new System.Drawing.Point(11, 474);
            this.axWindowsMediaPlayer2.Name = "axWindowsMediaPlayer2";
            this.axWindowsMediaPlayer2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer2.OcxState")));
            this.axWindowsMediaPlayer2.Size = new System.Drawing.Size(1082, 46);
            this.axWindowsMediaPlayer2.TabIndex = 14;
            // 
            // checkBoxAutoRefresh
            // 
            this.checkBoxAutoRefresh.AutoSize = true;
            this.checkBoxAutoRefresh.Checked = true;
            this.checkBoxAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoRefresh.Location = new System.Drawing.Point(22, 248);
            this.checkBoxAutoRefresh.Name = "checkBoxAutoRefresh";
            this.checkBoxAutoRefresh.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoRefresh.TabIndex = 15;
            this.checkBoxAutoRefresh.Text = "自动刷新";
            this.checkBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // labelHint
            // 
            this.labelHint.Location = new System.Drawing.Point(20, 267);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new System.Drawing.Size(148, 125);
            this.labelHint.TabIndex = 16;
            this.labelHint.Text = "labelHint";
            // 
            // checkBoxAutoScan
            // 
            this.checkBoxAutoScan.AutoSize = true;
            this.checkBoxAutoScan.Location = new System.Drawing.Point(96, 248);
            this.checkBoxAutoScan.Name = "checkBoxAutoScan";
            this.checkBoxAutoScan.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoScan.TabIndex = 17;
            this.checkBoxAutoScan.Text = "自动扫描";
            this.checkBoxAutoScan.UseVisualStyleBackColor = true;
            // 
            // linkLabelAutoDownload
            // 
            this.linkLabelAutoDownload.AutoSize = true;
            this.linkLabelAutoDownload.Location = new System.Drawing.Point(6, 17);
            this.linkLabelAutoDownload.Name = "linkLabelAutoDownload";
            this.linkLabelAutoDownload.Size = new System.Drawing.Size(53, 12);
            this.linkLabelAutoDownload.TabIndex = 18;
            this.linkLabelAutoDownload.TabStop = true;
            this.linkLabelAutoDownload.Text = "自动下载";
            this.linkLabelAutoDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAutoDownload_LinkClicked);
            // 
            // linkLabelManualDownload
            // 
            this.linkLabelManualDownload.AutoSize = true;
            this.linkLabelManualDownload.Location = new System.Drawing.Point(75, 17);
            this.linkLabelManualDownload.Name = "linkLabelManualDownload";
            this.linkLabelManualDownload.Size = new System.Drawing.Size(53, 12);
            this.linkLabelManualDownload.TabIndex = 19;
            this.linkLabelManualDownload.TabStop = true;
            this.linkLabelManualDownload.Text = "手动下载";
            this.linkLabelManualDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelManualDownload_LinkClicked);
            // 
            // groupBoxDownloadManager
            // 
            this.groupBoxDownloadManager.Controls.Add(this.buttonDeleteOsuMap);
            this.groupBoxDownloadManager.Controls.Add(this.progressBarDownload);
            this.groupBoxDownloadManager.Controls.Add(this.linkLabelAutoDownload);
            this.groupBoxDownloadManager.Controls.Add(this.linkLabelManualDownload);
            this.groupBoxDownloadManager.Location = new System.Drawing.Point(11, 401);
            this.groupBoxDownloadManager.Name = "groupBoxDownloadManager";
            this.groupBoxDownloadManager.Size = new System.Drawing.Size(161, 63);
            this.groupBoxDownloadManager.TabIndex = 21;
            this.groupBoxDownloadManager.TabStop = false;
            this.groupBoxDownloadManager.Text = "osu!下载管理";
            this.groupBoxDownloadManager.Visible = false;
            // 
            // buttonDeleteOsuMap
            // 
            this.buttonDeleteOsuMap.Location = new System.Drawing.Point(45, 20);
            this.buttonDeleteOsuMap.Name = "buttonDeleteOsuMap";
            this.buttonDeleteOsuMap.Size = new System.Drawing.Size(69, 26);
            this.buttonDeleteOsuMap.TabIndex = 21;
            this.buttonDeleteOsuMap.Text = "删除谱面";
            this.buttonDeleteOsuMap.UseVisualStyleBackColor = true;
            this.buttonDeleteOsuMap.Click += new System.EventHandler(this.buttonDeleteOsuMap_Click);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(6, 32);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(145, 23);
            this.progressBarDownload.TabIndex = 20;
            // 
            // buttonOMAExport
            // 
            this.buttonOMAExport.Location = new System.Drawing.Point(17, 369);
            this.buttonOMAExport.Name = "buttonOMAExport";
            this.buttonOMAExport.Size = new System.Drawing.Size(64, 23);
            this.buttonOMAExport.TabIndex = 22;
            this.buttonOMAExport.Text = "OMA";
            this.buttonOMAExport.UseVisualStyleBackColor = true;
            this.buttonOMAExport.Click += new System.EventHandler(this.buttonOMAExport_Click);
            // 
            // buttonExportMusic
            // 
            this.buttonExportMusic.Location = new System.Drawing.Point(98, 369);
            this.buttonExportMusic.Name = "buttonExportMusic";
            this.buttonExportMusic.Size = new System.Drawing.Size(64, 23);
            this.buttonExportMusic.TabIndex = 23;
            this.buttonExportMusic.Text = "导出音乐";
            this.buttonExportMusic.UseVisualStyleBackColor = true;
            this.buttonExportMusic.Click += new System.EventHandler(this.buttonExportMusic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 527);
            this.Controls.Add(this.buttonExportMusic);
            this.Controls.Add(this.buttonOMAExport);
            this.Controls.Add(this.groupBoxDownloadManager);
            this.Controls.Add(this.checkBoxAutoScan);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.checkBoxAutoRefresh);
            this.Controls.Add(this.axWindowsMediaPlayer2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Idling";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer2)).EndInit();
            this.groupBoxDownloadManager.ResumeLayout(false);
            this.groupBoxDownloadManager.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxScoreFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.CheckBox checkBoxExportMusic;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonRescan;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer2;
        private System.Windows.Forms.CheckBox checkBoxAutoplay;
        private System.Windows.Forms.ListView listViewNotCreated;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.CheckBox checkBoxAutoRefresh;
        private System.Windows.Forms.Button buttonDeleteUncreatedFolder;
        private System.Windows.Forms.Label labelHint;
        private System.Windows.Forms.CheckBox checkBoxAutoScan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel linkLabelAutoDownload;
        private System.Windows.Forms.LinkLabel linkLabelManualDownload;
        private System.Windows.Forms.GroupBox groupBoxDownloadManager;
        public System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Button buttonDeleteOsuMap;
        private System.Windows.Forms.Button buttonOMAExport;
        private System.Windows.Forms.Button buttonExportMusic;
    }
}

