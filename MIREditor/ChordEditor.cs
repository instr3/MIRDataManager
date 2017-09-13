using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIREditor
{
    class ChordEditor
    {
        // public readonly Keys[] InputKeys = new Keys[]{ Keys.D1, Keys.F1, Keys.D2, Keys.F2, Keys.D3, Keys.D4, Keys.F4, Keys.D5, Keys.F5, Keys.D6, Keys.F6, Keys.D7, Keys.D9, Keys.D0, Keys.OemMinus };
        public const double NEXT_CHORD_START_PERCENT = 0.5;
        public const double NEXT_CHORD_START_TIME = 0.3;
        public readonly bool[] DefaultRelativeMajMin = new bool[] { true, false, false, true, false, true, false, true, false, false, true,false };
        private Timeline TL;
        private SongInfo Info;
        private BeatEditor BeatEditor { get { return TL.BeatEditor; } }

        public const int CHORD_PART_HEIGHT = 30;
        public const int GRADIENT_START = 17;
        public const int GRADIENT_END = 7;

        public bool ShowRawChord = false;
        private bool enabled;
        public Font chordFont = FontManager.Instance.ChordFont;
        Brush transbrush = new SolidBrush(Color.FromArgb(50, Color.White));
        Brush rawChordFontBrush = Brushes.White;
        Pen pointPen = new Pen(Color.LightGreen, 1);
        Pen selectionPen = new Pen(Color.LightBlue, 2);
        Pen rawChordSeperatePen = Pens.White;
        private int pointLeftBeatID, pointRightBeatID;
        private int selectionLeftBeatID, selectionRightBeatID;
        public Chord CurrentChord;
        private bool ValidPointer
        {
            get
            {
                return pointLeftBeatID < pointRightBeatID && pointLeftBeatID >= 0 && pointRightBeatID < Info.Beats.Count;
            }
        }
        private bool ValidSelection
        {
            get
            {
                return selectionLeftBeatID < selectionRightBeatID && selectionLeftBeatID >= 0 && selectionRightBeatID < Info.Beats.Count;
            }
        }
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    if(enabled)
                    {
                        foreach(ChordClipboard cc in ChordClipboards)
                        {
                            cc.UpdateDraw();
                        }
                    }
                    else
                    {
                        //Disable
                    }
                    enabled = value;
                }
            }
        }


        public bool AutoPlayMidi { get; set; }
        public List<int> AlignBeats;
        public int AlignBeat = 4;
        public bool FixBoundOption = true;
        public bool ForceFrontAlign = false;

        public ChordClipboard[] ChordClipboards;

        public ChordEditor(Timeline tl)
        {
            TL = tl;
            Info = TL.Info;
            ChordClipboards = new ChordClipboard[4];
            ChordClipboards[0] = new ChordClipboard(Program.Form.pictureBox1, "1");
            ChordClipboards[1] = new ChordClipboard(Program.Form.pictureBox2, "2");
            ChordClipboards[2] = new ChordClipboard(Program.Form.pictureBox3, "3");
            ChordClipboards[3] = new ChordClipboard(Program.Form.pictureBox4, "4");
        }

        public void PerformInputChord(Chord chord)
        {
            if (ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "绘制 " + chord.ToString() + " 和弦");
                for (int i = pointLeftBeatID; i < pointRightBeatID; ++i)
                {
                    Info.Beats[i].SetChord(chord);
                }
            }

        }
        private int RightExpandBound(int beatID,int align)
        {
            if (align == 1) return beatID;
            //Need to pass passCount barStarts.
            int passCount = align / Info.MusicConfigure.MetreNumber - 1;
            int beatsPerBar = Info.MusicConfigure.MetreNumber;
            int counter = 0;
            while (beatID + counter <Info.Beats.Count)
            {
                if (Info.Beats[beatID + counter].BarAttribute>=1)
                {
                    if (--passCount < 0) break;
                }
                ++counter;
            }
            if (beatID + counter >= Info.Beats.Count)
                return Info.Beats.Count - 1;
            counter %= align;
            return beatID + counter;
        }
        private int LeftExpandBound(int beatID, int align)
        {
            if (align == 1) return beatID;
            //Need to pass passCount barStarts.
            int passCount = align / Info.MusicConfigure.MetreNumber - 1;
            int beatsPerBar = Info.MusicConfigure.MetreNumber;
            int counter = 0;
            while (beatID - counter>=0)//Can not pass leftmost beat
            {
                if(Info.Beats[beatID - counter].BarAttribute >= 1)
                {
                    if (--passCount < 0) break;
                }
                ++counter;
            }
            if (beatID - counter < 0)
                return 0;
            counter %= align;
            return beatID - counter;
        }

        internal void CopySelection(int v)
        {
            if(ValidSelection)
                ChordClipboards[v].Copy(Info, selectionLeftBeatID, selectionRightBeatID - 1);
        }

        internal void PasteSelection(int v)
        {
            if (ValidPointer)
                ChordClipboards[v].Paste(Info, pointLeftBeatID);
        }
        public void EditModeUpdate()
        {
            if (!Enabled) return;
            if(ShowRawChord)
                DrawRawChords();
            double curTime = TL.CurrentTime;
            pointLeftBeatID = BeatEditor.GetPreviousBeatID(curTime);
            pointRightBeatID = BeatEditor.GetNextBeatID(curTime);

            //Fix for bound detect
            if(FixBoundOption)
            {
                if (pointRightBeatID < Info.Beats.Count - 1&&pointLeftBeatID>=0)
                {
                    double time1 = Info.Beats[pointLeftBeatID].Time,
                        time2 = Info.Beats[pointRightBeatID].Time;
                    if ((time2 - curTime) / (time2 - time1) < NEXT_CHORD_START_PERCENT &&
                        time2 - curTime < NEXT_CHORD_START_TIME)
                    {
                        pointLeftBeatID++;
                        pointRightBeatID++;
                    }
                }
            }
            pointRightBeatID = RightExpandBound(pointRightBeatID, AlignBeat);
            if (ForceFrontAlign)
            {
                pointLeftBeatID = LeftExpandBound(pointLeftBeatID, AlignBeat);
            }
            if (ValidPointer)
            {
                double time1 = Info.Beats[pointLeftBeatID].Time,
                    time2 = Info.Beats[pointRightBeatID].Time;
                int pos1 = TL.Time2Pos(time1),
                    pos2 = TL.Time2Pos(time2);
                Rectangle rect = new Rectangle(pos1, TL.HorizonHeight - CHORD_PART_HEIGHT, pos2 - pos1, CHORD_PART_HEIGHT);
                TL.G.FillRectangle(transbrush, rect);
                TL.G.DrawRectangle(pointPen, rect);
            }
            if(ValidSelection)
            {
                double time1 = Info.Beats[selectionLeftBeatID].Time,
                    time2 = Info.Beats[selectionRightBeatID].Time;
                int pos1 = TL.Time2Pos(time1),
                    pos2 = TL.Time2Pos(time2);
                Rectangle rect = new Rectangle(pos1, TL.HorizonHeight - CHORD_PART_HEIGHT, pos2 - pos1, CHORD_PART_HEIGHT);
                //TL.G.FillRectangle(transbrush, rect);
                TL.G.DrawRectangle(selectionPen, rect);
            }
        }


        public void DrawChords()
        {
            if (Info.Beats.Count < 2) return;
            double tempLeftMostTime = TL.Pos2Time(-100), tempRightMostTime = TL.Pos2Time(TL.TargetRightPos + 100);
            int left = BeatEditor.GetPreviousBeatID(tempLeftMostTime)-1, right = BeatEditor.GetNextBeatID(tempRightMostTime);
            // Get the previous of previous beat of the left bound and the next beat of the right bound.
            if (left < 0) left = 0;
            if (right >= Info.Beats.Count) right = Info.Beats.Count - 1;
            Chord lastChord = null;
            Tonality lastTonality = null;
            ClearChordSwitchPoint();
            for (int i = left; i < right; ++i)
            {
                BeatInfo beat = Info.Beats[i];
                if (lastChord!=beat.Chord||lastTonality!=beat.Tonality)
                {
                    SetChordSwitchPoint(beat.Time, beat.Chord, beat.Tonality);
                    lastChord = beat.Chord;
                    lastTonality = beat.Tonality;
                }
                if (beat.SecondChordPercent>0)
                {
                    BeatInfo nextBeat = Info.Beats[i + 1];
                    double insertTime = nextBeat.Time - (nextBeat.Time - beat.Time) * beat.SecondChordPercent;
                    SetChordSwitchPoint(insertTime, beat.SecondChord, beat.Tonality);
                    lastChord = beat.SecondChord;
                    lastTonality = beat.Tonality;
                }
            }
            SetChordSwitchPoint(Info.Beats[right].Time, null, null);
            //Play chord parts
            if (AutoPlayMidi)
            {
                Chord newChord = tempPointingChord;
                if (CurrentChord?.ToString() != newChord?.ToString())
                {
                    CurrentChord = newChord;
                    if (TL.Playing&&CurrentChord!=null)
                    {
                        Program.MidiManager.PlayChordNotes(CurrentChord);
                    }
                }
            }
        }


        double tempLastSwitchTime;
        Chord tempLastSwitchChord = null;
        Tonality tempLastSwitchTonality = null;
        Chord tempPointingChord = null;
        private void ClearChordSwitchPoint()
        {
            tempLastSwitchChord = null;
            tempLastSwitchTonality = null;
            tempPointingChord = null;
        }
        private void SetChordSwitchPoint(double time,Chord chord,Tonality tonality)
        {
            if(tempLastSwitchChord!=null)
            {
                DrawChordBetween(tempLastSwitchTime, time, tempLastSwitchChord, tempLastSwitchTonality);
                if(tempLastSwitchTime<TL.CurrentTime&&time>=TL.CurrentTime)
                {
                    tempPointingChord = tempLastSwitchChord;
                }
            }
            tempLastSwitchTime = time;
            tempLastSwitchChord = chord;
            tempLastSwitchTonality = tonality;
        }
        private void DrawChordBetween(double leftTime, double rightTime,Chord chord,Tonality tonality)
        {
            int leftPos = TL.Time2Pos(leftTime);
            int rightPos = TL.Time2Pos(rightTime);
            if (leftPos > TL.TargetRightPos)
                return;
            if (rightPos > TL.TargetRightPos)
                rightPos = TL.TargetRightPos;
            string chordName = chord.ToString(tonality);
            KeyValuePair<Color, Color> solidColors = ColorSchema.GetGradientColorByChordName(chordName);
            KeyValuePair<Color, Color> transparentColors = ColorSchema.GetGradientTransparentColorByChordName(chordName);
            if (TL.RelativeLabel)
            {
                TL.G.DrawString(chordName, chordFont, new SolidBrush(solidColors.Key), leftPos, TL.HorizonHeight - CHORD_PART_HEIGHT);
            }
            else
            {
                TL.G.DrawString(chord.ToString(), chordFont, new SolidBrush(solidColors.Key), leftPos, TL.HorizonHeight - CHORD_PART_HEIGHT);
            }
            if (solidColors.Key == solidColors.Value)
            {
                TL.G.FillRectangle(new SolidBrush(transparentColors.Key),
                    new Rectangle(leftPos, TL.HorizonHeight - CHORD_PART_HEIGHT, rightPos - leftPos, CHORD_PART_HEIGHT));
                int[] notes = chord.ToNotes();
                foreach (int note in notes)
                {
                    Rectangle rect = new Rectangle(leftPos, TL.ChromaVisualizer.ChromaStart + (11 - note) * TL.ChromaVisualizer.ChromaHeight, rightPos - leftPos, TL.ChromaVisualizer.ChromaHeight);
                    TL.G.FillRectangle(new SolidBrush(transparentColors.Key), rect);
                    TL.G.DrawRectangle(new Pen(solidColors.Key), rect);
                }
            }
            else
            {
                TL.G.FillRectangle(new SolidBrush(transparentColors.Key),
                    new Rectangle(leftPos, TL.HorizonHeight - CHORD_PART_HEIGHT, rightPos - leftPos, CHORD_PART_HEIGHT - GRADIENT_START));
                TL.G.FillRectangle(new LinearGradientBrush(new Point(0, TL.HorizonHeight - GRADIENT_START), new Point(0, TL.HorizonHeight - GRADIENT_END), transparentColors.Key, transparentColors.Value),
                    new Rectangle(leftPos, TL.HorizonHeight - GRADIENT_START, rightPos - leftPos, GRADIENT_START - GRADIENT_END));
                TL.G.FillRectangle(new SolidBrush(transparentColors.Value),
                    new Rectangle(leftPos, TL.HorizonHeight - GRADIENT_END, rightPos - leftPos, GRADIENT_END));
                
                int[] notes = chord.ToNotes();
                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, TL.ChromaVisualizer.ChromaStart), new Point(0, TL.ChromaVisualizer.ChromaStart + TL.ChromaVisualizer.ChromaHeight),
                    solidColors.Key, solidColors.Value);
                LinearGradientBrush gradientTransBrush = new LinearGradientBrush(new Point(0, TL.ChromaVisualizer.ChromaStart), new Point(0, TL.ChromaVisualizer.ChromaStart + TL.ChromaVisualizer.ChromaHeight),
                    transparentColors.Key, transparentColors.Value);
                foreach (int note in notes)
                {
                    Rectangle rect = new Rectangle(leftPos, TL.ChromaVisualizer.ChromaStart + (11 - note) * TL.ChromaVisualizer.ChromaHeight, rightPos - leftPos, TL.ChromaVisualizer.ChromaHeight);
                    TL.G.FillRectangle(gradientTransBrush, rect);
                    TL.G.DrawRectangle(new Pen(gradientBrush), rect);
                }
            }
            
        }

        public void MoveSelectionStart(double time)
        {
            selectionLeftBeatID = LeftExpandBound(BeatEditor.GetNearestBeatID(time),AlignBeat);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionRightBeatID = RightExpandBound(selectionLeftBeatID+1,AlignBeat);
        }
        public void MoveSelectionEnd(double time)
        {
            selectionRightBeatID = RightExpandBound(BeatEditor.GetNearestBeatID(time), AlignBeat);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionLeftBeatID = LeftExpandBound(selectionRightBeatID-1,AlignBeat);
        }
        public void ClearSelection()
        {
            selectionLeftBeatID = selectionRightBeatID = -1;
        }
        public void DrawRawChords()
        {
            if (Info.RawChords == null || ShowRawChord == false) return;
            double tempLeftMostTime = TL.LeftMostTime, tempRightMostTime = TL.RightMostTime;
            foreach (RawChord rawChord in Info.RawChords)
            {
                if (rawChord.Time + rawChord.Length >= tempLeftMostTime && rawChord.Time <= tempRightMostTime)
                {
                    int pos1 = TL.Time2Pos(rawChord.Time);
                    int len = (int)(rawChord.Length * TL.TimeScale);
                    TL.G.DrawString(rawChord.Chord, chordFont, rawChordFontBrush, pos1, 0);
                    TL.G.FillRectangle(transbrush, pos1, 0, len, 10);
                    TL.G.DrawLine(rawChordSeperatePen, pos1, 0, pos1, 10);
                }
            }
        }
    }
}
