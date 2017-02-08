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
        static ColorSchema()
        {
            colorDict = new Dictionary<string, Color>();
            INIReader iniReader = new INIReader("ColorSchema.ini");
            foreach(KeyValuePair<string,string> kv in iniReader.Data)
            {
                if(kv.Key=="Name")
                {
                    name = kv.Value;
                }
                else try
                {
                    colorDict.Add(kv.Key, ColorTranslator.FromHtml(kv.Value));
                }
                catch(Exception e)
                {
                    throw new Exception("ColorSchema.ini文件格式错误："+e.Message);
                }
            }
            if(!colorDict.ContainsKey("N"))
            {
                throw new Exception("ColorSchema.ini文件格式错误：缺少缺省项(N)");
            }
        }
        public static Color GetColorByChordName(string chordName)
        {
            Color result;
            if(!colorDict.TryGetValue(chordName,out result))
            {
                result = colorDict["N"];
            }
            return result;
        }
        public static Color GetTransparentColorByChordName(string chordName,int transparency=50)
        {
            Color result;
            if (!colorDict.TryGetValue(chordName, out result))
            {
                result = colorDict["N"];
            }
            return Color.FromArgb(transparency, result);
        }
    }
}
