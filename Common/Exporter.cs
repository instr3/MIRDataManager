using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;

namespace Common
{
    public class Exporter
    {
        public string BeatTags;
        public string ChordTags;
        public string TonalityTags;
        private readonly string musicExtension;
        static void AppendLine(ref string str,params object[] args)
        {
            str += string.Join("\t", args) + Environment.NewLine;
        }
        void ExportChord(SongInfo info)
        {
            if (info.Beats.Count < 2) return;
            int left = 0, right = info.Beats.Count - 1;
            Chord lastChord = null;
            Tonality lastTonality = null;
            ClearChordSwitchPoint();
            for (int i = left; i < right; ++i)
            {
                BeatInfo beat = info.Beats[i];
                if (lastChord != beat.Chord)
                {
                    SetChordSwitchPoint(beat.Time, beat.Chord, beat.Tonality);
                    lastChord = beat.Chord;
                    lastTonality = beat.Tonality;
                }
                if (beat.SecondChordPercent > 0)
                {
                    BeatInfo nextBeat = info.Beats[i + 1];
                    double insertTime = nextBeat.Time - (nextBeat.Time - beat.Time) * beat.SecondChordPercent;
                    SetChordSwitchPoint(insertTime, beat.SecondChord, beat.Tonality);
                    lastChord = beat.SecondChord;
                    lastTonality = beat.Tonality;
                }
            }
            SetChordSwitchPoint(info.Beats[right].Time, null, null);
        }
        double tempLastSwitchTime;
        Chord tempLastSwitchChord = null;
        Tonality tempLastSwitchTonality = null;
        private void ClearChordSwitchPoint()
        {
            tempLastSwitchChord = null;
            tempLastSwitchTonality = null;
        }
        private void SetChordSwitchPoint(double time, Chord chord, Tonality tonality)
        {
            if (tempLastSwitchChord != null)
            {
                DrawChordBetween(tempLastSwitchTime, time, tempLastSwitchChord, tempLastSwitchTonality);
            }
            tempLastSwitchTime = time;
            tempLastSwitchChord = chord;
            tempLastSwitchTonality = tonality;
        }
        private void DrawChordBetween(double leftTime, double rightTime, Chord chord, Tonality tonality)
        {
            AppendLine(ref ChordTags, leftTime.ToString("F7"), rightTime.ToString("F7"), chord.ToString());
        }
        public Exporter(SongInfo songInfo,double MP3Length)
        {
            // Todo: Every single part is wrong.
            // Todo: Export issues, like the last chord and last tonality.
            BeatTags = "";
            ChordTags = "";
            TonalityTags = "";
            musicExtension = songInfo.MusicConfigure.Extension;
            List<BeatInfo> beats = songInfo.Beats;
            
            BeatInfo mp3End = new BeatInfo();
            mp3End.Time = MP3Length;
            int tct = 1;
            for(int i=0;i<beats.Count;++i)
            {
                BeatInfo beat = beats[i];
                if (beat.BarAttribute >= 1)
                    tct = 1;
                else if (beat.BarAttribute != 0)
                    continue;
                BeatInfo nextBeat = i == beats.Count - 1 ? mp3End : beats[i + 1];
                AppendLine(ref BeatTags, beat.Time.ToString("F7"), nextBeat.Time.ToString("F7"), tct);
                ++tct;
            }
            ExportChord(songInfo);
            for (int i = 0; i < beats.Count-1; ++i)
            {
                BeatInfo beat = beats[i];
                Tonality curTonality = beat.Tonality;
                for (; i < beats.Count-1; ++i)
                    if (beats[i].Tonality.ToString() != curTonality.ToString())
                        break;
                BeatInfo nextBeat = beats[i];

                AppendLine(ref TonalityTags, beat.Time.ToString("F7"), nextBeat.Time.ToString("F7"), curTonality.ToString());
                --i;
            }
        }
        public void ExportMusic(string folder, string filename, string musicPath)
        {
            string targetPath = folder + "\\music\\" + filename + "." + musicExtension;
            if (File.Exists(targetPath))
                File.Delete(targetPath);
            File.Copy(musicPath, targetPath);
        }
        public void ExportToFolder(string folder,string filename,bool exportMusic=false,string musicPath=null)
        {
            if (!Directory.Exists(folder + "\\beatlab"))
                Directory.CreateDirectory(folder + "\\beatlab");
            if (!Directory.Exists(folder + "\\chordlab"))
                Directory.CreateDirectory(folder + "\\chordlab");
            if (!Directory.Exists(folder + "\\keylab"))
                Directory.CreateDirectory(folder + "\\keylab");
            if (!Directory.Exists(folder + "\\music"))
                Directory.CreateDirectory(folder + "\\music");
            using (StreamWriter sw = new StreamWriter(folder + "\\beatlab\\" + filename + ".lab"))
                sw.Write(BeatTags);
            using (StreamWriter sw = new StreamWriter(folder + "\\chordlab\\" + filename + ".lab"))
                sw.Write(ChordTags);
            using (StreamWriter sw = new StreamWriter(folder + "\\keylab\\" + filename + ".lab"))
                sw.Write(TonalityTags);
            if(exportMusic)
            {
                string targetPath = folder + "\\music\\" + filename + "." + musicExtension;
                if (File.Exists(targetPath))
                    File.Delete(targetPath);
                File.Copy(musicPath, targetPath);
            }
        }
    }
}
