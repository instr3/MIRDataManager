using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIREditor;
using System.IO;
using System.Text.RegularExpressions;
using Common;

namespace OsuMapAnalyzer
{
    public class OsuMapAnalyzer
    {
        public static OsuMap Analyze(SongInfo songInfo, string difficultyFilename="")
        {
            OsuMap osuMap = new OsuMap();
            osuMap.SongInfo = songInfo;
            string osuFilename = Path.Combine(Settings.DatasetMusicFolder, songInfo.MiscConfigure.LinkedFile);
            if (difficultyFilename != "")
                osuFilename = Path.Combine(Path.GetDirectoryName(osuFilename), difficultyFilename);
            osuMap.TimingPoints = OsuAnalyzer.GetTimingPoints(osuFilename);
            int processedTimingPoints = 0;
            double currentBeatLength = osuMap.TimingPoints[0].BeatLength;
            double currentSliderSpeedPercent = 1;
            osuMap.HitObjects = new List<HitObject>();
            using (StreamReader sr = new StreamReader(osuFilename))
            {
                bool hitObjContext = false;
                while (!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine();
                    if (buffer.Contains("[HitObjects]"))
                    {
                        hitObjContext = true;
                    }
                    else if (buffer.Length >= 1 && buffer[0] == '[')
                    {
                        hitObjContext = false;
                    }
                    else if (hitObjContext)
                    {
                        string[] split = buffer.Split(',');
                        if (split.Length < 5)
                        {
                            throw new FormatException("Unknown format :" + buffer);
                        }
                        HitObject hitObject = new HitObject();
                        hitObject.PositionX = int.Parse(split[0]);
                        hitObject.PositionY = int.Parse(split[1]);
                        hitObject.StartTimeMS = int.Parse(split[2]);
                        int typeInfo = int.Parse(split[3]);
                        hitObject.Type = (HitObjectType)(typeInfo & 0xF);
                        hitObject.ComboOffset = typeInfo >> 4;
                        hitObject.SoundType = int.Parse(split[4]);
                        if (hitObject.IsType(HitObjectType.Slider))
                        {
                            hitObject.SegmentCount = int.Parse(split[6]);
                            hitObject.SpatialLength = double.Parse(split[7]);
                            while (processedTimingPoints < osuMap.TimingPoints.Count - 1 && osuMap.TimingPoints[processedTimingPoints + 1].Offset <= hitObject.StartTimeMS + 1e-6)
                            {
                                TimingPoint tp = osuMap.TimingPoints[++processedTimingPoints];
                                if (tp.BeatLength < 0)
                                {
                                    currentSliderSpeedPercent = -tp.BeatLength / 100.0;
                                }
                                else
                                {
                                    currentBeatLength = tp.BeatLength;
                                }
                            }
                            hitObject.EndTime = (int)Math.Floor(hitObject.SpatialLength * currentBeatLength *
                                hitObject.SegmentCount * 0.01 / (osuMap.SliderMultiplier / currentSliderSpeedPercent) + hitObject.StartTimeMS);
                        }
                        else if (hitObject.IsType(HitObjectType.Spinner))
                        {
                            hitObject.EndTime = int.Parse(split[5]);
                        }
                        osuMap.HitObjects.Add(hitObject);
                    }
                    else
                    {
                        string DivisorRegex;
                        Match match;
                        DivisorRegex = @"^SliderMultiplier:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            osuMap.SliderMultiplier = double.Parse(match.Groups["n"].Value);
                            continue;
                        }
                        DivisorRegex = @"^Mode:\s*(?<n>[^$]+)\s*$";
                        match = Regex.Match(buffer, DivisorRegex);
                        if (match.Success)
                        {
                            osuMap.Mode = int.Parse(match.Groups["n"].Value);
                            if (osuMap.Mode != 0)
                                return osuMap;
                            continue;
                        }
                    }
                }
            }

            return osuMap;
        }
        public static OsuMap Analyze(string osuFilename)
        {
            return Analyze(OsuAnalyzer.ExtractFromOSUFile(osuFilename,Settings.DatasetMusicFolder));
            
        }
    }
}
