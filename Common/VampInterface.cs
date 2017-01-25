using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class VampInterface
    {
        public const string VAMP_PATH = @"annotator\";
        public const string VAMP_EXE = @"sonic-annotator.exe";
        public const string CHILDINO_N3 = @"test";
        private static string GetVampCSV(string n3Name, string inputFile)
        {
            using (Process process = new Process())
            {
                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = VAMP_PATH + VAMP_EXE;
                p.Arguments = " -t \"" + VAMP_PATH + n3Name + ".n3\" \"" + inputFile + "\" -w csv --csv-stdout";
                p.RedirectStandardOutput = true;
                p.UseShellExecute = false;
                process.StartInfo = p;
                StringBuilder output = new StringBuilder();
                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            output.AppendLine(e.Data);
                        }
                    };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    outputWaitHandle.WaitOne();
                    return output.ToString();
                }
            }
        }

        public static List<RawChord> GetRawChord(string inputFile)
        {
            StringBuilder result = new StringBuilder(GetVampCSV("test", inputFile));
            if (result.Length == 0)
            {
                return null;
            }
            bool tmp = false;
            for (int i = 0; i < result.Length; ++i)
            {
                if (result[i] == '"') tmp = !tmp;
                if (result[i] == ',' && tmp) result[i] = ';';
            }
            string[] splited = result.ToString().Split(',');
            int n = (splited.Length - 1) / 2;
            List<RawChord> list = new List<RawChord>();
            for (int i = 0; i < n; ++i)
            {
                list.Add(new RawChord(
                    double.Parse(splited[i * 2 + 1]),
                    splited[i * 2 + 2].Substring(1, splited[i * 2 + 2].Length - 3))
                    );
            }
            for (int i = 0; i < n - 1; ++i)
            {
                list[i].Length = list[i + 1].Time - list[i].Time;
            }
            list[n - 1].Length = 9e99;
            return list;
        }

        public static Chroma GetChroma(string inputFile)
        {
            string res = GetVampCSV("chroma", inputFile);
            if (string.IsNullOrEmpty(res)) return null;
            return new Chroma(res);
        }
    }
}
