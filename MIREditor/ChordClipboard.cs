using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Forms;
using System.Drawing;

namespace MIREditor
{
    class ChordClipboard
    {
        List<BeatInfo> line;
        PictureBox pictureBox;
        Graphics g;
        string id;
        int maxWidth, maxHeight;
        public Font chordFont = SystemFonts.DefaultFont;
        public ChordClipboard(PictureBox renderPictureBox,string input_id)
        {
            pictureBox = renderPictureBox;
            g = pictureBox.CreateGraphics();
            maxWidth = pictureBox.Width;
            maxHeight = pictureBox.Height;
            id = input_id;
            Clear();
        }
        ~ChordClipboard()
        {
            Clear();
        }
        public void Clear()
        {
            line = null;
            UpdateDraw();
        }
        public void Copy(SongInfo info, int from,int to)
        {
            List<BeatInfo> beats = info.Beats;
            line = beats.GetRange(from, to - from + 1);
            UpdateDraw();
        }
        public bool Paste(SongInfo info,int from)
        {
            List<BeatInfo> beats = info.Beats;
            if (line == null||from>=beats.Count) return false;
            int count = line.Count;
            Program.EditManager.BeforePreformEdit(info, "粘贴剪切板" + id);
            for(int i=0;i<count;++i)
            {
                if (from + i >= beats.Count) break;
                beats[from + i].SetChord(line[i]);
            }
            return true;
        }
        public void UpdateDraw()
        {
            try
            {
                g.Clear(Color.White);
            }
            catch
            {

            }
            if (line == null) return;
            double deltaLength = maxWidth / (double)line.Count;
            int leftSameIndex = 0;
            for (int i = 0; i <=line.Count; ++i)
            {
                // Todo: second chord
                if (i == line.Count||line[i].Chord.ToString() != line[leftSameIndex].Chord.ToString())
                {
                    if (line[leftSameIndex].Chord != null)
                    {
                        string chordName = line[leftSameIndex].Chord.ToString(line[leftSameIndex].Tonality);
                        SolidBrush brush = new SolidBrush(ColorSchema.GetTransparentColorByChordName(chordName));
                        Color chordTextColor = ColorSchema.GetColorByChordName(chordName);

                        int leftPos = (int)(deltaLength * leftSameIndex);
                        int rightPos = (int)(deltaLength * i);
                        if (Program.TL.RelativeLabel)
                        {
                            g.FillRectangle(brush,
                                new Rectangle(leftPos, 0, rightPos - leftPos + 1, maxHeight));
                            g.DrawString(chordName, chordFont, new SolidBrush(chordTextColor), leftPos, 0);

                        }
                        else
                        {
                            g.FillRectangle(brush,
                                new Rectangle(leftPos, 0, rightPos - leftPos + 1, maxHeight));
                            g.DrawString(line[leftSameIndex].Chord.ToString(), chordFont, new SolidBrush(chordTextColor), leftPos, 0);
                        }
                    }
                    leftSameIndex = i;
                }
            }
        }
    }
}
