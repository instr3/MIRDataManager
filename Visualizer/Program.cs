using Common;
using MIREditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;

namespace Visualizer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Chord Subtitle by instr3");
            Console.WriteLine("[R] Press R to record relative chord");
            Console.WriteLine("[A] Press A to record absolute chord");
            Console.WriteLine("[*] Press anything else to preview");
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key==ConsoleKey.R)
            {
                ToImages(false);
            }
            else if (key.Key == ConsoleKey.A)
            {
                ToImages(true);
            }
            else
            {
                Bass.BASS_Init(-1, 190000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new VisualizeForm());
            }
        }

        static void ToImages(bool absoluteChord)
        {
            INIReader iniReader = new INIReader("Config.ini");
            string filename = iniReader["File"];
            SongInfo testSongInfo = ArchiveManager.ReadFromArchive(filename);
            string musicPath = Settings.DatasetMusicFolder + "\\" + testSongInfo.MusicConfigure.Location;
            TimeSpan totalTime = TimeSpan.FromSeconds(MiscWrapper.GetMP3Length(musicPath));

            SubtitleVisualizer SubtitleVisualizer = new SubtitleVisualizer(null, testSongInfo, true, absoluteChord, iniReader.Data);

            int FRAMERATE = 60;
            int playerState = 0;
            double endTime = 0;
            string tempPath = @"C:\temp";
            string outputPath = Application.StartupPath + "\\output";
            string sPath = Path.Combine(tempPath, "visualizer_output-" + DateTime.Now.ToString("MMddyy-HHmmss"));
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            for (int frame = 0; ; ++frame)
            {
                double currentTime = frame * (1.0 / FRAMERATE);
                Console.WriteLine("Frame " + frame + "(" + TimeSpan.FromSeconds(currentTime).ToString(@"hh\:mm\:ss\:fff") + "/" + totalTime.ToString(@"hh\:mm\:ss") + ")");
                if (playerState == 0 && !SubtitleVisualizer.DrawFrame(currentTime))
                {
                    if (absoluteChord)
                        break;
                    playerState = 2;
                    endTime = currentTime;
                }
                else if (playerState == 2)
                {
                    if (!SubtitleVisualizer.DrawStatistics(currentTime - endTime))
                        break;
                }
                SubtitleVisualizer.Image.Save(Path.Combine(sPath, frame.ToString("000000") + ".png"), System.Drawing.Imaging.ImageFormat.Png);
            }
            Console.WriteLine("Image done");

            using (Process process = new Process())
            {
                ProcessStartInfo p = new ProcessStartInfo();
                string outputName = Path.Combine(outputPath, Path.GetFileName(testSongInfo.MusicConfigure.Location));
                p.FileName = "ffmpeg";
                p.Arguments = string.Format(" -r 60 -thread_queue_size 2048 -i \"{0}\\%06d.png\" -i \"{1}\" -c:v libx264 -vf fps=60 -pix_fmt yuv420p \"{2}.mp4\"", sPath, musicPath, outputName);
                p.UseShellExecute = false;
                process.StartInfo = p;
                process.Start();
                process.WaitForExit();
                Console.WriteLine("Exit with " + process.ExitCode);
            }
            Directory.Delete(sPath, true);
            Console.WriteLine("Done! Press Enter To Exit.");
            Console.ReadLine();
            Application.Exit();
        }
    }
}
