using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRDataManager
{
    class Dataset
    {
        public List<DataFile> DataFiles;
        public Dataset(string path,bool configInfoOnly)
        {
            DataFiles = new List<DataFile>();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach(FileInfo file in dir.EnumerateFiles())
            {
                if(file.Extension==".arc")
                {
                    // Todo: Add Try-Catch
                    DataFile dataFile = new DataFile(file.FullName,configInfoOnly);
                    DataFiles.Add(dataFile);
                }
            }
        }

        internal List<DataFile> Where(string searchText, string scoreFilterText)
        {
            int scoreFilter;
            bool exceed;
            if(scoreFilterText.StartsWith("至少"))
            {
                scoreFilter = int.Parse(scoreFilterText.Replace("至少", ""));
                exceed = true;
            }
            else
            {
                scoreFilter = int.Parse(scoreFilterText);
                exceed = false;
            }
            searchText = searchText.Trim(' ').ToLower();
            return DataFiles.Where(
                s => (s.FileName.ToLower().Contains(searchText) || s.SongInfo.MusicConfigure.Title.ToLower().Contains(searchText)))
                .Where(s=>exceed?s.SongInfo.TagConfigure.Confidence>=scoreFilter:s.SongInfo.TagConfigure.Confidence==scoreFilter).ToList();
        }
        
    }
}
