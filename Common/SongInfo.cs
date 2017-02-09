using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SongInfo
    {
        public class MusicConfig
        {
            public string Location { get; set; }
            public string Extension { get; set; }
            public string MD5 { get; set; }
            public string Source { get; set; }
            public int MetreNumber { get; set; }
            public string Title { get; set; }
        }
        public class TagConfig
        {
            public string Tagger { get; set; }
            public DateTime Time { get; set; }
            public int Confidence { get; set; }
        }
        public class MiscConfig
        {
            public string LinkedFile { get; set; }
            public int osuMapID { get; set; }
        }
        public MusicConfig MusicConfigure;
        public TagConfig TagConfigure;
        public MiscConfig MiscConfigure;
        public List<BeatInfo> Beats;
        public List<RawChord> RawChords;
        public Chroma Chroma;
        public SongInfo()
        {

        }
        public SongInfo(string dataString,bool configInfoOnly=false)
        {
            List<string> lines = TextProcessor.LinesToList(dataString);
            while (lines.Count>0)
            {
                if(TextProcessor.IsDelimiter(lines[0]))
                {
                    switch(lines[0])
                    {
                        case "[MusicConfig]":
                            MusicConfigure = TextProcessor.GetClass(lines, typeof(MusicConfig)) as MusicConfig;
                            break;
                        case "[TagConfig]":
                            TagConfigure = TextProcessor.GetClass(lines, typeof(TagConfig)) as TagConfig;
                            break;
                        case "[MiscConfig]":
                            MiscConfigure = TextProcessor.GetClass(lines, typeof(MiscConfig)) as MiscConfig;
                            break;
                        case "[BeatInfo]":
                            int count = TextProcessor.GetIntValue(lines[1]);
                            if(!configInfoOnly)
                            {
                                Beats = new List<BeatInfo>(count);
                                for (int i = 0; i < count; ++i)
                                {
                                    Beats.Add(new BeatInfo(TextProcessor.GetStringValue(lines[2 + i])));
                                }
                            }
                            lines.RemoveRange(0, count + 2);
                            break;
                    }
                }
                else
                {
                    throw new FormatException("dataString Corrupted");
                }
            }
        }
        public override string ToString()
        {
            string res = string.Join("",
                TextProcessor.AddClass(MusicConfigure, typeof(MusicConfig)),
                TextProcessor.AddClass(TagConfigure, typeof(TagConfig)),
                TextProcessor.AddClass(MiscConfigure, typeof(MiscConfig)));
            res += TextProcessor.AddBlock("BeatInfo");
            res += TextProcessor.AddAttribute("Count", Beats.Count);
            for(int i=0;i<Beats.Count;++i)
            {
                BeatInfo beatInfo = Beats[i];
                string partString;
                if (beatInfo.SecondChordPercent > 0)
                {
                    partString = string.Format(@"{{{0},{1},{2},{3},{4},{5}}}",
                        beatInfo.Time.ToString("R"),
                        beatInfo.BarAttribute,
                        beatInfo.Tonalty,
                        beatInfo.Chord,
                        beatInfo.SecondChordPercent,
                        beatInfo.SecondChord);
                }
                else
                {
                    partString = string.Format(@"{{{0},{1},{2},{3}}}",
                        beatInfo.Time.ToString("R"),
                        beatInfo.BarAttribute,
                        beatInfo.Tonalty,
                        beatInfo.Chord);
                }
                res += TextProcessor.AddAttribute("Beat_" + i, partString);
            }
            return res;
        }
        public void CalcBeatChord()
        {
            if (Beats.Count == 0) return;
            int p = 0;
            while (p < RawChords.Count && RawChords[p].Time + RawChords[p].Length < Beats[0].Time)
            {
                ++p;
            }
            for (int i = 0; i < Beats.Count; ++i)
            {
                double beatStart = Beats[i].Time;
                double beatEnd = i == Beats.Count - 1 ? double.PositiveInfinity : Beats[i + 1].Time;
                Dictionary<string, double> chordLen = new Dictionary<string, double>();

                while (p < RawChords.Count && RawChords[p].Time + RawChords[p].Length < beatEnd)
                {
                    if (!chordLen.ContainsKey(RawChords[p].Chord))
                        chordLen[RawChords[p].Chord] = 0;
                    chordLen[RawChords[p].Chord] += Math.Min(RawChords[p].Length, RawChords[p].Time + RawChords[p].Length - beatStart);
                    ++p;
                }
                if (p < RawChords.Count)
                {
                    if (!chordLen.ContainsKey(RawChords[p].Chord))
                        chordLen[RawChords[p].Chord] = 0;
                    chordLen[RawChords[p].Chord] += beatEnd - Math.Max(beatStart, RawChords[p].Time);
                }
                string maxAtStr = "N";
                double maxValue = 0;
                foreach (KeyValuePair<string, double> kv in chordLen)
                {
                    if (kv.Value > maxValue)
                    {
                        maxValue = kv.Value;
                        maxAtStr = kv.Key;
                    }
                }
                Beats[i].SetChord(Chord.GetChordByAbsoluteChordName(maxAtStr));
            }

        }
        public void EstimateGlobalTonalty()
        {
            if (Chroma.GlobalChroma == null) return;
            int[] Adder = new int[] { 0, 2, 4, 5, 7, 9, 11 };
            int maxAt = 0;
            double maxValue = 0;
            for (int i = 0; i < 12; ++i)
            {
                double currentValue = 0;
                foreach (int j in Adder)
                {
                    currentValue += Chroma.GlobalChroma[(i + j) % 12];
                }
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxAt = i;
                }
            }
            int laPos = maxAt - 3 < 0 ? maxAt + 9 : maxAt - 3;
            Tonalty sampleTonalty = Tonalty.MajMinTonalty(maxAt, Chroma.GlobalChroma[laPos] > Chroma.GlobalChroma[maxAt] ? false : true);
            foreach (BeatInfo beat in Beats)
            {
                beat.Tonalty = sampleTonalty;
            }
        }
    }

}
