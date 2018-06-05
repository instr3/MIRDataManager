using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordEstimationInterface
{
    public class Chordogram
    {
        public double[,] LogProbability { get; private set; }
        public string[] ChordMap { get; private set; }
        public int NChord { get; private set; }
        public int NFrame { get; private set; }
        public double FrameDuration { get; private set; }
        public double FrameBias { get; private set; }

        double[,] logProbPrefixSum;
        public Chordogram(double[,] inputProbability,string[] inputChordMap,double frameDuration,double frameBias)
        {
            NChord = inputChordMap.Length;
            NFrame = inputProbability.GetLength(0);
            if (inputProbability.GetLength(1) != NChord)
                throw new FormatException("Input chord map and chordogram are inconsistent");
            logProbPrefixSum = new double[NFrame + 1, NChord];
            for (int i=0;i<NFrame;++i)
            {
                for(int c=0;c<NChord;++c)
                {
                    LogProbability[i, c] = Math.Log(inputProbability[i,c]);
                    logProbPrefixSum[i + 1, c] = logProbPrefixSum[i, c] + LogProbability[i, c];
                }
            }
            ChordMap = inputChordMap;
            FrameDuration = frameDuration;
            FrameBias = frameBias;
        }
        public string[] SmoothVitebi(double confidence, double selfTransProb, double lowProbabilityThreshold = 1e-20)
        {
            throw new NotImplementedException();
        }
        public KeyValuePair<string,double>[] IntervalQuery(double startTime,double endTime, int topLimit=-1)
        {
            int startFrame = Math.Max(0, (int)Math.Round((startTime - FrameBias) / FrameDuration));
            int endFrame = Math.Min(NFrame, (int)Math.Round((endTime - FrameBias) / FrameDuration));
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>(NChord);
            for (int c = 0; c < NChord; ++c)
            {
                result.Add(new KeyValuePair<string, double>(ChordMap[c], Math.Exp(logProbPrefixSum[endFrame, c] - logProbPrefixSum[startFrame, c])));
            }
            result.Sort((x, y) => -x.Value.CompareTo(y.Value));
            return result.ToArray();
        }
    }
}
