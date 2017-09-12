using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Tonality
    {
        private static string Scale2TonalityName(int scale)
        {
            if (scale == -1)
            {
                return "?";
            }
            return Chord.Num2Char[scale];
        }
        public bool IsOnNaturalScale(int note)
        {
            if (Root == -1)
            {
                return true;
            }
            int delta = ((note - Root) % 12 + 12) % 12;
            return (delta == 0 || delta == 2 || delta == 4 || delta == 5 || delta == 7 || delta == 9 || delta == 11);
        }

        public string NoteNameUnderTonality(int note)
        {
            if (Root == -1)
            {
                return Chord.Num2Char[note];
            }
            return Chord.Num2NoteString[note - Root < 0 ? note - Root + 12 : note - Root];
        }
        public readonly int Root; // Always Refer to 1(Do).
        public readonly bool MajMin;
        static readonly string tonalitySampleString = "C.D.EF.G.A.B";
        public Tonality(string tonalityName)
        {
            if (tonalityName == "?")
            {
                Root = -1;
                MajMin = false;
                return;
            }
            int p = 0;
            int tget = tonalitySampleString.IndexOf(tonalityName[p]);
            if (tget != -1)
            {
                Root = tget;
            }
            ++p;
            if (tonalityName[p] == '#')
            {
                ++Root;
                ++p;
                if (Root == 12) Root = 0;
            }
            else if (tonalityName[p] == 'b')
            {
                --Root;
                ++p;
                if (Root == -1) Root = 11;
            }
            if (tonalityName[p] != ' ')
                throw new FormatException("Tonality Format Error");
            ++p;
            if (tonalityName.Substring(p) == "Maj")
            {
                MajMin = true;
            }
            else if (tonalityName.Substring(p) == "min")
            {
                MajMin = false;
                Root = (Root + 3) % 12;
            }
            else
                throw new NotImplementedException("Unknown Tonality");
        }
        private Tonality()
        {
            Root = -1;
            MajMin = false;
        }
        public static Tonality MajMinTonality(int root,bool majmin)
        {
            return new Tonality(root, majmin);
        }
        private Tonality(int root, bool majmin)
        {
            Root = root;
            MajMin = majmin;
        }
        public override string ToString()
        {
            string res = Scale2TonalityName(Root);
            if (Root == -1) return res;
            if (MajMin == false)
            {
                res = Scale2TonalityName(Root - 3 < 0 ? Root + 9 : Root - 3);
                return res + " min";
            }
            return res + " Maj";
        }
        public static Tonality NoTonality = new Tonality();

        public static Tonality RelativeTonality(Tonality tonality)
        {
            return new Tonality(tonality.Root, !tonality.MajMin);
        }

        public static bool operator ==(Tonality lhs, Tonality rhs)
        {
            if (ReferenceEquals(lhs, null)) return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null)) return false;
            return lhs.Root == rhs.Root &&
                (lhs.Root == -1 || lhs.MajMin == rhs.MajMin);
        }
        public static bool operator !=(Tonality lhs, Tonality rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            if (obj is Tonality)
                return this == (Tonality)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
