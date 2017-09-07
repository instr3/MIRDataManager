using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuMapAnalyzer
{
    public enum HitObjectType
    {
        Normal = 1,
        Slider = 2,
        NewCombo = 4,
        NormalNewCombo = 5,
        SliderNewCombo = 6,
        Spinner = 8,
        ColourHax = 112,
        Hold = 128,
        ManiaLong = 128
    };
    public class HitObject
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int StartTimeMS { get; set; }
        public HitObjectType Type { get; set; }
        public int ComboOffset { get; set; }
        public int SoundType { get; set; }
        public int EndTime { private get; set; }
        public int SegmentCount { get; set; }
        public double SpatialLength { get; set; }
        public bool IsType(HitObjectType type)
        {
            return (Type & type) > 0;
        }
        public int GetEndTimeMS()
        {
            if (IsType(HitObjectType.Normal))
                return StartTimeMS;
            else if (IsType(HitObjectType.Spinner) || IsType(HitObjectType.Slider))
                return EndTime;
            else
                return StartTimeMS;
        }
        public double GetStartTime()
        {
            return StartTimeMS / 1000.0;
        }
        public double GetEndTime()
        {
            return GetEndTimeMS() / 1000.0;
        }
    }
}
