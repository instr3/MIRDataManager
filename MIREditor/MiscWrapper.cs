using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;

namespace MIREditor
{
    class MiscWrapper
    {
        public static double GetMP3Length(int stream)
        {
            long len_in_byte = Bass.BASS_ChannelGetLength(stream, BASSMode.BASS_POS_BYTES);
            double time = Bass.BASS_ChannelBytes2Seconds(stream, len_in_byte);
            return time;
        }
        public static double GetMP3Length(string fileName)
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            int stream = Bass.BASS_StreamCreateFile(fileName, 0, 0, BASSFlag.BASS_STREAM_DECODE);
            double time = GetMP3Length(stream);
            Bass.BASS_StreamFree(stream);
            return time;
        }
        public static string GetFileMD5(string filePath)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
                }
            }
        }
    }
}
