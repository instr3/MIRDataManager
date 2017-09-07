using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace MIREditor
{
    public class TimingPoint
    {
        public double Offset { get; set; }
        public double BeatLength { get; set; }
        public int TimeSignature { get; set; }
        public int SampleSet { get; set; }
        public int CustomSamples { get; set; }
        public int Volumn { get; set; }
        public bool TimingChange { get; set; }
        public int EffectFlag { get; set; }
        public double BPM { get { return BeatLength > 0 ? 60000 / BeatLength : 0; } }
        public double Position { get { return Offset / 1000.0; } }
    }
    public class OsuAnalyzer
    {
        public static string directoryName;
        static List<TimingPoint> timingPoints;
        public static List<TimingPoint> GetTimingPoints(string osuFilename)
        {
            ExtractFromOSUFile(osuFilename);
            return timingPoints;
        }
        public static SongInfo ExtractFromOSUFile(string osuFilename,string folderConstraint="", bool killIfTooOld=false)
        {
            if(!osuFilename.StartsWith(folderConstraint))
            {
                Logger.Log("[Error]The osu file is not included in the dataset.");
                return null;
            }
            timingPoints = new List<TimingPoint>();
            SongInfo songinfo = new SongInfo();
            //FileInfo osuFile = new FileInfo(osuFilename);
            //DirectoryInfo dir = osuFile.Directory;
            string directoryPath = Path.GetDirectoryName(osuFilename);
            string fileName = Path.GetFileName(osuFilename);
            directoryName = Path.GetFileName(directoryPath);

            //TempOSUFolderName = dir.Name;
            songinfo.MusicConfigure = new SongInfo.MusicConfig();
            songinfo.TagConfigure = new SongInfo.TagConfig();
            songinfo.TagConfigure.Tagger = Settings.TaggerName;
            songinfo.TagConfigure.Time = DateTime.Now;
            songinfo.TagConfigure.Confidence = 1;
            songinfo.MiscConfigure = new SongInfo.MiscConfig();
            songinfo.MiscConfigure.LinkedFile = directoryName + "/" + fileName;
            using (StreamReader sr = new StreamReader(osuFilename))
            {
                bool tpContext = false;
                while (!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine();
                    if (buffer.Contains("[TimingPoints]"))
                    {
                        tpContext = true;
                    }
                    else if (buffer.Length >= 1 && buffer[0] == '[')
                    {
                        tpContext = false;
                    }
                    else if (tpContext)
                    {
                        string[] split = buffer.Split(',');
                        if (split.Length >=2)
                        {
                            TimingPoint timingPoint = new TimingPoint();
                            timingPoint.Offset = double.Parse(split[0]);
                            timingPoint.BeatLength = double.Parse(split[1]);
                            if(split.Length>=3)
                            {
                                timingPoint.TimeSignature = int.Parse(split[2]);
                                if(split.Length==8)
                                {
                                    timingPoint.SampleSet = int.Parse(split[3]);
                                    timingPoint.CustomSamples = int.Parse(split[4]);
                                    timingPoint.Volumn = int.Parse(split[5]);
                                    timingPoint.TimingChange = int.Parse(split[6]) > 0;
                                    timingPoint.EffectFlag = int.Parse(split[7]);

                                }
                            }
                            timingPoints.Add(timingPoint);
                            //triple metre or quadruple metre
                            if (songinfo.MusicConfigure.MetreNumber == 0 && timingPoint.TimeSignature>0)
                            {
                                songinfo.MusicConfigure.MetreNumber = timingPoint.TimeSignature;
                            }
                        }
                    }
                    else
                    {
                        string DivisorRegex;
                        Match match;
                        DivisorRegex = @"^AudioFilename:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            FileInfo musicFile = new FileInfo(directoryPath + "\\" + match.Groups["n"].Value);
                            songinfo.MusicConfigure.Location = directoryName + "/" + match.Groups["n"].Value;
                            songinfo.MusicConfigure.Extension = musicFile.Extension.Replace(".", "");
                            songinfo.MusicConfigure.MD5 = MiscWrapper.GetFileMD5(musicFile.FullName);
                            songinfo.MusicConfigure.Source = "OSU";
                            continue;
                        }
                        DivisorRegex = @"^Title:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.MusicConfigure.Title = match.Groups["n"].Value;
                            continue;
                        }
                        DivisorRegex = @"^BeatmapSetID:\s*(?<n>[0-9]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            songinfo.MiscConfigure.osuMapID= int.Parse(match.Groups["n"].Value);
                            continue;
                        }
                    }
                }
            }
            if (songinfo.MusicConfigure.MetreNumber == 0)
            {
                songinfo.MusicConfigure.MetreNumber = 4;
            }
            if (songinfo.MiscConfigure.osuMapID == 0)
            {
                int folderNumber = 0;
                if (directoryName.Contains(' '))
                {
                    string folderNumberString;
                    folderNumberString = directoryName.Substring(0, directoryName.IndexOf(' '));
                    int.TryParse(folderNumberString, out folderNumber);
                }
                songinfo.MiscConfigure.osuMapID = folderNumber;
                /*TextInputForm textInputForm = new TextInputForm(folderNumber.ToString(), "由于格式过旧，请手动指定Beatmap Set ID");
                textInputForm.ShowDialog();
                if (textInputForm.Tag != null)
                {
                    if(int.TryParse(textInputForm.Tag.ToString(),out folderNumber))
                    {
                        songinfo.MiscConfigure.osuMapID = folderNumber;
                    }
                }*/
            }
            GetBeatInfo(osuFilename, songinfo, timingPoints);
            return songinfo;
        }

        private static void GetBeatInfo(string osuFilename, SongInfo songinfo, List<TimingPoint> timingPoints)
        {
            double MP3Length = MiscWrapper.GetMP3Length(Directory.GetParent(Path.GetDirectoryName(osuFilename)) + "\\" + songinfo.MusicConfigure.Location);
            
            List<BeatInfo> beats = new List<BeatInfo>();
            double DeltaEPS = 0.050;
            TimingPoint lastTp = null;
            for (int i = 0; i < timingPoints.Count; ++i)
            {
                TimingPoint tp;
                tp = timingPoints[i];
                if (tp.BeatLength > 0)
                {
                    if (lastTp != null)
                    {
                        double limitPosition = tp.Position - DeltaEPS;
                        double deltaTime = lastTp.BeatLength / 1000.0;
                        BeatInfo binfo = new BeatInfo();
                        binfo.Time = lastTp.Position;
                        //binfo.Tag = lastTp.BPM.ToString();
                        binfo.BarAttribute = 1;
                        int barCount = 0;
                        beats.Add(binfo);
                        for (int j=1; j * deltaTime + lastTp.Position <= limitPosition; ++j)
                        {
                            double pos = j * deltaTime + lastTp.Position;
                            binfo = new BeatInfo();
                            binfo.Time = pos;
                            //binfo.Tag = "-";
                            ++barCount;
                            if (barCount == songinfo.MusicConfigure.MetreNumber)
                            {
                                binfo.BarAttribute = 1;
                                barCount = 0;
                            }
                            beats.Add(binfo);
                        }
                    }
                    lastTp = tp;
                }
            }
            if (lastTp != null)
            {
                double limitPosition = MP3Length + DeltaEPS;
                double deltaTime = lastTp.BeatLength / 1000.0;
                BeatInfo binfo = new BeatInfo();
                binfo.Time = lastTp.Position;
                // binfo.Tag = lastTp.BPM.ToString();
                binfo.BarAttribute = 1;
                int barCount = 0;
                beats.Add(binfo);
                for (int j = 1; j * deltaTime + lastTp.Position <= limitPosition; ++j)
                {
                    double pos = j * deltaTime + lastTp.Position;
                    binfo = new BeatInfo();
                    binfo.Time = pos;
                    //binfo.Tag = "-";
                    ++barCount;
                    if (barCount == songinfo.MusicConfigure.MetreNumber)
                    {
                        binfo.BarAttribute = 1;
                        barCount = 0;
                    }
                    beats.Add(binfo);
                }
            }
            songinfo.Beats = beats;
        }
    }
}
