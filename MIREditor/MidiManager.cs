using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass.AddOn.Midi;
using Un4seen.Bass;
using Common;
using System.Windows.Forms;
using System.Threading;

namespace MIREditor
{
    class MidiManager
    {
        public BASS_MIDI_FONT[] Fonts;
        public int[] NoteStreams;
        int noteDelay = 40;
        private int volumeNote = 70;
        public int VolumeNote
        {
            get
            {
                return volumeNote;
            }
            set
            {
                volumeNote = value;
            }
        }
        public int NoteDelay
        {
            get
            {
                return noteDelay;
            }
            set
            {
                noteDelay = value;
            }
        }
        public void PlayInVolume(int channel,bool restart,int volume)
        {
            Bass.BASS_ChannelSetAttribute(channel, BASSAttribute.BASS_ATTRIB_VOL, volume / 100f);
            Bass.BASS_ChannelPlay(channel, restart);
        }
        public void Init()
        {
            int tget = BassMidi.BASS_MIDI_FontInit("piano.sf2");
            Fonts = new BASS_MIDI_FONT[1];
            Fonts[0].font = tget;
            Fonts[0].bank = 0;
            Fonts[0].preset = -1;
            NoteStreams = new int[256];
            for (int i=60;i<=60+12*4;++i)
            {
                NoteStreams[i]= BassMidi.BASS_MIDI_StreamCreateEvents(SingleNoteEvent(i), 100, 0, 0);
                BassMidi.BASS_MIDI_StreamSetFonts(NoteStreams[i], Fonts, 1);
            }
        }
        private BASS_MIDI_EVENT[] SingleNoteEvent(int note, int octave)
        {
            return SingleNoteEvent(note + octave * 12 + 60);
        }
        private BASS_MIDI_EVENT[] SingleNoteEvent(int midiNote)
        {
            return new BASS_MIDI_EVENT[]{
                new BASS_MIDI_EVENT(BASSMIDIEvent.MIDI_EVENT_TEMPO, 500000, 0, 0,0),

                new BASS_MIDI_EVENT(BASSMIDIEvent.MIDI_EVENT_PROGRAM, 0, 0, 0,0),
                new BASS_MIDI_EVENT(BASSMIDIEvent.MIDI_EVENT_NOTE, midiNote| 100<<8, 0, 0,0), // press the key
                new BASS_MIDI_EVENT(BASSMIDIEvent.MIDI_EVENT_NOTE, midiNote, 0, 200,0), // release the key after 200 ticks
                new BASS_MIDI_EVENT(BASSMIDIEvent.MIDI_EVENT_END, 0, 0, 400,0) // end after 400 ticks
            };
        }
        public void PlaySingleNote(int note,int octave=0)
        {
            PlayInVolume(NoteStreams[note + octave * 12 + 60], true, volumeNote);
        }
        public void PlayChordNotes(Chord chord)
        {
            Thread thread = new Thread(
                new ParameterizedThreadStart(DelayAndPlayNotes)
            );
            thread.Start(chord.ToNotesUnclamped());
        }
        private void DelayAndPlayNotes(object notes)
        {
            foreach(int note in (int[])notes)
            {
                PlaySingleNote(note);
                Thread.Sleep(noteDelay);
            }
        }

        public void PlayTestSong()
        {
            int stream = BassMidi.BASS_MIDI_StreamCreateFile("zumn.mid", 0, 0, 0, 0);
            BassMidi.BASS_MIDI_StreamSetFonts(stream, Fonts, 1);
            PlayInVolume(stream, true, volumeNote);
        }
    }
}
