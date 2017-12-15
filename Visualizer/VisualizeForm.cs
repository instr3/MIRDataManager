using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Visualizer
{
    public partial class VisualizeForm : Form
    {
        internal static SubtitleVisualizer SubtitleVisualizer;
        INIReader iniReader;
        string rawFilename;
        public VisualizeForm(string rawDataPath=null)
        {
            InitializeComponent();
            if(rawDataPath!=null)
            {
                rawFilename = rawDataPath;
            }
            else
            {
                iniReader =new INIReader("Config.ini");
                rawFilename = iniReader["File"];
            }
        }

        private void VisualizeForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        SongInfo testSongInfo;
        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(rawFilename))
            {
                testSongInfo = new SongInfo(sr.ReadToEnd());
            }
            
            SubtitleVisualizer = new SubtitleVisualizer(visualizePictureBox, testSongInfo, false, checkBox1.Checked);
            
            button1.Visible = false;
            SubtitleVisualizer.Play();
            playerState = 0;
            timer.Enabled = true;
        }
        int playerState;
        DateTime manualTimer;
        private void timer_Tick(object sender, EventArgs e)
        {
            if (playerState == 2)
            {
                SubtitleVisualizer.DrawStatistics((DateTime.Now - manualTimer).TotalSeconds);
            }
            else
            {
                double currentTime = playerState == 1 ? (DateTime.Now - manualTimer).TotalSeconds : SubtitleVisualizer.CurrentTime;
                if (!SubtitleVisualizer.DrawFrame(currentTime))
                {
                    manualTimer = DateTime.Now;
                    playerState = 2;
                }
                else if (playerState==0 && SubtitleVisualizer.Ended())
                {
                    manualTimer = DateTime.Now - TimeSpan.FromSeconds(SubtitleVisualizer.CurrentTime);
                    playerState = 1;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime -= 5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime += 5;
        }

        private void VisualizeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Enabled = false;
            SubtitleVisualizer.Pause();
        }
    }
}
