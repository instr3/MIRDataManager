using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FontManager
    {
        public Font ChordFont { get; private set; }
        public Font NoteFont { get; private set; }
        public Font DefaultFont { get; private set; }
        public string VisualizerScaleFontName { get; private set; }
        public string VisualizerSuffixFontName { get; private set; }
        public static FontManager Instance;
        static FontManager()
        {
            INIReader iniReader = new INIReader("Font.ini");
            Instance = new FontManager();
            Instance.ChordFont = new Font(iniReader["ChordFontFamily"], float.Parse(iniReader["ChordFontSize"]));
            Instance.NoteFont = new Font(iniReader["NoteFontFamily"], float.Parse(iniReader["NoteFontSize"]));
            Instance.VisualizerScaleFontName = iniReader["VisualizerScaleFontFamily"];
            Instance.VisualizerSuffixFontName = iniReader["VisualizerSuffixFontFamily"];
            // Instance.DefaultFont = new Font(iniReader.Data["DefaultFontFamily"], float.Parse(iniReader.Data["DefaultFontSize"]));
        }
    }
}
