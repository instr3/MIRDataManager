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
        public static Color HslToRgb(double Hue, double Saturation, double Lightness, int alpha = 255)
        {
            if (Hue >= 240 || Hue < 0 || Saturation > 240 || Saturation < 0 || Lightness > 240 || Lightness < 0)
            {
                throw new Exception("Error HSL input");
            }
            Hue = Hue / 2 * 3;
            Saturation /= 240;
            Lightness /= 240;
            double p2;
            if (Lightness <= 0.5) p2 = Lightness * (1 + Saturation);
            else p2 = Lightness + Saturation - Lightness * Saturation;

            double p1 = 2 * Lightness - p2;
            double double_r, double_g, double_b;
            if (Saturation == 0)
            {
                double_r = Lightness;
                double_g = Lightness;
                double_b = Lightness;
            }
            else
            {
                double QqhToRgb(double q1, double q2, double hue)
                {
                    if (hue > 360) hue -= 360;
                    else if (hue < 0) hue += 360;

                    if (hue < 60) return q1 + (q2 - q1) * hue / 60;
                    if (hue < 180) return q2;
                    if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
                    return q1;
                }
                double_r = QqhToRgb(p1, p2, Hue + 120);
                double_g = QqhToRgb(p1, p2, Hue);
                double_b = QqhToRgb(p1, p2, Hue - 120);
            }

            // Convert RGB to the 0 to 255 range.
            int r = (int)(double_r * 255.0);
            int g = (int)(double_g * 255.0);
            int b = (int)(double_b * 255.0);
            return Color.FromArgb(alpha, r, g, b);
        }

        static string name;
        static Dictionary<string, Color> colorDict;
        static Dictionary<string, KeyValuePair<Color, Color>> gradientColorDict;
        static ColorSchema()
        {
            colorDict = new Dictionary<string, Color>();
            InitChordColors();
            gradientColorDict = new Dictionary<string, KeyValuePair<Color, Color>>();
            /*INIReader iniReader = new INIReader("ColorSchema.ini");
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
            }*/
        }
        private static void InitChordColors()
        {
            int CircleMean(int a,int b)
            {
                if (a - b > -120 && a - b <= 120)
                    return (a + b) / 2;
                else return ((a + b + 240) / 2) % 240;
            }
            int[] rootHue = new int[12] { 20, 10, 200, 170, 80, 0, 30, 40, 110, 140, 215, 50 };

            int templatesCount = Chord.GetChordTemplatesCount();
            Tonality tonality = Tonality.MajMinTonality(0, true);
            bool[] onTonality = new bool[12] { true, false, true, false, true, true, false, true, false, true, false, true };
            
            for (int t = 0; t < templatesCount; ++t)
            {
                for (int i = 0; i < 12; ++i)
                {
                    Chord chord = Chord.EnumerateChord(t, i);
                    string chordName = chord.ToString(tonality);
                    int hue = rootHue[i];
                    int saturation;
                    int lightness;
                    if (chordName.IndexOf("sus4") > 0 || chordName.IndexOf("/4") > 0)
                    {
                        hue = CircleMean(rootHue[i], rootHue[(i + 5) % 12]);
                        saturation = 200;
                        lightness = 150;
                    }
                    else if (chordName.IndexOf("sus2") > 0 || chordName.IndexOf("/2") > 0)
                    {
                        hue = CircleMean(rootHue[i], rootHue[(i + 7) % 12]);
                        saturation = 200;
                        lightness = 150;
                    }
                    else if (chordName.IndexOf("°") > 0 || chordName.IndexOf("ø") > 0 || chordName.IndexOf("+") > 0)
                    {
                        saturation = 120;
                        lightness = 170;
                    }
                    else if (chordName[0] == 'i' || chordName[0] == 'v')
                    {
                        saturation = 140;
                        lightness = 170;
                    }
                    else
                    {
                        saturation = 220;
                        lightness = 150;
                    }
                    int[] notes = chord.ToNotes();
                    foreach(int note in notes)
                    {
                        if(!onTonality[note])
                        {
                            saturation -= 40;
                            lightness -= 30;
                        }
                    }
                    if (notes.Length >= 4)
                    {
                        lightness = Math.Min(lightness, 140);
                    }
                    if (saturation < 40)
                        saturation = 40;
                    if (lightness < 120)
                        lightness = 120;
                    Color color = HslToRgb(hue, saturation, lightness);
                    colorDict.Add(chordName, color);
                }
            }
            colorDict["N"]= HslToRgb(160, 0, 200);
        }
        /*private static string RemoveInversion(string chordname)
        {
            int index = chordname.IndexOf('/');
            if (index >= 0)
                return chordname.Substring(0, index);
            else
                return chordname;
        }*/
        public static Color GetColorByChordName(string chordName)
        {
            Color result;
            if(!colorDict.TryGetValue(chordName,out result))
            {
                result = colorDict["N"];
            }
            return result;
        }
        public static KeyValuePair<Color,Color> GetGradientColorByChordName(string chordName)
        {
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
