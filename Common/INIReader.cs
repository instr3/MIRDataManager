using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class INIReader
    {
        private string filename;
        public Dictionary<string, string> Data
        {
            get { return data; }
        }
        private Dictionary<string, string> data;
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public string this[string key]
        {
            get
            {
                if (data.ContainsKey(key))
                    return data[key];
                else
                    throw new Exception(filename + "文件中应该含有键" + key + "，但未找到！");
            }
        }
        public INIReader(string filename)
        {
            this.filename = filename;
            data = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(this.filename))
            {
                List<string> lines = TextProcessor.LinesToList(sr.ReadToEnd());
                foreach(string line in lines)
                {
                    if (TextProcessor.IsDelimiter(line)) continue;
                    data[TextProcessor.GetKey(line)] = TextProcessor.GetStringValue(line);
                }
            }
        }
    }
}
