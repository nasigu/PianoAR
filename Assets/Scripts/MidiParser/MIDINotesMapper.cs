using System;
using UnityEngine;
using CSharpSynth.Midi;

// Token: 0x02000124 RID: 292
public class MIDINotesMapper : MonoBehaviour
{
    // Token: 0x0600095A RID: 2394 RVA: 0x0002250B File Offset: 0x0002090B
    public static MIDINotesMapper.MIDINote FromMidiEvent(MidiEvent evt)
    {
        return MIDINotesMapper.FromNoteIndex(evt.noteIndex);
    }

    // Token: 0x0600095B RID: 2395 RVA: 0x00022518 File Offset: 0x00020918
    public static MIDINotesMapper.MIDINote FromNoteIndex(int noteIdx)
    {
        string name = string.Empty;
        switch (noteIdx % 12)
        {
            case 0:
                name = "A";
                break;
            case 1:
                name = "A#";
                break;
            case 2:
                name = "B";
                break;
            case 3:
                name = "C";
                break;
            case 4:
                name = "C#";
                break;
            case 5:
                name = "D";
                break;
            case 6:
                name = "D#";
                break;
            case 7:
                name = "E";
                break;
            case 8:
                name = "F";
                break;
            case 9:
                name = "F#";
                break;
            case 10:
                name = "G";
                break;
            case 11:
                name = "G#";
                break;
        }
        return new MIDINotesMapper.MIDINote
        {
            OctaveIndex = noteIdx / 12 - 1,
            Name = name
        };
    }

    // Token: 0x02000125 RID: 293
    public class MIDINote
    {
        // Token: 0x170001EC RID: 492
        // (get) Token: 0x0600095D RID: 2397 RVA: 0x00022616 File Offset: 0x00020A16
        // (set) Token: 0x0600095E RID: 2398 RVA: 0x0002261E File Offset: 0x00020A1E
        public string Name { get; set; }

        // Token: 0x170001ED RID: 493
        // (get) Token: 0x0600095F RID: 2399 RVA: 0x00022627 File Offset: 0x00020A27
        // (set) Token: 0x06000960 RID: 2400 RVA: 0x0002262F File Offset: 0x00020A2F
        public int OctaveIndex { get; set; }
    }
}