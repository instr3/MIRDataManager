using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using MIREditor;
using Un4seen.Bass;
using System.Drawing.Drawing2D;

namespace Visualizer
{
    class SubtitleVisualizer
    {

        public Graphics Target { get; private set; }
        public SongInfo Info { get; private set; }
        public int MP3stream { get; private set; }
        public double MP3Length { get; private set; }
        public Graphics G { get; private set; }
        public int BeatsPerSegment;
        public const int GRAPHIC_WIDTH = 1024;
        public const int GRAPHIC_HEIGHT = 768;
        public const int SUBTITLE_WIDTH = 768;
        public const double RELATIVE_NOTES_SPEED_4 = 0.5;
        public const double RELATIVE_NOTES_SPEED_3 = 1.0;
        public double RELATIVE_NOTES_SPEED;
        public long CurrentPosition
        {
            get
            {
                return Bass.BASS_ChannelGetPosition(MP3stream);
            }
            set
            {
                if (value < 0) value = 0;
                Bass.BASS_ChannelSetPosition(MP3stream, value);
            }
        }
        public double CurrentTime
        {
            get
            {
                return Bass.BASS_ChannelBytes2Seconds(MP3stream, CurrentPosition);
            }
            set
            {
                if (value < 0) value = 0;
                CurrentPosition = Bass.BASS_ChannelSeconds2Bytes(MP3stream, value);
            }
        }

        private PictureBox pictureBox;
        private BufferedGraphics myBuffer;

        public SubtitleVisualizer(PictureBox bindingPictureBox, SongInfo info)
        {
            // 1. Preparing graph buffers
            pictureBox = bindingPictureBox;
            bindingPictureBox.BackColor = Color.Black;
            Target = bindingPictureBox.CreateGraphics();
            myBuffer = BufferedGraphicsManager.Current.Allocate(Target, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
            G = myBuffer.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // 2. Preparing players
            string mp3Path = Settings.DatasetMusicFolder + "\\" + info.MusicConfigure.Location;
            if (File.Exists(mp3Path))
            {
                string md5 = MiscWrapper.GetFileMD5(mp3Path);
                if (md5 != info.MusicConfigure.MD5)
                {
                    MessageBox.Show("音频文件MD5发生改变！");
                }
                info.MusicConfigure.MD5 = md5;
            }
            else
            {
                throw new Exception("音频文件失踪");
            }
            MP3stream = Bass.BASS_StreamCreateFile(mp3Path, 0, 0, BASSFlag.BASS_SAMPLE_SOFTWARE | BASSFlag.BASS_STREAM_PRESCAN);
            MP3Length = MiscWrapper.GetMP3Length(MP3stream);
            //Bass.BASS_ChannelPlay(MP3stream, true);

            // 3. Preparing song annotations
            Info = info;
            PreprocessSongInfo();
        }
        public void Play()
        {
            Bass.BASS_ChannelPlay(MP3stream, true);
        }
        class SegmentInfoStruct
        {
            public readonly int StartBeat;
            public readonly int EndBeat;
            public SegmentInfoStruct(int start,int end)
            {
                StartBeat = start;
                EndBeat = end;
            }
        }
        List<SegmentInfoStruct> segments;
        void PreprocessSongInfo()
        {
            if (Info.MusicConfigure.MetreNumber % 3 == 0)
            {
                BeatsPerSegment = 12;
                RELATIVE_NOTES_SPEED = RELATIVE_NOTES_SPEED_3;
            }
            else
            {
                BeatsPerSegment = 8;
                RELATIVE_NOTES_SPEED = RELATIVE_NOTES_SPEED_4;
            }
            int beat_begin_id = 0;
            segments = new List<SegmentInfoStruct>();
            for (int i=0;i<Info.Beats.Count;++i)
            {
                BeatInfo beat = Info.Beats[i];
                if(beat.BarAttribute==2 || (i-beat_begin_id)>=BeatsPerSegment || i==Info.Beats.Count-1 || (i>0 && Info.Beats[i].Tonalty!=Info.Beats[i-1].Tonalty))
                {
                    segments.Add(new SegmentInfoStruct(beat_begin_id, i));
                    beat_begin_id = i;
                }
            }
        }

        protected Pen rectPen = new Pen(Color.White, 1.9f);
        protected Pen scalePen1 = new Pen(Color.FromArgb(170,Color.White), 1.9f);
        protected Pen scalePen2 = new Pen(Color.FromArgb(70, Color.White), 1.9f);
        protected Font scaleFont = new Font(FontManager.Instance.VisualizerScaleFontName, 24f);
        protected Font smallSuffixFont = new Font(FontManager.Instance.VisualizerSuffixFontName, 18f);
        protected Font scriptFont = new Font(FontManager.Instance.VisualizerSuffixFontName, 12f);
        protected Font suffixFont = new Font(FontManager.Instance.VisualizerSuffixFontName, 24f);

        protected int DrawSimpleText(string text,int font_id,int left, int top, int alpha=255)
        {
            Brush alphaWhiteBrush = new SolidBrush(Color.FromArgb(alpha, Color.White));
            int result;
            switch(font_id)
            {
                case 0:
                    G.DrawString(text, scaleFont, alphaWhiteBrush, new Point(left, top));
                    result=(int)(G.MeasureString(text, scaleFont).Width);
                    return result - 8;
                case 1:
                    G.DrawString(text, scriptFont, alphaWhiteBrush, new Point(left, top));
                    result=(int)(G.MeasureString(text, scriptFont).Width);
                    return result - 8;
                case 2:
                    result = 0;
                    int length = text.Length;
                    if (length>0 && (text[length-1] == '6' || text[length-1] == '7'))
                    {
                        string text1 = text.Substring(0, length - 1);
                        string text2 = text.Substring(length - 1);
                        if(text1!="")
                        {
                            G.DrawString(text1, suffixFont, alphaWhiteBrush, new Point(left, top));
                            result += (int)(G.MeasureString(text1, suffixFont).Width) - 8;
                        }
                        G.DrawString(text2, scriptFont, alphaWhiteBrush, new Point(left + result, top));
                        result += (int)(G.MeasureString(text2, suffixFont).Width) - 8;
                    }
                    else
                    {
                        G.DrawString(text, suffixFont, alphaWhiteBrush, new Point(left, top));
                        result = (int)(G.MeasureString(text, suffixFont).Width);
                    }
                    return result - 8;
                case 3:
                    result = 0;
                    if(text!="" && (text[0]=='#' ||text[0]=='b'))
                    {
                        G.DrawString(text.Substring(0,1), scriptFont, alphaWhiteBrush, new Point(left, top));
                        result += (int)(G.MeasureString(text, scriptFont).Width) - 16;
                        text = text.Substring(1);
                    }
                    G.DrawString(text, suffixFont, alphaWhiteBrush, new Point(left+result, top));
                    result += (int)(G.MeasureString(text, suffixFont).Width);
                    return result - 8;
                case 4:
                    G.DrawString(text, smallSuffixFont, alphaWhiteBrush, new Point(left, top));
                    result = (int)(G.MeasureString(text, smallSuffixFont).Width);
                    return result - 8;
                default:
                    throw new NotImplementedException();
            }
        }

        protected void DrawChordText(Chord chord, int left, int top, int alpha, Tonalty tonalty=null)
        {
            if (tonalty == null)
                tonalty = Tonalty.NoTonalty;
            if(chord.Scale!=-1)
            {
                string scaleText = tonalty.NoteNameUnderTonalty(chord.Scale);
                if (scaleText.Length == 2)
                {
                    if (scaleText[1] == 'b' || scaleText[1] == '#')
                    {
                        left += DrawSimpleText(scaleText.Substring(1, 1), 1, left, top, alpha);
                        left += DrawSimpleText(scaleText.Substring(0, 1), 0, left, top, alpha);
                    }
                    else
                    {
                        left += DrawSimpleText(scaleText.Substring(0, 1), 1, left, top, alpha);
                        left += DrawSimpleText(scaleText.Substring(1, 1), 0, left, top, alpha);
                    }
                }
                else
                {
                    left += DrawSimpleText(scaleText, 0, left, top, alpha);
                }
                string suffix = chord.ToAbosluteSuffix();
                left += DrawSimpleText(suffix, 2, left, top, alpha);
            }
        }

        protected static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        private double ClampedLerp(double from,double to,double t)
        {
            return t < 0 ? from : t > 1 ? to : from * (1 - t) + to * t;
        }
        protected void DrawChord(Chord chord, double left_percent, double width_percent, double row, double timespan, Tonalty tonalty = null, bool dark=false)
        {
            if (tonalty == null)
                throw new NotImplementedException();
            if (timespan < 0)
                return;
            int top = (int)Math.Round(GRAPHIC_HEIGHT - (row + 1) * 50);
            int left = (int)Math.Round((GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + left_percent * SUBTITLE_WIDTH);
            int width = (int)Math.Round(width_percent * SUBTITLE_WIDTH);
            double keep_timespan = width_percent * BeatsPerSegment;
            int[] scales = chord.ToRelativeScales(tonalty.Root);
            int chord_alpha = 0;
            int notes_alpha = 0;
            if(keep_timespan+1e-6<(scales.Length)* RELATIVE_NOTES_SPEED) // To short to show notes data
            {
                chord_alpha = (int)ClampedLerp(0, 255, (timespan) / RELATIVE_NOTES_SPEED);
                notes_alpha = 0;
            }
            else if (timespan > (scales.Length) * RELATIVE_NOTES_SPEED) // Notes vanished
            {
                chord_alpha = (int)ClampedLerp(0, 255, (timespan - (scales.Length) * RELATIVE_NOTES_SPEED) / RELATIVE_NOTES_SPEED);
                notes_alpha = (int)ClampedLerp(255, 0, (timespan - (scales.Length) * RELATIVE_NOTES_SPEED) / RELATIVE_NOTES_SPEED);
                int scale_left = left + 5;
                int i = 0;
                if (notes_alpha > 0)
                {
                    while (i < scales.Length)
                    {
                        scale_left += 10 + DrawSimpleText(Chord.Num2NoteString[scales[i]], 3, scale_left, top + 5, notes_alpha);
                        i += 1;
                    }
                }
            }
            else
            {
                chord_alpha = 0;
                int scale_left = left + 5;
                int i = 0;
                while (i<scales.Length && timespan>0)
                {
                    notes_alpha= (int)ClampedLerp(0, 255, (timespan) / RELATIVE_NOTES_SPEED);
                    timespan -= RELATIVE_NOTES_SPEED;
                    scale_left += 10 + DrawSimpleText(Chord.Num2NoteString[scales[i]], 3, scale_left, top + 5, notes_alpha);
                    i += 1;
                }
            }
            using (GraphicsPath path = RoundedRect(new Rectangle(left, top, width, 40), 8))
            {
                G.DrawPath(rectPen, path);
                DrawChordText(chord, left + 5, top + 5, chord_alpha, tonalty);
            }
        }
        protected void DrawPivot(double percent,int row)
        {
            int top = GRAPHIC_HEIGHT - (row + 1) * 50;
            int left = (int)Math.Round((GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + percent * SUBTITLE_WIDTH);
            int right = (GRAPHIC_WIDTH + SUBTITLE_WIDTH) / 2;
            int outer = 2;
            G.FillRectangle(Brushes.Black, new Rectangle(left, top - outer, right - left + outer, 50 + outer));
            G.DrawLine(rectPen, new Point(left, top), new Point(left, top + 40));

        }
        protected void DrawScale(double percent, int row)
        {
            int top = GRAPHIC_HEIGHT - (row + 1) * 50;
            int bottom = GRAPHIC_HEIGHT - row * 50 - 10;
            int ymid = (top + bottom) / 2;
            int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2;
            int right = (GRAPHIC_WIDTH + SUBTITLE_WIDTH) / 2;
            int left_limit = (int)Math.Round((GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + percent * SUBTITLE_WIDTH);
            G.DrawLine(rectPen, new Point(left, bottom), new Point(right, bottom));
            G.DrawLine(rectPen, new Point(left, top), new Point(right, top));
            double segLength = (right - left) / (double)BeatsPerSegment;
            for (int i=0;i<=BeatsPerSegment;++i)
            {
                int height = i % Info.MusicConfigure.MetreNumber == 0 ? 6 : 2;
                int x = (int)Math.Round(segLength * i + left);
                G.DrawLine(x > left_limit?scalePen1:scalePen2, new Point(x, ymid - height / 2), new Point(x, ymid + height / 2));
            }

        }
        private void DrawFadingCover(int startRow, int endRow)
        {
            int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 - 5;
            int right = (GRAPHIC_WIDTH + SUBTITLE_WIDTH) / 2 + 5;
            int top = GRAPHIC_HEIGHT - (endRow + 1) * 50;
            int bottom = GRAPHIC_HEIGHT - startRow * 50;
            Brush gredientBrush = new LinearGradientBrush(new Point(left, top), new Point(left, bottom), Color.FromArgb(255, Color.Black), Color.FromArgb(0, Color.Black));
            G.FillRectangle(gredientBrush, new Rectangle(left, top, right - left, bottom - top));
            G.FillRectangle(Brushes.Black, new Rectangle(left, top - 55, right - left, 55));
        }
        public void DrawTest()
        {
            Random random = new Random();
            for(int t=-1;t<12;++t)
            {
                int left = 0;
                for (int i = 0; i < 11; ++i)
                {
                    for (int b = 0; b <= 1; ++b)
                    {
                        int ran = (random.Next() % 4 + 1);
                        if (ran == 3) ran = 1;
                        int width = ran * 80;
                        
                        DrawChord(Chord.SimpleTraid(i, b > 0), left, (t+1)*50, width, 100, Tonalty.MajMinTonalty(t, true));
                        left += width;
                    }
                }
            }
            myBuffer.Render(Target);
        }

        protected int currentSegID;
        private void SwitchToNextSegment()
        {
            currentSegID++;
        }
        private void DrawHistoricalSegments(double currentTime, Tonalty gredientTonalty)
        {
            double SHIFT_TIME = 0.25;
            int MAX_HISTORY_DISPLAY = 9;
            int HISTORY_FADING_START_ROW = 4;
            double startTime;
            if (currentSegID == 0)
                startTime = Info.Beats[segments[currentSegID].StartBeat].Time;
            else
                startTime = Info.Beats[segments[currentSegID-1].EndBeat].Time;
            double shiftValue = ClampedLerp(0,1.0,(currentTime - startTime) /SHIFT_TIME);
            //shiftValue = shiftValue * shiftValue * (2 - shiftValue) * (2 - shiftValue);
            shiftValue = (1 - Math.Cos(Math.PI * shiftValue)) / 2;
            int start_i = Math.Max(0, currentSegID - MAX_HISTORY_DISPLAY - 1);
            Tonalty tonalty = currentSegID < segments.Count ? Info.Beats[segments[currentSegID].StartBeat].Tonalty : Info.Beats[segments[segments.Count-1].StartBeat].Tonalty;
            double row = shiftValue;
            for (int i = currentSegID - 1; i >= start_i; --i)
            {
                if (row >= MAX_HISTORY_DISPLAY + 1)
                    break;
                Tonalty oldTonalty = Info.Beats[segments[i].StartBeat].Tonalty;
                if (oldTonalty.Root != tonalty.Root)
                {
                    double TONALTY_INDICATION_HEIGHT_IN_ROW = 0.5;
                    if(currentSegID - 1==i) // Shift need to be faster
                        row *= (1+ TONALTY_INDICATION_HEIGHT_IN_ROW);
                    else
                        row += TONALTY_INDICATION_HEIGHT_IN_ROW;
                    int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + 10;
                    int top= (int)Math.Round(GRAPHIC_HEIGHT - row * 50 - 10);
                    int alpha = (int)(255 * ClampedLerp(0, 1, row / (1 + TONALTY_INDICATION_HEIGHT_IN_ROW)));
                    DrawSimpleText("^ 1 = " + new string(Chord.Num2Char[oldTonalty.Root].Reverse().ToArray()), 4, left, top, alpha);
                    tonalty = oldTonalty;
                }
                if (row >= MAX_HISTORY_DISPLAY + 1)
                    break;
                // DrawSeg(currentTime, i, currentSegID - i - (1 - shiftValue), gredientTonalty);
                DrawSeg(currentTime, i, row, gredientTonalty);
                row += 1.0;
            }
            DrawFadingCover(HISTORY_FADING_START_ROW, MAX_HISTORY_DISPLAY);
        }
        private void DrawSeg(double currentTime, int segID, double row, Tonalty gredientTonalty)
        {
            double startTime = Info.Beats[segments[segID].StartBeat].Time;
            double endTime = Info.Beats[segments[segID].EndBeat].Time;
            double duration = (endTime - startTime) / (segments[segID].EndBeat - segments[segID].StartBeat) * BeatsPerSegment;
            double lastTime = 0.0;
            BeatInfo lastBeat = Info.Beats[segments[segID].StartBeat];
            int lastBeatID = segments[segID].StartBeat;
            for (int i = segments[segID].StartBeat + 1; i <= segments[segID].EndBeat; ++i)
            {
                BeatInfo beat = Info.Beats[i];
                if (i == segments[segID].EndBeat || beat.Chord != lastBeat.Chord || beat.Tonalty != lastBeat.Tonalty)
                {
                    double newTime = beat.Time - startTime;
                    double lastBeatDuration = Info.Beats[lastBeatID + 1].Time - Info.Beats[lastBeatID].Time;
                    bool gredientTonaltyEnabled = gredientTonalty != null && gredientTonalty != lastBeat.Tonalty;
                    DrawChord(lastBeat.Chord, lastTime / duration, (newTime - lastTime) / duration, row, (currentTime - startTime - lastTime) / lastBeatDuration, gredientTonaltyEnabled ? gredientTonalty : lastBeat.Tonalty, gredientTonaltyEnabled);
                    lastTime = newTime;
                    lastBeat = beat;
                    lastBeatID = i;
                }
            }
        }
        private int DrawTonaltyText(int root,int left, int top)
        {
            string scaleText = new string(Chord.Num2Char[root].Reverse().ToArray());
            left += DrawSimpleText(scaleText, 3, left, top);
            return left;
        }
        private Tonalty DrawTonalty(Tonalty tonalty, Tonalty oldTonalty, double timespan)
        {
            double TONALTY_TRANSITION_TIME = 1.0;
            int TONALTY_TOP = 100;
            int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + 50;
            left += DrawSimpleText("1 = ", 3, left, TONALTY_TOP) + 8;
            if (oldTonalty!=null && tonalty!=oldTonalty && timespan>=0 && timespan<TONALTY_TRANSITION_TIME)
            {
                int scale_old = oldTonalty.Root;
                int scale_new = tonalty.Root;
                int delta_scale = (scale_new - scale_old + 12) % 12;
                if (delta_scale < 6)
                    delta_scale += 12;
                double progress = Math.Sin(Math.PI/2*timespan / TONALTY_TRANSITION_TIME) * delta_scale;
                int progress_lower = (int)Math.Floor(progress);
                DrawTonaltyText((oldTonalty.Root + progress_lower) % 12, left, (int)(TONALTY_TOP + 50 * (progress - progress_lower)));
                DrawTonaltyText((oldTonalty.Root + progress_lower + 1) % 12, left, (int)(TONALTY_TOP + 50 * (progress - progress_lower - 1)));
                return Tonalty.MajMinTonalty((oldTonalty.Root + (int)Math.Round(progress)) % 12, tonalty.MajMin);
            }
            else
            {
                DrawTonaltyText(tonalty.Root, left, TONALTY_TOP);
                return tonalty;
            }
            /*if (scaleText.Length == 2)
            {
                left += DrawSimpleText(scaleText.Substring(1, 1), 1, left, TONALTY_TOP);
                left += DrawSimpleText(scaleText.Substring(0, 1), 2, left, TONALTY_TOP);
            }
            else
            {
                left += DrawSimpleText(scaleText, 0, left, TONALTY_TOP);
            }*/

        }
        public void DrawFrame()
        {
            double currentTime = CurrentTime + 0.1;
            while (currentSegID < segments.Count && currentTime > Info.Beats[segments[currentSegID].EndBeat].Time)
            {
                SwitchToNextSegment();
            }
            G.Clear(Color.Black);
            Tonalty gredientTonalty = null;
            if (currentSegID < segments.Count)
            {
                double startTime = Info.Beats[segments[currentSegID].StartBeat].Time;
                double endTime = Info.Beats[segments[currentSegID].EndBeat].Time;
                double duration = (endTime - startTime) / (segments[currentSegID].EndBeat - segments[currentSegID].StartBeat) * BeatsPerSegment;
                double percent = (currentTime - startTime) / duration;
                if (currentSegID == 0)
                    gredientTonalty=DrawTonalty(Info.Beats[segments[currentSegID].StartBeat].Tonalty, null, 0);
                else
                    gredientTonalty=DrawTonalty(Info.Beats[segments[currentSegID].StartBeat].Tonalty, Info.Beats[segments[currentSegID - 1].StartBeat].Tonalty, currentTime - startTime);
                DrawSeg(currentTime, currentSegID, 0, null);
                if (percent>=0)
                    DrawPivot(percent, 0);
                DrawScale(percent, 0);
            }
            DrawHistoricalSegments(currentTime, null);
            myBuffer.Render(Target);
        }
    }
}
