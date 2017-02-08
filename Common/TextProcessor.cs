using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TextProcessor
    {
        public static bool IsDelimiter(string str)
        {
            return str.StartsWith("[") && str.EndsWith("]");
        }
        public static List<string> LinesToList(string str)
        {
            return str.Split('\n')
                .Select(s => s.Trim())
                .Where(s => s != "").ToList();
        }
        public static object GetClass(List<string> textlines,Type type)
        {
            if(IsDelimiter(textlines[0]))
                textlines.RemoveAt(0);
            object obj = type.GetConstructor(new Type[0]).Invoke(new object[0]);
            for(int i=0;i<textlines.Count;++i)
            {
                string line = textlines[i];
                if(IsDelimiter(line))
                {
                    textlines.RemoveRange(0, i);
                    return obj;
                }
                int eq = line.IndexOf('=');
                if (eq == -1) throw new FormatException("Config File Corrupted");
                string left = line.Substring(0, eq).Trim();
                string right = line.Substring(eq + 1).Trim();
                PropertyInfo prop = type.GetProperty(left);
                if(prop==null)
                {
                    throw new ArgumentException("类" + type.Name + "不存在属性" + left);
                }
                prop.SetValue(obj, Convert.ChangeType(right, prop.PropertyType));
            }
            textlines.Clear();
            return obj;
        }
        public static int GetIntValue(string line)
        {
            int eq = line.IndexOf('=');
            if (eq == -1) throw new FormatException("Config File Corrupted");
            return int.Parse(line.Substring(eq + 1).Trim());
        }
        public static string GetStringValue(string line)
        {
            int eq = line.IndexOf('=');
            if (eq == -1) throw new FormatException("Config File Corrupted");
            return line.Substring(eq + 1).Trim();
        }
        public static string GetKey(string line)
        {
            int eq = line.IndexOf('=');
            if (eq == -1) throw new FormatException("Config File Corrupted");
            return line.Substring(0, eq).Trim();
        }
        public static string AddBlock(string name)
        {
            return string.Format("[{0}]" + Environment.NewLine, name);
        }
        public static string AddAttribute(string name, object value)
        {
            return string.Format("{0}={1}" + Environment.NewLine, name, value);
        }
        public static string AddClass(object obj,Type type)
        {
            PropertyInfo[] props = type.GetProperties();
            string res = "";
            res += AddBlock(type.Name);
            foreach (var p in props)
            {
                res += AddAttribute(p.Name, p.GetValue(obj));
            }
            return res;

        }
    }
}
