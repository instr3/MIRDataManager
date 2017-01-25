using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Chord : ICloneable
    {
        public enum MutedChordTypeEnum
        {
            NMark = 0,
            XMark = 1,
            QMark = 2
        };
        public enum ChordStructureEnum
        {
            Other = -1,
            Major = 0,
            Minor = 1,
            Diminished = 2,
            Augmented = 3,
            Sus4 = 4,
            Sus2 = 5
        }
        public static string[] AbsoluteChordStructureName =
            new string[] { "", "m", "dim", "aug", "sus4", "sus2" };
        public static string[] RelativeChordStructureName =
            new string[] { "", "", "°", "+", "sus4", "sus2" };
        public static string[] Num2RomeBig = new string[] { "I", "I#", "II", "IIIb", "III", "IV", "IV#", "V", "VIb", "VI", "VIIb", "VII" };
        public static string[] Num2RomeSmall = new string[] { "i", "i#", "ii", "iiib", "iii", "iv", "iv#", "v", "vib", "vi", "viib", "vii" };

        public static string[] Num2Char = new string[] { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "Ab", "A", "Bb", "B" };
        public static string[] Num2NoteString = new string[] { "1", "#1", "2", "b3", "3", "4", "#4", "5", "b6", "6", "b7", "7" };
        private readonly int Scale;// Absolute
        private readonly ChordStructureEnum Structure;
        private readonly MutedChordTypeEnum MutedChordType;
        public static Chord NoChord = new Chord(MutedChordTypeEnum.NMark);
        public static Chord UnknownChord = new Chord(MutedChordTypeEnum.QMark);
        public static Chord UnrepresentableChord = new Chord(MutedChordTypeEnum.XMark);
        public override string ToString()
        {
            if (Scale == -1)
            {
                switch (MutedChordType)
                {
                    case MutedChordTypeEnum.NMark:
                        return "N";
                    case MutedChordTypeEnum.XMark:
                        return "X";
                    case MutedChordTypeEnum.QMark:
                        return "?";
                }
            }
            return Num2Char[Scale] + AbsoluteChordStructureName[(int)Structure];
        }
        public string ToString(Tonalty tonalty)
        {
            if (tonalty.Root == -1)
            {
                return ToString();
            }
            if (Scale == -1)
            {
                switch (MutedChordType)
                {
                    case MutedChordTypeEnum.NMark:
                        return "N";
                    case MutedChordTypeEnum.XMark:
                        return "X";
                    case MutedChordTypeEnum.QMark:
                        return "?";
                }
            }
            int delta = Scale - tonalty.Root;
            if (delta < 0) delta += 12;
            return (Structure == ChordStructureEnum.Minor || Structure == ChordStructureEnum.Diminished ?
                Num2RomeSmall[delta] : Num2RomeBig[delta]) + RelativeChordStructureName[(int)Structure];
        }
        public static Chord SimpleTraid(int scale, bool majmin)
        {
            return new Chord(scale, majmin? ChordStructureEnum.Major:ChordStructureEnum.Minor);
        }
        public static Chord DiminishTraid(int scale)
        {
            return new Chord(scale, ChordStructureEnum.Diminished);
        }
        public static Chord AugmentedTraid(int scale)
        {
            return new Chord(scale, ChordStructureEnum.Augmented);
        }
        public static Chord Suspended4Traid(int scale)
        {
            return new Chord(scale, ChordStructureEnum.Sus4);
        }
        public static Chord Suspended2Traid(int scale)
        {
            return new Chord(scale, ChordStructureEnum.Sus2);
        }
        private Chord(int scale, ChordStructureEnum structure)
        {
            if (scale < 0 || scale >= 12)
            {
                throw new ArgumentException("Scale out of range");
            }
            Scale = scale;
            Structure = structure;
        }
        private Chord(MutedChordTypeEnum mutedChordType)
        {
            Scale = -1;
            Structure = ChordStructureEnum.Other;
            MutedChordType = mutedChordType;
        }
        public Chord(string absoluteChordName)
        {
            int p = 0;
            switch (absoluteChordName[p])
            {
                case 'N':
                    Scale = -1;
                    Structure = ChordStructureEnum.Other;
                    MutedChordType = MutedChordTypeEnum.NMark;
                    return;
                case 'X':
                    Scale = -1;
                    Structure = ChordStructureEnum.Other;
                    MutedChordType = MutedChordTypeEnum.XMark;
                    return;
                case '?':
                    Scale = -1;
                    Structure = ChordStructureEnum.Other;
                    MutedChordType = MutedChordTypeEnum.QMark;
                    return;
                case 'C':
                    Scale = 0;
                    break;
                case 'D':
                    Scale = 2;
                    break;
                case 'E':
                    Scale = 4;
                    break;
                case 'F':
                    Scale = 5;
                    break;
                case 'G':
                    Scale = 7;
                    break;
                case 'A':
                    Scale = 9;
                    break;
                case 'B':
                    Scale = 11;
                    break;
                default:
                    throw new ArgumentException("No such chord!");
            }
            ++p;
            if (absoluteChordName.Length>p && absoluteChordName[p] == '#')
            {
                ++Scale;
                ++p;
                if (Scale == 12) Scale = 0;
            }
            else if (absoluteChordName.Length > p && absoluteChordName[p] == 'b')
            {
                --Scale;
                ++p;
                if (Scale == -1) Scale = 11;
            }
            string str = absoluteChordName.Substring(p);
            if(str=="")
                Structure = ChordStructureEnum.Major;
            else if(str[0]=='m')
                Structure = str.StartsWith("maj") ? ChordStructureEnum.Major : ChordStructureEnum.Minor;
            else if(str.StartsWith("dim"))
                Structure = ChordStructureEnum.Diminished;
            else if (str.StartsWith("aug"))
                Structure = ChordStructureEnum.Augmented;
            else if (str.StartsWith("sus4"))
                Structure = ChordStructureEnum.Sus4;
            else if (str.StartsWith("sus2"))
                Structure = ChordStructureEnum.Sus2;
            else
                Structure = ChordStructureEnum.Major;
        }
        public Chord ShiftPitch(int pitch)
        {
            if (Scale == -1) return this;
            return new Chord((Scale + pitch % 12 + 12) % 12, Structure);
        }
        public int[] ToNotes()
        {
            int[] result = ToNotesUnclamped();
            for (int i = 0; i < result.Length; ++i)
                if (result[i] >= 12) result[i] -= 12;
            return result;
        }
        public int[] ToNotesUnclamped()
        {
            int[] secondDelta = new int[] { 4, 3, 3, 4, 5, 2 };
            int[] thirdDelta = new int[] { 7, 7, 6, 8, 7, 7 };
            if (Scale == -1)
            {
                return new int[0];
            }
            int[] result = new int[3];
            result[0] = Scale;
            result[1] = Scale + secondDelta[(int)Structure];
            result[2] = Scale + thirdDelta[(int)Structure];
            return result;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public override bool Equals(object obj)
        {
            
            if (obj is Chord)
            {
                if(Scale==-1)
                {
                    return Scale == (obj as Chord).Scale &&
                        MutedChordType == (obj as Chord).MutedChordType;
                }
                else
                {
                    return Scale == (obj as Chord).Scale &&
                        Structure == (obj as Chord).Structure;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            // Todo: can be better
            return ToString().GetHashCode();
        }

        public bool IsMutedChord()
        {
            return Scale == -1;
        }
    }
}
