using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIREditor;
namespace OsuMapAnalyzer
{
    public class OsuMap
    {
        public SongInfo SongInfo;
        public List<HitObject> HitObjects = new List<HitObject>();
        public List<TimingPoint> TimingPoints;
        public double SliderMultiplier;

        public int Mode;

        public string ToSonicVisualizer(bool recordEnd)
        {
            string res = "";
            foreach(HitObject h in HitObjects)
            {
                res += (h.StartTimeMS / 1000.0).ToString() + "\t";
                if (h.IsType(HitObjectType.Normal))
                {
                    res += "hit" + h.ComboOffset + "\n";
                }
                else
                {
                    res += (h.IsType(HitObjectType.Slider) ? "Slider" :
                    h.IsType(HitObjectType.Spinner) ? "Spinner" : "Others") + Environment.NewLine;
                    if (recordEnd)
                        res += (h.GetEndTimeMS() / 1000.0).ToString() + "\t" + "End" + Environment.NewLine;
                }
            }
            return res;
        }
    }
}
