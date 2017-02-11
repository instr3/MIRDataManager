namespace MIREditor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TimelinePictureBox = new System.Windows.Forms.PictureBox();
            this.openInfoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.logText = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开最近ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开最近项1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开最近项2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开最近项3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重做ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInfoFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openOSUFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonRightBracket = new System.Windows.Forms.Button();
            this.buttonLeftBracket = new System.Windows.Forms.Button();
            this.checkBoxMouseSwitch = new System.Windows.Forms.CheckBox();
            this.labelScale = new System.Windows.Forms.Label();
            this.buttonPause = new System.Windows.Forms.Button();
            this.radioButtonRelative = new System.Windows.Forms.RadioButton();
            this.radioButtonAbsolute = new System.Windows.Forms.RadioButton();
            this.trackBarMIDIDelay = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxAutoPlayChord = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.trackBarVolumeMIDI = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBarVolumeMain = new System.Windows.Forms.TrackBar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.checkBoxExportMusic = new System.Windows.Forms.CheckBox();
            this.button_ExportTXT = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxOSUMapID = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxConfigConfidence = new System.Windows.Forms.ListBox();
            this.textBoxConfigTagger = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.button_BERemoveRight = new System.Windows.Forms.Button();
            this.button_BERemoveLeft = new System.Windows.Forms.Button();
            this.button_BEExtendBeat = new System.Windows.Forms.Button();
            this.buttonCutInsertChord = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button_TESwitchMajMin = new System.Windows.Forms.Button();
            this.comboBox_TETonalty = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button_TESelectAll = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBox_Metre = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button_BEBarStart = new System.Windows.Forms.Button();
            this.button_BENew = new System.Windows.Forms.Button();
            this.button_BEDelete = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button_BESelectAll = new System.Windows.Forms.Button();
            this.button_BERor = new System.Windows.Forms.Button();
            this.button_BERol = new System.Windows.Forms.Button();
            this.button_BENormalize = new System.Windows.Forms.Button();
            this.button_BEMerge = new System.Windows.Forms.Button();
            this.button_BEDivide = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxChordKeyboard = new System.Windows.Forms.CheckBox();
            this.groupBoxChordKeyboard = new System.Windows.Forms.GroupBox();
            this.pianoLabelQ = new System.Windows.Forms.Label();
            this.pianoLabelX = new System.Windows.Forms.Label();
            this.pianoLabelN = new System.Windows.Forms.Label();
            this.dataGridViewChord = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRawChord = new System.Windows.Forms.Button();
            this.comboBoxAlignBeats = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChordLabelQ = new System.Windows.Forms.Label();
            this.ChordLabelX = new System.Windows.Forms.Label();
            this.ChordLabelN = new System.Windows.Forms.Label();
            this.ChordLabel12 = new System.Windows.Forms.Label();
            this.ChordLabel11 = new System.Windows.Forms.Label();
            this.ChordLabel9 = new System.Windows.Forms.Label();
            this.ChordLabel10 = new System.Windows.Forms.Label();
            this.ChordLabel8 = new System.Windows.Forms.Label();
            this.ChordLabel7 = new System.Windows.Forms.Label();
            this.ChordLabel6 = new System.Windows.Forms.Label();
            this.ChordLabel5 = new System.Windows.Forms.Label();
            this.ChordLabel4 = new System.Windows.Forms.Label();
            this.ChordLabel3 = new System.Windows.Forms.Label();
            this.ChordLabel2 = new System.Windows.Forms.Label();
            this.ChordLabel1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.buttonVamp = new System.Windows.Forms.Button();
            this.buttonChroma = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TimelinePictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMIDIDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMIDI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMain)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxChordKeyboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChord)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimelinePictureBox
            // 
            this.TimelinePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimelinePictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.TimelinePictureBox.Location = new System.Drawing.Point(12, 3);
            this.TimelinePictureBox.Name = "TimelinePictureBox";
            this.TimelinePictureBox.Size = new System.Drawing.Size(600, 200);
            this.TimelinePictureBox.TabIndex = 1;
            this.TimelinePictureBox.TabStop = false;
            this.TimelinePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseDown);
            this.TimelinePictureBox.MouseEnter += new System.EventHandler(this.TimelinePictureBox_MouseEnter);
            this.TimelinePictureBox.MouseLeave += new System.EventHandler(this.TimelinePictureBox_MouseLeave);
            this.TimelinePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseMove);
            this.TimelinePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TimelinePictureBox_MouseUp);
            // 
            // openInfoFileDialog
            // 
            this.openInfoFileDialog.Filter = "存档标注文件|*.arc";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // logText
            // 
            this.logText.Enabled = false;
            this.logText.Location = new System.Drawing.Point(16, 571);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logText.Size = new System.Drawing.Size(628, 54);
            this.logText.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(660, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.打开最近ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.新建ToolStripMenuItem.Text = "新建...";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.打开ToolStripMenuItem.Text = "打开...";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 打开最近ToolStripMenuItem
            // 
            this.打开最近ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开最近项1ToolStripMenuItem,
            this.打开最近项2ToolStripMenuItem,
            this.打开最近项3ToolStripMenuItem});
            this.打开最近ToolStripMenuItem.Enabled = false;
            this.打开最近ToolStripMenuItem.Name = "打开最近ToolStripMenuItem";
            this.打开最近ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.打开最近ToolStripMenuItem.Text = "打开最近";
            // 
            // 打开最近项1ToolStripMenuItem
            // 
            this.打开最近项1ToolStripMenuItem.Name = "打开最近项1ToolStripMenuItem";
            this.打开最近项1ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.打开最近项1ToolStripMenuItem.Text = "打开最近项1";
            // 
            // 打开最近项2ToolStripMenuItem
            // 
            this.打开最近项2ToolStripMenuItem.Name = "打开最近项2ToolStripMenuItem";
            this.打开最近项2ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.打开最近项2ToolStripMenuItem.Text = "打开最近项2";
            // 
            // 打开最近项3ToolStripMenuItem
            // 
            this.打开最近项3ToolStripMenuItem.Name = "打开最近项3ToolStripMenuItem";
            this.打开最近项3ToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.打开最近项3ToolStripMenuItem.Text = "打开最近项3";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(196, 6);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.另存为ToolStripMenuItem.Text = "另存为...";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(196, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤销ToolStripMenuItem,
            this.重做ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.DropDownOpening += new System.EventHandler(this.编辑ToolStripMenuItem_DropDownOpening);
            // 
            // 撤销ToolStripMenuItem
            // 
            this.撤销ToolStripMenuItem.Name = "撤销ToolStripMenuItem";
            this.撤销ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.撤销ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.撤销ToolStripMenuItem.Text = "撤销";
            this.撤销ToolStripMenuItem.Click += new System.EventHandler(this.撤销ToolStripMenuItem_Click);
            // 
            // 重做ToolStripMenuItem
            // 
            this.重做ToolStripMenuItem.Name = "重做ToolStripMenuItem";
            this.重做ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.重做ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.重做ToolStripMenuItem.Text = "重做";
            this.重做ToolStripMenuItem.Click += new System.EventHandler(this.重做ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.关于ToolStripMenuItem.Text = "关于...";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // saveInfoFileDialog
            // 
            this.saveInfoFileDialog.DefaultExt = "myinfo";
            this.saveInfoFileDialog.Filter = "存档标注文件|*.arc";
            // 
            // openOSUFileDialog
            // 
            this.openOSUFileDialog.Filter = "OSU文件|*.osu";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.buttonRightBracket);
            this.groupBox4.Controls.Add(this.buttonLeftBracket);
            this.groupBox4.Controls.Add(this.checkBoxMouseSwitch);
            this.groupBox4.Controls.Add(this.labelScale);
            this.groupBox4.Controls.Add(this.buttonPause);
            this.groupBox4.Controls.Add(this.radioButtonRelative);
            this.groupBox4.Controls.Add(this.radioButtonAbsolute);
            this.groupBox4.Controls.Add(this.trackBarMIDIDelay);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.checkBoxAutoPlayChord);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.trackBarVolumeMIDI);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.trackBarVolumeMain);
            this.groupBox4.Location = new System.Drawing.Point(5, 210);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(611, 65);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "主控";
            // 
            // buttonRightBracket
            // 
            this.buttonRightBracket.Location = new System.Drawing.Point(392, 39);
            this.buttonRightBracket.Name = "buttonRightBracket";
            this.buttonRightBracket.Size = new System.Drawing.Size(30, 20);
            this.buttonRightBracket.TabIndex = 29;
            this.buttonRightBracket.Text = ">";
            this.buttonRightBracket.UseVisualStyleBackColor = true;
            this.buttonRightBracket.Click += new System.EventHandler(this.buttonRightBracket_Click);
            // 
            // buttonLeftBracket
            // 
            this.buttonLeftBracket.Location = new System.Drawing.Point(356, 39);
            this.buttonLeftBracket.Name = "buttonLeftBracket";
            this.buttonLeftBracket.Size = new System.Drawing.Size(30, 20);
            this.buttonLeftBracket.TabIndex = 28;
            this.buttonLeftBracket.Text = "<";
            this.buttonLeftBracket.UseVisualStyleBackColor = true;
            this.buttonLeftBracket.Click += new System.EventHandler(this.buttonLeftBracket_Click);
            // 
            // checkBoxMouseSwitch
            // 
            this.checkBoxMouseSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxMouseSwitch.AutoSize = true;
            this.checkBoxMouseSwitch.Location = new System.Drawing.Point(287, 39);
            this.checkBoxMouseSwitch.Name = "checkBoxMouseSwitch";
            this.checkBoxMouseSwitch.Size = new System.Drawing.Size(63, 22);
            this.checkBoxMouseSwitch.TabIndex = 26;
            this.checkBoxMouseSwitch.Text = "左键试听";
            this.checkBoxMouseSwitch.UseVisualStyleBackColor = true;
            this.checkBoxMouseSwitch.CheckedChanged += new System.EventHandler(this.checkBoxMouseSwitch_CheckedChanged);
            // 
            // labelScale
            // 
            this.labelScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelScale.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelScale.Location = new System.Drawing.Point(545, 13);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(60, 46);
            this.labelScale.TabIndex = 22;
            this.labelScale.Text = "<-缩放->";
            this.labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelScale.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelScale_MouseDown);
            this.labelScale.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelScale_MouseMove);
            this.labelScale.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelScale_MouseUp);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(490, 14);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(50, 45);
            this.buttonPause.TabIndex = 9;
            this.buttonPause.Text = "暂停";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // radioButtonRelative
            // 
            this.radioButtonRelative.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonRelative.AutoSize = true;
            this.radioButtonRelative.Checked = true;
            this.radioButtonRelative.Location = new System.Drawing.Point(84, 39);
            this.radioButtonRelative.Name = "radioButtonRelative";
            this.radioButtonRelative.Size = new System.Drawing.Size(63, 22);
            this.radioButtonRelative.TabIndex = 8;
            this.radioButtonRelative.TabStop = true;
            this.radioButtonRelative.Text = "相对和弦";
            this.radioButtonRelative.UseVisualStyleBackColor = true;
            this.radioButtonRelative.CheckedChanged += new System.EventHandler(this.radioButtonRelative_CheckedChanged);
            // 
            // radioButtonAbsolute
            // 
            this.radioButtonAbsolute.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButtonAbsolute.AutoSize = true;
            this.radioButtonAbsolute.Location = new System.Drawing.Point(7, 39);
            this.radioButtonAbsolute.Name = "radioButtonAbsolute";
            this.radioButtonAbsolute.Size = new System.Drawing.Size(63, 22);
            this.radioButtonAbsolute.TabIndex = 7;
            this.radioButtonAbsolute.Text = "绝对和弦";
            this.radioButtonAbsolute.UseVisualStyleBackColor = true;
            this.radioButtonAbsolute.CheckedChanged += new System.EventHandler(this.radioButtonAbsolute_CheckedChanged);
            // 
            // trackBarMIDIDelay
            // 
            this.trackBarMIDIDelay.Location = new System.Drawing.Point(380, 14);
            this.trackBarMIDIDelay.Maximum = 200;
            this.trackBarMIDIDelay.Name = "trackBarMIDIDelay";
            this.trackBarMIDIDelay.Size = new System.Drawing.Size(104, 45);
            this.trackBarMIDIDelay.TabIndex = 6;
            this.trackBarMIDIDelay.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMIDIDelay.Value = 40;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(321, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 5;
            this.label12.Text = "MIDI延时";
            // 
            // checkBoxAutoPlayChord
            // 
            this.checkBoxAutoPlayChord.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxAutoPlayChord.AutoSize = true;
            this.checkBoxAutoPlayChord.Location = new System.Drawing.Point(170, 39);
            this.checkBoxAutoPlayChord.Name = "checkBoxAutoPlayChord";
            this.checkBoxAutoPlayChord.Size = new System.Drawing.Size(111, 22);
            this.checkBoxAutoPlayChord.TabIndex = 4;
            this.checkBoxAutoPlayChord.Text = "自动播放MIDI和弦";
            this.checkBoxAutoPlayChord.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(160, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "MIDI音量";
            // 
            // trackBarVolumeMIDI
            // 
            this.trackBarVolumeMIDI.Location = new System.Drawing.Point(211, 14);
            this.trackBarVolumeMIDI.Maximum = 100;
            this.trackBarVolumeMIDI.Name = "trackBarVolumeMIDI";
            this.trackBarVolumeMIDI.Size = new System.Drawing.Size(104, 45);
            this.trackBarVolumeMIDI.TabIndex = 2;
            this.trackBarVolumeMIDI.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarVolumeMIDI.Value = 70;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "主音量";
            // 
            // trackBarVolumeMain
            // 
            this.trackBarVolumeMain.Location = new System.Drawing.Point(50, 14);
            this.trackBarVolumeMain.Maximum = 100;
            this.trackBarVolumeMain.Name = "trackBarVolumeMain";
            this.trackBarVolumeMain.Size = new System.Drawing.Size(104, 45);
            this.trackBarVolumeMain.TabIndex = 0;
            this.trackBarVolumeMain.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarVolumeMain.Value = 50;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox10);
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(628, 506);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "配置与导出";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.checkBoxExportMusic);
            this.groupBox10.Controls.Add(this.button_ExportTXT);
            this.groupBox10.Location = new System.Drawing.Point(231, 288);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(260, 209);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "导出";
            // 
            // checkBoxExportMusic
            // 
            this.checkBoxExportMusic.AutoSize = true;
            this.checkBoxExportMusic.Location = new System.Drawing.Point(23, 53);
            this.checkBoxExportMusic.Name = "checkBoxExportMusic";
            this.checkBoxExportMusic.Size = new System.Drawing.Size(96, 16);
            this.checkBoxExportMusic.TabIndex = 2;
            this.checkBoxExportMusic.Text = "同时导出音乐";
            this.checkBoxExportMusic.UseVisualStyleBackColor = true;
            // 
            // button_ExportTXT
            // 
            this.button_ExportTXT.Location = new System.Drawing.Point(23, 24);
            this.button_ExportTXT.Name = "button_ExportTXT";
            this.button_ExportTXT.Size = new System.Drawing.Size(155, 23);
            this.button_ExportTXT.TabIndex = 1;
            this.button_ExportTXT.Text = "导出绝对和弦->数据集";
            this.button_ExportTXT.UseVisualStyleBackColor = true;
            this.button_ExportTXT.Click += new System.EventHandler(this.button_ExportTXT_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.textBoxOSUMapID);
            this.groupBox7.Controls.Add(this.buttonSave);
            this.groupBox7.Controls.Add(this.comboBoxConfigConfidence);
            this.groupBox7.Controls.Add(this.textBoxConfigTagger);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Location = new System.Drawing.Point(7, 287);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(217, 210);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "重要配置";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 156);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 12);
            this.label17.TabIndex = 6;
            this.label17.Text = "mapID";
            // 
            // textBoxOSUMapID
            // 
            this.textBoxOSUMapID.Enabled = false;
            this.textBoxOSUMapID.Location = new System.Drawing.Point(54, 153);
            this.textBoxOSUMapID.Name = "textBoxOSUMapID";
            this.textBoxOSUMapID.Size = new System.Drawing.Size(133, 21);
            this.textBoxOSUMapID.TabIndex = 5;
            this.textBoxOSUMapID.TextChanged += new System.EventHandler(this.textBoxOSUMapID_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(32, 180);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(155, 22);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "保存快捷键(Ctrl+S)";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxConfigConfidence
            // 
            this.comboBoxConfigConfidence.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxConfigConfidence.FormattingEnabled = true;
            this.comboBoxConfigConfidence.ItemHeight = 12;
            this.comboBoxConfigConfidence.Items.AddRange(new object[] {
            "0（不适合标注）",
            "1（未标注）",
            "2（不完整标注）",
            "3（未确定正确）",
            "4（三和弦基本正确）",
            "5（三和弦核验正确）",
            "6（七和弦基本正确）",
            "7（七和弦核验正确）"});
            this.comboBoxConfigConfidence.Location = new System.Drawing.Point(54, 17);
            this.comboBoxConfigConfidence.Name = "comboBoxConfigConfidence";
            this.comboBoxConfigConfidence.Size = new System.Drawing.Size(133, 100);
            this.comboBoxConfigConfidence.TabIndex = 4;
            this.comboBoxConfigConfidence.SelectedIndexChanged += new System.EventHandler(this.comboBoxConfigConfidence_SelectedIndexChanged);
            // 
            // textBoxConfigTagger
            // 
            this.textBoxConfigTagger.Enabled = false;
            this.textBoxConfigTagger.Location = new System.Drawing.Point(54, 127);
            this.textBoxConfigTagger.Name = "textBoxConfigTagger";
            this.textBoxConfigTagger.Size = new System.Drawing.Size(133, 21);
            this.textBoxConfigTagger.TabIndex = 3;
            this.textBoxConfigTagger.TextChanged += new System.EventHandler(this.textBoxConfigTagger_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 130);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 2;
            this.label16.Text = "标注者";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "分级";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox12);
            this.tabPage2.Controls.Add(this.groupBox9);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(628, 506);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "节拍与调性";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.White;
            this.groupBox12.Controls.Add(this.button_BERemoveRight);
            this.groupBox12.Controls.Add(this.button_BERemoveLeft);
            this.groupBox12.Controls.Add(this.button_BEExtendBeat);
            this.groupBox12.Controls.Add(this.buttonCutInsertChord);
            this.groupBox12.Location = new System.Drawing.Point(203, 287);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(214, 84);
            this.groupBox12.TabIndex = 16;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "杂项操作";
            // 
            // button_BERemoveRight
            // 
            this.button_BERemoveRight.Location = new System.Drawing.Point(110, 49);
            this.button_BERemoveRight.Name = "button_BERemoveRight";
            this.button_BERemoveRight.Size = new System.Drawing.Size(95, 23);
            this.button_BERemoveRight.TabIndex = 4;
            this.button_BERemoveRight.Text = "删除之后节拍";
            this.button_BERemoveRight.UseVisualStyleBackColor = true;
            this.button_BERemoveRight.Click += new System.EventHandler(this.button_BERemoveRight_Click);
            // 
            // button_BERemoveLeft
            // 
            this.button_BERemoveLeft.Location = new System.Drawing.Point(9, 49);
            this.button_BERemoveLeft.Name = "button_BERemoveLeft";
            this.button_BERemoveLeft.Size = new System.Drawing.Size(95, 23);
            this.button_BERemoveLeft.TabIndex = 3;
            this.button_BERemoveLeft.Text = "删除之前节拍";
            this.button_BERemoveLeft.UseVisualStyleBackColor = true;
            this.button_BERemoveLeft.Click += new System.EventHandler(this.button_BERemoveLeft_Click);
            // 
            // button_BEExtendBeat
            // 
            this.button_BEExtendBeat.Location = new System.Drawing.Point(110, 20);
            this.button_BEExtendBeat.Name = "button_BEExtendBeat";
            this.button_BEExtendBeat.Size = new System.Drawing.Size(95, 23);
            this.button_BEExtendBeat.TabIndex = 2;
            this.button_BEExtendBeat.Text = "延拓节拍至此";
            this.button_BEExtendBeat.UseVisualStyleBackColor = true;
            this.button_BEExtendBeat.Click += new System.EventHandler(this.button_BEExtendBeat_Click);
            // 
            // buttonCutInsertChord
            // 
            this.buttonCutInsertChord.Location = new System.Drawing.Point(9, 20);
            this.buttonCutInsertChord.Name = "buttonCutInsertChord";
            this.buttonCutInsertChord.Size = new System.Drawing.Size(95, 23);
            this.buttonCutInsertChord.TabIndex = 0;
            this.buttonCutInsertChord.Text = "和弦切分(Ins)";
            this.buttonCutInsertChord.UseVisualStyleBackColor = true;
            this.buttonCutInsertChord.Click += new System.EventHandler(this.buttonCutInsertChord_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.White;
            this.groupBox9.Controls.Add(this.button_TESwitchMajMin);
            this.groupBox9.Controls.Add(this.comboBox_TETonalty);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.button_TESelectAll);
            this.groupBox9.Location = new System.Drawing.Point(7, 406);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(175, 84);
            this.groupBox9.TabIndex = 15;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "调性区间操作";
            // 
            // button_TESwitchMajMin
            // 
            this.button_TESwitchMajMin.Location = new System.Drawing.Point(90, 20);
            this.button_TESwitchMajMin.Name = "button_TESwitchMajMin";
            this.button_TESwitchMajMin.Size = new System.Drawing.Size(75, 23);
            this.button_TESwitchMajMin.TabIndex = 8;
            this.button_TESwitchMajMin.Text = "关系调切换";
            this.button_TESwitchMajMin.UseVisualStyleBackColor = true;
            this.button_TESwitchMajMin.Click += new System.EventHandler(this.button_TESwitchMajMin_Click);
            // 
            // comboBox_TETonalty
            // 
            this.comboBox_TETonalty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_TETonalty.FormattingEnabled = true;
            this.comboBox_TETonalty.Items.AddRange(new object[] {
            "C Maj",
            "C# Maj",
            "D Maj",
            "Eb Maj",
            "E Maj",
            "F Maj",
            "F# Maj",
            "G Maj",
            "Ab Maj",
            "A Maj",
            "Bb Maj",
            "B Maj",
            "C min",
            "C# min",
            "D min",
            "Eb min",
            "E min",
            "F min",
            "F# min",
            "G min",
            "Ab min",
            "A min",
            "Bb min",
            "B min",
            "?"});
            this.comboBox_TETonalty.Location = new System.Drawing.Point(90, 48);
            this.comboBox_TETonalty.Name = "comboBox_TETonalty";
            this.comboBox_TETonalty.Size = new System.Drawing.Size(75, 20);
            this.comboBox_TETonalty.TabIndex = 7;
            this.comboBox_TETonalty.SelectedIndexChanged += new System.EventHandler(this.comboBox_TETonalty_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 6;
            this.label15.Text = "修改调性：";
            // 
            // button_TESelectAll
            // 
            this.button_TESelectAll.Location = new System.Drawing.Point(9, 20);
            this.button_TESelectAll.Name = "button_TESelectAll";
            this.button_TESelectAll.Size = new System.Drawing.Size(75, 23);
            this.button_TESelectAll.TabIndex = 5;
            this.button_TESelectAll.Text = "全选";
            this.button_TESelectAll.UseVisualStyleBackColor = true;
            this.button_TESelectAll.Click += new System.EventHandler(this.button_BESelectAll_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.White;
            this.groupBox8.Controls.Add(this.comboBox_Metre);
            this.groupBox8.Controls.Add(this.label14);
            this.groupBox8.Location = new System.Drawing.Point(433, 287);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(185, 84);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "全局节拍操作";
            // 
            // comboBox_Metre
            // 
            this.comboBox_Metre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Metre.FormattingEnabled = true;
            this.comboBox_Metre.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBox_Metre.Location = new System.Drawing.Point(107, 34);
            this.comboBox_Metre.Name = "comboBox_Metre";
            this.comboBox_Metre.Size = new System.Drawing.Size(62, 20);
            this.comboBox_Metre.TabIndex = 1;
            this.comboBox_Metre.SelectedIndexChanged += new System.EventHandler(this.comboBox_Metre_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 39);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "全局节拍数：";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.White;
            this.groupBox6.Controls.Add(this.button_BEBarStart);
            this.groupBox6.Controls.Add(this.button_BENew);
            this.groupBox6.Controls.Add(this.button_BEDelete);
            this.groupBox6.Location = new System.Drawing.Point(203, 377);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(119, 116);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "节拍单点操作";
            // 
            // button_BEBarStart
            // 
            this.button_BEBarStart.Location = new System.Drawing.Point(9, 78);
            this.button_BEBarStart.Name = "button_BEBarStart";
            this.button_BEBarStart.Size = new System.Drawing.Size(95, 23);
            this.button_BEBarStart.TabIndex = 3;
            this.button_BEBarStart.Text = "修改起始属性";
            this.button_BEBarStart.UseVisualStyleBackColor = true;
            this.button_BEBarStart.Click += new System.EventHandler(this.button_BEBarStart_Click);
            // 
            // button_BENew
            // 
            this.button_BENew.Enabled = false;
            this.button_BENew.Location = new System.Drawing.Point(9, 49);
            this.button_BENew.Name = "button_BENew";
            this.button_BENew.Size = new System.Drawing.Size(95, 23);
            this.button_BENew.TabIndex = 2;
            this.button_BENew.Text = "新增节拍";
            this.button_BENew.UseVisualStyleBackColor = true;
            this.button_BENew.Click += new System.EventHandler(this.button_BENew_Click);
            // 
            // button_BEDelete
            // 
            this.button_BEDelete.Location = new System.Drawing.Point(9, 20);
            this.button_BEDelete.Name = "button_BEDelete";
            this.button_BEDelete.Size = new System.Drawing.Size(95, 23);
            this.button_BEDelete.TabIndex = 0;
            this.button_BEDelete.Text = "删除当前节拍";
            this.button_BEDelete.UseVisualStyleBackColor = true;
            this.button_BEDelete.Click += new System.EventHandler(this.button_BEDelete_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.button_BESelectAll);
            this.groupBox5.Controls.Add(this.button_BERor);
            this.groupBox5.Controls.Add(this.button_BERol);
            this.groupBox5.Controls.Add(this.button_BENormalize);
            this.groupBox5.Controls.Add(this.button_BEMerge);
            this.groupBox5.Controls.Add(this.button_BEDivide);
            this.groupBox5.Location = new System.Drawing.Point(6, 284);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(176, 116);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "节拍区间操作";
            // 
            // button_BESelectAll
            // 
            this.button_BESelectAll.Location = new System.Drawing.Point(9, 20);
            this.button_BESelectAll.Name = "button_BESelectAll";
            this.button_BESelectAll.Size = new System.Drawing.Size(75, 23);
            this.button_BESelectAll.TabIndex = 5;
            this.button_BESelectAll.Text = "全选";
            this.button_BESelectAll.UseVisualStyleBackColor = true;
            this.button_BESelectAll.Click += new System.EventHandler(this.button_BESelectAll_Click);
            // 
            // button_BERor
            // 
            this.button_BERor.Location = new System.Drawing.Point(90, 78);
            this.button_BERor.Name = "button_BERor";
            this.button_BERor.Size = new System.Drawing.Size(75, 23);
            this.button_BERor.TabIndex = 4;
            this.button_BERor.Text = "循环右移";
            this.button_BERor.UseVisualStyleBackColor = true;
            this.button_BERor.Click += new System.EventHandler(this.button_BERor_Click);
            // 
            // button_BERol
            // 
            this.button_BERol.Location = new System.Drawing.Point(9, 78);
            this.button_BERol.Name = "button_BERol";
            this.button_BERol.Size = new System.Drawing.Size(75, 23);
            this.button_BERol.TabIndex = 3;
            this.button_BERol.Text = "循环左移";
            this.button_BERol.UseVisualStyleBackColor = true;
            this.button_BERol.Click += new System.EventHandler(this.button_BERol_Click);
            // 
            // button_BENormalize
            // 
            this.button_BENormalize.Location = new System.Drawing.Point(90, 20);
            this.button_BENormalize.Name = "button_BENormalize";
            this.button_BENormalize.Size = new System.Drawing.Size(75, 23);
            this.button_BENormalize.TabIndex = 2;
            this.button_BENormalize.Text = "节拍规整化";
            this.button_BENormalize.UseVisualStyleBackColor = true;
            this.button_BENormalize.Click += new System.EventHandler(this.button_BENormalize_Click);
            // 
            // button_BEMerge
            // 
            this.button_BEMerge.Location = new System.Drawing.Point(90, 49);
            this.button_BEMerge.Name = "button_BEMerge";
            this.button_BEMerge.Size = new System.Drawing.Size(75, 23);
            this.button_BEMerge.TabIndex = 1;
            this.button_BEMerge.Text = "泛化节拍";
            this.button_BEMerge.UseVisualStyleBackColor = true;
            this.button_BEMerge.Click += new System.EventHandler(this.button_BEMerge_Click);
            // 
            // button_BEDivide
            // 
            this.button_BEDivide.Location = new System.Drawing.Point(9, 49);
            this.button_BEDivide.Name = "button_BEDivide";
            this.button_BEDivide.Size = new System.Drawing.Size(75, 23);
            this.button_BEDivide.TabIndex = 0;
            this.button_BEDivide.Text = "细分节拍";
            this.button_BEDivide.UseVisualStyleBackColor = true;
            this.button_BEDivide.Click += new System.EventHandler(this.button_BEDivide_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxChordKeyboard);
            this.tabPage1.Controls.Add(this.groupBoxChordKeyboard);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(628, 506);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "和弦";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxChordKeyboard
            // 
            this.checkBoxChordKeyboard.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxChordKeyboard.Location = new System.Drawing.Point(563, 292);
            this.checkBoxChordKeyboard.Name = "checkBoxChordKeyboard";
            this.checkBoxChordKeyboard.Size = new System.Drawing.Size(51, 49);
            this.checkBoxChordKeyboard.TabIndex = 17;
            this.checkBoxChordKeyboard.Text = "全和弦键盘";
            this.checkBoxChordKeyboard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxChordKeyboard.UseVisualStyleBackColor = true;
            this.checkBoxChordKeyboard.CheckedChanged += new System.EventHandler(this.checkBoxChordKeyboard_CheckedChanged);
            // 
            // groupBoxChordKeyboard
            // 
            this.groupBoxChordKeyboard.Controls.Add(this.pianoLabelQ);
            this.groupBoxChordKeyboard.Controls.Add(this.pianoLabelX);
            this.groupBoxChordKeyboard.Controls.Add(this.pianoLabelN);
            this.groupBoxChordKeyboard.Controls.Add(this.dataGridViewChord);
            this.groupBoxChordKeyboard.Location = new System.Drawing.Point(6, 281);
            this.groupBoxChordKeyboard.Name = "groupBoxChordKeyboard";
            this.groupBoxChordKeyboard.Size = new System.Drawing.Size(614, 220);
            this.groupBoxChordKeyboard.TabIndex = 22;
            this.groupBoxChordKeyboard.TabStop = false;
            this.groupBoxChordKeyboard.Text = "全和弦键盘";
            this.groupBoxChordKeyboard.Visible = false;
            // 
            // pianoLabelQ
            // 
            this.pianoLabelQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pianoLabelQ.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pianoLabelQ.Location = new System.Drawing.Point(568, 156);
            this.pianoLabelQ.Name = "pianoLabelQ";
            this.pianoLabelQ.Size = new System.Drawing.Size(31, 32);
            this.pianoLabelQ.TabIndex = 24;
            this.pianoLabelQ.Text = "?";
            this.pianoLabelQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pianoLabelQ.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pianoLabel_MouseClick);
            // 
            // pianoLabelX
            // 
            this.pianoLabelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pianoLabelX.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pianoLabelX.Location = new System.Drawing.Point(568, 113);
            this.pianoLabelX.Name = "pianoLabelX";
            this.pianoLabelX.Size = new System.Drawing.Size(31, 32);
            this.pianoLabelX.TabIndex = 23;
            this.pianoLabelX.Text = "X";
            this.pianoLabelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pianoLabelX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pianoLabel_MouseClick);
            // 
            // pianoLabelN
            // 
            this.pianoLabelN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pianoLabelN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pianoLabelN.Location = new System.Drawing.Point(568, 71);
            this.pianoLabelN.Name = "pianoLabelN";
            this.pianoLabelN.Size = new System.Drawing.Size(31, 32);
            this.pianoLabelN.TabIndex = 22;
            this.pianoLabelN.Text = "N";
            this.pianoLabelN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pianoLabelN.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pianoLabel_MouseClick);
            // 
            // dataGridViewChord
            // 
            this.dataGridViewChord.AllowUserToAddRows = false;
            this.dataGridViewChord.AllowUserToDeleteRows = false;
            this.dataGridViewChord.AllowUserToResizeColumns = false;
            this.dataGridViewChord.AllowUserToResizeRows = false;
            this.dataGridViewChord.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewChord.ColumnHeadersHeight = 22;
            this.dataGridViewChord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewChord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dataGridViewChord.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewChord.Location = new System.Drawing.Point(7, 16);
            this.dataGridViewChord.MultiSelect = false;
            this.dataGridViewChord.Name = "dataGridViewChord";
            this.dataGridViewChord.RowHeadersWidth = 25;
            this.dataGridViewChord.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewChord.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewChord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewChord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewChord.Size = new System.Drawing.Size(548, 195);
            this.dataGridViewChord.TabIndex = 21;
            this.dataGridViewChord.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewChord_CellMouseDown);
            this.dataGridViewChord.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewChord_CellMouseUp);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "C";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 42;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "C#";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 42;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "D";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 42;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Eb";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 42;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "E";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 42;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "F";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 42;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "F#";
            this.Column7.Name = "Column7";
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 42;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "G";
            this.Column8.Name = "Column8";
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 42;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Ab";
            this.Column9.Name = "Column9";
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 42;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "A";
            this.Column10.Name = "Column10";
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 42;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Bb";
            this.Column11.Name = "Column11";
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 42;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "B";
            this.Column12.Name = "Column12";
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 42;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(7, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(611, 135);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "剪切板";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(529, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "F11";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "Shift+F11";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Location = new System.Drawing.Point(68, 104);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(455, 24);
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(529, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "F10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "Shift+F10";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(68, 74);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(455, 24);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(529, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "F9";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Shift+F9";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(68, 44);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(455, 24);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ctrl+V";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ctrl+C";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(68, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(455, 24);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonRawChord);
            this.groupBox2.Controls.Add(this.comboBoxAlignBeats);
            this.groupBox2.Location = new System.Drawing.Point(467, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 83);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "杂项";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "填充节拍";
            // 
            // buttonRawChord
            // 
            this.buttonRawChord.Location = new System.Drawing.Point(5, 15);
            this.buttonRawChord.Name = "buttonRawChord";
            this.buttonRawChord.Size = new System.Drawing.Size(81, 21);
            this.buttonRawChord.TabIndex = 10;
            this.buttonRawChord.Text = "原始和弦(O)";
            this.buttonRawChord.UseVisualStyleBackColor = true;
            this.buttonRawChord.Click += new System.EventHandler(this.buttonRawChord_Click);
            // 
            // comboBoxAlignBeats
            // 
            this.comboBoxAlignBeats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlignBeats.FormattingEnabled = true;
            this.comboBoxAlignBeats.Location = new System.Drawing.Point(7, 55);
            this.comboBoxAlignBeats.Name = "comboBoxAlignBeats";
            this.comboBoxAlignBeats.Size = new System.Drawing.Size(79, 20);
            this.comboBoxAlignBeats.TabIndex = 0;
            this.comboBoxAlignBeats.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlignBeats_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChordLabelQ);
            this.groupBox1.Controls.Add(this.ChordLabelX);
            this.groupBox1.Controls.Add(this.ChordLabelN);
            this.groupBox1.Controls.Add(this.ChordLabel12);
            this.groupBox1.Controls.Add(this.ChordLabel11);
            this.groupBox1.Controls.Add(this.ChordLabel9);
            this.groupBox1.Controls.Add(this.ChordLabel10);
            this.groupBox1.Controls.Add(this.ChordLabel8);
            this.groupBox1.Controls.Add(this.ChordLabel7);
            this.groupBox1.Controls.Add(this.ChordLabel6);
            this.groupBox1.Controls.Add(this.ChordLabel5);
            this.groupBox1.Controls.Add(this.ChordLabel4);
            this.groupBox1.Controls.Add(this.ChordLabel3);
            this.groupBox1.Controls.Add(this.ChordLabel2);
            this.groupBox1.Controls.Add(this.ChordLabel1);
            this.groupBox1.Location = new System.Drawing.Point(6, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 83);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快捷输入";
            // 
            // ChordLabelQ
            // 
            this.ChordLabelQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabelQ.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabelQ.Location = new System.Drawing.Point(391, 45);
            this.ChordLabelQ.Name = "ChordLabelQ";
            this.ChordLabelQ.Size = new System.Drawing.Size(40, 30);
            this.ChordLabelQ.TabIndex = 21;
            this.ChordLabelQ.Text = "?";
            this.ChordLabelQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabelQ.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabelX
            // 
            this.ChordLabelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabelX.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabelX.Location = new System.Drawing.Point(351, 45);
            this.ChordLabelX.Name = "ChordLabelX";
            this.ChordLabelX.Size = new System.Drawing.Size(40, 30);
            this.ChordLabelX.TabIndex = 20;
            this.ChordLabelX.Text = "X";
            this.ChordLabelX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabelX.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabelN
            // 
            this.ChordLabelN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabelN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabelN.Location = new System.Drawing.Point(311, 45);
            this.ChordLabelN.Name = "ChordLabelN";
            this.ChordLabelN.Size = new System.Drawing.Size(40, 30);
            this.ChordLabelN.TabIndex = 19;
            this.ChordLabelN.Text = "N";
            this.ChordLabelN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabelN.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel12
            // 
            this.ChordLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel12.Location = new System.Drawing.Point(245, 45);
            this.ChordLabel12.Name = "ChordLabel12";
            this.ChordLabel12.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel12.TabIndex = 18;
            this.ChordLabel12.Text = "vii";
            this.ChordLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel12.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel11
            // 
            this.ChordLabel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel11.Location = new System.Drawing.Point(225, 15);
            this.ChordLabel11.Name = "ChordLabel11";
            this.ChordLabel11.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel11.TabIndex = 17;
            this.ChordLabel11.Text = "VIIb";
            this.ChordLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel9
            // 
            this.ChordLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel9.Location = new System.Drawing.Point(185, 15);
            this.ChordLabel9.Name = "ChordLabel9";
            this.ChordLabel9.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel9.TabIndex = 16;
            this.ChordLabel9.Text = "v#";
            this.ChordLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel9.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel10
            // 
            this.ChordLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel10.Location = new System.Drawing.Point(205, 45);
            this.ChordLabel10.Name = "ChordLabel10";
            this.ChordLabel10.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel10.TabIndex = 15;
            this.ChordLabel10.Text = "vi";
            this.ChordLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel10.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel8
            // 
            this.ChordLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel8.Location = new System.Drawing.Point(165, 45);
            this.ChordLabel8.Name = "ChordLabel8";
            this.ChordLabel8.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel8.TabIndex = 14;
            this.ChordLabel8.Text = "V";
            this.ChordLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel7
            // 
            this.ChordLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel7.Location = new System.Drawing.Point(145, 15);
            this.ChordLabel7.Name = "ChordLabel7";
            this.ChordLabel7.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel7.TabIndex = 13;
            this.ChordLabel7.Text = "iv#";
            this.ChordLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel6
            // 
            this.ChordLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel6.Location = new System.Drawing.Point(125, 45);
            this.ChordLabel6.Name = "ChordLabel6";
            this.ChordLabel6.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel6.TabIndex = 12;
            this.ChordLabel6.Text = "IV";
            this.ChordLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel5
            // 
            this.ChordLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel5.Location = new System.Drawing.Point(85, 45);
            this.ChordLabel5.Name = "ChordLabel5";
            this.ChordLabel5.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel5.TabIndex = 11;
            this.ChordLabel5.Text = "iii";
            this.ChordLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel4
            // 
            this.ChordLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel4.Location = new System.Drawing.Point(65, 15);
            this.ChordLabel4.Name = "ChordLabel4";
            this.ChordLabel4.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel4.TabIndex = 10;
            this.ChordLabel4.Text = "IIIb";
            this.ChordLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel3
            // 
            this.ChordLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel3.Location = new System.Drawing.Point(45, 45);
            this.ChordLabel3.Name = "ChordLabel3";
            this.ChordLabel3.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel3.TabIndex = 9;
            this.ChordLabel3.Text = "ii";
            this.ChordLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel2
            // 
            this.ChordLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel2.Location = new System.Drawing.Point(25, 15);
            this.ChordLabel2.Name = "ChordLabel2";
            this.ChordLabel2.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel2.TabIndex = 8;
            this.ChordLabel2.Text = "i#";
            this.ChordLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // ChordLabel1
            // 
            this.ChordLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChordLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChordLabel1.Location = new System.Drawing.Point(5, 45);
            this.ChordLabel1.Name = "ChordLabel1";
            this.ChordLabel1.Size = new System.Drawing.Size(40, 30);
            this.ChordLabel1.TabIndex = 7;
            this.ChordLabel1.Text = "I";
            this.ChordLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChordLabel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChordLabels_MouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(628, 506);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "可视化";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.buttonVamp);
            this.groupBox11.Controls.Add(this.buttonChroma);
            this.groupBox11.Location = new System.Drawing.Point(16, 288);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(123, 100);
            this.groupBox11.TabIndex = 30;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "快捷键组";
            // 
            // buttonVamp
            // 
            this.buttonVamp.Location = new System.Drawing.Point(6, 20);
            this.buttonVamp.Name = "buttonVamp";
            this.buttonVamp.Size = new System.Drawing.Size(110, 28);
            this.buttonVamp.TabIndex = 28;
            this.buttonVamp.Text = "Vamp(L)";
            this.buttonVamp.UseVisualStyleBackColor = true;
            this.buttonVamp.Click += new System.EventHandler(this.buttonVamp_Click);
            // 
            // buttonChroma
            // 
            this.buttonChroma.Location = new System.Drawing.Point(6, 55);
            this.buttonChroma.Name = "buttonChroma";
            this.buttonChroma.Size = new System.Drawing.Size(110, 30);
            this.buttonChroma.TabIndex = 29;
            this.buttonChroma.Text = "Chroma(P)";
            this.buttonChroma.UseVisualStyleBackColor = true;
            this.buttonChroma.Click += new System.EventHandler(this.buttonChroma_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 532);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.TimelinePictureBox);
            this.panel1.Location = new System.Drawing.Point(18, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(619, 278);
            this.panel1.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Chord Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.TimelinePictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMIDIDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMIDI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolumeMain)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxChordKeyboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChord)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openInfoFileDialog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开最近ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开最近项1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开最近项2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开最近项3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        internal System.Windows.Forms.PictureBox TimelinePictureBox;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveInfoFileDialog;
        private System.Windows.Forms.OpenFileDialog openOSUFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤销ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重做ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxAutoPlayChord;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarVolumeMIDI;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBarVolumeMain;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarMIDIDelay;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_BENormalize;
        private System.Windows.Forms.Button button_BEMerge;
        private System.Windows.Forms.Button button_BEDivide;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAlignBeats;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label ChordLabelQ;
        private System.Windows.Forms.Label ChordLabelX;
        private System.Windows.Forms.Label ChordLabelN;
        private System.Windows.Forms.Label ChordLabel12;
        private System.Windows.Forms.Label ChordLabel11;
        private System.Windows.Forms.Label ChordLabel9;
        private System.Windows.Forms.Label ChordLabel10;
        private System.Windows.Forms.Label ChordLabel8;
        private System.Windows.Forms.Label ChordLabel7;
        private System.Windows.Forms.Label ChordLabel6;
        private System.Windows.Forms.Label ChordLabel5;
        private System.Windows.Forms.Label ChordLabel4;
        private System.Windows.Forms.Label ChordLabel3;
        private System.Windows.Forms.Label ChordLabel2;
        private System.Windows.Forms.Label ChordLabel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button_BENew;
        private System.Windows.Forms.Button button_BEDelete;
        private System.Windows.Forms.Button button_BEBarStart;
        private System.Windows.Forms.Button button_BERol;
        private System.Windows.Forms.Button button_BERor;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBox_Metre;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button_BESelectAll;
        private System.Windows.Forms.RadioButton radioButtonRelative;
        private System.Windows.Forms.RadioButton radioButtonAbsolute;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button_TESelectAll;
        private System.Windows.Forms.ComboBox comboBox_TETonalty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button_TESwitchMajMin;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pictureBox4;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button button_ExportTXT;
        private System.Windows.Forms.TextBox textBoxConfigTagger;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBoxExportMusic;
        private System.Windows.Forms.ListBox comboBoxConfigConfidence;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.CheckBox checkBoxChordKeyboard;
        private System.Windows.Forms.GroupBox groupBoxChordKeyboard;
        private System.Windows.Forms.DataGridView dataGridViewChord;
        private System.Windows.Forms.Label pianoLabelQ;
        private System.Windows.Forms.Label pianoLabelX;
        private System.Windows.Forms.Label pianoLabelN;
        private System.Windows.Forms.CheckBox checkBoxMouseSwitch;
        private System.Windows.Forms.Button buttonRightBracket;
        private System.Windows.Forms.Button buttonLeftBracket;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button buttonVamp;
        private System.Windows.Forms.Button buttonChroma;
        private System.Windows.Forms.Button buttonRawChord;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxOSUMapID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button button_BEExtendBeat;
        private System.Windows.Forms.Button buttonCutInsertChord;
        private System.Windows.Forms.Button button_BERemoveRight;
        private System.Windows.Forms.Button button_BERemoveLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
    }
}