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
    public partial class EncodeForm : Form
    {
        string songFolderName;
        SongInfo songInfo;
        string[] osuFiles;
        string arcFilename;
        public EncodeForm(SongInfo inputSongInfo,string inputArcFilename)
        {
            songInfo = inputSongInfo;
            arcFilename = inputArcFilename;
            songFolderName = Path.GetDirectoryName(songInfo.MusicConfigure.Location);
            InitializeComponent();
        }

        private void EncodeForm_Load(object sender, EventArgs e)
        {
            textBoxSongName.Text = songFolderName;
            textBoxMetre.Text = songInfo.MusicConfigure.MetreNumber.ToString();
            try
            {
                textBoxBPM.Text = Math.Round(60.0 / (songInfo.Beats[1].Time - songInfo.Beats[0].Time)).ToString();
            }
            catch
            {
                textBoxBPM.Text = "错误";
            }
            osuFiles = Directory.GetFiles(Path.Combine(Settings.DatasetMusicFolder, songFolderName), "*.osu").Select(s => Path.GetFileName(s)).ToArray();
            if(osuFiles.Length==0)
            {
                return;
            }
            int id1 = osuFiles[0].IndexOf('['), id2 = osuFiles[0].Length-osuFiles[0].LastIndexOf(']');
            foreach (string filename in osuFiles)
            {
                if (id1 != -1 && id2 != -1 && id1 + id2 + 1 <= filename.Length&&filename[id1]=='['&&filename[filename.Length-id2]==']')
                    listBoxDifficulties.Items.Add(filename.Substring(id1+1, filename.Length - id1 - id2 - 1));
                else
                    listBoxDifficulties.Items.Add(filename);
            }

        }
        string selectedFilename;
        OsuMap osuMap=null;
        string outText = "";
        private void listBoxDifficulties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDifficulties.SelectedIndex == -1)
                return;
            selectedFilename = osuFiles[listBoxDifficulties.SelectedIndex];
            osuMap = OsuMapAnalyzer.Analyze(songInfo, osuFiles[listBoxDifficulties.SelectedIndex]);
            outText = OsuMapEncoder.Encode(osuMap, checkBoxInvalid.Checked);
            textBoxAbstract.Text = EasyAnalysis(outText,osuMap);
        }
        public static string EasyAnalysis(string encodedMap,OsuMap osuMap)
        {
            string[] list = encodedMap.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            int rowCount = list.Length;
            int goodCount = 0;
            double allBPM = 0;

            HashSet<string> set = new HashSet<string>();
            int allObjects = osuMap.HitObjects.Count;
            foreach(string item in list)
            {
                if (!item.Contains('-'))
                    goodCount++;
                string[] split = item.Split('\t');
                set.Add(split[2]);
                allBPM += 60.0/(double.Parse(split[1]) - double.Parse(split[0]));
            }
            double goodRate = goodCount / (double)rowCount;
            double avgBPM = allBPM / rowCount;
            double avgObj = allObjects / (double)rowCount;
            return string.Format("总样本数：{0}/{1}" + Environment.NewLine + "样本质量：{2}%" +
                Environment.NewLine + "不同样本数：{3}" + Environment.NewLine + "平均GPM：{4}" +
                Environment.NewLine + "平均物件数：{5}",
                goodCount, rowCount, (goodRate * 100).ToString("0.00"), set.Count, avgBPM.ToString("0.00"),
                avgObj);
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            if(osuMap!=null)
                new ViewTextForm(outText.Replace("\n",Environment.NewLine)).ShowDialog();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

            if (osuMap != null)
            {

                if (!Directory.Exists(Settings.ExportFolder + "\\osumaplab"))
                    Directory.CreateDirectory(Settings.ExportFolder + "\\osumaplab");
                using (StreamWriter sw = new StreamWriter(Settings.ExportFolder + "\\osumaplab\\" + songFolderName + ".lab"))
                    sw.Write(outText);
                using (StreamWriter sw = new StreamWriter(Settings.ExportFolder + "\\osumaplab\\" + songFolderName + ".linkedInfo"))
                {
                    sw.WriteLine(arcFilename);
                    sw.WriteLine(selectedFilename);
                }
                Close();
            }
        }
    }
}
