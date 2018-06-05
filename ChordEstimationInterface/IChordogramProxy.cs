using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordEstimationInterface
{
    public interface IChordogramProxy
    {
        Chordogram Extract(string filename);
    }
}
