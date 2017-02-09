using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using Common;

namespace MIREditor
{
    static class Program
    {
        public static string ArchiveFolder;
        public static string DatasetMusicFolder;
        public static string ExportFolder;
        public static string FullArchiveFilePath = null;
        public static string TaggerName;
        public static Timeline TL;
        public static MainForm Form;
        public static EditManager EditManager;
        public static MidiManager MidiManager;


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Bass.BASS_Init(-1, 190000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

            INIReader INIReader = new INIReader("settings.ini");
            ArchiveFolder = INIReader.Data["ArchiveFolder"];
            ExportFolder = INIReader.Data["ExportFolder"];
            DatasetMusicFolder = INIReader.Data["DatasetMusicFolder"];
            TaggerName = INIReader.Data["TaggerName"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form = new MainForm(args);
            Application.Run(Form);
        }
    }
}
