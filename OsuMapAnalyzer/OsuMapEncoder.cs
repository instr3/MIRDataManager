using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuMapAnalyzer
{
    class OsuMapEncoder
    {
        public static int NotesPerGroup = 8;
        public static bool DebugMode = false;
        public static string Encode(OsuMap osuMap,bool invalid=false)
        {
            //List<int[]> result = new List<int[]>();
            string result = "";
            int beatID = 0;
            int hitID = 0;
            int[] beatIndex = new int[osuMap.SongInfo.Beats.Count];
            for (beatID = osuMap.SongInfo.Beats.Count - 2; beatID >= 0; --beatID)
            {
                if (osuMap.SongInfo.Beats[beatID + 1].BarAttribute >= 1)
                    beatIndex[beatID] = osuMap.SongInfo.MusicConfigure.MetreNumber;
                else
                {
                    beatIndex[beatID] = (beatIndex[beatID + 1] + osuMap.SongInfo.MusicConfigure.MetreNumber-1)% osuMap.SongInfo.MusicConfigure.MetreNumber;
                }
            }
            for (beatID = 1; beatID < osuMap.SongInfo.Beats.Count; ++beatID)
            {
                if (osuMap.SongInfo.Beats[beatID].BarAttribute >= 1)
                    beatIndex[beatID] = 1;
                else
                    beatIndex[beatID] = beatIndex[beatID - 1]%osuMap.SongInfo.MusicConfigure.MetreNumber + 1;
            }
            for (beatID=0;beatID<osuMap.SongInfo.Beats.Count-1;++beatID)
            {
                double beatStartTime = osuMap.SongInfo.Beats[beatID].Time;
                double beatEndTime = osuMap.SongInfo.Beats[beatID+1].Time;
                int[] discretedArray = new int[NotesPerGroup];
                while (hitID<osuMap.HitObjects.Count)
                {
                    if (osuMap.HitObjects[hitID].GetStartTime() < beatEndTime)
                    {
                        ProcessHitObject(discretedArray, beatStartTime, beatEndTime, osuMap.HitObjects[hitID], NotesPerGroup);
                        if (osuMap.HitObjects[hitID].GetEndTime() < beatEndTime-(beatEndTime-beatStartTime)*(0.5/NotesPerGroup))
                            ++hitID;
                        else
                            break;
                    }
                    else break;
                }
                if(invalid)
                {
                    for (int i = 0; i < NotesPerGroup; ++i)
                        discretedArray[i] = -1;
                }
                result +=
                    string.Join("\t", beatStartTime, beatEndTime,
                    string.Join(",", beatIndex[beatID], osuMap.SongInfo.MusicConfigure.MetreNumber,
                    string.Join(",", discretedArray)));
                result += '\n';
                //result.Add(discretedArray);
            }
            return result;
        }
        static void ProcessHitObject(int[] array,double beatStart,double beatEnd,HitObject hitObject,int notesPerGroup)
        {
            bool startAccurate, endAccurate;
            int startDiscreted = GetNearestDiscretedPoint(beatStart, beatEnd, hitObject.GetStartTime(), notesPerGroup,out startAccurate);
            int endDiscreted = GetNearestDiscretedPoint(beatStart, beatEnd, hitObject.GetEndTime(), notesPerGroup,out endAccurate);
            bool overwrite = false;
            if (!startAccurate||!endAccurate)
            {
                overwrite = true;
            }
            if (hitObject.IsType(HitObjectType.Slider))
            {
                int segmentDiscreted;
                if ((endDiscreted - startDiscreted) % hitObject.SegmentCount != 0)
                {
                    if(DebugMode)
                        Console.WriteLine("Bad Slider(" + hitObject.SegmentCount + " Segments Of " + (endDiscreted - startDiscreted) + " Discreted Points) @ Time " + hitObject.GetStartTime());
                    segmentDiscreted = endDiscreted - startDiscreted;
                    overwrite = true;
                    //throw new Exception("Bad Slider!");
                }
                else segmentDiscreted = (endDiscreted - startDiscreted) / hitObject.SegmentCount;
                for (int i = startDiscreted; i < endDiscreted; ++i)
                {
                    SetIfInRange(array, i, overwrite?-1:2);
                }
                SetIfInRange(array, startDiscreted, overwrite ? -1 : 5);
                for (int i = 1; i < hitObject.SegmentCount; ++i)
                {
                    SetIfInRange(array, startDiscreted + segmentDiscreted * i, overwrite?-1:3);
                }
            }
            else if (hitObject.IsType(HitObjectType.Normal))
            {
                SetIfInRange(array, startDiscreted, overwrite?-1:1);
            }
            else if (hitObject.IsType(HitObjectType.Spinner))
            {
                for (int i = startDiscreted; i < endDiscreted; ++i)
                {
                    SetIfInRange(array, i, 4);
                }
            }
            else throw new NotSupportedException();
        }
        static void SetIfInRange(int[] array,int position,int value)
        {
            if (position >= 0 && position < array.Length)
                array[position] = value;
        }
        static int GetNearestDiscretedPoint(double beatStart, double beatEnd, double hitTime, int notesPerGroup,out bool accurate)
        {
            double exactValue = (hitTime - beatStart) * notesPerGroup / (beatEnd - beatStart);
            int roundValue = (int)Math.Round(exactValue);
            /*if (roundValue < 0)
                return -1;
            if (roundValue > notesPerGroup)
                return notesPerGroup + 1;*/
            if (Math.Abs(roundValue-exactValue)>0.1)
            {
                if (DebugMode)
                    Console.WriteLine("Not accurate data (" + ((hitTime - beatStart) / (beatEnd - beatStart)) + ") @ " + hitTime);
                accurate = false;
            }
            else
            {
                accurate = true;
            }
            return roundValue;
        }
        public static double GetGoodness(OsuMap osuMap)
        {
            string encodedMap = Encode(osuMap);
            if (osuMap.Mode != 0)
                return -1.0;
            string[] list = encodedMap.Split('\n').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            int rowCount = list.Length;
            int goodCount = 0;
            double allBPM = 0;

            HashSet<string> set = new HashSet<string>();
            int allObjects = osuMap.HitObjects.Count;
            foreach (string item in list)
            {
                if (!item.Contains('-'))
                    goodCount++;
                string[] split = item.Split('\t');
                set.Add(split[2]);
                allBPM += 60.0 / (double.Parse(split[1]) - double.Parse(split[0]));
            }
            double goodRate = goodCount / (double)rowCount;
            double avgBPM = allBPM / rowCount;
            double avgObj = allObjects / (double)rowCount;
            if (avgObj > 1.5 || avgObj < 0.75)
                return -1.0;
            return avgObj;
        }
    }
}
