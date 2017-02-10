using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace MIRDataManager
{
    static class Program
    {
        public static string ArchiveFolder;
        public static string ExportFolder;
        public static string DatasetMusicFolder;

        public static string OsuMapLink;
        public static string OsuMirrorDownloadLink;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            INIReader INIReader = new INIReader("settings.ini");
            ArchiveFolder = INIReader["ArchiveFolder"];
            ExportFolder = INIReader["ExportFolder"];
            DatasetMusicFolder = INIReader["DatasetMusicFolder"];
            OsuMapLink = INIReader["OsuMapLink"];
            OsuMirrorDownloadLink = INIReader["OsuMirrorDownloadLink"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
