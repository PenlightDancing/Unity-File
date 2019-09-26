using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Generator : MonoBehaviour
{
    public static Generator instance;
    private Transform cameraPos;

    public float prevTime;

    public int offset;

    public GameObject normalNote;
    public GameObject slideNote;

    public GameObject longStartNote;
    public GameObject longMiddleNote;
    public GameObject longEndNote;

    public Transform noteBunddle;

    public GameObject spinNote;
    public GameObject longSpinStartNote;
    public GameObject longSpinMiddleNote;
    public GameObject longSpinEndNote;

    public GameObject yontaNote;

    public GameObject changeBPM;

    public GameObject yontaWings;
    public static int maxScore;
    public static int maxCombo;

    public List<Vector3> genPos = new List<Vector3>();

    private void Awake()
    {
        instance = this;
        maxScore = 0;
    }

    /// Start is called before the first frame update
    private void Start()
    {
        Debug.Log(noteBunddle.name);
    }

    [ContextMenu("ss")]
    private void T()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            genPos.Add(transform.GetChild(i).transform.position);
        }
    }

    private void Update()
    {
    }

    public void SetNoteWingSpeed(float bpm)
    {
        Debug.Log(prevTime);
        normalNote.GetComponent<NoteBase>().speed = 240 / (prevTime);
        longStartNote.GetComponent<NoteBase>().speed = 180 / (prevTime);
        spinNote.GetComponent<NoteBase>().speed = 240 / (prevTime);
        longSpinStartNote.GetComponent<NoteBase>().speed = 240 / prevTime;

        normalNote.GetComponent<NoteBase>().sizePerSec = 0.5f / (prevTime);
        longStartNote.GetComponent<NoteBase>().sizePerSec = 0.5f / (prevTime);
        spinNote.GetComponent<NoteBase>().sizePerSec = 0.5f / (prevTime);
        longSpinStartNote.GetComponent<NoteBase>().sizePerSec = 0.5f / prevTime;
        slideNote.GetComponent<NoteBase>().sizePerSec = 0.5f / prevTime;
        yontaNote.GetComponent<NoteBase>().sizePerSec = 0.5f / prevTime;
    }


    /// <summary>
    /// Create NormalNote
    /// </summary>
    /// <param name="index">Create Position</param>
    /// <param name="beat">Create Beat</param>
    public GameObject GenerateNote(float beat, int index, Valve.VR.SteamVR_Input_Sources hands)
    {
        beat += offset;
        GameObject note = Instantiate(normalNote, noteBunddle);
        note.name = "LongNote(" + beat + "," + index + ")";
        note.GetComponent<NoteBase>().noteTime = beat;
        note.GetComponent<NoteBase>().noteHands = hands;
        note.transform.position = genPos[index - 1];
        note.transform.LookAt(transform.position);
        note.transform.Rotate(0, 0, -90);
        NoteManager.instance.EnQueue(note.GetComponent<NoteBase>());
        maxScore += 300;
        maxCombo++;
        return note;
    }

    /// <summary>
    /// Create LongNote
    /// </summary>
    /// <param name="startIndex">Create Start Postion</param>
    /// <param name="middleIndex">Middle Postions</param>
    /// <param name="endIndex">Create End Postion</param>
    /// <param name="startTime">Start Note Beat</param>
    /// <param name="endBeat">End Note Beat</param>
    public GameObject[] GenerateNote(float startTime, int startIndex, List<int> middleIndex, int endIndex, float endBeat, Valve.VR.SteamVR_Input_Sources hands)
    {
        startTime += offset;
        endBeat += offset;
        GameObject startNote = Instantiate(longStartNote, noteBunddle);
        startNote.name = "LongNote(" + startTime + "," + startIndex + ")";
        startNote.GetComponent<LongNote>().isStartNote = true;
        startNote.GetComponent<LongNote>().noteTime = startTime;
        startNote.GetComponent<LongNote>().noteHands = hands;
        startNote.GetComponent<LongNote>().transform.position = genPos[startIndex - 1];
        startNote.transform.LookAt(Generator.instance.transform.position);
        startNote.GetComponent<LongNote>().sizePerSec = 0.5f / (prevTime);
        NoteManager.instance.EnQueue(startNote.GetComponent<NoteBase>());
        float middleBeat = ((float)(endBeat - startTime) / (middleIndex.Count + 1));
        startNote.GetComponent<LongNote>().unitBeat = middleBeat;
        for (int i = 0; i < middleIndex.Count; i++)
        {
            GameObject middleNote = Instantiate(longMiddleNote);
            middleNote.transform.position = genPos[middleIndex[i] - 1];
            middleNote.transform.LookAt(Generator.instance.transform.position);
            middleNote.transform.Rotate(90, 0, 0);
            middleNote.transform.parent = startNote.transform;
            startNote.GetComponent<LongNote>().nextPos.Enqueue(middleNote);
        }
        GameObject endNote = Instantiate(longEndNote);

        endNote.transform.position = genPos[endIndex - 1];
        endNote.transform.LookAt(Generator.instance.transform.position);
        endNote.GetComponent<LongNote>().sizePerSec = 0.5f / ((prevTime + endBeat - startTime));
        startNote.GetComponent<LongNote>().nextPos.Enqueue(endNote);
        //endNote.transform.Rotate(-90, 0, 0);
        GameObject[] result = new GameObject[2];
        result[0] = startNote;
        result[1] = endNote;
        endNote.transform.parent = startNote.transform;
        maxScore += 150 * 2 + 20 * middleIndex.Count;
        maxCombo += 2 + middleIndex.Count;
        return result;
    }

    /// <summary>
    /// Create SpinNote
    /// </summary>
    /// <param name="index">Create Position</param>
    /// <param name="minSpin">Minimum Spin Count</param>
    /// <param name="stayBeat">Stay Note Time</param>
    /// <param name="beat">Create Beat</param>
    public GameObject GenerateNote(float beat, int index, int minSpin, float stayBeat)
    {
        beat += offset;
        GameObject note = Instantiate(spinNote, noteBunddle);
        note.name = "SpinNote(" + beat + "," + index + ")";
        note.transform.position = genPos[index - 1];
        transform.LookAt(Generator.instance.transform.position);
        note.transform.Rotate(0, 0, -90);
        note.GetComponent<SpinNote>().noteTime = beat;
        note.GetComponent<SpinNote>().stayBeat = stayBeat;
        note.GetComponent<SpinNote>().minSpin = minSpin;
        NoteManager.instance.EnQueue(note.GetComponent<NoteBase>());
        maxScore += 30 * minSpin;
        maxCombo += minSpin;
        return note;
    }

    /// <summary>
    /// Create Long Spin Note
    /// </summary>
    /// <param name="startIndex">Create Start Postion</param>
    /// <param name="middleIndex">Middle Postions</param>
    /// <param name="endIndex">Create End Postion</param>
    /// <param name="minSpin">Minimum Spin Count</param>
    /// <param name="startTime">Start Note Beat</param>
    /// <param name="endBeat">End Note Beat</param>
    public GameObject[] GenerateNote(float startTime, int startIndex, List<int> middleIndex, int endIndex, int minSpin, float stayBeat, float endBeat)
    {
        startTime += offset;
        endBeat += offset;
        GameObject startNote = Instantiate(longSpinStartNote, noteBunddle);
        startNote.name = "LongSpinNote(" + startTime + "," + startIndex + ")";
        startNote.GetComponent<LongSpinNote>().isStartNote = true;
        startNote.GetComponent<LongSpinNote>().minSpin = minSpin;
        startNote.GetComponent<NoteBase>().noteTime = startTime;
        startNote.transform.position = genPos[startIndex - 1];
        startNote.transform.LookAt(Generator.instance.transform.position);
        startNote.GetComponent<NoteBase>().sizePerSec = 0.5f / (prevTime);
        NoteManager.instance.EnQueue(startNote.GetComponent<NoteBase>());
        float middleBeat = ((float)(endBeat - startTime) / (middleIndex.Count + 1));
        // Debug.Log(startTime + " and " + endBeat + " = " + (endBeat - startTime));
        startNote.GetComponent<LongSpinNote>().unitBeat = middleBeat;
        for (int i = 0; i < middleIndex.Count; i++)
        {
            GameObject middleNote = Instantiate(longMiddleNote);
            middleNote.transform.position = genPos[middleIndex[i] - 1];
            middleNote.transform.LookAt(Generator.instance.transform.position);
            middleNote.transform.Rotate(90, 0, 0);
            middleNote.transform.parent = startNote.transform;
            startNote.GetComponent<LongSpinNote>().nextPos.Enqueue(middleNote);
        }
        GameObject endNote = Instantiate(longSpinEndNote);

        endNote.transform.position = genPos[endIndex - 1];
        endNote.transform.LookAt(Generator.instance.transform.position);
        endNote.GetComponent<LongSpinNote>().sizePerSec = 0.5f / ((prevTime + endBeat - startTime));
        startNote.GetComponent<LongSpinNote>().nextPos.Enqueue(endNote);
        //endNote.transform.Rotate(-90, 0, 0);
        GameObject[] result = new GameObject[2];
        result[0] = startNote;
        result[1] = endNote;
        endNote.transform.parent = startNote.transform;
        maxScore += 20 * minSpin;
        maxCombo += minSpin;
        return result;
    }

    /// <summary>
    /// Create Slide Note
    /// </summary>
    /// <param name="beat"></param>
    /// <param name="index"></param>
    /// <param name="rotate"></param>
    /// <param name="hands"></param>
    /// <returns></returns>
    public GameObject GenerateNote(float beat, int index, float rotate, Valve.VR.SteamVR_Input_Sources hands)
    {
        beat += offset;
        GameObject sldNote = Instantiate(slideNote, noteBunddle);
        sldNote.name = "SlideNote(" + beat + "," + index + ")";
        sldNote.GetComponent<NoteBase>().noteTime = beat;
        sldNote.GetComponent<NoteBase>().noteHands = hands;
        sldNote.GetComponent<SlideNote>().rotate = rotate;
        sldNote.transform.position = genPos[index - 1];
        NoteManager.instance.EnQueue(sldNote.GetComponent<NoteBase>());
        maxScore += 300;
        maxCombo++;
        return sldNote;
    }
    /// <summary>
    /// Create YonTa Note
    /// </summary>
    /// <param name="beat"></param>
    /// <param name="index"></param>
    /// <param name="yontaCount"></param>
    /// <param name="times"></param>
    /// <param name="hands"></param>
    /// <returns></returns>
    public GameObject GenerateNote(float beat, int index, int yontaCount, List<float> times, Valve.VR.SteamVR_Input_Sources hands)
    {
        beat += offset;
        GameObject yonNote = Instantiate(yontaNote, noteBunddle);
        for (int i = 0; i < yontaCount; i++)
        {
            yonNote.GetComponent<YontaNote>().targetWingQueue.Enqueue(Instantiate(yontaWings, yonNote.transform));
        }
        yonNote.name = "YonTaNote(" + beat + "," + index + ")";
        yonNote.GetComponent<NoteBase>().noteTime = beat;
        yonNote.GetComponent<NoteBase>().noteHands = hands;
        yonNote.GetComponent<YontaNote>().YonTaCount = yontaCount;
        yonNote.GetComponent<YontaNote>().sizeSecs.Enqueue(0.5f / prevTime);
        float b = beat ;
        for (int i = 0; i < times.Count; i++)
        {
            yonNote.GetComponent<YontaNote>().sizeSecs.Enqueue(0.5f / (times[i] - b));
            if(i>0)
                b = times[i];
        }
        yonNote.transform.position = genPos[index - 1];
        NoteManager.instance.EnQueue(yonNote.GetComponent<NoteBase>());
        maxScore += yontaCount * 300;
        maxCombo += yontaCount;
        return yonNote;
    }

    public void GenerateChangeBPM(float time, float bpm)
    {
        changeBPM.GetComponent<BPMChanger>().SetChangeBPM(bpm);
        changeBPM.GetComponent<BPMChanger>().noteTime = time;
    }

    public static void SetMaxScore()
    {
        Debug.Log("nMaxScore 함수 실행");
       UIManager.nMaxScore =  maxScore;
    }
}