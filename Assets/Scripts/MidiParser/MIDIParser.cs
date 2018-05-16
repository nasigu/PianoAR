// MIDIParser
using CSharpSynth.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Parserino
{
    public class MIDIParser
    {
        
        public static List<List<MidiEvent>> MidiFileToMidiEvents(string filename)
        {
            MidiFile midiFile = null;
            try
            {
                midiFile = new MidiFile(filename);
            }
            catch (Exception ex)
            {
                Debug.Log("Error Loading Midi:\n" + ex.Message);
                return null;
            }
            List<List<MidiEvent>> list = new List<List<MidiEvent>>();
            byte noteIndex = 0;
            float num = 60f / (float)((int)midiFile.BeatsPerMinute * midiFile.MidiHeader.DeltaTiming);
            MidiTrack[] tracks = midiFile.Tracks;
            foreach (MidiTrack midiTrack in tracks)
            {
                float num2 = 0f;
                List<MidiEvent> list2 = new List<MidiEvent>();
                Dictionary<int, float> dictionary = new Dictionary<int, float>();
                CSharpSynth.Midi.MidiEvent[] midiEvents = midiTrack.MidiEvents;
                foreach (CSharpSynth.Midi.MidiEvent midiEvent in midiEvents)
                {
                    Debug.Log("ch: " + midiEvent.channel + " p1: " + midiEvent.parameter1 + " p2: " + midiEvent.parameter2 + " dt: " + midiEvent.deltaTime);
                    if (midiEvent.midiChannelEvent == MidiHelper.MidiChannelEvent.Note_On || midiEvent.midiChannelEvent == MidiHelper.MidiChannelEvent.Note_Off)
                    {
                        num2 += (float)(double)midiEvent.deltaTime * num;
                        noteIndex = midiEvent.parameter1;
                        try
                        {
                            if (dictionary.ContainsKey(noteIndex) && midiEvent.midiChannelEvent == MidiHelper.MidiChannelEvent.Note_Off)
                            {
                                MidiEvent midiEvent2 = list2.LastOrDefault((MidiEvent n) => n.noteIndex == noteIndex);
                                if (midiEvent2 != null)
                                {
                                    midiEvent2.duration = num2 - dictionary[noteIndex];
                                    dictionary.Remove(noteIndex);
                                }
                            }
                            else if (midiEvent.midiChannelEvent == MidiHelper.MidiChannelEvent.Note_On)
                            {
                                list2.Add(new MidiEvent
                                {
                                    startTime = num2,
                                    noteIndex = noteIndex
                                });
                                dictionary.Add(noteIndex, num2);
                            }
                        }
                        catch (Exception message)
                        {
                            Debug.Log(message);
                            Debug.Log(num2);
                            Debug.Log(noteIndex);
                            Debug.Log(midiEvent);
                        }
                    }
                }
                list.Add(list2);
                Debug.Log("convertedNotes.Count: " + list2.Count);
                Debug.Log("convertedNotes: " + list2);
                foreach (MidiEvent item in list2)
                {
                    Debug.Log(string.Format("noteIndex = {0},            startTime = {1},            duration = {2}", item.noteIndex, item.startTime, item.duration));
                }
            }
            return list;
        }
    }
}
