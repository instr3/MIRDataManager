using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ColorSchema
    {
        /*static Dictionary<string, string> RawColor_Dark = new Dictionary<string, string>
        {
            {"IV","#8B0000"},
            {"iv","#5E2B2B"},
            {"V","#8B8B00"},
            {"I","#8B5A00"},
            {"vi","#00008B"},
            {"ii","#8B1C62"},
            {"II","#3E0387"},
            {"iii","#006400"},
            {"III","#008B00"},
            {"N","#1C1C1C"}
        };*/

        static string name;
        static Dictionary<string, Color> colorDict;
        static Dictionary<string, KeyValuePair<Color, Color>> gradientColorDict;
        static ColorSchema()
        {
            colorDict = new Dictionary<string, Color>();
            gradientColorDict = new Dictionary<string, KeyValuePair<Color, Color>>();
            INIReader iniReader = new INIReader("ColorSchema.ini");
            string[] patternStringsBig = new string[12];
            string[] patternStringsSmall = new string[12];
            patternStringsBig[0] = "{I}";
            patternStringsSmall[0] = "{i}";
            for (int i = 1; i < 12; ++i)
            {
                patternStringsBig[i] = "{I+" + i + "}";
                patternStringsSmall[i] = "{i+" + i + "}";
            }
            foreach (KeyValuePair<string,string> kv in iniReader)
            {
                if (kv.Key == "Name")
                {
                    name = kv.Value;
                }
                else
                {
                    try
                    {
                        if (kv.Key.Contains("{"))
                        {
                            for (int i = 0; i < 12; ++i)
                            {
                                string patternRight = kv.Value;
                                for (int k = 0; k < 12; ++k)
                                {
                                    patternRight = patternRight
                                        .Replace(patternStringsBig[k], Chord.Num2RomeBig[(i + k) % 12])
                                        .Replace(patternStringsSmall[k], Chord.Num2RomeSmall[(i + k) % 12]);
                                }
                                gradientColorDict.Add(kv.Key
                                    .Replace(patternStringsBig[0], Chord.Num2RomeBig[i])
                                    .Replace(patternStringsSmall[0], Chord.Num2RomeSmall[i]),
                                    new KeyValuePair<Color, Color>(
                                        GetColorByChordName(patternRight.Split(new string[] { "|||" }, StringSplitOptions.None)[0]),
                                        GetColorByChordName(patternRight.Split(new string[] { "|||" }, StringSplitOptions.None)[1])));
                            }
                        }
                        else
                        {
                            colorDict.Add(kv.Key, ColorTranslator.FromHtml(kv.Value));
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("ColorSchema.ini文件格式错误：" + e.Message);
                    }
                }
            }
            if(!colorDict.ContainsKey("N"))
            {
                throw new Exception("ColorSchema.ini文件格式错误：缺少缺省项(N)");
            }
        }
        private static string RemoveInversion(string chordname)
        {
            int index = chordname.IndexOf('/');
            if (index >= 0)
                return chordname.Substring(0, index);
            else
                return chordname;
        }
        public static Color GetColorByChordName(string chordName)
        {
            chordName = RemoveInversion(chordName);
            Color result;
            if(!colorDict.TryGetValue(chordName,out result))
            {
                result = colorDict["N"];
            }
            return result;
        }
        public static KeyValuePair<Color,Color> GetGradientColorByChordName(string chordName)
        {
            chordName = RemoveInversion(chordName);
            KeyValuePair<Color,Color> result;
            if (!gradientColorDict.TryGetValue(chordName, out result))
            {
                Color single;
                if (!colorDict.TryGetValue(chordName, out single))
                {
                    single = colorDict["N"];
                }
                result = new KeyValuePair<Color, Color>(single, single);
            }
            return result;
        }
        public static Color GetTransparentColorByChordName(string chordName,int transparency=50)
        {
            return Color.FromArgb(transparency, GetColorByChordName(chordName));
        }
        public static KeyValuePair<Color, Color> GetGradientTransparentColorByChordName(string chordName, int transparency = 50)
        {
            KeyValuePair<Color, Color> result = GetGradientColorByChordName(chordName);
            return new KeyValuePair<Color, Color>(
                Color.FromArgb(transparency, result.Key),
                Color.FromArgb(transparency, result.Value));
        }
    }
}
