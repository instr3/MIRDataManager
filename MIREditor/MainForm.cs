using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass.AddOn.Midi;
using Common;
using System.IO;

namespace MIREditor
{
    public partial class MainForm : Form
    {
        public Tonality RelativeLabelTonality = Tonality.NoTonality;
        public Label[] ChordLabels;
        DataGridViewRow[] relativeChordRows;
        DataGridViewRow[] absoluteChordRows;
        public bool[] DefaultChordLabelsMajMin = { true, false, false, true, false, true, false, true, false, false, true, false };
        public string preloadFile = null;
        public string preCreateFile = null;
        MouseButtons listenMouseButton = MouseButtons.Right, inputMouseButton = MouseButtons.Left;

        #region init
        public MainForm(string[] args)
        {
            InitializeComponent();
            InitChordLabels();
            openOSUFileDialog.InitialDirectory = Settings.DatasetMusicFolder;
            openInfoFileDialog.InitialDirectory = Settings.ArchiveFolder;
            if (args.Length>0)
            {
                if(args[0]=="<create>")
                {
                    if(args.Length>1)
                    {
                        List<string> argsList = args.ToList();
                        argsList.RemoveAt(0);
                        preCreateFile = string.Join(" ", argsList);
                    }
                    else
                    {
                        preCreateFile = " ";
                    }
                }
                else preloadFile = string.Join(" ", args.ToArray());
            }
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            Program.EditManager = new EditManager();
            Program.MidiManager = new MidiManager();
            Program.MidiManager.Init();
            TimelinePictureBox.MouseWheel += TimelinePictureBox_MouseWheel;
            timer1.Enabled = true;
            Logger.Register(logText);
            RefreshInterface();
            if(!string.IsNullOrEmpty(preloadFile))
            {
                string fullFileName = Settings.ArchiveFolder + "\\" + preloadFile;
                ArchiveManager.SwitchSongInfo(ArchiveManager.ReadFromArchive(fullFileName), fullFileName);
            }
            else if(!string.IsNullOrEmpty(preCreateFile))
            {
                openOSUFileDialog.InitialDirectory = Settings.DatasetMusicFolder+"\\"+preCreateFile;
                新建ToolStripMenuItem_Click(sender, e);
                openOSUFileDialog.InitialDirectory = Settings.DatasetMusicFolder;
            }
        }
        public void RefreshInterface()
        {
            tabControl1_SelectedIndexChanged(null, null);
            Triggers.ChordLabelChangeTrigger = true;
            RelativeLabelTonality = Tonality.NoTonality;
            if (Program.TL != null)
            {
                trackBarVolumeMain.DataBindings.Clear();
                trackBarVolumeMain.DataBindings.Add("Value", Program.TL, "VolumeMain", true, DataSourceUpdateMode.OnPropertyChanged);
                trackBarVolumeMIDI.DataBindings.Clear();
                trackBarVolumeMIDI.DataBindings.Add("Value", Program.MidiManager, "VolumeNote", true, DataSourceUpdateMode.OnPropertyChanged);
                trackBarMIDIDelay.DataBindings.Clear();
                trackBarMIDIDelay.DataBindings.Add("Value", Program.MidiManager, "NoteDelay", true, DataSourceUpdateMode.OnPropertyChanged);
                checkBoxAutoPlayChord.DataBindings.Clear();
                checkBoxAutoPlayChord.DataBindings.Add("Checked", Program.TL.ChordEditor, "AutoPlayMidi", true, DataSourceUpdateMode.OnPropertyChanged);
                comboBox_Metre.Text = Program.TL.Info.MusicConfigure.MetreNumber.ToString();
                //if (Program.TL.Info.Tonality != -1)
                //    comboBox_GLTonality.SelectedIndex = Program.TL.Info.Tonality;
                comboBoxAlignBeats.Items.Clear();
                Program.TL.ChordEditor.AlignBeats = new List<int>();
                int bd = Program.TL.Info.MusicConfigure.MetreNumber;
                for (int i = 1; i < bd; ++i)
                {
                    if (bd % i == 0)
                    {
                        comboBoxAlignBeats.Items.Add(i + "拍");
                        Program.TL.ChordEditor.AlignBeats.Add(i);
                    }
                }
                int index = comboBoxAlignBeats.Items.Count;
                for (int i = 1; i * bd <= 12 || i == 1; i *= 2)
                {
                    comboBoxAlignBeats.Items.Add(i + "小节");
                    Program.TL.ChordEditor.AlignBeats.Add(i * bd);
                }
                comboBoxAlignBeats.SelectedIndex = index;
                radioButtonAbsolute.DataBindings.Clear();
                radioButtonAbsolute.DataBindings.Add("Checked", Program.TL, "NotRelativeLabel", true, DataSourceUpdateMode.OnPropertyChanged);
                radioButtonRelative.DataBindings.Clear();
                radioButtonRelative.DataBindings.Add("Checked", Program.TL, "RelativeLabel", true, DataSourceUpdateMode.OnPropertyChanged);

                comboBoxConfigConfidence.SelectedIndex = Program.TL.Info.TagConfigure.Confidence;
                textBoxConfigTagger.Text = Program.TL.Info.TagConfigure.Tagger;
                textBoxOSUMapID.Text = Program.TL.Info.MiscConfigure.osuMapID.ToString();
                Text = "Edit Mode: " + Program.TL.Info.MusicConfigure.Title;
            }
            else
            {
                Text = "Chord Editor";
            }
        }
        #endregion

        #region capture controls
        private void TimelinePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseDown(e.X, e.Y);
        }

        private void TimelinePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseUp(e.X, e.Y);

        }

        private void TimelinePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseMove(e.X, e.Y);
        }
        private void TimelinePictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseWheel(e.Delta);
        }
        private void TimelinePictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseEnter();

        }

        private void TimelinePictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.MouseLeave();

        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (Program.TL != null)
                Program.TL.KeyEvent(e.KeyCode, e.Control, e.Alt, e.Shift);
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyUp(e.KeyCode, e.Control, e.Alt, e.Shift);

        }
        #endregion

        #region chordlabel input
        private void ChordLabels_Paint(object sender,PaintEventArgs e)
        {
            Label owner = sender as Label;
            e.Graphics.Clear(Color.White);
            using (Brush mainBrush = new SolidBrush(owner.BackColor))
            {
                if (owner.ForeColor != owner.BackColor)
                {
                    using (Brush alterBrush = new LinearGradientBrush(new Point(0, owner.Height / 2 - 1), new Point(0, owner.Height), owner.BackColor, owner.ForeColor))
                    {
                        e.Graphics.FillRectangle(alterBrush, new Rectangle(0, owner.Height / 2, owner.Width, owner.Height / 2));
                        e.Graphics.FillRectangle(mainBrush, new Rectangle(0, 0, owner.Width, owner.Height / 2));
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(mainBrush, new Rectangle(0, 0, owner.Width, owner.Height));
                }
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(owner.Text, owner.Font, Brushes.Black,
                new Rectangle(0, 0, owner.Width, owner.Height), stringFormat);

        }
        private void InitChordLabels()
        {
            ChordLabels = new Label[] { ChordLabel1,ChordLabel2,ChordLabel3,ChordLabel4,ChordLabel5,ChordLabel6,
                ChordLabel7,ChordLabel8,ChordLabel9,ChordLabel10,ChordLabel11,ChordLabel12,ChordLabelN,ChordLabelX,ChordLabelQ
            };
            for (int id = 0; id < 15; ++id)
            {
                ChordLabels[id].Font = FontManager.Instance.ChordFont;
                ChordLabels[id].Paint += new PaintEventHandler(ChordLabels_Paint);
                ChordLabels[id].BackColor = Color.White;
                ChordLabels[id].ForeColor = Color.White;
                ChordLabels[id].Text = "";
            }
        }
        private void UpdateChordLabels()
        {
            Tonality currentTonality = Program.TL.ChromaVisualizer.GetCurrentTonality();
            if (Triggers.ChordLabelChangeTrigger || RelativeLabelTonality.ToString() != currentTonality.ToString())
            {
                Triggers.ChordLabelChangeTrigger = false;
                RelativeLabelTonality = currentTonality;
                for (int id = 0; id < 15; ++id)
                {
                    Chord inputChord = Program.TL.ChordShortcuts.GetChordInput(
                        id, Program.TL.IsCtrlDown, Program.TL.IsAltDown, Program.TL.IsShiftDown, RelativeLabelTonality, Program.TL.RelativeLabel);
                    if (inputChord != null)
                    {
                        KeyValuePair<Color,Color> colors = ColorSchema.GetGradientTransparentColorByChordName(inputChord.ToString(RelativeLabelTonality),50);
                        ChordLabels[id].BackColor = colors.Key;
                        ChordLabels[id].ForeColor = colors.Value;
                        if (Program.TL.RelativeLabel)
                            ChordLabels[id].Text = inputChord.ToString(RelativeLabelTonality);
                        else
                            ChordLabels[id].Text = inputChord.ToString();
                    }
                    else
                    {
                        ChordLabels[id].Text = "";
                        ChordLabels[id].BackColor = Color.White;
                        ChordLabels[id].ForeColor = Color.White;
                    }
                }
                if (checkBoxChordKeyboard.Checked)
                    InitChordKeyboard();
            }
        }
        private int GetChordLabelIndex(object sender)
        {
            for (int i = 0; i < ChordLabels.Length; ++i)
            {
                if (ChordLabels[i] == sender)
                    return i;
            }
            return -1;
        }

        private void pianoLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
            {
                if (e.Button == inputMouseButton)
                {
                    if (sender.Equals(pianoLabelN))
                        Program.TL.ChordEditor.PerformInputChord(Chord.NoChord);
                    else if (sender.Equals(pianoLabelX))
                        Program.TL.ChordEditor.PerformInputChord(Chord.UnrepresentableChord);
                    else if (sender.Equals(pianoLabelQ))
                        Program.TL.ChordEditor.PerformInputChord(Chord.UnknownChord);

                }
            }
        }
        private void ChordLabels_MouseClick(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
            {
                int id = GetChordLabelIndex(sender);
                Chord inputChord = Program.TL.ChordShortcuts.GetChordInput(
                    id, Program.TL.IsCtrlDown, Program.TL.IsAltDown, Program.TL.IsShiftDown, RelativeLabelTonality, Program.TL.RelativeLabel);
                if (inputChord != null)
                {
                    if (e.Button == listenMouseButton)
                    {
                        Program.MidiManager.PlayChordNotes(inputChord);
                    }
                    else if (e.Button == inputMouseButton)
                    {
                        Program.TL.ChordEditor.PerformInputChord(inputChord);
                    }
                }
            }
        }
        #endregion

        #region menu
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openInfoFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            ArchiveManager.SwitchSongInfo(ArchiveManager.ReadFromArchive(openInfoFileDialog.FileName), openInfoFileDialog.FileName);
            // FileManager.TryOpen(openInfoFileDialog.FileName);
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                {
                    if (!string.IsNullOrEmpty(Program.FullArchiveFilePath))
                    {
                        ArchiveManager.SaveToArchive(Program.FullArchiveFilePath, Program.TL.Info);
                        return;
                    }
                    另存为ToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                saveInfoFileDialog.InitialDirectory = Settings.ArchiveFolder;
                if (OsuAnalyzer.directoryName != null)
                    saveInfoFileDialog.FileName = OsuAnalyzer.directoryName;
                if (saveInfoFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                ArchiveManager.SaveToArchive(saveInfoFileDialog.FileName, Program.TL.Info);
                Program.FullArchiveFilePath = saveInfoFileDialog.FileName;
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openOSUFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            SongInfo info = OsuAnalyzer.ExtractFromOSUFile(openOSUFileDialog.FileName, Settings.DatasetMusicFolder);
            if (info != null)
            {
                ArchiveManager.SwitchSongInfo(info);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                if (Program.EditManager.CanUndo)
                {
                    List<BeatInfo> beats = Program.EditManager.Undo(Program.TL.Info);
                    if (beats == null)
                    {
                        MessageBox.Show("发生严重错误!");
                    }
                    else Program.TL.Info.Beats = beats;

                }

            }
        }
        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                if (Program.EditManager.CanRedo)
                {
                    List<BeatInfo> beats = Program.EditManager.Redo();
                    if (beats == null)
                    {
                        MessageBox.Show("发生严重错误!");
                    }
                    else Program.TL.Info.Beats = beats;
                }

            }

        }


        private void 编辑ToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            撤销ToolStripMenuItem.Enabled = Program.EditManager.CanUndo;
            重做ToolStripMenuItem.Enabled = Program.EditManager.CanRedo;
            撤销ToolStripMenuItem.Text = "撤销" + Program.EditManager.LastUndoInfo;
            重做ToolStripMenuItem.Text = "重做" + Program.EditManager.LastRedoInfo;
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by instr3");
        }
        #endregion

        #region beat editor
        private void comboBoxAlignBeats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null && comboBoxAlignBeats.SelectedIndex != -1)
            {
                Program.TL.ChordEditor.AlignBeat =
                    Program.TL.ChordEditor.AlignBeats[comboBoxAlignBeats.SelectedIndex];
            }
        }

        private void button_BEDivide_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.DivideInterval();
        }

        private void button_BEMerge_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.MergeInterval();
        }

        private void button_BENormalize_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.NormalizeInterval();

        }

        private void button_BERol_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.LeftRotateInterval();
        }

        private void button_BERor_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.RightRotateInterval();

        }

        private void button_BEDelete_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.DeleteSingleBeat();

        }

        private void button_BENew_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.CreateSingleBeat();

        }

        private void button_BEBarStart_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.ModifyBarStartOfSingleBeat(false);

        }
        private void button_BEEnhanceDownbeat_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.ModifyBarStartOfSingleBeat(true);

        }


        private void comboBox_Metre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null && comboBox_Metre.SelectedItem != null)
                Program.TL.Info.MusicConfigure.MetreNumber = int.Parse(comboBox_Metre.SelectedItem.ToString());

        }

        private void button_BESelectAll_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.SelectAllBeat();
        }

        private void comboBox_GLTonality_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Program.TL != null && comboBox_GLTonality.SelectedIndex != -1)
            //    Program.TL.Info.Tonality = comboBox_GLTonality.SelectedIndex;

        }

        private void comboBox_TETonality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null && comboBox_TETonality.SelectedIndex != -1)
                Program.TL.BeatEditor.TonalityModify(comboBox_TETonality.SelectedItem as string);
            comboBox_TETonality.SelectedIndex = -1;
        }

        private void button_TESwitchMajMin_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.TonalitySwitchMajMin();

        }
        private void button_BEExtendBeat_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.ExtendBeat();

        }

        private void button_BERemoveLeft_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.RemoveLeft();
        }

        private void button_BERemoveRight_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.BeatEditor.RemoveRight();

        }
        #endregion

        #region shortcut buttons
        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.Space, false, false, false);
        }
        private void buttonVamp_Click(object sender, EventArgs e)
        {
            if (Program.TL != null) 
                Program.TL.KeyEvent(Keys.L, false, false, false);
        }
        private void buttonChroma_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.P, false, false, false);
        }
        private void buttonRawChord_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.O, false, false, false);
        }

        private void buttonLeftBracket_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.Oemcomma, false, false, false);

        }
        private void buttonCutInsertChord_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.Insert, false, false, false);
        }

        private void buttonRightBracket_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.KeyEvent(Keys.OemPeriod, false, false, false);
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Program.TL != null)
                保存ToolStripMenuItem_Click(sender, e);

        }
        private bool labelScale_Down;
        private int labelScale_X;
        private double labelScale_TimeScale;
        private void labelScale_MouseDown(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
            {
                labelScale_Down = true;
                labelScale_X = e.X;
                labelScale_TimeScale = Program.TL.TimeScale;
            }
        }

        private void labelScale_MouseMove(object sender, MouseEventArgs e)
        {
            if (Program.TL != null && labelScale_Down)
            {
                Program.TL.TimeScale = labelScale_TimeScale * Math.Exp((e.X - labelScale_X)/200.0);
            }
        }

        private void labelScale_MouseUp(object sender, MouseEventArgs e)
        {
            if (Program.TL != null)
                labelScale_Down = false;
        }
        #endregion

        #region chord keyboard datagridview functions
        private void checkBoxChordKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxChordKeyboard.Visible = checkBoxChordKeyboard.Checked;
            if(Program.TL!=null)
                if (checkBoxChordKeyboard.Checked)
                    InitChordKeyboard();
        }
        private Color Lerp(Color c1, Color c2, float t)
        {
            return Color.FromArgb(Lerp(c1.R, c2.R, t), Lerp(c1.G, c2.G, t), Lerp(c1.B, c2.B, t));
        }

        private int Lerp(int a, int b, float t)
        {
            float tp = 1.0f - t;
            return (int)(tp * a + t * b);
        }
        private void SetGridCellChord(DataGridViewCell cell,Chord chord,Tonality tonality)
        {
            int color = 255 - 40 * chord.ToNotes().Count(x => !tonality.IsOnNaturalScale(x));
            cell.Tag = chord;
            cell.Value = chord.ToString(tonality);
            //cell.Style.BackColor = Color.FromArgb(color, color, color);
            KeyValuePair<Color,Color> colors = ColorSchema.GetGradientColorByChordName(chord.ToString(tonality));
            cell.Style.BackColor =
                Lerp(
                colors.Key,
                Color.FromArgb(color, color, color),
                0.8f);
            cell.Style.ForeColor =
                Lerp(
                colors.Value,
                Color.FromArgb(color, color, color),
                0.8f);
            cell.Style.Font = FontManager.Instance.ChordFont;
            //if (color == 255)
            //    cell.Style.Font = new Font(cell.InheritedStyle.Font, FontStyle.Underline);
            //cell.Style.ForeColor= Color.FromArgb(color, color, color);

        }
        private void InitChordKeyboardRows()
        {
            dataGridViewChord.RowHeadersDefaultCellStyle.Padding = new Padding(dataGridViewChord.RowHeadersWidth);
            dataGridViewChord.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
            dataGridViewChord.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            for (int i = 0; i < 12; ++i)
            {
                dataGridViewChord.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewChord.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridViewChord.Rows.Clear();
            relativeChordRows = new DataGridViewRow[Chord.GetChordTemplatesCount()];
            absoluteChordRows = new DataGridViewRow[Chord.GetChordTemplatesCount()];
            for (int i = 0; i < relativeChordRows.Length; ++i)
            {
                relativeChordRows[i] = new DataGridViewRow();
                DataGridViewRow row = relativeChordRows[i];
                row.HeaderCell.Value = Chord.GetChordTemplateAbbr(i);
            }
            dataGridViewChord.Rows.AddRange(relativeChordRows);
            for (int i = 0; i < relativeChordRows.Length; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    SetGridCellChord(relativeChordRows[i].Cells[j], Chord.EnumerateChord(i,j), Tonality.MajMinTonality(0, true));
                }
            }
            dataGridViewChord.Rows.Clear();
            for (int i = 0; i < absoluteChordRows.Length; ++i)
            {
                absoluteChordRows[i] = new DataGridViewRow();
                DataGridViewRow row = absoluteChordRows[i];
                row.HeaderCell.Value = Chord.GetChordTemplateAbbr(i);
                row.HeaderCell.Style.Font = FontManager.Instance.ChordFont;
            }
            dataGridViewChord.Rows.AddRange(absoluteChordRows);
            for (int i = 0; i < relativeChordRows.Length; ++i)
            {
                for (int j = 0; j < 12; ++j)
                {
                    SetGridCellChord(absoluteChordRows[i].Cells[j], Chord.EnumerateChord(i, j), Tonality.NoTonality);
                }
            }
            dataGridViewChord.Rows.Clear();
        }
        private void InitChordKeyboard()
        {
            if (relativeChordRows == null)
                InitChordKeyboardRows();
            Tonality tonality = Program.TL.ChromaVisualizer.GetCurrentTonality();
            bool relativeMode = Program.TL.RelativeLabel && tonality.Root != -1;
            if (relativeMode.Equals(dataGridViewChord.Tag)) return;
            dataGridViewChord.Rows.Clear();
            if (relativeMode)
            {
                for (int i = 0; i < 12; ++i)
                {
                    dataGridViewChord.Columns[i].HeaderText = "<" + Chord.Num2NoteString[i] + ">";
                    dataGridViewChord.Columns[i].HeaderCell.Style.Font = FontManager.Instance.NoteFont;
                }
                dataGridViewChord.Rows.AddRange(relativeChordRows);
            }
            else
            {
                for (int i = 0; i < 12; ++i)
                {
                    dataGridViewChord.Columns[i].HeaderText = "<" + Chord.Num2Char[i] + ">";
                    dataGridViewChord.Columns[i].HeaderCell.Style.Font = FontManager.Instance.NoteFont;
                }
                dataGridViewChord.Rows.AddRange(absoluteChordRows);
            }
            dataGridViewChord.Tag = relativeMode;
            dataGridViewChord.CurrentCell = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewChord.Rows.Clear();
            dataGridViewChord.Rows.AddRange(relativeChordRows);
        }

        private void dataGridViewChord_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Program.TL != null)
            {
                if (e.ColumnIndex != -1)
                {
                    Tonality tonality = Program.TL.ChromaVisualizer.GetCurrentTonality();
                    bool relativeMode = Program.TL.RelativeLabel && tonality.Root != -1;
                    int delta = relativeMode ? tonality.Root : 0;
                    if (e.RowIndex == -1)
                    {
                        Program.MidiManager.PlaySingleNote(delta + e.ColumnIndex);
                    }
                    else
                    {
                        Chord template =
                            ((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as Chord)
                            .ShiftPitch(delta);
                        if (e.Button == listenMouseButton)
                            Program.MidiManager.PlayChordNotes(template);
                        else if(e.Button==inputMouseButton)
                            Program.TL.ChordEditor.PerformInputChord(template);
                    }
                }
            }

        }

        private void dataGridViewChord_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewChord.CurrentCell = null;
        }

        void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            object o = dataGridViewChord.Rows[e.RowIndex].HeaderCell.Value;

            e.Graphics.DrawString(
                o != null ? o.ToString() : "",
                FontManager.Instance.ChordFont,
                Brushes.Black,
                new PointF((float)e.RowBounds.Left + 2, (float)e.RowBounds.Top + 4));
        }
        void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            using (Brush gridBrush = new SolidBrush(dataGridViewChord.GridColor),
                backColorBrush = e.CellStyle.ForeColor == e.CellStyle.BackColor ?
                new SolidBrush(e.CellStyle.BackColor) as Brush :
                new LinearGradientBrush(new Point(0, e.CellBounds.Top), new Point(0, e.CellBounds.Bottom), e.CellStyle.BackColor, e.CellStyle.ForeColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // Erase the cell.
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                    // Draw the grid lines (only the right and bottom lines;
                    // DataGridView takes care of the others).
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                        e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                        e.CellBounds.Bottom - 1);
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                        e.CellBounds.Top, e.CellBounds.Right - 1,
                        e.CellBounds.Bottom);

                    // Draw the text content of the cell, ignoring alignment.
                    if (e.Value != null)
                    {
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;
                        e.Graphics.DrawString((string)e.Value, e.CellStyle.Font, Brushes.Black,
                            new Rectangle(e.CellBounds.Left - 2, e.CellBounds.Top, e.CellBounds.Width + 4, e.CellBounds.Height), stringFormat);
                        //e.Graphics.DrawString((string)e.Value, e.CellStyle.Font,
                        //    Brushes.Black, e.CellBounds.Left+e.CellBounds.Width/2,
                        //    e.CellBounds.Top + e.CellBounds.Height / 2, stringFormat);
                    }
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region exporter
        
        private void button_ExportTXT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.FullArchiveFilePath))
                另存为ToolStripMenuItem_Click(sender, e);
            if (string.IsNullOrEmpty(Program.FullArchiveFilePath))
                return;
            if (Program.TL != null)
            {
                Exporter exporter = new Exporter(Program.TL.Info, Program.TL.MP3Length);
                try
                {
                    exporter.ExportToFolder(Settings.ExportFolder,
                        Path.GetFileNameWithoutExtension(Program.FullArchiveFilePath),
                        checkBoxExportMusic.Checked,
                        Settings.DatasetMusicFolder + "\\" + Program.TL.Info.MusicConfigure.Location);
                    Logger.Log("Exported successfully.");
                }
                catch (Exception ex)
                {
                    Logger.Log("[Error]" + ex.Message);
                }

            }

        }
        #endregion
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                Program.TL.Draw();
                UpdateChordLabels();
            }

        }
        internal void ActivateTimer()
        {
            timer1.Enabled = true;
        }

        internal void DeactivateTimer()
        {
            timer1.Enabled = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                Program.TL.ChordEditor.Enabled = false;
                Program.TL.BeatEditor.Enabled = false;
                switch (tabControl1.SelectedTab.Text)
                {
                    case "和弦":
                        Program.TL.ChordEditor.Enabled = true;
                        break;
                    case "节拍与调性":
                        Program.TL.BeatEditor.Enabled = true;
                        break;
                }
            }
        }



        private void radioButtonAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            Triggers.ChordLabelChangeTrigger = true;
        }

        private void radioButtonRelative_CheckedChanged(object sender, EventArgs e)
        {
            Triggers.ChordLabelChangeTrigger = true;

        }



        private void textBoxConfigTagger_TextChanged(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.Info.TagConfigure.Tagger = textBoxConfigTagger.Text;

        }
        private void textBoxOSUMapID_TextChanged(object sender, EventArgs e)
        {
            if (Program.TL != null)
            {
                int result;
                if (int.TryParse(textBoxOSUMapID.Text, out result))
                {
                    Program.TL.Info.MiscConfigure.osuMapID = result;
                }
            }
        }

        private void comboBoxConfigConfidence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.TL != null)
                Program.TL.Info.TagConfigure.Confidence = comboBoxConfigConfidence.SelectedIndex;
        }

        private void checkBoxMouseSwitch_CheckedChanged(object sender, EventArgs e)
        {
            inputMouseButton = checkBoxMouseSwitch.Checked ? MouseButtons.Right : MouseButtons.Left;
            listenMouseButton = checkBoxMouseSwitch.Checked ? MouseButtons.Left : MouseButtons.Right;
        }
    }
}
