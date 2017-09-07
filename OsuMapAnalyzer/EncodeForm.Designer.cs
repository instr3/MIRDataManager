namespace OsuMapAnalyzer
{
    partial class EncodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxDifficulties = new System.Windows.Forms.ListBox();
            this.textBoxSongName = new System.Windows.Forms.TextBox();
            this.textBoxAbstract = new System.Windows.Forms.TextBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBPM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMetre = new System.Windows.Forms.TextBox();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.checkBoxInvalid = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "歌曲：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "难度列表";
            // 
            // listBoxDifficulties
            // 
            this.listBoxDifficulties.FormattingEnabled = true;
            this.listBoxDifficulties.ItemHeight = 12;
            this.listBoxDifficulties.Location = new System.Drawing.Point(15, 67);
            this.listBoxDifficulties.Name = "listBoxDifficulties";
            this.listBoxDifficulties.Size = new System.Drawing.Size(248, 124);
            this.listBoxDifficulties.TabIndex = 3;
            this.listBoxDifficulties.SelectedIndexChanged += new System.EventHandler(this.listBoxDifficulties_SelectedIndexChanged);
            // 
            // textBoxSongName
            // 
            this.textBoxSongName.Enabled = false;
            this.textBoxSongName.Location = new System.Drawing.Point(61, 10);
            this.textBoxSongName.Name = "textBoxSongName";
            this.textBoxSongName.Size = new System.Drawing.Size(202, 21);
            this.textBoxSongName.TabIndex = 5;
            // 
            // textBoxAbstract
            // 
            this.textBoxAbstract.Location = new System.Drawing.Point(15, 197);
            this.textBoxAbstract.Multiline = true;
            this.textBoxAbstract.Name = "textBoxAbstract";
            this.textBoxAbstract.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAbstract.Size = new System.Drawing.Size(248, 74);
            this.textBoxAbstract.TabIndex = 7;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(189, 277);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "导出";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "BPM：";
            // 
            // textBoxBPM
            // 
            this.textBoxBPM.Enabled = false;
            this.textBoxBPM.Location = new System.Drawing.Point(137, 37);
            this.textBoxBPM.Name = "textBoxBPM";
            this.textBoxBPM.Size = new System.Drawing.Size(52, 21);
            this.textBoxBPM.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Metre：";
            // 
            // textBoxMetre
            // 
            this.textBoxMetre.Enabled = false;
            this.textBoxMetre.Location = new System.Drawing.Point(240, 37);
            this.textBoxMetre.Name = "textBoxMetre";
            this.textBoxMetre.Size = new System.Drawing.Size(21, 21);
            this.textBoxMetre.TabIndex = 10;
            // 
            // buttonDetails
            // 
            this.buttonDetails.Location = new System.Drawing.Point(98, 277);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(75, 23);
            this.buttonDetails.TabIndex = 11;
            this.buttonDetails.Text = "详细";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // checkBoxInvalid
            // 
            this.checkBoxInvalid.AutoSize = true;
            this.checkBoxInvalid.Location = new System.Drawing.Point(26, 283);
            this.checkBoxInvalid.Name = "checkBoxInvalid";
            this.checkBoxInvalid.Size = new System.Drawing.Size(60, 16);
            this.checkBoxInvalid.TabIndex = 12;
            this.checkBoxInvalid.Text = "测试集";
            this.checkBoxInvalid.UseVisualStyleBackColor = true;
            // 
            // EncodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 304);
            this.Controls.Add(this.checkBoxInvalid);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.textBoxMetre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxAbstract);
            this.Controls.Add(this.textBoxBPM);
            this.Controls.Add(this.textBoxSongName);
            this.Controls.Add(this.listBoxDifficulties);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EncodeForm";
            this.Text = "EncodeForm";
            this.Load += new System.EventHandler(this.EncodeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxDifficulties;
        private System.Windows.Forms.TextBox textBoxSongName;
        private System.Windows.Forms.TextBox textBoxAbstract;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBPM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMetre;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.CheckBox checkBoxInvalid;
    }
}