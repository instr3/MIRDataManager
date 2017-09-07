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

namespace Visualizer
{
    public partial class VisualizeForm : Form
    {
        internal static SubtitleVisualizer SubtitleVisualizer;
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
            //string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\39804 xi - FREEDOM DiVE.arc";
            string filename = @"C:\Users\jjy\Documents\2jjy\Programming\AudioProject\Datasets\osu\raw\183656 Mutsuhiko Izumi - Tengoku to Jigoku.arc";

            SongInfo testSongInfo = ArchiveManager.ReadFromArchive(filename);
            SubtitleVisualizer = new SubtitleVisualizer(visualizePictureBox, testSongInfo);
            timer.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            SubtitleVisualizer.Play();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SubtitleVisualizer.DrawFrame();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime -= 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubtitleVisualizer.CurrentTime += 10;
        }
    }
}
