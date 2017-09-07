using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsuMapAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OsuMap osuMap = OsuMapAnalyzer.Analyze(textBox1.Text);
            using (StreamWriter sw = new StreamWriter("out.lab"))
                sw.Write(osuMap.ToSonicVisualizer(true));
            using (StreamWriter sw = new StreamWriter("out(no end).lab"))
                sw.Write(osuMap.ToSonicVisualizer(false));
            using (StreamWriter sw = new StreamWriter("beat.lab"))
                sw.Write(osuMap.SongInfo.ExportBeatsToLab());
            textBox2.Text = OsuMapEncoder.Encode(osuMap).Replace("\n", Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OsuMap osuMap = OsuMapAnalyzer.Analyze(textBox1.Text);
            new EncodeForm(osuMap.SongInfo,"").ShowDialog();
        }
        private void exportOsuMap(OsuMap osuMap)
        {
            if (osuMap != null)
            {
                string songFolderName = Path.GetDirectoryName(osuMap.SongInfo.MusicConfigure.Location);
                string outText = OsuMapEncoder.Encode(osuMap);
                if (!Directory.Exists(Settings.ExportFolder + "\\osumaplab"))
                    Directory.CreateDirectory(Settings.ExportFolder + "\\osumaplab");
                using (StreamWriter sw = new StreamWriter(Settings.ExportFolder + "\\osumaplab\\" + songFolderName + ".lab"))
                    sw.Write(outText);
                string mp3Path = Path.Combine(Settings.DatasetMusicFolder, osuMap.SongInfo.MusicConfigure.Location);
                string targetMp3Path = Path.Combine(Settings.ExportFolder, "music", songFolderName + ".mp3");
                if(!File.Exists(targetMp3Path))
                {
                    File.Copy(mp3Path, targetMp3Path);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string[] allFolders = Directory.GetDirectories(Settings.DatasetMusicFolder);
            foreach(string folderPath in allFolders)
            {
                string[] allOsu = Directory.GetFiles(folderPath, "*.osu");
                double maxGoodness = 0;
                OsuMap maxGoodnessMap = null;
                string maxGoodnessDiff = "";
                foreach(string osuFilePath in allOsu)
                {
                    OsuMap osuMap = OsuMapAnalyzer.Analyze(osuFilePath);
                    double goodness = OsuMapEncoder.GetGoodness(osuMap);
                    if(goodness>maxGoodness)
                    {
                        maxGoodness = goodness;
                        maxGoodnessMap = osuMap;
                        maxGoodnessDiff = osuFilePath;
                    }
                }
                textBox2.AppendText(Path.GetFileName(folderPath) + Environment.NewLine + "Value:" + maxGoodness.ToString("0.00") + Environment.NewLine);
                if (maxGoodnessMap!=null)
                {
                    textBox2.AppendText(Path.GetFileName(maxGoodnessDiff) + Environment.NewLine);
                    exportOsuMap(maxGoodnessMap);
                }
                Application.DoEvents();
            }
        }
    }
}
