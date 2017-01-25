using common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chordgen
{
    class Tonalty
    {
        public static string Scale2TonaltyName(int scale)
        {
            if(scale==-1)
            {
                return "?";
            }
            return Chord.Num2Char[scale];
        }
        public static bool IsOnScale(int note,int scale)
        {
            if(scale==-1)
            {
                return true;
            }
            int delta = note - scale < 0 ? note - scale + 12 : note - scale;
            return (delta == 0 || delta == 2 || delta == 4 || delta == 5 || delta == 7 || delta == 9 || delta == 11);
        }

        public static string NoteNameUnderTonalty(int note,int scale)
        {
            if(scale==-1)
            {
                return Chord.Num2Char[note];
            }
            return Chord.Num2NoteString[note - scale < 0 ? note - scale + 12 : note - scale];
        }
        public int Root;
        public bool MajMin;


    }
}
