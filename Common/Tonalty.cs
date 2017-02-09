using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Tonalty
    {
        private static string Scale2TonaltyName(int scale)
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

        public string NoteNameUnderTonalty(int note)
        {
            if (Root == -1)
            {
                return Chord.Num2Char[note];
            }
            return Chord.Num2NoteString[note - Root < 0 ? note - Root + 12 : note - Root];
        }
        public readonly int Root; // Always Refer to 1(Do).
        public readonly bool MajMin;
        static readonly string tonaltySampleString = "C.D.EF.G.A.B";
        public Tonalty(string tonaltyName)
        {
            if (tonaltyName == "?")
            {
                Root = -1;
                MajMin = false;
                return;
            }
            int p = 0;
            int tget = tonaltySampleString.IndexOf(tonaltyName[p]);
            if (tget != -1)
            {
                Root = tget;
            }
            ++p;
            if (tonaltyName[p] == '#')
            {
                ++Root;
                ++p;
                if (Root == 12) Root = 0;
            }
            else if (tonaltyName[p] == 'b')
            {
                --Root;
                ++p;
                if (Root == -1) Root = 11;
            }
            if (tonaltyName[p] != ' ')
                throw new FormatException("Tonalty Format Error");
            ++p;
            if (tonaltyName.Substring(p) == "Maj")
            {
                MajMin = true;
            }
            else if (tonaltyName.Substring(p) == "min")
            {
                MajMin = false;
                Root = (Root + 3) % 12;
            }
            else
                throw new NotImplementedException("Unknown Tonalty");
        }
        private Tonalty()
        {
            Root = -1;
            MajMin = false;
        }
        public static Tonalty MajMinTonalty(int root,bool majmin)
        {
            return new Tonalty(root, majmin);
        }
        private Tonalty(int root, bool majmin)
        {
            Root = root;
            MajMin = majmin;
        }
        public override string ToString()
        {
            string res = Scale2TonaltyName(Root);
            if (Root == -1) return res;
            if (MajMin == false)
            {
                res = Scale2TonaltyName(Root - 3 < 0 ? Root + 9 : Root - 3);
                return res + " min";
            }
            return res + " Maj";
        }
        public static Tonalty NoTonalty = new Tonalty();

        public static Tonalty RelativeTonalty(Tonalty tonalty)
        {
            return new Tonalty(tonalty.Root, !tonalty.MajMin);
        }

        public static bool operator ==(Tonalty lhs, Tonalty rhs)
        {
            if (ReferenceEquals(lhs, null)) return ReferenceEquals(rhs, null);
            if (ReferenceEquals(rhs, null)) return false;
            return lhs.Root == rhs.Root &&
                (lhs.Root == -1 || lhs.MajMin == rhs.MajMin);
        }
        public static bool operator !=(Tonalty lhs, Tonalty rhs)
        {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj)
        {
            if (obj is Tonalty)
                return this == (Tonalty)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
