using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Common;
using System.Runtime.Serialization.Formatters.Binary;

namespace MIREditor
{
    class ArchiveManager
    {
        public static SongInfo ReadFromArchive(string archiveFilePath)
        {
            SongInfo info;
            using (StreamReader sr = new StreamReader(archiveFilePath))
            {
                info = new SongInfo(sr.ReadToEnd());
            }
            if (!TryLoadVampData(archiveFilePath + ".vampcache", info))
            {
                Logger.Log("[Warning]Load fail: Cannot load vamp cache \"" + archiveFilePath + ".vampcache.");
                Logger.Log("[Info]Press L to create a new one.");
            }
            return info;
        }
        public static void SaveToArchive(string archiveFilePath, SongInfo info)
        {
            using (StreamWriter sw = new StreamWriter(archiveFilePath))
            {
                sw.Write(info.ToString());
            }
            if (!File.Exists(archiveFilePath + ".vampcache"))
            {
                if(info.RawChords!=null||info.Chroma!=null)
                    SaveVampData(archiveFilePath + ".vampcache", info);
            }
            Logger.Log("[Info]Saved successfully.");
        }


        public static void SwitchSongInfo(SongInfo info,string archiveFileName=null)
        {
            if (Program.TL != null)
            {
                Program.TL.Destroy();
            }
            Program.FullArchiveFilePath = archiveFileName;
            Program.TL = new Timeline(Program.Form.TimelinePictureBox, info);
            Program.TL.Init();
            Program.Form.RefreshInterface();
            Program.EditManager.Reset();
        }

        private static void SaveVampData(string vampDataFileName, SongInfo songInfo)
        {
            using (FileStream fs = new FileStream(vampDataFileName, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, songInfo.Chroma);
                bf.Serialize(fs, songInfo.RawChords);
            }
        }
        private static bool TryLoadVampData(string vampDataFileName,SongInfo songInfo)
        {
            try
            {
                using (FileStream fs = new FileStream(vampDataFileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    songInfo.Chroma = bf.Deserialize(fs) as Chroma;
                    songInfo.RawChords = bf.Deserialize(fs) as List<RawChord>;
                }
            }
            catch
            {
                if(File.Exists(vampDataFileName))
                {
                    File.Delete(vampDataFileName);
                }
                return false;
            }
            return true;
        }
    }
}
