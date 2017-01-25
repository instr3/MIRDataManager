using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class INIReader
    {
        public string Filename { get; private set; }
        public Dictionary<string, string> Data;
        public INIReader(string filename)
        {
            Filename = filename;
            Data = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(Filename))
            {
                string[] lines = sr.ReadToEnd().Split('\n').Select(s => s.Trim('\r', '\t', ' ')).Where(s => s != "").ToArray();
                foreach(string line in lines)
                {
                    if (TextProcessor.IsDelimiter(line)) continue;
                    Data[TextProcessor.GetKey(line)] = TextProcessor.GetStringValue(line);
                }
            }
        }
    }
}
