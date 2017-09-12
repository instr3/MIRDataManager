using Common;
using MIREditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Un4seen.Bass;

namespace Visualizer
{
    class SubtitleVisualizer
    {   

        public Graphics Target { get; private set; }
        public SongInfo Info { get; private set; }
        public int MP3stream { get; private set; }
        public double MP3Length { get; private set; }
        public Graphics G { get; private set; }
        public Dictionary<string, string> MetaData { get; private set; }
        public int BeatsPerSegment;
        public const int GRAPHIC_WIDTH = 1024;
        public const int GRAPHIC_HEIGHT = 768;
        public const int ROW_BASE_HEIGHT = 745;
        public const int SUBTITLE_WIDTH = 768;
        public const double RELATIVE_NOTES_SPEED_4 = 0.5;
        public const double RELATIVE_NOTES_SPEED_3 = 1.0;
        public double RELATIVE_NOTES_SPEED;
        public Bitmap Image { get; private set; }
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
                // currentSegID = 0; // Prevent drawing errors
                CurrentPosition = Bass.BASS_ChannelSeconds2Bytes(MP3stream, value);
            }
        }
        private PictureBox pictureBox;
        private BufferedGraphics myBuffer;

        public SubtitleVisualizer(PictureBox bindingPictureBox, SongInfo info, bool renderToFile,Dictionary<string,string> metaData)
        {
            // 1. Preparing graph buffers
            if (renderToFile)
            {
                Image = new Bitmap(GRAPHIC_WIDTH, GRAPHIC_HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                G = Graphics.FromImage(Image);
            }
            else
            {
                pictureBox = bindingPictureBox;
                bindingPictureBox.BackColor = Color.Black;
                Target = bindingPictureBox.CreateGraphics();
                myBuffer = BufferedGraphicsManager.Current.Allocate(Target, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height));
                G = myBuffer.Graphics;
            }
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
            MetaData = metaData;
            Info = info;
            PreprocessSongInfo();
        }
        public void Play()
        {
            Bass.BASS_ChannelPlay(MP3stream, true);
        }
        public bool Ended()
        {
            return Bass.BASS_ChannelIsActive(MP3stream) == BASSActive.BASS_ACTIVE_STOPPED;
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
                BeatsPerSegment = int.Parse(MetaData["BeatsPerSegment"]);
                RELATIVE_NOTES_SPEED = double.Parse(MetaData["RelativeMovingSpeed"]);
            }
            int beat_begin_id = 0;
            segments = new List<SegmentInfoStruct>();
            for (int i=0;i<Info.Beats.Count;++i)
            {
                BeatInfo beat = Info.Beats[i];
                // Ignore leading non downbeats
                if (segments.Count==0 && beat_begin_id == i && beat.BarAttribute < 1)
                {
                    beat_begin_id++;
                    continue;
                }
                if (beat.BarAttribute==2 || (i-beat_begin_id)>=BeatsPerSegment || i==Info.Beats.Count-1 || (i>0 && Info.Beats[i].Tonality!=Info.Beats[i-1].Tonality))
                {
                    if(i == Info.Beats.Count - 1 && i - beat_begin_id<BeatsPerSegment) // Test if the last segment is unnecessary
                    {
                        bool valid = false;
                        for(int j=beat_begin_id;j<i;++j)
                        {
                            if(Info.Beats[j].Chord.Scale!=-1)
                            {
                                valid = true;
                                break;
                            }
                        }
                        if (!valid)
                            break;
                    }
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

        protected Font titleFont = new Font(FontManager.Instance.VisualizerSuffixFontName, 28f);
        protected Font metadataFont = new Font(FontManager.Instance.VisualizerSuffixFontName, 18f);
        private int[,] chordWidth;
        protected int DrawSimpleText(string text,int font_id,int left, int top, int alpha=255)
        {
            if (text == "")
                return 0;
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
                    G.DrawString(text, suffixFont, alphaWhiteBrush, new Point(left, top));
                    result = (int)(G.MeasureString(text, suffixFont).Width);
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
                    result = 0;
                    if (text != "" && (text[0] == '#' || text[0] == 'b'))
                    {
                        G.DrawString(text.Substring(0, 1), scriptFont, alphaWhiteBrush, new Point(left, top));
                        result += (int)(G.MeasureString(text, scriptFont).Width) - 14;
                        text = text.Substring(1);
                    }
                    G.DrawString(text, smallSuffixFont, alphaWhiteBrush, new Point(left + result, top));
                    result += (int)(G.MeasureString(text, smallSuffixFont).Width);
                    return result - 8;
                case 15:
                    G.DrawString(text, scriptFont, alphaWhiteBrush, new Point(left, top + 15));
                    result = (int)(G.MeasureString(text, scriptFont).Width);
                    return result - 8;
                default:
                    throw new NotImplementedException();
            }
        }

        protected int DrawChordText(Chord chord, int left, int top, int alpha, Tonality tonality=null)
        {
            if (tonality == null)
                tonality = Tonality.NoTonality;
            if(chord.Scale!=-1)
            {
                string scaleText = tonality.NoteNameUnderTonality(chord.Scale);
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
                Chord.ScriptAnnotationStruct suffixStruct = chord.ToScriptAnnotation();
                left += DrawSimpleText(suffixStruct.suffix, 2, left, top, alpha);
                int width1 = DrawSimpleText(suffixStruct.superscript, 1, left, top, alpha);
                int width2 = DrawSimpleText(suffixStruct.subscript, 15, left, top, alpha);
                left += Math.Max(width1, width2);
                if(suffixStruct.inversion!=-1)
                {
                    left += DrawSimpleText("/", 2, left, top, alpha) - 4;
                    scaleText = tonality.NoteNameUnderTonality(suffixStruct.inversion);
                    if (scaleText.Length == 2)
                    {
                        left += 4;
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
                }
            }
            return left;
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
        protected void DrawChord(Chord chord, double left_percent, double width_percent, double row, double timespan, Tonality tonality = null)
        {
            if (tonality == null)
                throw new NotImplementedException();
            if (timespan < 0)
                return;
            int top = (int)Math.Round(ROW_BASE_HEIGHT - (row + 1) * 50);
            int left = (int)Math.Round((GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + left_percent * SUBTITLE_WIDTH);
            int width = (int)Math.Round(width_percent * SUBTITLE_WIDTH);
            double keep_timespan = width_percent * BeatsPerSegment;
            int[] scales = chord.ToRelativeScales(tonality.Root);
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
                DrawChordText(chord, left + 5, top + 5, chord_alpha, tonality);
            }
        }
        protected void DrawPivot(double percent,int row)
        {
            int top = ROW_BASE_HEIGHT - (row + 1) * 50;
            int left = (int)Math.Round((GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + percent * SUBTITLE_WIDTH);
            int right = (GRAPHIC_WIDTH + SUBTITLE_WIDTH) / 2;
            int outer = 2;
            G.FillRectangle(Brushes.Black, new Rectangle(left, top - outer, right - left + outer, 50 + outer));
            G.DrawLine(rectPen, new Point(left, top), new Point(left, top + 40));

        }
        protected void DrawScale(double percent, int row)
        {
            int top = ROW_BASE_HEIGHT - (row + 1) * 50;
            int bottom = ROW_BASE_HEIGHT - row * 50 - 10;
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
            int top = ROW_BASE_HEIGHT - (endRow + 1) * 50;
            int bottom = ROW_BASE_HEIGHT - startRow * 50;
            Brush gradientBrush = new LinearGradientBrush(new Point(left, top), new Point(left, bottom), Color.FromArgb(255, Color.Black), Color.FromArgb(0, Color.Black));
            G.FillRectangle(gradientBrush, new Rectangle(left, top, right - left, bottom - top));
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
                        
                        DrawChord(Chord.SimpleTraid(i, b > 0), left, (t+1)*50, width, 100, Tonality.MajMinTonality(t, true));
                        left += width;
                    }
                }
            }
            if(myBuffer!=null)
                myBuffer.Render(Target);
        }

        /*protected int currentSegID;
        private void SwitchToNextSegment()
        {
            currentSegID++;
        }*/
        private void DrawHistoricalSegments(int currentSegID, double currentTime)
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
            Tonality tonality = currentSegID < segments.Count ? Info.Beats[segments[currentSegID].StartBeat].Tonality : Info.Beats[segments[segments.Count-1].StartBeat].Tonality;
            double row = shiftValue;
            for (int i = currentSegID - 1; i >= start_i; --i)
            {
                if (row >= MAX_HISTORY_DISPLAY + 1)
                    break;
                Tonality oldTonality = Info.Beats[segments[i].StartBeat].Tonality;
                if (oldTonality.Root != tonality.Root)
                {
                    double TONALTY_INDICATION_HEIGHT_IN_ROW = 0.5;
                    if(currentSegID - 1==i) // Shift need to be faster
                        row *= (1+ TONALTY_INDICATION_HEIGHT_IN_ROW);
                    else
                        row += TONALTY_INDICATION_HEIGHT_IN_ROW;
                    int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + 10;
                    int top= (int)Math.Round(ROW_BASE_HEIGHT - row * 50 - 10);
                    int alpha = (int)(255 * ClampedLerp(0, 1, row / (1 + TONALTY_INDICATION_HEIGHT_IN_ROW)));
                    left += DrawSimpleText("^ 1 = ", 4, left, top, alpha) + 6;
                    left+=DrawSimpleText(new string(Chord.Num2Char[oldTonality.Root].Reverse().ToArray()), 4, left, top, alpha);
                    tonality = oldTonality;
                }
                if (row >= MAX_HISTORY_DISPLAY + 1)
                    break;
                // DrawSeg(currentTime, i, currentSegID - i - (1 - shiftValue), gradientTonality);
                DrawSeg(currentTime, i, row);
                row += 1.0;
            }
            DrawFadingCover(HISTORY_FADING_START_ROW, MAX_HISTORY_DISPLAY);
        }
        private void DrawSeg(double currentTime, int segID, double row)
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
                if (i == segments[segID].EndBeat || beat.Chord != lastBeat.Chord || beat.Tonality != lastBeat.Tonality)
                {
                    double newTime = beat.Time - startTime;
                    double lastBeatDuration = Info.Beats[lastBeatID + 1].Time - Info.Beats[lastBeatID].Time;
                    // bool gradientTonalityEnabled = gradientTonality != null && gradientTonality != lastBeat.Tonality;
                    DrawChord(lastBeat.Chord, lastTime / duration, (newTime - lastTime) / duration, row, (currentTime - startTime - lastTime) / lastBeatDuration, lastBeat.Tonality);
                    lastTime = newTime;
                    lastBeat = beat;
                    lastBeatID = i;
                }
            }
        }
        private int DrawTonalityText(int root,int left, int top)
        {
            string scaleText = new string(Chord.Num2Char[root].Reverse().ToArray());
            return DrawSimpleText(scaleText, 3, left, top);
        }
        private Tonality DrawTonality(Tonality tonality, Tonality oldTonality, double timespan)
        {
            double TONALTY_TRANSITION_TIME = 1.5;
            int TONALTY_TOP = 170;
            int left = (GRAPHIC_WIDTH - SUBTITLE_WIDTH) / 2 + 5;
            left += DrawSimpleText("1 = ", 3, left, TONALTY_TOP) + 8;
            if (oldTonality!=null && tonality!=oldTonality && timespan>=0 && timespan<TONALTY_TRANSITION_TIME)
            {
                int scale_old = oldTonality.Root;
                int scale_new = tonality.Root;
                int delta_scale = (scale_new - scale_old + 12) % 12;
                if (delta_scale < 3)
                    delta_scale += 12;
                double progress = Math.Sin(Math.PI/2*Math.Sqrt(timespan / TONALTY_TRANSITION_TIME)) * delta_scale;
                int progress_lower = (int)Math.Floor(progress);
                int width1=DrawTonalityText((oldTonality.Root + progress_lower) % 12, left, (int)(TONALTY_TOP + 50 * (progress - progress_lower)));
                int width2=DrawTonalityText((oldTonality.Root + progress_lower + 1) % 12, left, (int)(TONALTY_TOP + 50 * (progress - progress_lower - 1)));

                G.FillRectangle(Brushes.Black, new Rectangle(left, TONALTY_TOP - 50, width1 + 16, 50));
                G.FillRectangle(Brushes.Black, new Rectangle(left, TONALTY_TOP + 40, width2 + 16, 35));

                return Tonality.MajMinTonality((oldTonality.Root + (int)Math.Round(progress)) % 12, tonality.MajMin);
            }
            else
            {
                DrawTonalityText(tonality.Root, left, TONALTY_TOP);
                return tonality;
            }

        }
        static int TITLE_TOP = 55;
        public void DrawMetadata()
        {
            int METADATA_RIGHT = 130;
            string title = MetaData["Title"];//"I AM A SUPER LONG TITLE PLACEHOLDER";
            int width = (int)(G.MeasureString(title, titleFont).Width);
            int left = (GRAPHIC_WIDTH - width) / 2;
            G.DrawString(title, titleFont, Brushes.White, new Point(left, TITLE_TOP));
            string line1 = MetaData["Line1"];//"Music by Mr.Unknown";
            string line2 = MetaData["Line2"];//"Words by Mr.Unknown";
            int width1 = (int)(G.MeasureString(line1, metadataFont).Width);
            int width2 = (int)(G.MeasureString(line2, metadataFont).Width);
            G.DrawString(line1, metadataFont, Brushes.White, new Point(GRAPHIC_WIDTH - METADATA_RIGHT - width1, TITLE_TOP + 45));
            G.DrawString(line2, metadataFont, Brushes.White, new Point(GRAPHIC_WIDTH - METADATA_RIGHT - width2, TITLE_TOP + 80));
        }
        private void DrawIntroCover(double currentTime)
        {
            double INTRO_TIME = 2.5;
            int COVER_TOP = 0; // TITLE_TOP + 115;
            if(currentTime<=INTRO_TIME)
            {
                int alphaValue = (int)ClampedLerp(255, 0, currentTime / INTRO_TIME);
                G.FillRectangle(new SolidBrush(Color.FromArgb(alphaValue, Color.Black)), new Rectangle(0, COVER_TOP, GRAPHIC_WIDTH, GRAPHIC_HEIGHT - COVER_TOP));

            }
        }
        public bool DrawFrame()
        {
            return DrawFrame(CurrentTime + 0.1);
        }
        public bool DrawFrame(double currentTime)
        {
            int currentSegID = 0;
            while (currentSegID < segments.Count && currentTime > Info.Beats[segments[currentSegID].EndBeat].Time)
            {
                ++currentSegID;
            }
            G.Clear(Color.Black);
            if (currentSegID < segments.Count)
            {
                double startTime = Info.Beats[segments[currentSegID].StartBeat].Time;
                double endTime = Info.Beats[segments[currentSegID].EndBeat].Time;
                double duration = (endTime - startTime) / (segments[currentSegID].EndBeat - segments[currentSegID].StartBeat) * BeatsPerSegment;
                double percent = (currentTime - startTime) / duration;
                DrawSeg(currentTime, currentSegID, 0);
                if (percent>=0)
                    DrawPivot(percent, 0);
                DrawScale(percent, 0);
            }
            else
                DrawScale(0, 0);
            DrawHistoricalSegments(currentSegID, currentTime);
            if (currentSegID == 0)
            {
                DrawTonality(Info.Beats[segments[currentSegID].StartBeat].Tonality, null, 0);
            }
            else
            {
                int segID = Math.Min(currentSegID, segments.Count - 1);
                double startTime = Info.Beats[segments[segID].StartBeat].Time;
                DrawTonality(Info.Beats[segments[segID].StartBeat].Tonality, Info.Beats[segments[segID - 1].StartBeat].Tonality, currentTime - startTime);
            }
            DrawIntroCover(currentTime);
            if (currentSegID >= segments.Count)
            {
                DrawIntroCover(Info.Beats[segments[segments.Count - 1].EndBeat].Time + 5 - currentTime);
                
            }
            DrawMetadata();
            if(myBuffer!=null)
                myBuffer.Render(Target);
            if (currentTime > Info.Beats[segments[segments.Count - 1].EndBeat].Time + 5)
                return false;
            return true;
        }
        struct ChordStatisticsStruct
        {
            public Chord chord;
            public int depth;
            public int count;
        }
        List<ChordStatisticsStruct> chordStatistics;
        const int CHORD_STATISTICS_COLUMN_WIDTH = 330;
        const int CHORD_STATISTICS_ROW_HEIGHT = 40;
        private void DrawStatisticsEntry(ChordStatisticsStruct data,int left, int chord_width, int top)
        {
            int right = left + CHORD_STATISTICS_COLUMN_WIDTH;
            left += 20;
            right -= 20;
            // G.DrawLine(rectPen, new Point(left, top + 33 - CHORD_STATISTICS_ROW_HEIGHT), new Point(right, top + 33 - CHORD_STATISTICS_ROW_HEIGHT));
            G.DrawLine(rectPen, new Point(left, top + 33), new Point(right, top + 33));
            right -= chord_width;
            left += data.depth * 30 + 5;
            string countString = data.count.ToString() + "×";
            G.DrawString(countString, suffixFont, Brushes.White, new Point(left, top));
            // right -= chordWidth[data.chord.TemplateID, data.chord.Scale];
            DrawChordText(data.chord, right, top, 255, Tonality.MajMinTonality(0, true));
         }
        public bool DrawStatistics()
        {
            return DrawStatistics(CurrentTime);
        }
        public bool DrawStatistics(double currentTime)
        {
            if (chordStatistics == null)
            {
                Dictionary<Chord, int> chordDict = new Dictionary<Chord, int>();
                Dictionary<Chord, List<Chord>> childList = new Dictionary<Chord, List<Chord>>();
                void CreateRelativeChord(Chord relativeChord)
                {
                    Chord parentChord = relativeChord.GetParentChord();
                    if (parentChord == null)
                        parentChord = Chord.NoChord;
                    if (!childList.ContainsKey(parentChord))
                    {
                        childList[parentChord] = new List<Chord>();
                    }
                    childList[parentChord].Add(relativeChord);
                    chordDict[relativeChord] = 0;
                }
                // Create a tree
                for (int i = 0; i < Info.Beats.Count; ++i)
                {
                    BeatInfo beat = Info.Beats[i];
                    if (beat.Chord.Scale != -1 && i != Info.Beats.Count - 1 && beat.Tonality.Root != -1)
                    {
                        Chord relativeChord = Chord.EnumerateChord(beat.Chord.TemplateID, (beat.Chord.Scale + 12 - beat.Tonality.Root) % 12);
                        do
                        {
                            if (!chordDict.ContainsKey(relativeChord))
                                CreateRelativeChord(relativeChord);
                            chordDict[relativeChord]++;
                            relativeChord = relativeChord.GetParentChord();
                        } while (relativeChord != null);
                    }
                }
                chordStatistics = new List<ChordStatisticsStruct>();
                void DFS(Chord relativeChord, int depth)
                {
                    bool visible = true;
                    List<Chord> unsortedChildList = childList.ContainsKey(relativeChord) ? childList[relativeChord] : new List<Chord>();
                    if (unsortedChildList.Count == 1 && chordDict[unsortedChildList[0]] == chordDict[relativeChord]) // Omit a node with exactly one child chord type
                        visible = false;
                    if (relativeChord == Chord.NoChord) // Virtual root
                        visible = false;
                    if (visible)
                    {
                        ChordStatisticsStruct s = new ChordStatisticsStruct();
                        s.chord = relativeChord;
                        s.depth = depth;
                        s.count = chordDict[relativeChord];
                        chordStatistics.Add(s);
                    }
                    unsortedChildList.Sort((x,y)=>(chordDict[y]).CompareTo(chordDict[x])); // Large to small
                    foreach (Chord child in unsortedChildList)
                    {
                        DFS(child, depth + (visible ? 1 : 0));
                    }
                }
                DFS(Chord.NoChord, 0);
                chordWidth = new int[Chord.GetChordTemplatesCount(), 12];
                for(int i=0;i< Chord.GetChordTemplatesCount();++i)
                {
                    for(int j=0;j<12;++j)
                    {
                        chordWidth[i, j] = DrawChordText(Chord.EnumerateChord(i, j), 0, 0, 0);
                    }
                }
            }
            G.Clear(Color.Black);
            int MAX_ROWS_PER_COLUMN = 13;
            int columns = (chordStatistics.Count - 1) / MAX_ROWS_PER_COLUMN + 1;
            int left = (GRAPHIC_WIDTH - CHORD_STATISTICS_COLUMN_WIDTH * columns) / 2;
            for (int c=0;c<columns;++c)
            {
                int clipStart = c * MAX_ROWS_PER_COLUMN;
                List <ChordStatisticsStruct> clip = chordStatistics.GetRange(clipStart, Math.Min(MAX_ROWS_PER_COLUMN, chordStatistics.Count - clipStart));
                int width = 0;
                foreach (ChordStatisticsStruct cs in clip)
                    width = Math.Max(width, chordWidth[cs.chord.TemplateID, cs.chord.Scale]);
                for (int i = 0; i < clip.Count; ++i)
                {
                    DrawStatisticsEntry(clip[i], left, width, 200 + i * CHORD_STATISTICS_ROW_HEIGHT);
                }
                left += CHORD_STATISTICS_COLUMN_WIDTH;

            }
            DrawIntroCover(currentTime);
            DrawMetadata();
            DrawIntroCover(10 - currentTime);// Actually, it's blackscreen cover
            if(myBuffer!=null)
                myBuffer.Render(Target);
            return currentTime < 10;
        }
    }
}
