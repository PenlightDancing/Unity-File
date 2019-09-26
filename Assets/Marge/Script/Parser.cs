using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public TextAsset textAsset;

    private GameObject prevObject = null;
    private GameObject curObject = null;

    public GameObject line;

    // Start is called before the first frame update
    private void Start()
    {
        ParseSongTextAsset();
        Generator.SetMaxScore();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void ParseSongTextAsset()
    {
        List<string> list = new List<string>(textAsset.text.Split('\n'));
        SoundManager.instance.curBPM = float.Parse(list[0].Split(',')[0]);
        for (int i = 1; i < list.Count; i++)
        {
            Process(list[i]);
        }
    }

    private void Process(string k)
    {
        string[] key = k.Split(',');
        Valve.VR.SteamVR_Input_Sources applyHand = Valve.VR.SteamVR_Input_Sources.Any;
        switch (key[0])
        {
            case "Normal":
                switch (key[3])
                {
                    case "L":
                        applyHand = Valve.VR.SteamVR_Input_Sources.LeftHand;
                        break;

                    case "R":
                        applyHand = Valve.VR.SteamVR_Input_Sources.RightHand;
                        break;
                }
                curObject = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), applyHand);
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);

                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = curObject;
                break;

            case "Long":
                List<int> middleIndex = new List<int>();
                int i = 0;
                for (i = 4; key[i] != ")"; i++)
                {
                    middleIndex.Add(int.Parse(key[i]));
                }
                switch (key[i + 3])
                {
                    case "L":
                        applyHand = Valve.VR.SteamVR_Input_Sources.LeftHand;
                        break;

                    case "R":
                        applyHand = Valve.VR.SteamVR_Input_Sources.RightHand;
                        break;
                }
                GameObject[] _result = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), middleIndex, int.Parse(key[i + 1]), float.Parse(key[i + 2]), applyHand);
                curObject = _result[0];
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);
                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = _result[1];
                break;

            case "Spin":
                curObject = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), int.Parse(key[3]), float.Parse(key[4]));
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);
                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = curObject;
                break;

            case "LongSpin":
                List<int> spinMiddleIndex = new List<int>();
                int j = 0;
                for (j = 4; key[j] != ")"; j++)
                {
                    spinMiddleIndex.Add(int.Parse(key[j]));
                }
                GameObject[] _midleresult = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), spinMiddleIndex, int.Parse(key[j + 1]), int.Parse(key[j + 2]), float.Parse(key[j + 3]), float.Parse(key[j + 4]));
                curObject = _midleresult[0];
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);
                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = _midleresult[1];
                break;

            case "Slide":
                switch (key[4])
                {
                    case "L":
                        applyHand = Valve.VR.SteamVR_Input_Sources.LeftHand;
                        break;

                    case "R":
                        applyHand = Valve.VR.SteamVR_Input_Sources.RightHand;
                        break;

                    default:
                        Debug.Log("Slide Error");
                        break;
                }
                curObject = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), float.Parse(key[3]), applyHand);
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);
                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = curObject;
                break;

            case "Yonta":
                List<float> yontaTimes = new List<float>();
                int e = 0;
                for (e = 5; key[e] != ")"; e++)
                {
                    yontaTimes.Add(float.Parse(key[e]));
                }
                switch (key[e + 1])
                {
                    case "L":
                        applyHand = Valve.VR.SteamVR_Input_Sources.LeftHand;
                        break;

                    case "R":
                        applyHand = Valve.VR.SteamVR_Input_Sources.RightHand;
                        break;
                }
                curObject = Generator.instance.GenerateNote(float.Parse(key[1]), int.Parse(key[2]), int.Parse(key[3]), yontaTimes, applyHand);
                if (prevObject != null)
                {
                    prevObject.GetComponent<NoteBase>().line = Instantiate(line, prevObject.transform);
                    GameObject _line = prevObject.GetComponent<NoteBase>().line;
                    _line.GetComponent<LineRenderer>().SetPosition(0, prevObject.transform.position);
                    _line.GetComponent<LineRenderer>().SetPosition(1, curObject.transform.position);
                    prevObject.GetComponent<NoteBase>().nextNote = curObject;
                }
                prevObject = curObject;
                break;

            case "ChangeBPM":
                Generator.instance.GenerateChangeBPM(float.Parse(key[1]), float.Parse(key[2]));
                break;

            default:
                Debug.LogWarning("시발 님아 파일 이상한거 끌고왔음\n" + k);
                break;
        }
    }
}