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
        public Chord Chord { get; set; }
        public int BarAttribute { get; set; }
        public Tonalty Tonalty { get; set; }
        public BeatInfo()
        {
            Tonalty = Tonalty.NoTonalty;
            Chord = Chord.NoChord;
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
            Chord = new Chord(groups[3]);
        }
        public override string ToString()
        {
            return Time.ToString() + '\t' + BarAttribute.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
