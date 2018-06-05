using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordEstimationInterface
{
    public class PythonChordogramProxy : IChordogramProxy
    {
        string libraryName;
        public PythonChordogramProxy(string inputLibraryName)
        {
            libraryName = inputLibraryName;
        }

        public Chordogram Extract(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
