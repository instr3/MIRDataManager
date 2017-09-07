using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Chord : ICloneable
    {
        private class RawChordTemplate
        {
            public string Description { get; set; }
            public string Label { get; set; }
            public string RelativeLabel { get; set; }
            public string SoundNotes { get; set; }
            public string Abbr { get; set; }
            public int[] Notes;
        }
        private static Chord[,] chordFlyweights;
        private static Dictionary<string, Chord> lookupPool;
        private static RawChordTemplate[] templates;
        private static int majorTraidID, minorTraidID;
        private readonly int scale;// Absolute
        public int Scale
        {
            get { return scale; }
        }
        public int TemplateID
        {
            get { return templateID; }
        }
        private readonly int templateID;
        private readonly MutedChordTypeEnum MutedChordType;
        public enum MutedChordTypeEnum
        {
            NotMuted = -1,
            NMark = 0,
            XMark = 1,
            QMark = 2
        };
        public static string[] Num2RomeBig = new string[] { "I", "I#", "II", "IIIb", "III", "IV", "IV#", "V", "VIb", "VI", "VIIb", "VII" };
        public static string[] Num2RomeSmall = new string[] { "i", "i#", "ii", "iiib", "iii", "iv", "iv#", "v", "vib", "vi", "viib", "vii" };

        public static string[] Num2Char = new string[] { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "Ab", "A", "Bb", "B" };
        public static string[] Num2NoteString = new string[] { "1", "#1", "2", "b3", "3", "4", "#4", "5", "b6", "6", "b7", "7" };

        public static Chord NoChord;
        public static Chord UnknownChord;
        public static Chord UnrepresentableChord;
        static Chord()
        {
            List<RawChordTemplate> rawList = new List<RawChordTemplate>();
            List<string> lines;
            using (StreamReader sr = new StreamReader("ChordTemplate.ini"))
            {
                lines = TextProcessor.LinesToList(sr.ReadToEnd());
            }
            majorTraidID = -1;
            minorTraidID = -1;
            while (lines.Count > 0)
            {
                RawChordTemplate template = TextProcessor.GetClass(lines, typeof(RawChordTemplate)) as RawChordTemplate;
                template.Notes = template.SoundNotes
                    .Substring(1, template.SoundNotes.Length - 2)
                    .Split(',')
                    .Select(s => int.Parse(s))
                    .ToArray();
                rawList.Add(template);
                if (template.Label == "{X}")
                    majorTraidID = rawList.Count - 1;
                if (template.Label == "{X}m")
                    minorTraidID = rawList.Count - 1;
            }
            if (majorTraidID == -1 || minorTraidID == -1)
            {
                throw new Exception("和弦文件格式错误：未找到大三或小三和弦");
            }
            templates = rawList.ToArray();
            chordFlyweights = new Chord[templates.Length, 12];
            lookupPool = new Dictionary<string, Chord>();
            for (int i = 0; i < templates.Length; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    Chord chord = new Chord(j, i, MutedChordTypeEnum.NotMuted);
                    chordFlyweights[i, j] = chord;
                    lookupPool[chord.ToString()] = chord;
                }
            }
            NoChord = new Chord(-1, -1, MutedChordTypeEnum.NMark);
            lookupPool[NoChord.ToString()] = NoChord;
            UnknownChord = new Chord(-1, -1, MutedChordTypeEnum.QMark);
            lookupPool[UnknownChord.ToString()] = UnknownChord;
            UnrepresentableChord = new Chord(-1, -1, MutedChordTypeEnum.XMark);
            lookupPool[UnrepresentableChord.ToString()] = UnrepresentableChord;
        }
        private Chord(int inputScale, int inputTemplateID, MutedChordTypeEnum inputMutedChordType)
        {
            scale = inputScale;
            templateID = inputTemplateID;
            MutedChordType = inputMutedChordType;
        }
        public static int GetChordTemplatesCount()
        {
            return templates.Length;
        }
        public static Chord EnumerateChord(int templateID, int scale)
        {
            return chordFlyweights[templateID, scale];
        }
        public static string GetChordTemplateAbbr(int templateID)
        {
            return templates[templateID].Abbr;
        }
        public override string ToString()
        {
            if (scale == -1)
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
            return templates[templateID].Label.Replace("{X}", Num2Char[scale]);
        }
        public string ToString(Tonalty tonalty)
        {
            if (tonalty.Root == -1)
            {
                return ToString();
            }
            if (scale == -1)
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
            int delta = scale - tonalty.Root;
            if (delta < 0) delta += 12;
            return templates[templateID].RelativeLabel
                .Replace("{I}", Num2RomeBig[delta])
                .Replace("{i}", Num2RomeSmall[delta]);

        }
        public string ToRelativeSuffix()
        {
            return templates[templateID].RelativeLabel
                .Replace("{I}", "")
                .Replace("{i}", "");
        }
        public string ToAbosluteSuffix()
        {
            return templates[templateID].Label
                .Replace("{X}", "");
        }
        public static Chord SimpleTraid(int scale, bool majmin)
        {
            return chordFlyweights[majmin ? majorTraidID : minorTraidID, scale];
        }
        public static Chord GetChordByAbsoluteChordName(string absoluteChordName)
        {
            absoluteChordName = absoluteChordName.Replace("maj", "M");
            Chord result;
            if (lookupPool.TryGetValue(absoluteChordName, out result))
            {
                return result;
            }
            if(absoluteChordName.Contains("/"))
            {
                return GetChordByAbsoluteChordName(absoluteChordName.Substring(0, absoluteChordName.IndexOf("/")));
            }
            throw new NotImplementedException();
        }
        public Chord ShiftPitch(int pitch)
        {
            return chordFlyweights[templateID, (scale + pitch % 12 + 12) % 12];
        }
        public int[] ToNotes()
        {
            if (scale == -1) return new int[0];
            return templates[templateID].Notes.Select(x => (x + scale) % 12).ToArray();
        }
        public int [] ToRelativeScales(int do_pos)
        {
            if (scale == -1)
                return new int[] { };
            return templates[templateID].Notes.Select(x => (x + scale + 12 - do_pos) % 12).ToArray();
        }
        public int[] ToNotesUnclamped()
        {
            if (scale == -1) return new int[0];
            return templates[templateID].Notes.Select(x => x + scale).ToArray();
        }
        public object Clone()
        {
            return this;
        }
        public override bool Equals(object obj)
        {
            if (obj is Chord)
            {
                return this == obj;
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
            return scale == -1;
        }
    }
}
