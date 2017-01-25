using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class RawChord
    {
        public readonly double Time;
        public readonly string Chord;
        public double Length;
        public RawChord(double time, string chord)
        {
            Time = time;
            Chord = chord;
        }
    }
}
