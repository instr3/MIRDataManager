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
using MIREditor;
using System.IO;
using System.Diagnostics;

namespace Visualizer
{
    public partial class VisualizeForm : Form
    {
        internal static SubtitleVisualizer SubtitleVisualizer;
        INIReader iniReader = new INIReader("Config.ini");
        public VisualizeForm()
        {
            InitializeComponent();
        }

        private void VisualizeForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void VisualizeForm_Shown(object sender, EventArgs e)
        {
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\429316 Tonon - Shirayuki -sirayuki-.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\219763 Drop - Granat.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\92107 Ken Nakagawa - Try.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\166239 Nishiura Tomohito - Shop.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\527431 Brad Breeck - Gravity Falls Theme Song.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\90935 IOSYS - Endless Tewi-ma Park.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\183656 Mutsuhiko Izumi - Tengoku to Jigoku.arc";
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\92 Portal - Still Alive.arc";
            
        }
        SongInfo testSongInfo;
        private void button1_Click(object sender, EventArgs e)
        {
            string filename = iniReader["File"];
            testSongInfo = ArchiveManager.ReadFromArchive(filename);
            
            SubtitleVisualizer = new SubtitleVisualizer(visualizePictureBox, testSongInfo, checkBox1.Checked,iniReader.Data);
            if(checkBox1.Checked)
            {
                ToImages();
            }
            else
            {
                button1.Visible = false;
                SubtitleVisualizer.Play();
                playerState = 0;
                timer.Enabled = true;
            }
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
        private void ToImages()
        {
            Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime -= 5;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime += 5;
        }
    }
}
