using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Settings
    {
        // public static Settings Instance;
        public static string ArchiveFolder;
        public static string DatasetMusicFolder;
        public static string ExportFolder;
        public static string FullArchiveFilePath = null;
        public static string TaggerName;
        public static string OsuMapLink;
        public static string OsuMirrorDownloadLink;

        static Settings()
        {
            INIReader INIReader = new INIReader("settings.ini");
            ArchiveFolder = INIReader["ArchiveFolder"];
            ExportFolder = INIReader["ExportFolder"];
            DatasetMusicFolder = INIReader["DatasetMusicFolder"];
            TaggerName = INIReader["TaggerName"];
            OsuMapLink = INIReader["OsuMapLink"];
            OsuMirrorDownloadLink = INIReader["OsuMirrorDownloadLink"];
        }
    }
}
