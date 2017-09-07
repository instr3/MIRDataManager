﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace MIRDataManager
{
    public partial class Form1 : Form
    {
        public DataFile CurrentSelectedDataFile;
        Dataset dataset;
        public Form1()
        {
            InitializeComponent();
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }
        List<KeyValuePair<string,DateTime>> NotCreatedSongFolders;
        private void InitAllOsuSongFolders()
        {
            NotCreatedSongFolders = new List<KeyValuePair<string, DateTime>>();
            DirectoryInfo dir = new DirectoryInfo(Settings.DatasetMusicFolder);
            foreach (DirectoryInfo subdir in dir.EnumerateDirectories())
            {
                if (subdir.GetFiles("*.osu").Length > 0)
                {
                    bool found = false;
                    foreach (DataFile dataFile in dataset.DataFiles)
                    {
                        if (dataFile.SongInfo.MusicConfigure.Location.Contains(subdir.Name))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        NotCreatedSongFolders.Add(new KeyValuePair<string, DateTime>(subdir.Name,subdir.CreationTime));
                    }
                }
            }
        }
        private string GetSongPathInFolder(string folderPath)
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            long maxSize = -1;
            string maxSizeAt = null;
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                if (file.Extension == ".mp3")
                {
                    if(file.Length> maxSize)
                    {
                        maxSize = file.Length;
                        maxSizeAt = file.FullName;
                    }
                }
            }
            return maxSizeAt;
        }
        private void UpdateNotCreatedList()
        {
            listViewNotCreated.Items.Clear();
            string searchText = textBoxSearch.Text.ToLower();
            List<ListViewItem> collection = new List<ListViewItem>();
            foreach(KeyValuePair<string,DateTime> folder in NotCreatedSongFolders)
            {
                if(folder.Key.ToLower().Contains(searchText))
                {
                    collection.Add(new ListViewItem(new string[] {
                        folder.Key,
                        folder.Value.ToString("yyyy-MM-dd HH:mm:ss")
                    }));
                }
            }
            listViewNotCreated.Items.AddRange(collection.ToArray());
        }
        private void UpdateListView()
        {
            int[] memory = listView1.SelectedIndices.Cast<int>().ToArray();
            string[] memoryText = memory.Select(i => listView1.Items[i].SubItems[0].Text).ToArray();
            int scroll = listView1.TopItem == null ? 0 : listView1.TopItem.Index;
            listView1.Items.Clear();
            List<DataFile> listedDataFiles = dataset.Where(textBoxSearch.Text, comboBoxScoreFilter.SelectedItem.ToString());
            List<ListViewItem> collection = new List<ListViewItem>();
            foreach (DataFile f in listedDataFiles)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    f.FileName,
                    f.SongInfo.MusicConfigure.Title,
                    f.SongInfo.TagConfigure.Tagger,
                    f.SongInfo.TagConfigure.Time.ToString("yyyy-MM-dd HH:mm:ss"),
                    f.SongInfo.TagConfigure.Confidence.ToString()
                });
                item.Tag = f;
                collection.Add(item);
            }
            listView1.Items.AddRange(collection.ToArray());
            Text = listView1.Items.Count + " 条结果";
            for (int i=0;i< memory.Length;++i)
            {
                if(memory[i]<listView1.Items.Count&&listView1.Items[memory[i]].SubItems[0].Text==memoryText[i])
                {
                    listView1.Items[memory[i]].Selected = true;
                }
            }
            if(listView1.Items.Count>0)
                listView1.TopItem=listView1.Items[Math.Min(scroll,listView1.Items.Count-1)];
        }
        private void TryPlay(string songPath)
        {
            if (File.Exists(songPath))
            {
                labelHint.Text = "双击进行标记。音频文件" + songPath;
                if (checkBoxAutoplay.Checked)
                {
                    axWindowsMediaPlayer2.URL = songPath;
                }
            }
            else
            {
                labelHint.Text = "谱面/音频未找到，请先下载："+Environment.NewLine + songPath;
                axWindowsMediaPlayer2.URL = "";
            }
        }
        private double GetMP3Length(string fileName)
        {
            int stream = Bass.BASS_StreamCreateFile(fileName, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            long len_in_byte = Bass.BASS_ChannelGetLength(stream, BASSMode.BASS_POS_BYTES);
            double time = Bass.BASS_ChannelBytes2Seconds(stream, len_in_byte);
            Bass.BASS_StreamFree(stream);
            return time;
        }
        private void StartEditor(string parameters)
        {
            axWindowsMediaPlayer2.Ctlcontrols.pause();
            Text = "Please wait...";
            Process process = Process.Start("MIREditor.exe", parameters);
            process.WaitForExit();
            Text = "Please refresh";
            if (checkBoxAutoScan.Checked)
                buttonRescan_Click(null, null);
            else if (checkBoxAutoRefresh.Checked)
                buttonRefresh_Click(null, null);
        }
        private void ExportArchiveByFileName(string fileName, bool exportMusic)
        {
            using (StreamReader sr = new StreamReader(Settings.ArchiveFolder + "\\" + fileName))
            {
                SongInfo songInfo = new SongInfo(sr.ReadToEnd());
                Exporter exporter = new Exporter(songInfo,
                    GetMP3Length(Settings.DatasetMusicFolder + "\\" + songInfo.MusicConfigure.Location));
                exporter.ExportToFolder(Settings.ExportFolder,
                    Path.GetFileNameWithoutExtension(fileName),
                    exportMusic,
                    Settings.DatasetMusicFolder + "\\" + songInfo.MusicConfigure.Location);
            }
        }
        private void ExportMusicByFileName(string fileName)
        {
            using (StreamReader sr = new StreamReader(Settings.ArchiveFolder + "\\" + fileName))
            {
                SongInfo songInfo = new SongInfo(sr.ReadToEnd());
                Exporter exporter = new Exporter(songInfo,
                    GetMP3Length(Settings.DatasetMusicFolder + "\\" + songInfo.MusicConfigure.Location));
                exporter.ExportMusic(Settings.ExportFolder,
                    Path.GetFileNameWithoutExtension(fileName),
                    Settings.DatasetMusicFolder + "\\" + songInfo.MusicConfigure.Location);
            }
        }

        public void UpdateSelectedDataFile(DataFile dataFile)
        {
            string songPath = Settings.DatasetMusicFolder + "\\" + dataFile.SongInfo.MusicConfigure.Location;
            TryPlay(songPath);
            CurrentSelectedDataFile = dataFile;
            if(dataFile.SongInfo.MiscConfigure.osuMapID!=0)
            {
                groupBoxDownloadManager.Visible = true;
                progressBarDownload.Visible = false;
                linkLabelAutoDownload.Visible = false;
                linkLabelManualDownload.Visible = false;
                buttonDeleteOsuMap.Visible = false;
                if (!File.Exists(songPath))
                {
                    if(dataFile.Downloading)
                    {
                        progressBarDownload.Visible = true;
                        progressBarDownload.Value = 0;
                    }
                    else
                    {
                        linkLabelAutoDownload.Visible = true;
                        linkLabelManualDownload.Visible = true;
                    }
                }
                else
                {
                    buttonDeleteOsuMap.Visible = true;
                }
            }
            else
            {
                groupBoxDownloadManager.Visible = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            labelHint.Text = "";
            comboBoxScoreFilter.SelectedIndex = 0;
            dataset = new Dataset(Settings.ArchiveFolder, true);
            UpdateListView();
            InitAllOsuSongFolders();
            UpdateNotCreatedList();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string selectedFileName = listView1.SelectedItems[0].SubItems[0].Text;
            //MessageBox.Show(selectedFileName);
            StartEditor(selectedFileName);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (dataset != null)
            {
                UpdateListView();
                UpdateNotCreatedList();
            }
        }

        private void comboBoxScoreFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dataset!=null)
                UpdateListView();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            StartEditor("<create>");
        }

        private void listViewNotCreated_DoubleClick(object sender, EventArgs e)
        {
            if (listViewNotCreated.SelectedItems.Count == 0) return;
            StartEditor("<create> " + listViewNotCreated.SelectedItems[0].SubItems[0].Text);
        }

        private void buttonRescan_Click(object sender, EventArgs e)
        {
            buttonRefresh_Click(sender, e);
            InitAllOsuSongFolders();
            UpdateNotCreatedList();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            dataset = new Dataset(Settings.ArchiveFolder, true);
            UpdateListView();
        }

        private void buttonRename_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string rawFileName=listView1.SelectedItems[0].SubItems[0].Text;
            string fileName = rawFileName.EndsWith(".arc") ? Path.GetFileNameWithoutExtension(rawFileName) : rawFileName;
            TextInputForm textInputForm = new TextInputForm(fileName,"请输入新文件名");
            textInputForm.ShowDialog();
            if(textInputForm.Tag!=null&& textInputForm.Tag.ToString()!="")
            {
                string newFileName = textInputForm.Tag.ToString();
                if (!newFileName.EndsWith(".arc")) newFileName += ".arc";
                if(Path.GetInvalidFileNameChars().Any(c=>newFileName.Contains(c)))
                {
                    MessageBox.Show("文件名不能包含非法字符！");
                    return;
                }
                try
                {
                    File.Move(Settings.ArchiveFolder + "\\" + rawFileName, Settings.ArchiveFolder + "\\" + newFileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                buttonRefresh_Click(sender, e);
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            {
                buttonRename_Click(sender, e);
            }
            else if(e.KeyCode==Keys.A&&e.Control)
            {
                buttonSelectAll_Click(sender, e);
            }
            else if(e.KeyCode==Keys.Delete)
            {
                buttonDelete_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Space)
            {
                listView1_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.PageUp)
            {
                listView1_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, -1, -1, -1, -1));
            }
        }
        private void listViewNotCreated_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                listViewNotCreated_DoubleClick(sender, e);
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode==Keys.PageDown || e.KeyCode==Keys.PageUp)
            {
                listViewNotCreated_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, -1, -1, -1, -1));
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count==0)
            {
                MessageBox.Show("未选中任何项目");
                return;
            }
            string logText = "";
            int totalCount = listView1.SelectedItems.Count;
            int successCount = 0;
            int failCount = 0;
            foreach (ListViewItem it in listView1.SelectedItems)
            {
                logText += it.SubItems[0].Text;
                try
                {
                    ExportArchiveByFileName(it.SubItems[0].Text, checkBoxExportMusic.Checked);
                    logText += " success" + Environment.NewLine;
                    successCount++;
                }
                catch(Exception ex)
                {
                    logText += " error:" + ex.Message + Environment.NewLine;
                    failCount++;
                }
                Text = "正在导出，共 " + totalCount + " 个，成功 " + successCount + " 个，失败 " + failCount + " 个";
                Application.DoEvents();
            }
            using (StreamWriter sw = new StreamWriter("export.log"))
                sw.Write(logText);
            UpdateListView();
            MessageBox.Show("导出完成，成功 " + successCount + " 个，失败 " + failCount + " 个。");
        }
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
                item.Selected = true;
            listView1.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                MessageBox.Show("没有选中任何项目");
            else
            {
                string hints = "你确实要删除以下项目的标注文件吗？谱面文件将不会被删除。";
                foreach (ListViewItem it in listView1.SelectedItems)
                    hints += Environment.NewLine + it.SubItems[0].Text;
                if(MessageBox.Show(hints,"警告",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    foreach (ListViewItem it in listView1.SelectedItems)
                    {
                        string fileName = it.SubItems[0].Text;
                        File.Delete(Settings.ArchiveFolder + "\\" + fileName);
                        if(File.Exists(Settings.ArchiveFolder + "\\" + fileName + ".vampcache"))
                        {
                            File.Delete(Settings.ArchiveFolder + "\\" + fileName + ".vampcache");
                        }
                    }
                    buttonRefresh_Click(sender, e);
                }
            }
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = sender as ListView; 
            if (listView.ListViewItemSorter != null && (listView.ListViewItemSorter as ListViewColumnComparer).Column == e.Column)
            {
                listView.ListViewItemSorter = new ListViewColumnComparer(e.Column, !(listView.ListViewItemSorter as ListViewColumnComparer).Reverse);
            }
            else
            {
                listView.ListViewItemSorter = new ListViewColumnComparer(e.Column, false);
            }

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            if (e.Button == MouseButtons.Right)
                listView1_DoubleClick(sender, e);
            UpdateSelectedDataFile(listView1.SelectedItems[0].Tag as DataFile);

        }

        private void listViewNotCreated_MouseClick(object sender, MouseEventArgs e)
        {
            if (listViewNotCreated.SelectedItems.Count == 0) return;
            if (e.Button == MouseButtons.Right)
                listViewNotCreated_DoubleClick(sender, e);
            else if (checkBoxAutoplay.Checked)
            {
                string text = listViewNotCreated.SelectedItems[0].SubItems[0].Text;
                string songPath = GetSongPathInFolder(Settings.DatasetMusicFolder + "\\" + text);
                if(!string.IsNullOrEmpty(songPath))
                {
                    groupBoxDownloadManager.Visible = false;
                    TryPlay(songPath);
                }
            }
        }

        private void buttonDeleteUncreatedFolder_Click(object sender, EventArgs e)
        {
            if (listViewNotCreated.SelectedItems.Count == 0)
                MessageBox.Show("没有选中任何项目");
            else
            {
                string hints = "你确实要删除以下项目吗？（移动到Hidden目录）";
                foreach (ListViewItem it in listViewNotCreated.SelectedItems)
                    hints += Environment.NewLine + it.SubItems[0].Text;
                if (MessageBox.Show(hints, "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!Directory.Exists(Settings.DatasetMusicFolder + "\\Hidden"))
                        Directory.CreateDirectory(Settings.DatasetMusicFolder + "\\Hidden");
                    axWindowsMediaPlayer2.Ctlcontrols.stop();
                    axWindowsMediaPlayer2.URL = "";
                    foreach (ListViewItem it in listViewNotCreated.SelectedItems)
                    {
                        string dirName = it.SubItems[0].Text;
                        try
                        {
                            Directory.Move(Settings.DatasetMusicFolder + "\\" + dirName, Settings.DatasetMusicFolder + "\\Hidden\\" + dirName);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("删除错误，信息：" + ex.Message);
                        }
                    }
                    buttonRescan_Click(sender, e);
                }
            }

        }
        #region TestCodes
        private void button1_Click(object sender, EventArgs e)
        {
            Dataset tempSet = new Dataset(Settings.ArchiveFolder, false);
            foreach(DataFile f in tempSet.DataFiles)
            {
                //if(f.SongInfo.MiscConfigure.osuMapID==0)
                {
                    int folderNumber = 0;
                    string dirName = f.FileName;
                    if (dirName.Contains(' '))
                    {
                        string folderNumberString;
                        folderNumberString = dirName.Substring(0, dirName.IndexOf(' '));
                        int.TryParse(folderNumberString, out folderNumber);
                    }
                    if(folderNumber!=0 && folderNumber!=12)
                    {
                        f.SongInfo.MiscConfigure.osuMapID = folderNumber;
                        f.SaveToFile();
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(Settings.DatasetMusicFolder + "\\" + f.SongInfo.MiscConfigure.LinkedFile))
                        {
                            while (!sr.EndOfStream)
                            {
                                string buffer = sr.ReadLine();
                                string DivisorRegex;
                                Match match;
                                DivisorRegex = @"^BeatmapSetID:\s*(?<n>[0-9]+)\s*$";
                                match = Regex.Match(buffer, DivisorRegex);
                                if (match.Success)
                                {
                                    f.SongInfo.MiscConfigure.osuMapID = int.Parse(match.Groups["n"].Value);
                                    continue;
                                }
                            }
                        }
                        if (f.SongInfo.MiscConfigure.osuMapID == 0)
                        {
                            TextInputForm textInputForm = new TextInputForm(f.FileName, "请找到并输入下列曲目的osu beatmap set id！");
                            textInputForm.ShowDialog();
                            int result = 0;
                            if (textInputForm.Tag != null)
                                if (int.TryParse(textInputForm.Tag.ToString(), out result))
                                    f.SongInfo.MiscConfigure.osuMapID = result;
                        }
                        if (f.SongInfo.MiscConfigure.osuMapID != 0)
                        {
                            string oldpath = Settings.DatasetMusicFolder + "\\" + Path.GetDirectoryName(f.SongInfo.MusicConfigure.Location);
                            string newpath = Settings.DatasetMusicFolder + "\\" + f.SongInfo.MiscConfigure.osuMapID.ToString() + " " + Path.GetDirectoryName(f.SongInfo.MusicConfigure.Location);
                            f.SongInfo.MusicConfigure.Location = f.SongInfo.MiscConfigure.osuMapID.ToString() + " " + f.SongInfo.MusicConfigure.Location;
                            f.SongInfo.MiscConfigure.LinkedFile = f.SongInfo.MiscConfigure.osuMapID.ToString() + " " + f.SongInfo.MiscConfigure.LinkedFile;
                            Directory.Move(oldpath, newpath);
                            f.SaveToFile();
                            File.Move(Settings.ArchiveFolder + "\\" + f.FileName, Settings.ArchiveFolder + "\\" + f.SongInfo.MiscConfigure.osuMapID.ToString() + " " + f.FileName);
                            if(File.Exists(Settings.ArchiveFolder + "\\" + f.FileName + ".vampcache"))
                            {
                                File.Move(Settings.ArchiveFolder + "\\" + f.FileName + ".vampcache", Settings.ArchiveFolder + "\\" + f.SongInfo.MiscConfigure.osuMapID.ToString() + " " + f.FileName + ".vampcache");
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dataset tempSet = new Dataset(Settings.ArchiveFolder, false);
            foreach (DataFile f in tempSet.DataFiles)
            {
                if(!File.Exists(Settings.DatasetMusicFolder + "\\" + f.SongInfo.MusicConfigure.Location))
                {
                    if(File.Exists(Settings.DatasetMusicFolder + "\\" + f.SongInfo.MiscConfigure.LinkedFile))
                    {
                        using (StreamReader sr = new StreamReader(Settings.DatasetMusicFolder + "\\" + f.SongInfo.MiscConfigure.LinkedFile))
                        {
                            while (!sr.EndOfStream)
                            {
                                string buffer = sr.ReadLine();
                                string DivisorRegex;
                                Match match;
                                DivisorRegex = @"^AudioFilename:\s*(?<n>[^$]+)\s*$";
                                match = Regex.Match(buffer, DivisorRegex);
                                if (match.Success)
                                {
                                    f.SongInfo.MusicConfigure.Location=Path.GetDirectoryName(f.SongInfo.MiscConfigure.LinkedFile) + "/" + match.Groups["n"].Value;
                                    f.SaveToFile();
                                    continue;
                                }
                            }
                        }

                    }
                }
            }
        }
        #endregion
        private void linkLabelAutoDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentSelectedDataFile == null) return;
            int id = CurrentSelectedDataFile.SongInfo.MiscConfigure.osuMapID;
            if (id == 0)
            {
                MessageBox.Show("没有osu Beatmap Set ID信息，无法下载");
                return;
            }
            DataDownloader downloader = new DataDownloader(CurrentSelectedDataFile, this);
            downloader.StartDownload();
        }

        private void linkLabelManualDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentSelectedDataFile == null) return;
            int id = CurrentSelectedDataFile.SongInfo.MiscConfigure.osuMapID;
            if (id == 0)
            {
                MessageBox.Show("没有osu Beatmap Set ID信息，无法下载");
                return;
            }
            Process.Start(string.Format(Settings.OsuMapLink, id));
        }

        private void buttonDeleteOsuMap_Click(object sender, EventArgs e)
        {
            if (CurrentSelectedDataFile == null) return;
            int id = CurrentSelectedDataFile.SongInfo.MiscConfigure.osuMapID;
            if (id == 0)
            {
                MessageBox.Show("没有osu Beatmap Set ID信息，无法下载");
                return;
            }
            string dirName = Path.GetDirectoryName(CurrentSelectedDataFile.SongInfo.MusicConfigure.Location);
            if(MessageBox.Show("你确定要删除歌曲文件夹下的 " + dirName + " 及其子目录下的全部内容吗？" + Environment.NewLine + "标注文件(.arc)将不会删除。","警告",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                axWindowsMediaPlayer2.URL = "";
                try
                {
                    Directory.Delete(Path.Combine(Settings.DatasetMusicFolder, dirName), true);
                }
                catch
                {
                    MessageBox.Show("删除失败！");
                }
                UpdateSelectedDataFile(CurrentSelectedDataFile);
            }
        }

        private void buttonOMAExport_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            string filename = listView1.SelectedItems[0].SubItems[0].Text;
            SongInfo songInfo;
            using (StreamReader sr = new StreamReader(Path.Combine(Settings.ArchiveFolder,filename)))
            {
                songInfo = new SongInfo(sr.ReadToEnd());
            }
            OsuMapAnalyzer.EncodeForm encodeForm = new OsuMapAnalyzer.EncodeForm(songInfo,filename);
            encodeForm.ShowDialog();
        }

        private void buttonExportMusic_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项目");
                return;
            }
            string logText = "";
            int totalCount = listView1.SelectedItems.Count;
            int successCount = 0;
            int failCount = 0;
            foreach (ListViewItem it in listView1.SelectedItems)
            {
                logText += it.SubItems[0].Text;
                try
                {
                    ExportMusicByFileName(it.SubItems[0].Text);
                    logText += " success" + Environment.NewLine;
                    successCount++;
                }
                catch (Exception ex)
                {
                    logText += " error:" + ex.Message + Environment.NewLine;
                    failCount++;
                }
                Text = "正在导出，共 " + totalCount + " 个，成功 " + successCount + " 个，失败 " + failCount + " 个";
                Application.DoEvents();
            }
            using (StreamWriter sw = new StreamWriter("export.log"))
                sw.Write(logText);
            UpdateListView();
            MessageBox.Show("导出完成，成功 " + successCount + " 个，失败 " + failCount + " 个。");

        }
        
    }
}
