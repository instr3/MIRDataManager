using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRDataManager
{
    class DataFile
    {
        public SongInfo SongInfo;
        public string FileName;
        public DataFile(string filePath)
        {
            FileName = Path.GetFileName(filePath);
            using (StreamReader sw = new StreamReader(filePath))
            {
                SongInfo = new SongInfo(sw.ReadToEnd(), true);
            }
        }
    }
}
