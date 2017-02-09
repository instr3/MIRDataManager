using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BeatInfo : ICloneable
    {
        public double Time { get; set; }
        public Chord Chord { get; private set; }
        public int BarAttribute { get; set; }
        public Tonalty Tonalty { get; set; }
        public double SecondChordPercent { get; private set; }
        public Chord SecondChord { get; private set; }
        public BeatInfo()
        {
            Tonalty = Tonalty.NoTonalty;
            SetChord(Chord.NoChord);
        }
        public BeatInfo(string dataString)
        {
            if (dataString.StartsWith("{") && dataString.EndsWith("}"))
                dataString = dataString.Substring(1, dataString.Length - 2);
            string[] groups = dataString.Split(',');
            if (groups.Length < 4)
                throw new FormatException("Bad BeatInfo DataString");
            Time = double.Parse(groups[0]);
            BarAttribute = int.Parse(groups[1]);
            Tonalty = new Tonalty(groups[2]);
            Chord = Chord.GetChordByAbsoluteChordName(groups[3]);
            if (groups.Length>=6)
            {
                SecondChordPercent = double.Parse(groups[4]);
                SecondChord = Chord.GetChordByAbsoluteChordName(groups[5]);
            }
        }
        public override string ToString()
        {
            return Time.ToString() + '\t' + BarAttribute.ToString();
        }
        public void SetChord(Chord chord)
        {
            Chord = chord;
            SecondChordPercent = 0;
            SecondChord = null;
        }
        public void SetChord(Chord chord1,Chord chord2,double secondChordPercent)
        {
            if (chord1 == chord2)
            {
                SetChord(chord1);
            }
            else
            {
                Chord = chord1;
                SecondChordPercent = secondChordPercent;
                SecondChord = chord2;
            }
        }
        public void SetChord(BeatInfo info)
        {
            Chord = info.Chord;
            SecondChordPercent = info.SecondChordPercent;
            SecondChord = info.SecondChord;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public Chord GetEndingChord()
        {
            return SecondChordPercent > 0 ? SecondChord : Chord;
        }
    }
}
