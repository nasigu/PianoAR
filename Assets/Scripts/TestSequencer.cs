using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using SmfLite;


public class TestSequencer : MonoBehaviour
{
    public TextAsset sourceFile;

    public Transform note;
    public Transform blacknote;
    private Transform keyboard;
    private Transform currentKey;
    private Transform firstNote;

    private int spawnOffsetIndex;
    private float offsetNote = 0.547871f;

    private float xPos;
    private float yPos;
    private float zPos;


    private MidiFileContainer song;
    private MidiTrackSequencer sequencer;
    #region Rhytm
    public float bpm = 60f;
    private float minute = 60f;
    private float bar;
    private float half;
    private float quarter;
    private float eight;
    private float sixth;
    private float thitrySecond;
    private float lenghtOffset = 0.2f;
    #endregion
    private byte currentNote;
    private float startTime;

   [HideInInspector]
   public bool calibrating = false;
   [HideInInspector]
   public bool startSong = false;

   public Transform SubMenu;
   public Transform CalibrationMenu;
   public Transform SaveDeleteLoadMenu;
   GameObject[] firstLanes;
   GameObject[] secondLanes;
   [HideInInspector]
   public Vector3 newScale;
   [HideInInspector]
   public Vector3 newPosition;
   [HideInInspector]
   public Vector3 newHeight;

    #region noteDurationHandler
    [HideInInspector]
    public float handlerC2;
    [HideInInspector]
    public float handlerD2;
    [HideInInspector]
    public float handlerE2;
    [HideInInspector]
    public float handlerF2;
    [HideInInspector]
    public float handlerG2;
    [HideInInspector]
    public float handlerA2;
    [HideInInspector]
    public float handlerB2;
    [HideInInspector]
    public float handlerC3;
    [HideInInspector]
    public float handlerD3;
    [HideInInspector]
    public float handlerE3;
    [HideInInspector]
    public float handlerF3;
    [HideInInspector]
    public float handlerG3;
    [HideInInspector]
    public float handlerA3;
    [HideInInspector]
    public float handlerB3;
    [HideInInspector]
    public float handlerC4;
    //blackNotes
    [HideInInspector]
    public float handlerC2c;
    [HideInInspector]
    public float handlerD2c;
    [HideInInspector]
    public float handlerF2c;
    [HideInInspector]
    public float handlerG2c;
    [HideInInspector]
    public float handlerA2c;
    [HideInInspector]
    public float handlerC3c;
    [HideInInspector]
    public float handlerD3c;
    [HideInInspector]
    public float handlerF3c;
    [HideInInspector]
    public float handlerG3c;
    [HideInInspector]
    public float handlerA3c;
    #endregion

    float noteDuration;

    /*public Transform BarNote;
    public Transform HalfNote;
    public Transform QuarterNote;
    public Transform EightNote;*/
    [HideInInspector]
    public Transform noteChanger;
    [HideInInspector]
    public Transform myNewNote;
    [HideInInspector]
    public Vector3 vec = new Vector3(0, 0, 0);
    [HideInInspector]
    public float pos;
    [HideInInspector]
    public Note notescript;
    [HideInInspector]
    public float timeOfNote;
    [HideInInspector]
    public bool noteStatus;
    [HideInInspector]
    public string noteDimension;
    [HideInInspector]
    public bool blackNote;






    void Awake()
        {
            bar = 4 * minute / bpm;
            half = bar / 2;
            quarter = half / 2;
            eight = quarter / 2;
            sixth = eight / 2;
            thitrySecond = sixth / 2;
        }

        public void ResetAndPlay()
        {
            sequencer = new MidiTrackSequencer(song.tracks[0], song.division, bpm);
            ApplyMessages(sequencer.Start());

        }

        IEnumerator Start()
        {
            if (SubMenu.gameObject.activeSelf)
            {
                SubMenu.gameObject.SetActive(false);
            }

            if (CalibrationMenu.gameObject.activeSelf)
            {
                CalibrationMenu.gameObject.SetActive(false);
            }

            if (SaveDeleteLoadMenu.gameObject.activeSelf)
            {
                SaveDeleteLoadMenu.gameObject.SetActive(false);
            }

            firstNote = GameObject.FindGameObjectWithTag("FirstNote").GetComponent<Transform>();

            keyboard = GameObject.FindGameObjectWithTag("KeyBoard").GetComponent<Transform>();

            song = MidiFileLoader.Load(sourceFile.bytes);

            yield return new WaitForSeconds(1.0f);
            //ResetAndPlay ();
        }

        void Update()
        {
            if (startSong == true)
            {
                if (sequencer != null && sequencer.Playing)
                {
                    ApplyMessages(sequencer.Advance(Time.deltaTime));
                }
            }
        }


        // Обработчик MIDI-сообщений, имеет структуру {status; data1; data2}, где status определяет нужно нажать на ноту или отпустить;
        // data1 - какую ноту нажать; data2 - с какой силой (velocity) необходимо нажать на ноту.
        void ApplyMessages(List<MidiEvent> messages)
        {
            
            if (messages != null)
            {
                foreach (var m in messages)
                {
                    // Статус 0x90 - сообщение NoteOn
                    // Статус 0x80 - сообщение NoteOff  

                    if (m.status == 0x90)
                    {
                        string mString = m.ToString();
                        string xxx = mString.Remove(0, mString.IndexOf("=")+2);
                        float.TryParse(xxx, out timeOfNote);
                        blackNote = false;
                        #region SwitchStateofNoteOn
                        switch (m.data1)
                        {
                            case 48:
                                handlerC2 = timeOfNote;
                                currentKey = GameObject.Find("C2").GetComponent<Transform>();
                                break;
                            case 50:
                                handlerD2 = timeOfNote;
                                currentKey = GameObject.Find("D2").GetComponent<Transform>();
                                break;
                            case 52:
                                handlerE2 = timeOfNote;
                                currentKey = GameObject.Find("E2").GetComponent<Transform>();
                                break;
                            case 53:
                                handlerF2 = timeOfNote;
                                currentKey = GameObject.Find("F2").GetComponent<Transform>();
                                break;
                            case 55:
                                handlerG2 = timeOfNote;
                                currentKey = GameObject.Find("G2").GetComponent<Transform>();
                                break;
                            case 57:
                                handlerA2 = timeOfNote;
                                currentKey = GameObject.Find("A2").GetComponent<Transform>();
                                break;
                            case 59:
                                handlerB2 = timeOfNote;
                                currentKey = GameObject.Find("B2").GetComponent<Transform>();
                                break;
                            case 60:
                                handlerC3 = timeOfNote;
                                currentKey = GameObject.Find("C3").GetComponent<Transform>();
                                break;
                            case 62:
                                handlerD3 = timeOfNote;
                                currentKey = GameObject.Find("D3").GetComponent<Transform>();
                                break;
                            case 64:
                                handlerE3 = timeOfNote;
                                currentKey = GameObject.Find("E3").GetComponent<Transform>();
                                break;
                            case 65:
                                handlerF3 = timeOfNote;
                                currentKey = GameObject.Find("F3").GetComponent<Transform>();
                                break;
                            case 67:
                                handlerG3 = timeOfNote;
                                currentKey = GameObject.Find("G3").GetComponent<Transform>();
                                break;
                            case 69:
                                handlerA3 = timeOfNote;
                                currentKey = GameObject.Find("A3").GetComponent<Transform>();
                                break;
                            case 71:
                                handlerB3 = timeOfNote;
                                currentKey = GameObject.Find("B3").GetComponent<Transform>();
                                break;
                            case 72:
                                handlerC4 = timeOfNote;
                                currentKey = GameObject.Find("C4").GetComponent<Transform>();
                                break;
                        //blackNotes
                            case 49:
                                handlerC2c = timeOfNote;
                                currentKey = GameObject.Find("C2#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 51:
                                handlerD2c = timeOfNote;
                                currentKey = GameObject.Find("D2#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 54:
                                handlerF2c = timeOfNote;
                                currentKey = GameObject.Find("F2#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 56:
                                handlerG2c = timeOfNote;
                                currentKey = GameObject.Find("G2#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 58:
                                handlerA2c = timeOfNote;
                                currentKey = GameObject.Find("A2#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 61:
                                handlerC3c = timeOfNote;
                                currentKey = GameObject.Find("C3#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 63:
                                handlerD3c = timeOfNote;
                                currentKey = GameObject.Find("D3#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 66:
                                handlerF3c = timeOfNote;
                                currentKey = GameObject.Find("F3#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 68:
                                handlerG3c = timeOfNote;
                                currentKey = GameObject.Find("G3#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            case 70:
                                handlerA3c = timeOfNote;
                                currentKey = GameObject.Find("A3#").GetComponent<Transform>();
                                blackNote = true;
                            break;
                            default:
                                break;
                        }
                        #endregion
                    if(blackNote == true)
                    {
                        myNewNote = Instantiate(blacknote, new Vector3(currentKey.position.x, currentKey.position.y + 7f, currentKey.position.z), Quaternion.identity);
                        myNewNote.transform.parent = keyboard;
                        myNewNote.name = timeOfNote.ToString();
                        myNewNote.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                        myNewNote = Instantiate(note, new Vector3(currentKey.position.x, currentKey.position.y + 7f, currentKey.position.z), Quaternion.identity);
                        myNewNote.transform.parent = keyboard;
                        myNewNote.name = timeOfNote.ToString();
                        myNewNote.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                    if(m.status == 0x80)
                    {
                        switch (m.data1)
                        {
                            case 48:
                                noteDuration = Time.time - handlerC2;
                                noteChanger = GameObject.Find(handlerC2.ToString()).GetComponent<Transform>();
                                break;
                            case 50:
                                noteDuration = Time.time - handlerD2;
                                noteChanger = GameObject.Find(handlerD2.ToString()).GetComponent<Transform>();
                                break;
                            case 52:
                                noteDuration = Time.time - handlerE2;
                                noteChanger = GameObject.Find(handlerE2.ToString()).GetComponent<Transform>();
                                break;
                            case 53:
                                noteDuration = Time.time - handlerF2;
                                noteChanger = GameObject.Find(handlerF2.ToString()).GetComponent<Transform>();
                                break;
                            case 55:
                                noteDuration = Time.time - handlerG2;
                                noteChanger = GameObject.Find(handlerG2.ToString()).GetComponent<Transform>();
                                break;
                            case 57:
                                noteDuration = Time.time - handlerA2;
                                noteChanger = GameObject.Find(handlerA2.ToString()).GetComponent<Transform>();
                                break;
                            case 59:
                                noteDuration = Time.time - handlerB2;
                                noteChanger = GameObject.Find(handlerB2.ToString()).GetComponent<Transform>();
                                break;
                            case 60:
                                noteDuration = Time.time - handlerC3;
                                noteChanger = GameObject.Find(handlerC3.ToString()).GetComponent<Transform>();
                                break;
                            case 62:
                                noteDuration = Time.time - handlerD3;
                                noteChanger = GameObject.Find(handlerD3.ToString()).GetComponent<Transform>();
                                break;
                            case 64:
                                noteDuration = Time.time - handlerE3;
                                noteChanger = GameObject.Find(handlerE3.ToString()).GetComponent<Transform>();
                                break;
                            case 65:
                                noteDuration = Time.time - handlerF3;
                                noteChanger = GameObject.Find(handlerF3.ToString()).GetComponent<Transform>();
                                break;
                            case 67:
                                noteDuration = Time.time - handlerG3;
                                noteChanger = GameObject.Find(handlerG3.ToString()).GetComponent<Transform>();
                                break;
                            case 69:
                                noteDuration = Time.time - handlerA3;
                                noteChanger = GameObject.Find(handlerA3.ToString()).GetComponent<Transform>();
                                break;
                            case 71:
                                noteDuration = Time.time - handlerB3;
                                noteChanger = GameObject.Find(handlerB3.ToString()).GetComponent<Transform>();
                                break;
                            case 72:
                                noteDuration = Time.time - handlerC4;
                                noteChanger = GameObject.Find(handlerC4.ToString()).GetComponent<Transform>();
                                break;
                            case 49:
                                noteDuration = Time.time - handlerC2c;
                                noteChanger = GameObject.Find(handlerC2c.ToString()).GetComponent<Transform>();
                                break;
                            case 51:
                                noteDuration = Time.time - handlerD2c;
                                noteChanger = GameObject.Find(handlerD2c.ToString()).GetComponent<Transform>();
                                break;
                            case 54:
                                noteDuration = Time.time - handlerF2c;
                                noteChanger = GameObject.Find(handlerF2c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 56:
                                noteDuration = Time.time - handlerG2c;
                                noteChanger = GameObject.Find(handlerG2c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 58:
                                noteDuration = Time.time - handlerA2c;
                                noteChanger = GameObject.Find(handlerA2c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 61:
                                noteDuration = Time.time - handlerC3c;
                                noteChanger = GameObject.Find(handlerC3c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 63:
                                noteDuration = Time.time - handlerD3c;
                                noteChanger = GameObject.Find(handlerD3c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 66:
                                noteDuration = Time.time - handlerF3c;
                                noteChanger = GameObject.Find(handlerF3c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 68:
                                noteDuration = Time.time - handlerG3c;
                                noteChanger = GameObject.Find(handlerG3c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;
                            case 70:
                                noteDuration = Time.time - handlerA3c;
                                noteChanger = GameObject.Find(handlerA3c.ToString()).GetComponent<Transform>();
                                blackNote = true;
                                break;

                            default:
                                break;
                        }

                        SetNoteLenght(noteDuration);

                    switch (noteDimension)
                    {
                        case "bar":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.16f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y + 0.38f, noteChanger.position.z);
                            break;
                        case "half":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.08f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y + 0.183f, noteChanger.position.z);
                            break;
                        case "quarter":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.04f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y + 0.085f, noteChanger.position.z);
                            break;
                        case "eight":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.02f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y + 0.036f, noteChanger.position.z);
                            break;
                        case "sixth":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.01f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y + 0.013f, noteChanger.position.z);
                            break;
                        case "thirtysecond":
                            noteChanger.localScale = new Vector3(noteChanger.localScale.x, 0.005f, 1f);
                            noteChanger.position = new Vector3(noteChanger.position.x, noteChanger.position.y, noteChanger.position.z);
                            break;
                    }
                    noteChanger.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
            }
        }
    #region Determine Note Lenght
    public string SetNoteLenght(float noteDuration)
        {
            
            if(noteDuration <= bar+lenghtOffset && noteDuration >= bar-lenghtOffset)
            {
                noteDimension = "bar"; 
            }

            if(noteDuration <= half + lenghtOffset && noteDuration >= half - lenghtOffset)
            {
                noteDimension = "half";
            }

            if (noteDuration <= quarter + lenghtOffset && noteDuration >= quarter - lenghtOffset)
            {
                noteDimension = "quarter";
            }

            if (noteDuration <= eight + 0.1f && noteDuration >= eight - 0.1f)
            {
                noteDimension = "eight";
            }

            if (noteDuration <= sixth + 0.1f && noteDuration >= sixth - 0.1f)
            {
                noteDimension = "sixth";
            }

            if (noteDuration <= thitrySecond + 0.05f && noteDuration >= thitrySecond - 0.05f)
            {
                noteDimension = "thirtysecond";
            }


        return noteDimension;
        }
    #endregion


    /* void OnGUI ()
     {
         if (GUI.Button (new Rect (0, 0, 300, 50), "Reset")) {
             ResetAndPlay ();
         }
     }*/

    // UI Handler Part------------------------------------------------------------------------------------------------------------------------
    #region UI
    public void OpenSubMenu()
        {
            if (SubMenu.gameObject.activeSelf)
            {
                SubMenu.gameObject.SetActive(false);
                CalibrationMenu.gameObject.SetActive(false);
                SaveDeleteLoadMenu.gameObject.SetActive(false);
            }
            else
            {
                SubMenu.gameObject.SetActive(true);
            }
        }

        public void Calibrating(string direction)
        {
            firstLanes = GameObject.FindGameObjectsWithTag("FirstLine");
            secondLanes = GameObject.FindGameObjectsWithTag("SecondLine");
            float offsetScale = 0.0005f; //0.00005f
            float offsetHeight = 0.005f;
            float offsetPosition = 0.005f;
            switch (direction)
            {
                case "more":
                    newScale = keyboard.localScale = new Vector3(keyboard.localScale.x + offsetScale, keyboard.localScale.y, keyboard.localScale.z);
                    break;
                case "less":
                    newScale = keyboard.localScale = new Vector3(keyboard.localScale.x - offsetScale, keyboard.localScale.y, keyboard.localScale.z);
                    break;
                case "right":
                    newPosition = keyboard.position = new Vector3(keyboard.position.x + offsetPosition, keyboard.position.y, keyboard.position.z);
                    break;
                case "left":
                    newPosition = keyboard.position = new Vector3(keyboard.position.x - offsetPosition, keyboard.position.y, keyboard.position.z);
                    break;
                case "up":
                    newHeight = keyboard.position = new Vector3(keyboard.position.x, keyboard.position.y, keyboard.position.z + offsetHeight);
                    break;
                case "down":
                    newHeight = keyboard.position = new Vector3(keyboard.position.x, keyboard.position.y, keyboard.position.z - offsetHeight);
                    break;
            }

        }

        public void OpenCalibrationMenu()
        {
            if (CalibrationMenu.gameObject.activeSelf)
            {
                CalibrationMenu.gameObject.SetActive(false);
                SaveDeleteLoadMenu.gameObject.SetActive(false);
            }
            else
            {
                CalibrationMenu.gameObject.SetActive(true);
                SaveDeleteLoadMenu.gameObject.SetActive(true);
            }
        }

        public void StartPlayingSong()
        {
            startSong = true;
            ResetAndPlay();
            //Debug.Log(startSong);
        }

        #region SaveLoadDeleteMenu
       /* public void OpenSaveLoadDeleteMenu()
        {
            if (SaveDeleteLoadMenu.gameObject.activeSelf)
            {
                SaveDeleteLoadMenu.gameObject.SetActive(false);
            }
            else
            {
                SaveDeleteLoadMenu.gameObject.SetActive(true);
            }
        }*/

        public void Save()
        {
            PlayerPrefs.SetFloat("Position",newPosition.x);
            PlayerPrefs.SetFloat("Scale", newScale.x);
            PlayerPrefs.SetFloat("Height", newHeight.z);
        }

        public void Load()
        {
            float loadPos = PlayerPrefs.GetFloat("Position", -0.952f);
            float loadScale = PlayerPrefs.GetFloat("Scale", 0.2f);
            float loadHeight = PlayerPrefs.GetFloat("Height", -0.516f);
            keyboard.localScale = new Vector3(loadScale, keyboard.localScale.y, keyboard.localScale.z);
            keyboard.position = new Vector3(loadPos, keyboard.position.y, loadHeight);         
        }

        public void Delete()
        {
            PlayerPrefs.DeleteAll();
        }
        #endregion

        #endregion

    }
