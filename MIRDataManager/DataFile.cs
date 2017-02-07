using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRDataManager
{
    public class DataFile
    {
        public SongInfo SongInfo;
        public string FileName;
        public string FilePath;
        public bool ConfigInfoOnly;
        public bool Downloading;
        public DataFile(string filePath, bool configInfoOnly)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            using (StreamReader sw = new StreamReader(filePath))
            {
                SongInfo = new SongInfo(sw.ReadToEnd(), configInfoOnly);
            }
            ConfigInfoOnly = configInfoOnly;
        }
        public void SaveToFile()
        {
            if(ConfigInfoOnly)
            {
                throw new NotSupportedException();
            }
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.Write(SongInfo.ToString());
            }
        }
    }
}
