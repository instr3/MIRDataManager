namespace OsuMapAnalyzer
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 78);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(677, 198);
            this.textBox2.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.FormattingEnabled = true;
            this.textBox1.Items.AddRange(new object[] {
            "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\436773 Yunomi - Ment" +
                "ai Cosmic\\Yunomi - Mentai Cosmic (alacat) [Easy].osu",
            "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\103260 DenporuP - Hi" +
                "torinbo Envy\\DenporuP - Hitorinbo Envy (SapphireGhost) [Hide].osu",
            "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\39804 xi - FREEDOM D" +
                "iVE\\xi - FREEDOM DiVE (Nakagawa-Kanon) [FOUR DIMENSIONS].osu",
            "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\155118 Drop - Granat" +
                "\\Drop - Granat (Lan wings) [Hard].osu",
            "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\109343 Linda Yang - " +
                "Bie Kan Wo Zhi Shi Yi Zhi Yang\\Linda Yang - Bie Kan Wo Zhi Shi Yi Zhi Yang (S o " +
                "a p) [Spring Roll\'s Hard].osu"});
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(677, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "C:\\Users\\jjy\\Documents\\2jjy\\Game\\Class_GameAction\\osu!\\Songs\\103260 DenporuP - Hi" +
    "torinbo Envy\\DenporuP - Hitorinbo Envy (SapphireGhost) [Hide].osu";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(189, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 288);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

