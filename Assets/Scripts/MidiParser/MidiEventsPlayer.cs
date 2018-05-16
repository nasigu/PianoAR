// MidiEventsPlayer
using CSharpSynth.Synthesis;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MidiEventsPlayer : MonoBehaviour
{
    private float[] sampleBuffer;

    private float gain = 1f;

    private StreamSynthesizer midiStreamSynthesizer;

    private int midiNote = 60;

    private int midiNoteVolume = 100;

    private int midiInstrument = 1;

    private void Start()
    {
        this.midiStreamSynthesizer = new StreamSynthesizer(44100, 2, 1024, 40);
        this.sampleBuffer = new float[this.midiStreamSynthesizer.BufferSize];
        this.midiStreamSynthesizer.LoadBank("GM Bank/gm");
    }

    public void NoteOn(byte noteIndex)
    {
        this.midiStreamSynthesizer.NoteOn(1, noteIndex, 128, 0);
    }

    public void NoteOff(byte noteIndex)
    {
        this.midiStreamSynthesizer.NoteOff(1, noteIndex);
    }

    public void Mute()
    {
        this.midiStreamSynthesizer.NoteOffAll(true);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (this.midiStreamSynthesizer != null)
        {
            this.midiStreamSynthesizer.GetNext(this.sampleBuffer);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = this.sampleBuffer[i] * this.gain;
            }
        }
    }
}
