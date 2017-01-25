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
        public readonly string BeatTags;
        public readonly string ChordTags;
        public readonly string TonaltyTags;
        private readonly string musicExtension;
        static void AppendLine(ref string str,params object[] args)
        {
            str += string.Join("\t", args) + Environment.NewLine;
        }
        public Exporter(SongInfo songInfo,double MP3Length)
        {
            // Todo: Export issues, like the last chord and last tonalty.
            BeatTags = "";
            ChordTags = "";
            TonaltyTags = "";
            musicExtension = songInfo.MusicConfigure.Extension;
            List<BeatInfo> beats = songInfo.Beats;
            
            BeatInfo mp3End = new BeatInfo();
            mp3End.Time = MP3Length;
            int tct = 1;
            for(int i=0;i<beats.Count;++i)
            {
                BeatInfo beat = beats[i];
                if (beat.BarAttribute == 1)
                    tct = 1;
                else if (beat.BarAttribute != 0)
                    continue;
                BeatInfo nextBeat = i == beats.Count - 1 ? mp3End : beats[i + 1];
                AppendLine(ref BeatTags, beat.Time.ToString("F7"), nextBeat.Time.ToString("F7"), tct);
                ++tct;
            }

            for (int i = 0; i < beats.Count; ++i)
            {
                BeatInfo beat = beats[i];
                Chord curChord = beat.Chord;
                for (; i < beats.Count; ++i)
                    if (beats[i].Chord.ToString() != curChord.ToString())
                        break;
                BeatInfo nextBeat = i == beats.Count ? mp3End : beats[i];
                
                AppendLine(ref ChordTags, beat.Time.ToString("F7"), nextBeat.Time.ToString("F7"), curChord.ToString());
            }
            for (int i = 0; i < beats.Count; ++i)
            {
                BeatInfo beat = beats[i];
                Tonalty curTonalty = beat.Tonalty;
                for (; i < beats.Count; ++i)
                    if (beats[i].Tonalty.ToString() != curTonalty.ToString())
                        break;
                BeatInfo nextBeat = i == beats.Count ? mp3End : beats[i];

                AppendLine(ref TonaltyTags, beat.Time.ToString("F7"), nextBeat.Time.ToString("F7"), curTonalty.ToString());
            }
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
                sw.Write(TonaltyTags);
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
