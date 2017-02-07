using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace MIRDataManager
{
    class DataDownloader
    {
        public string DownloadLink;
        public Form1 Form;
        WebClient client;
        DataFile DataFile;
        public DataDownloader(DataFile dataFile,Form1 form)
        {
            DataFile = dataFile;
            Form = form;
            DownloadLink = string.Format(Program.OsuMirrorDownloadLink, dataFile.SongInfo.MiscConfigure.osuMapID);
        }
        public void StartDownload()
        {
            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCompleted);
            client.Headers.Add("User-Agent: Other");
            client.DownloadDataAsync(new Uri(DownloadLink));
            DataFile.Downloading = true;
            Form.UpdateSelectedDataFile(DataFile);
        }

        private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if(e.Error!=null)
            {
                MessageBox.Show("下载出错：" + e.Error.Message);
                DataFile.Downloading = false;
                return;
            }
            if (e.Cancelled)
            {
                DataFile.Downloading = false;
                return;
            }
            string folderName =Path.GetFileNameWithoutExtension(new ContentDisposition(client.ResponseHeaders["Content-Disposition"]).FileName).Replace(".","");
            
            string extractPath = Path.Combine(Program.DatasetMusicFolder, folderName);
            if(!Directory.Exists(extractPath))
            {
                Directory.CreateDirectory(extractPath);
            }
            DecompressToFile(e.Result, extractPath);
            Form.progressBarDownload.Invoke(new MethodInvoker(() => Form.UpdateSelectedDataFile(DataFile)));
            client.Dispose();
            DataFile.Downloading = false;
            return;
        }
        private void DecompressToFile(byte[] data,string path)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (ZipArchive zip = new ZipArchive(ms))
                {
                    foreach (ZipArchiveEntry entry in zip.Entries)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(path, entry.FullName)));
                        entry.ExtractToFile(Path.Combine(path, entry.FullName));
                    }
                }
            }
        }
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if(Form.CurrentSelectedDataFile == DataFile)
                Form.progressBarDownload.Invoke(new MethodInvoker(() => Form.progressBarDownload.Value = e.ProgressPercentage));
        }
    }
}
