using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    class ChromaVisualizer
    {
        private Timeline TL;
        private BeatEditor BeatEditor;
        private SongInfo Info;
        private Bitmap[] chromaBitmaps;
        public int ChromaStart = 75;
        public int ChromaHeight = 10;
        public int ChromaTextHeight = 15;

        public bool Prepared;
        Font tonalityFont = FontManager.Instance.NoteFont;
        Brush tonalityBrush = Brushes.LightGreen;
        Font noteFont = FontManager.Instance.NoteFont;
        Brush noteFrontBrush = Brushes.White;
        Brush noteBackBrush = Brushes.Black;
        Pen chromaSelectPen = Pens.LightGray;
        int chromaTextStart = 71;
        int fontShadowDX = 1, fontShadowDY = 1;


        public enum TimelineChromaMode
        {
            FrameFull = 0,
            FrameScale=1,
            //Bar = 1,
            Global = 2,
            None = 3
        }
        public TimelineChromaMode ChromaMode;

        public ChromaVisualizer(Timeline tl)
        {
            TL = tl;
            Info = TL.Info;
            BeatEditor = TL.BeatEditor;
        }

        public Tonality GetCurrentTonality()
        {
            if (Info.Beats.Count < 2) return Tonality.NoTonality;
            int id = BeatEditor.GetPreviousBeatID(Program.TL.CurrentTime);
            if (id == -1) id = 0;
            return Info.Beats[id].Tonality;
        }

        public void ChangeChromaMode()
        {
            ChromaMode++;
            if (ChromaMode > TimelineChromaMode.None) ChromaMode = 0;
        }
        public void PrepareChromaFrameImage()
        {
            if (Info.Chroma == null) return;
            chromaBitmaps = new Bitmap[12];
            for (int j = 0; j < 12; ++j)
                chromaBitmaps[j] = new Bitmap(Info.Chroma.Frames.Length, 1);
            int pos = 0;
            foreach (Chroma.ChromaFrame frame in Info.Chroma.Frames)
            {
                for (int j = 0; j < 12; ++j)
                {
                    int percent = (int)((frame.D[j] / Info.Chroma.GlobalMax) * 255);
                    chromaBitmaps[j].SetPixel(pos, 0, Color.FromArgb(percent, percent, percent));
                }
                ++pos;
            }
            Prepared = true;
        }
        public void DrawTonality()
        {
            if (Info.Beats.Count < 2) return;
            double tempLeftMostTime = TL.LeftMostTime, tempRightMostTime = TL.RightMostTime;
            int left = BeatEditor.GetPreviousBeatID(tempLeftMostTime) - 1, right = BeatEditor.GetNextBeatID(tempRightMostTime);
            // Get the previous of previous beat of the left bound and the next beat of the right bound.
            if (left < 0) left = 0;
            // Tonality of the last beat is wrong and useless
            if (right >= Info.Beats.Count - 1) right = Info.Beats.Count - 2;
            Tonality lastTonality = null;
            int rightPos = TL.TargetRightPos;
            for (int i=right;i>=left;--i)
            {
                int pos = TL.Time2Pos(Info.Beats[i].Time);
                if (pos <= 0)
                {
                    lastTonality = Info.Beats[i].Tonality;
                    break;
                }
                if(i==0||Info.Beats[i-1].Tonality.ToString()!=Info.Beats[i].Tonality.ToString())
                {
                    DrawTonalityAt(Info.Beats[i].Tonality, pos, rightPos - pos);
                    rightPos = pos;
                }
            }
            if (lastTonality != null)
                DrawTonalityAt(lastTonality, 0, rightPos);
        }
        public void DrawTonalityAt(Tonality tonality,int graphicPosition,int restrictedLength)
        {
            TL.G.DrawString(tonality.ToString(), tonalityFont, tonalityBrush,
                new Rectangle(graphicPosition, 0, restrictedLength, ChromaTextHeight));
            if (tonality == null) return;
            for (int j = 0; j < 12; ++j)
            {
                if (ChromaMode != TimelineChromaMode.FrameScale || tonality.IsOnNaturalScale(j))
                {
                    TL.G.DrawString(tonality.NoteNameUnderTonality(j), noteFont, noteBackBrush,
                        new Rectangle(graphicPosition + fontShadowDX, chromaTextStart + (11 - j) * ChromaHeight + fontShadowDY, restrictedLength, ChromaTextHeight));
                    TL.G.DrawString(tonality.NoteNameUnderTonality(j), noteFont, noteFrontBrush,
                        new Rectangle(graphicPosition, chromaTextStart + (11 - j) * ChromaHeight, restrictedLength, ChromaTextHeight));
                }
            }
        }
        public void DrawChroma()
        {
            if (Info.Chroma == null) return;
            if (Info.Beats.Count < 2) return;
            int lpos = TL.Time2Pos(0);
            if (lpos < 0) lpos = 0;
            Tonality tonality = GetCurrentTonality();
            if (ChromaMode !=TimelineChromaMode.None)
            {
                if(Prepared)
                {
                    int pos1 = TL.Time2Pos(0);
                    int pos2 = TL.Time2Pos(TL.MP3Length);
                    for (int j = 0; j < 12; ++j)
                    {
                        if (ChromaMode!=TimelineChromaMode.FrameScale || tonality.IsOnNaturalScale(j))
                        {
                            if (ChromaMode == TimelineChromaMode.Global)
                            {
                                int percent = (int)((Info.Chroma.GlobalChroma[j] / Info.Chroma.MaxGlobalChroma) * 255);
                                TL.G.FillRectangle(new SolidBrush(Color.FromArgb(percent, percent, percent)), new Rectangle(0, ChromaStart + (11 - j) * ChromaHeight, TL.TargetRightPos, ChromaHeight));
                            }
                            else
                            {
                                //float scale = Info.Chroma.Frames.Length / (float)(pos2 - pos1); // MP3Length*TimeScale=pos2-pos1
                                //TL.G.DrawImage(chromaBitmaps[j], new Rectangle(0, ChromaStart + (11 - j) * ChromaHeight, TL.TargetRightPos, ChromaHeight),new Rectangle((int)(-pos1*scale), 0, (int)(TL.TargetRightPos * scale),1), GraphicsUnit.Pixel);
                                TL.G.DrawImage(chromaBitmaps[j], new Rectangle(pos1, ChromaStart + (11 - j) * ChromaHeight, pos2 - pos1, ChromaHeight));
                            }
                        }
                    }
                }

                /*foreach (Chroma.ChromaFrame frame in Info.chroma.Frames)
                {
                    double time = frame.Time;
                    int pos = TL.Time2Pos(time);
                    int len = (int)(Info.chroma.FrameLength * TL.TimeScale + 1);
                    if (pos + len >= 0 && pos <= TL.TargetRightPos)
                    {
                        for (int j = 0; j < 12; ++j)
                        {
                            int percent = (int)((frame.D[j] / Info.chroma.GlobalMax) * 255);
                            TL.G.FillRectangle(new SolidBrush(Color.FromArgb(percent, percent, percent)), new Rectangle(pos, ChromaStart + (11 - j) * ChromaHeight, len, ChromaHeight));
                        }
                    }
                }*/
            }
            if(TL.IsMouseInControl&&TL.CurrentMouseMode==Timeline.MouseMode.Chroma&& ChromaMode != TimelineChromaMode.None)
            {
                int id = GetChromaID(TL.MousePosY);
                if (ChromaMode != TimelineChromaMode.FrameScale || tonality.IsOnNaturalScale(id))
                {
                    int pos1 = Math.Max(0, TL.Time2Pos(0));
                    int pos2 = Math.Max(TL.TargetRightPos - 1, TL.Time2Pos(TL.MP3Length));
                    TL.G.DrawRectangle(chromaSelectPen, new Rectangle(pos1, ChromaStart + (11 - id) * ChromaHeight, pos2 - pos1, ChromaHeight));
                }
            }
        }
        private int GetChromaID(int y)
        {
            return 11 - (y - ChromaStart) / ChromaHeight;
        }
        public void ClickOnChromas(int x, int y)
        {
            Tonality tonality = GetCurrentTonality();
            if (ChromaMode != TimelineChromaMode.None)
            {
                int id = GetChromaID(y);
                if (ChromaMode != TimelineChromaMode.FrameScale || tonality.IsOnNaturalScale(id))
                    Program.MidiManager.PlaySingleNote(id);
            }
        }
    }
}
