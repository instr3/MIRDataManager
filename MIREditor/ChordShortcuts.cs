using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    class ChordShortcuts
    {
        Keys[] shortcutKeys;
        Chord[,] absoluteChords;
        Chord[,] relativeChords;
        private string RemoveBrackets(string str)
        {
            if (str.StartsWith("{") && str.EndsWith("}"))
            {
                return str.Substring(1, str.Length - 2);
            }
            return str;
        }
        private void ThrowFormatException(string keyName)
        {
            throw new Exception("Shortcuts.ini中格式错误：" + keyName + "项格式有误");
        }
        void InitChordGroup(INIReader iniReader,string groupName, Chord[,] chords)
        {
            for (int i = 0; i < 8; ++i)
            {
                string str = RemoveBrackets(iniReader[groupName + i]);
                if (str == "")
                    continue;
                string[] rawChords = str.Split(',');
                if (rawChords.Length != 12)
                    ThrowFormatException(groupName + i);
                for (int j = 0; j < 12; ++j)
                {
                    try
                    {
                        if(rawChords[j]!="")
                            chords[i,j] = Chord.GetChordByAbsoluteChordName(rawChords[j]);
                    }
                    catch
                    {
                        ThrowFormatException(groupName + i + "(" + Chord.Num2Char[j] + ")");
                    }
                }
            }
        }
        public ChordShortcuts()
        {
            INIReader iniReader = new INIReader("Shortcuts.ini");
            shortcutKeys = new Keys[15]{ Keys.D1, Keys.F1, Keys.D2, Keys.F2, Keys.D3, Keys.D4, Keys.F4, Keys.D5, Keys.F5, Keys.D6, Keys.F6, Keys.D7, Keys.N, Keys.X, Keys.OemQuestion };
            absoluteChords = new Chord[8, 12];
            relativeChords = new Chord[8, 12];
            InitChordGroup(iniReader, "Absolute", absoluteChords);
            InitChordGroup(iniReader, "Relative", relativeChords);
        }
        public int GetKeyID(Keys keyCode)
        {
            for (int id = 0; id < 15; ++id)
            {
                if (keyCode == shortcutKeys[id])
                {
                    return id;
                }
            }
            return -1;
        }
        public Chord GetChordInput(int id, bool control,bool alt,bool shift,Tonality currentTonality,bool isRelativeLabel)
        {
            if (id == -1)
                return null;
            if(id>=12)
            {
                switch ((Chord.MutedChordTypeEnum)(id - 12))
                {
                    case Chord.MutedChordTypeEnum.NMark:
                        return Chord.NoChord;
                    case Chord.MutedChordTypeEnum.QMark:
                        return Chord.UnknownChord;
                    case Chord.MutedChordTypeEnum.XMark:
                        return Chord.UnrepresentableChord;
                    default:
                        throw new ArgumentException("和弦编号下标越界");
                }
            }
            else
            {
                int row = 0;
                if (shift) row |= 1;
                if (control) row |= 2;
                if (alt) row |= 4;
                Chord chord = (isRelativeLabel ? relativeChords : absoluteChords)[row, id];
                if (chord!=null&&isRelativeLabel && currentTonality.Root != -1)
                    return chord.ShiftPitch(currentTonality.Root);
                else
                    return chord;
            }
        }
    }
}
