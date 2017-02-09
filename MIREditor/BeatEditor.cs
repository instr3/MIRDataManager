using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIREditor
{
    class BeatEditor
    {
        private Timeline TL;
        private SongInfo Info;
        private bool enabled;
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
                    if (enabled)
                    {
                        //Enable 
                    }
                    else
                    {
                        //Disable
                    }
                    enabled = value;
                }
            }
        }
        private int pointBeatID;
        private bool ValidPointer
        {
            get { return pointBeatID >= 0 && pointBeatID < Info.Beats.Count; }
        }
        private int selectionLeftBeatID, selectionRightBeatID;
        private bool ValidSelection
        {
            get
            {
                return selectionLeftBeatID < selectionRightBeatID && selectionLeftBeatID >= 0 && selectionRightBeatID < Info.Beats.Count;
            }
        }
        Pen whitePen = new Pen(Color.White);
        Pen selectionPen = new Pen(Color.LightPink, 2);
        Brush transbrush = new SolidBrush(Color.FromArgb(50, Color.White));
        Pen pointerPen = new Pen(Color.Red, 2);
        public BeatEditor(Timeline tl)
        {
            TL = tl;
            Info = TL.Info;
        }
        public int GetPreviousBeatID(double time)
        {
            int left = -1, right = Info.Beats.Count, mid;
            while(right>left+1)
            {
                mid = (left + right) >> 1;
                if (time < Info.Beats[mid].Time) right = mid; else left = mid;
            }
            return left;
        }
        public int GetNextBeatID(double time)
        {
            int left = -1, right = Info.Beats.Count, mid;
            while (right > left + 1)
            {
                mid = (left + right) >> 1;
                if (time < Info.Beats[mid].Time) right = mid; else left = mid;
            }
            return right;
        }
        public int GetNearestBeatID(double time)
        {
            int left = -1, right = Info.Beats.Count, mid;
            while (right > left + 1)
            {
                mid = (left + right) >> 1;
                if (time < Info.Beats[mid].Time) right = mid; else left = mid;
            }
            if (right == Info.Beats.Count) return left;
            if (left == -1) return right;
            return Info.Beats[right].Time - time < time - Info.Beats[left].Time ? right : left;

        }
        public BeatInfo GetPreviousBeat(double time)
        {
            int id = GetPreviousBeatID(time);
            return id == -1 ? null : Info.Beats[id];
        }
        public BeatInfo GetNextBeat(double time)
        {
            int id = GetNextBeatID(time);
            return id == Info.Beats.Count ? null : Info.Beats[id];
        }
        public BeatInfo GetNearestBeat(double time)
        {
            int id = GetNearestBeatID(time);
            return id == -1 ? null : Info.Beats[id];
        }
        public void DrawBeatLine()
        {
            double tempLeftMostTime = TL.LeftMostTime, tempRightMostTime = TL.RightMostTime;
            int left = GetNextBeatID(tempLeftMostTime), right = GetPreviousBeatID(tempRightMostTime);
            //foreach (BeatInfo beat in Info.beats)
            for(int i=left;i<=right;++i)
            {
                BeatInfo beat = Info.Beats[i];
                if (beat.Time >= tempLeftMostTime && beat.Time <= tempRightMostTime)
                {
                    int pos = TL.Time2Pos(beat.Time);
                    TL.G.DrawLine(whitePen, new Point(pos, TL.HorizonHeight), new Point(pos, TL.HorizonHeight - (beat.BarAttribute == 1 ? 10 : beat.BarAttribute == 0 ? 7 : 0)));
                }
            }
        }
        public void EditModeUpdate()
        {
            if (!Enabled) return;
            pointBeatID = GetNearestBeatID(TL.CurrentTime);
            if(ValidPointer)
            {
                double time = Info.Beats[pointBeatID].Time;
                int pos = TL.Time2Pos(time);
                TL.G.DrawRectangle(pointerPen, new Rectangle(pos - 2, TL.HorizonHeight - 20, 4, 20));
            }
            if (ValidSelection)
            {
                double time1 = Info.Beats[selectionLeftBeatID].Time,
                time2 = Info.Beats[selectionRightBeatID].Time;
                int pos1 = TL.Time2Pos(time1),
                    pos2 = TL.Time2Pos(time2);
                Rectangle rect = new Rectangle(pos1, TL.HorizonHeight - 30, pos2 - pos1, 30);
                TL.G.FillRectangle(transbrush, rect);
                TL.G.DrawRectangle(selectionPen, rect);
            }

        }

        public void MoveSelectionStart(double time)
        {
            selectionLeftBeatID = GetNearestBeatID(time);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionRightBeatID = selectionLeftBeatID + 1;
        }
        public void MoveSelectionEnd(double time)
        {
            selectionRightBeatID = GetNearestBeatID(time);
            if (selectionRightBeatID <= selectionLeftBeatID)
                selectionLeftBeatID = selectionRightBeatID - 1;
        }
        public void ClearSelection()
        {
            selectionLeftBeatID = selectionRightBeatID = -1;
        }

        internal void DivideInterval()
        {
            if(ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "细分节拍");
                List<BeatInfo> newRange = new List<BeatInfo>((selectionRightBeatID - selectionLeftBeatID) * 2 + 1);
                for (int i=selectionLeftBeatID;i<selectionRightBeatID;++i)
                {
                    newRange.Add(Info.Beats[i]);
                    BeatInfo beat = Info.Beats[i].Clone() as BeatInfo;
                    beat.Time = (Info.Beats[i].Time + Info.Beats[i + 1].Time) / 2;
                    beat.BarAttribute = 0;
                    newRange.Add(beat);
                }
                newRange.Add(Info.Beats[selectionRightBeatID]);
                Info.Beats.RemoveRange(selectionLeftBeatID, selectionRightBeatID - selectionLeftBeatID + 1);
                Info.Beats.InsertRange(selectionLeftBeatID, newRange);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void MergeInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "泛化节拍");
                List<BeatInfo> newRange = new List<BeatInfo>((selectionRightBeatID - selectionLeftBeatID) * 2 + 1);
                bool use = true;
                for (int i = selectionLeftBeatID; i <= selectionRightBeatID; ++i)
                {
                    if(use)
                        newRange.Add(Info.Beats[i]);
                    use = !use;
                }
                Info.Beats.RemoveRange(selectionLeftBeatID, selectionRightBeatID - selectionLeftBeatID + 1);
                Info.Beats.InsertRange(selectionLeftBeatID, newRange);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void NormalizeInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "节拍规整化");
                int bar = 0;
                for (int i = selectionLeftBeatID; i <= selectionRightBeatID; ++i)
                {
                    Info.Beats[i].BarAttribute = bar == 0 ? 1 : 0;
                    if ((++bar) % Info.MusicConfigure.MetreNumber == 0) bar = 0;
                }
            }
        }
        internal void LeftRotateInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "循环左移");
                int firstStart = Info.Beats[selectionLeftBeatID].BarAttribute;
                for (int i = selectionLeftBeatID; i < selectionRightBeatID; ++i)
                {
                    Info.Beats[i].BarAttribute = Info.Beats[i + 1].BarAttribute;
                }
                Info.Beats[selectionRightBeatID].BarAttribute = firstStart;
            }
        }

        internal void RightRotateInterval()
        {
            if (ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "循环右移");
                int firstStart = Info.Beats[selectionRightBeatID].BarAttribute;
                for (int i = selectionRightBeatID; i > selectionLeftBeatID; --i)
                {
                    Info.Beats[i].BarAttribute = Info.Beats[i - 1].BarAttribute;
                }
                Info.Beats[selectionLeftBeatID].BarAttribute = firstStart;
            }
        }

        internal void DeleteSingleBeat()
        {
            if(ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "删除当前节拍");
                Info.Beats.RemoveAt(pointBeatID);
                selectionLeftBeatID = selectionRightBeatID = -1;
            }
        }

        internal void ModifyBarStartOfSingleBeat()
        {
            if (ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "修改起始属性");
                if (Info.Beats[pointBeatID].BarAttribute == 1)
                    Info.Beats[pointBeatID].BarAttribute = 0;
                else if (Info.Beats[pointBeatID].BarAttribute == 0)
                    Info.Beats[pointBeatID].BarAttribute = 1;
                else throw new Exception("Pointer illegal");
            }
        }

        internal void CreateSingleBeat()
        {
            if (ValidPointer)
            {
                Program.EditManager.BeforePreformEdit(Info, "新增节拍");
                throw new NotImplementedException();
            }
        }

        internal void SelectAllBeat()
        {
            selectionLeftBeatID = 0;
            selectionRightBeatID = Info.Beats.Count - 1;
        }

        internal void TonaltyModify(string newTonalty)
        {
            if (ValidSelection)
            {
                Tonalty tonalty = new Tonalty(newTonalty);
                Program.EditManager.BeforePreformEdit(Info, "修改调性为" + newTonalty);
                for (int i = selectionLeftBeatID; i < selectionRightBeatID; ++i)
                {
                    Info.Beats[i].Tonalty = tonalty;
                }
            }

        }

        internal void TonaltySwitchMajMin()
        {
            if(ValidSelection)
            {
                Program.EditManager.BeforePreformEdit(Info, "修改关系大小调");
                for (int i=selectionLeftBeatID;i<selectionRightBeatID;++i)
                {
                    Info.Beats[i].Tonalty = Tonalty.RelativeTonalty(Info.Beats[i].Tonalty);
                }
            }
        }
        private void SetSecondChord(BeatInfo beatInfo,Chord chord1,Chord chord2,double secondChordPrecent)
        {
            Program.EditManager.BeforePreformEdit(Info, "插入切分和弦" + chord1 + "-" + chord2 + "(" + (int)(secondChordPrecent * 100) + "%)");
            beatInfo.SetChord(chord1, chord2, secondChordPrecent);
        }
        public void SwitchCutSecondChord()
        {
            int beatID = GetPreviousBeatID(TL.CurrentTime);
            if (beatID < 0 || beatID == Info.Beats.Count - 1) return;
            BeatInfo beat = Info.Beats[beatID];
            Chord previousChord = beatID > 0 ? Info.Beats[beatID - 1].GetEndingChord() : Chord.NoChord;
            Chord nextChord = beatID < Info.Beats.Count - 2 ? Info.Beats[beatID + 1].Chord : Chord.NoChord;
            Chord currentChord1 = beat.Chord;
            Chord currentChord2 = beat.SecondChordPercent == 0 ? currentChord1 : beat.SecondChord;
            if(currentChord1==previousChord)
            {
                if(currentChord2==nextChord)// 1+2 to 0
                {
                    SetSecondChord(beat, currentChord1, null, 0);
                }
                else// 1 to 2
                {
                    SetSecondChord(beat, currentChord2, nextChord, 0.5);
                }
            }
            else if(currentChord2==nextChord)
            {
                if(currentChord1==currentChord2)//2+0 to 1
                {
                    SetSecondChord(beat, previousChord, currentChord2, 0.5);
                }
                else// 2 to 0
                {
                    SetSecondChord(beat, currentChord1, null, 0);
                }
                
            }
            else // 0 to 1 or others to 1
            {
                SetSecondChord(beat, previousChord, currentChord1, 0.5);
            }
            
        }

        public void RemoveLeft()
        {
            Program.EditManager.BeforePreformEdit(Info, "删除之前节奏");
            double currentTime = TL.CurrentTime;
            if (Info.Beats.Count(x => x.Time >= currentTime) < 2)
                Logger.Log("[Error] You cannot remove so many beats.");
            Info.Beats.RemoveAll(x => x.Time < currentTime);
        }

        public void RemoveRight()
        {
            Program.EditManager.BeforePreformEdit(Info, "删除之后节奏");
            double currentTime = TL.CurrentTime;
            if (Info.Beats.Count(x => x.Time <= currentTime) < 2)
                Logger.Log("[Error] You cannot remove so many beats.");
            Info.Beats.RemoveAll(x => x.Time > currentTime);
        }

        public void ExtendBeat()
        {
            if (Info.Beats.Count < 2) return;
            double currentTime = TL.CurrentTime;
            BeatInfo firstBeat = Info.Beats[0];
            BeatInfo lastBeat = Info.Beats[Info.Beats.Count - 1];
            BeatInfo addedBeat;
            List<BeatInfo> addedList = new List<BeatInfo>();
            if (firstBeat.Time> currentTime)
            {
                Program.EditManager.BeforePreformEdit(Info, "向前延拓节奏");
                double deltaTime = (Info.Beats[1].Time - firstBeat.Time);
                int i = 0;
                do
                {
                    addedBeat = firstBeat.Clone() as BeatInfo;
                    addedBeat.BarAttribute = 0;
                    addedBeat.Time = firstBeat.Time - deltaTime * (++i);
                    addedList.Add(addedBeat);
                } while (addedBeat.Time > currentTime);
                addedList.Reverse();
                Info.Beats.InsertRange(0, addedList);
            }
            else if(lastBeat.Time<currentTime)
            {
                Program.EditManager.BeforePreformEdit(Info, "向后延拓节奏");
                double deltaTime = (lastBeat.Time - Info.Beats[Info.Beats.Count - 2].Time);
                int i = 0;
                Info.Beats.RemoveAt(Info.Beats.Count - 1);
                do
                {
                    addedBeat = Info.Beats[Info.Beats.Count - 2].Clone() as BeatInfo;
                    addedBeat.BarAttribute = 0;
                    addedBeat.Time = lastBeat.Time + deltaTime * (i++);
                    addedList.Add(addedBeat);
                } while (addedBeat.Time < currentTime);
                Info.Beats.AddRange(addedList);
            }
        }
    }
}
