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
            public string MirexSuffix { get; set; }
            public string RelativeLabel { get; set; }
            public string SoundNotes { get; set; }
            public string ScriptAnnotation { get; set; }
            public string Parent { get; set; }
            public string Abbr { get; set; }
            public int[] Notes;
        }
        private static Chord[,] chordFlyweights;
        private static int[] nextInversionTemplateID; // For inversion input only
        private static string[,] chordFlyweightLabels;
        public struct ScriptAnnotationStruct
        {
            public string suffix1;
            public string superscript;
            public string subscript;
            public string suffix2;
            public int inversion;
        }
        private static ScriptAnnotationStruct[,] chordFlyweightScriptAnnotations;
        private static Chord[,] parentChord;
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
            List<int> nextInversion = new List<int>();
            Dictionary<string, int> descriptionDict = new Dictionary<string, int>();
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
                descriptionDict[template.Description] = rawList.Count;
                rawList.Add(template);
                nextInversion.Add(nextInversion.Count);
                if (template.Label == "{X}")
                    majorTraidID = rawList.Count - 1;
                if (template.Label == "{X}m")
                    minorTraidID = rawList.Count - 1;
                if (!template.Label.Contains("/"))
                {
                    for (int i = 1; i < template.Notes.Length; ++i) // Inversions
                    {
                        int new_root_delta = template.Notes[i] % 12;
                        string suffix = "/{X+" + new_root_delta.ToString() + "}";
                        RawChordTemplate template_inversion = new RawChordTemplate();
                        template_inversion.Description = template.Description + " Slash " + Num2NoteString[new_root_delta];
                        template_inversion.Label = template.Label + suffix;
                        template_inversion.Abbr = "";
                        template_inversion.RelativeLabel = template.RelativeLabel + "/" + Num2NoteString[new_root_delta];
                        template_inversion.MirexSuffix = template.MirexSuffix + "/" + Num2NoteString[new_root_delta];
                        template_inversion.ScriptAnnotation = template.ScriptAnnotation + suffix;
                        template_inversion.Parent = template.Description;
                        template_inversion.Notes = new int[template.Notes.Length];
                        for (int j = 0; j < template_inversion.Notes.Length; ++j)
                        {
                            if (j + i >= template.Notes.Length)
                                template_inversion.Notes[j] = template.Notes[j + i - template.Notes.Length] + 12; // Raise an octave
                            else
                                template_inversion.Notes[j] = template.Notes[j + i];
                        }
                        descriptionDict[template_inversion.Description] = rawList.Count;
                        rawList.Add(template_inversion);
                        nextInversion.Add(nextInversion.Count);
                        // Adjust next inversion
                        int temp = nextInversion[rawList.Count - 2];
                        nextInversion[rawList.Count - 2] = nextInversion[rawList.Count - 1];
                        nextInversion[rawList.Count - 1] = temp;
                    }
                }
            }
            if (majorTraidID == -1 || minorTraidID == -1)
            {
                throw new Exception("和弦文件格式错误：未找到大三或小三和弦");
            }
            templates = rawList.ToArray();
            nextInversionTemplateID = nextInversion.ToArray();
            chordFlyweights = new Chord[templates.Length, 12];
            chordFlyweightLabels = new string[templates.Length, 12];
            chordFlyweightScriptAnnotations = new ScriptAnnotationStruct[templates.Length, 12];
            parentChord = new Chord[templates.Length, 12];
            lookupPool = new Dictionary<string, Chord>();
            for (int i = 0; i < templates.Length; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    Chord chord = new Chord(j, i, MutedChordTypeEnum.NotMuted);
                    chordFlyweights[i, j] = chord;
                    string label = templates[i].Label.Replace("{X}", Num2Char[j]);
                    string scriptAnnotation;
                    if (templates[i].ScriptAnnotation.StartsWith("{X}"))
                    {
                        scriptAnnotation = templates[i].ScriptAnnotation.Substring(3);
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported Annotation Format: " + templates[i].ScriptAnnotation);
                    }
                    scriptAnnotation = scriptAnnotation.Replace("{X}", j.ToString());
                    for (int k = 0; k < 12; ++k)
                    {
                        label = label.Replace("{X+" + k.ToString() + "}", Num2Char[(j + k) % 12]);
                        scriptAnnotation = scriptAnnotation.Replace("{X+" + k.ToString() + "}", ((j + k) % 12).ToString());
                    }
                    chordFlyweightLabels[i, j] = label;
                    lookupPool[chord.ToString()] = chord;
                    string ClipSuffixPart(string src,char delimiter, out string result)
                    {
                        if (src.Contains(delimiter))
                        {
                            int index = src.IndexOf(delimiter);
                            result = src.Substring(index + 1);
                            string clip_src = src.Substring(0, index);
                            return clip_src;
                        }
                        else
                        {
                            result = "";
                            return src;
                        }
                    }
                    chordFlyweightScriptAnnotations[i, j] = new ScriptAnnotationStruct();
                    string tmp;
                    scriptAnnotation = ClipSuffixPart(scriptAnnotation, '/', out tmp);
                    chordFlyweightScriptAnnotations[i, j].inversion = tmp == "" ? -1 : int.Parse(tmp);
                    scriptAnnotation = ClipSuffixPart(scriptAnnotation, '+', out chordFlyweightScriptAnnotations[i, j].suffix2);
                    scriptAnnotation = ClipSuffixPart(scriptAnnotation, '_', out chordFlyweightScriptAnnotations[i, j].subscript);
                    scriptAnnotation = ClipSuffixPart(scriptAnnotation, '^', out chordFlyweightScriptAnnotations[i, j].superscript);
                    chordFlyweightScriptAnnotations[i, j].suffix1 = scriptAnnotation;

                }
            }
            for (int i = 0; i < templates.Length; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    if (templates[i].Parent != "")
                        parentChord[i, j] = chordFlyweights[descriptionDict[templates[i].Parent], j];
                    else
                        parentChord[i, j] = null;
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
        public Chord GetNextInversion()
        {
            if (scale == -1)
                return this;
            return chordFlyweights[nextInversionTemplateID[templateID], scale];
        }
        public Chord GetParentChord()
        {
            if (scale == -1)
                return null;
            return parentChord[templateID, scale];
        }
        public string ToMirexString()
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
                        return "X";
                }
            }
            return Num2Char[scale] + ":" + templates[templateID].MirexSuffix;
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
            return chordFlyweightLabels[templateID, scale];
            //return templates[templateID].Label.Replace("{X}", Num2Char[scale]);
        }
        public string ToString(Tonality tonality)
        {
            if (tonality.Root == -1)
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
            int delta = scale - tonality.Root;
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
        public ScriptAnnotationStruct ToScriptAnnotation()
        {
            return chordFlyweightScriptAnnotations[templateID, scale];
        }
        public static Chord SimpleTraid(int scale, bool majmin)
        {
            return chordFlyweights[majmin ? majorTraidID : minorTraidID, scale];
        }
        public static Chord GetChordByAbsoluteChordName(string absoluteChordName)
        {
            absoluteChordName = absoluteChordName.Replace("maj", "M").Replace("m7b5", "m7(b5)");
            Chord result;
            if (lookupPool.TryGetValue(absoluteChordName, out result))
            {
                return result;
            }
            if(absoluteChordName.Contains("/"))
            {
                return GetChordByAbsoluteChordName(absoluteChordName.Substring(0, absoluteChordName.IndexOf("/")));
            }
            throw new NotImplementedException(absoluteChordName+" is not recognized");
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
            // Adjust for inversions
            int rebase = (templates[templateID].Notes[0] + scale) / 12;
            return templates[templateID].Notes.Select(x => x + scale - rebase * 12).ToArray();
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
            return (scale == -1 ? -1 : templateID * 12 + scale).GetHashCode();
        }

        public bool IsMutedChord()
        {
            return scale == -1;
        }
    }
}
