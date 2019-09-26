using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEditor;

[Serializable]
public class PenLight
{
    public string name;             //야광봉이름
    public int itemIndex;           //야광봉 인덱스(업적 클리어시 값 변경)
    public string getDate;             //야광봉 획득일자
    public string description;      //야광봉 설명
    public string rarity;           //야광봉 희귀도
    public bool _lock;               //잠금
    public bool use;

    [Serializable]
    public struct RGBData
    {
        public int R;
        public int G;
        public int B;
    }

    public RGBData RGB;
}

[Serializable]
public class Achievement
{
    public string name;
    public int achiveIndex;
    public int getData;
    public string description;
    public bool _lock;
}

[Serializable]

public class Song
{
    public string Title;
    public string Artist;
    public float BPM;

    public string GetData(int Index)
    {
        switch (Index)
        {
            case 0:
                return Title;
            case 1:
                return Artist;
            case 2:
                return "BPM : " + BPM.ToString();

            default:
                return "Errer";
        }
    }
}

class RSC
{


}

public static class JsonHellper
{
    public static T[] FromJson<T>(string json, int value)
    {
        switch (value)
        {
            case 1:
                pen<T> pen = JsonUtility.FromJson<pen<T>>(json);

                return pen.Penlight;
            case 2:
                ach<T> ach = JsonUtility.FromJson<ach<T>>(json);
                return ach.Achievement;
            default:
                song<T> song = JsonUtility.FromJson<song<T>>(json);
                return song.Song;
        }
    }

    public static string ToJson<T>(T[] array, int value)
    {
        switch (value)
        {
            case 1:
                pen<T> pen = new pen<T>();
                pen.Penlight = array;
                return JsonUtility.ToJson(pen);


            case 2:
                ach<T> ach = new ach<T>();
                ach.Achievement = array;
                return JsonUtility.ToJson(ach);
            default:
                song<T> song = new song<T>();
                song.Song = array;
                return JsonUtility.ToJson(song);
        }
    }

    public static string ToJson<T>(T[] array, bool prettyPrint, int value)
    {
        switch (value)
        {
            case 1:
                pen<T> pen = new pen<T>();
                pen.Penlight = array;
                return JsonUtility.ToJson(pen, prettyPrint);


            case 2:
                ach<T> ach = new ach<T>();
                ach.Achievement = array;
                return JsonUtility.ToJson(ach, prettyPrint);
            default:
                song<T> song = new song<T>();
                song.Song = array;
                return JsonUtility.ToJson(song, prettyPrint);

        }
    }

    private class pen<T>
    {
        public T[] Penlight;
    }

    private class ach<T>
    {
        public T[] Achievement;
    }

    private class song<T>
    {
        public T[] Song;
    }

}

public class Json : MonoBehaviour
{
    public PenLight[] penLightDatas;
    public Achievement[] achievements;
    public static Song[] song;
    private string songjson = "/Resources/Saves/Song.json";
    private string penLightjson = "/Resources/Saves/PenLight.json";
    private string achievementjson = "/Resources/Saves/Achievement.json";
    private int Count;                      //데이터 카운트
    private int ButtonCheck = 1;            //정렬 기준
    private int ListCount;
    public int ButtonCount = 0;            //컬렉션 버튼 카운트
    public int DataValue = 0;
    public GameObject ListObject;
    public GameObject[] ControllerObject;
    public GameObject ViewController;
    private UIManager uimanager;
    bool Check = false;
    public void ListPage(int PageList)
    {
        switch (PageList)
        {
            case 1:
                if (penLightDatas.Length > ButtonCount + 4)
                    ButtonCount += 4;
                else
                {
                    ButtonCount += penLightDatas.Length - ButtonCount;
                }
                break;

            case 2:
                if (ButtonCount - 4 > 0)
                    ButtonCount -= 4;
                else
                {
                    ButtonCount -= Mathf.Abs(4 - ButtonCount);
                }
                break;
        }
    }

    public void Save()
    {
        string objectDataJson = JsonHellper.ToJson(penLightDatas, prettyPrint: true, 1);
        File.WriteAllText(Application.dataPath + penLightjson, objectDataJson);
        objectDataJson = JsonHellper.ToJson(achievements, prettyPrint: true, 2);
        File.WriteAllText(Application.dataPath + achievementjson, objectDataJson);
    }

    public void Load()
    {

        TextAsset pendataLoad = Resources.Load("Saves/Penlight") as TextAsset;
        penLightDatas = JsonHellper.FromJson<PenLight>(pendataLoad.ToString(), 1);


        Count = penLightDatas.Length;
        Debug.Log("파일 불러오기");

        TextAsset achdataLoad = Resources.Load("Saves/Achievement") as TextAsset;
        achievements = JsonHellper.FromJson<Achievement>(achdataLoad.ToString(), 2);


        TextAsset songdataLoad = Resources.Load("Saves/Song") as TextAsset;
        song = JsonHellper.FromJson<Song>(songdataLoad.ToString(), 3);
    }


    public void Sort()
    {
        List<PenLight> penLightDataList = new List<PenLight>();
        for (int i = 0; i < penLightDatas.Length; i++)
        {
            penLightDataList.Add(penLightDatas[i]);
        }

        foreach (PenLight pen in penLightDataList)
        {
            if (pen._lock)
            {
                penLightDataList.Sort(delegate (PenLight a, PenLight b)
                {
                    int iCom = 1;
                    if (a._lock && b._lock)
                    {
                        switch (ButtonCheck)
                        {
                            case 1:
                                Debug.Log(a.name + " " + b.name + b.itemIndex.CompareTo(a.itemIndex));
                                return b.itemIndex.CompareTo(a.itemIndex);

                            case 2:
                                Debug.Log(a.name + " " + b.name + a.itemIndex.CompareTo(b.itemIndex));
                                return a.itemIndex.CompareTo(b.itemIndex);

                            case 3:
                                if (a.rarity == "Epic")
                                {
                                    if (a.rarity != b.rarity)
                                    {
                                        return 1;
                                    }
                                }
                                else if (a.rarity == "Normal")
                                {
                                    return -1;
                                }
                                else if (a.rarity == "Rare")
                                {
                                    if (b.rarity == "Epic")
                                    {
                                        return -1;
                                    }
                                }
                                if (a.rarity == b.rarity)
                                {
                                    return a.itemIndex.CompareTo(b.itemIndex);
                                }
                                break;

                            case 4:
                                if (a.rarity == "Epic")
                                {
                                    return -1;
                                }
                                else if (a.rarity == "Normal")
                                {
                                    if (a.rarity != b.rarity)
                                    {
                                        return 1;
                                    }
                                }
                                else if (a.rarity == "Rare")
                                {
                                    if (b.rarity == "Normal")
                                    {
                                        return -1;
                                    }
                                }
                                if (a.rarity == b.rarity)
                                {
                                    return a.itemIndex.CompareTo(b.itemIndex);
                                }
                                break;
                        }
                    }
                    else if (a._lock)
                    {
                        iCom = -1;
                    }
                    return iCom;
                });
            }
        }
        penLightDatas = penLightDataList.ToArray();
    }

    public void CheckChange(int sortList)
    {
        if (sortList == 1)
        {
            switch (ButtonCheck)
            {
                case 1:
                case 3:
                    ButtonCheck = 2;
                    break;

                case 2:
                case 4:
                    ButtonCheck = 1;
                    break;
            }
        }
        else if (sortList == 2)
        {
            switch (ButtonCheck)
            {
                case 1:
                case 3:
                    ButtonCheck = 4;
                    break;

                case 2:
                case 4:
                    ButtonCheck = 3;
                    break;
            }
        }
        uimanager.ColButtonSprite(ButtonCheck);
    }

    private void InitController()
    {
        ControllerObject = new GameObject[penLightDatas.Length];
        ListCount = 0;

        foreach (PenLight pen in penLightDatas)
        {


            ControllerObject[penLightDatas[ListCount].itemIndex - 1] = Instantiate(Resources.Load("PenLight/" + pen.name) as GameObject);
            ControllerObject[penLightDatas[ListCount].itemIndex - 1].SetActive(false);
            ListCount++;

        }
    }

    public void ChageController(int Value)
    {
        DataValue = Value;
        Debug.LogError(Value);
        GameObject Data = Instantiate(ControllerObject[penLightDatas[Value].itemIndex - 1], ViewController.transform.position, ViewController.transform.rotation, ViewController.transform.parent);
        Data.transform.localScale = ViewController.transform.localScale;
        Data.SetActive(true);
        if (penLightDatas[ButtonCount + DataValue].name == "Penlight")
        {
            Data.transform.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            Data.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(penLightDatas[ButtonCount + Value].RGB.R, penLightDatas[ButtonCount + Value].RGB.G, penLightDatas[ButtonCount + Value].RGB.B));
        }
        Destroy(ViewController);
        ViewController = Data;
        if (penLightDatas[ButtonCount + DataValue].use)
        {
            uimanager.ButtonDisenble();
        }
        else
        {
            uimanager.Buttonenble();
        }
    }

    public void ChangeControllerModel()
    {
        ParticleEffter particleParent = GameObject.Find("ParticleParent").GetComponent<ParticleEffter>();
        GameObject Right, Left;
        List<PenLight> Data = new List<PenLight>();
        particleParent.ParticleColorChange(penLightDatas, ButtonCount, DataValue);
        for (int i = 0; i < penLightDatas.Length; i++)
        {
            penLightDatas[i].use = false;
        }
        penLightDatas[ButtonCount + DataValue].use = true;
        if (penLightDatas[ButtonCount + DataValue].use)
        {
            uimanager.ButtonDisenble();
        }
        else
        {
            uimanager.Buttonenble();
        }
        if (GameObject.Find("Controller (right)").transform.childCount > 2)
        {
            Right = GameObject.Find("Controller (right)").transform.GetChild(2).gameObject;
            Right.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            GameObject RightData = Instantiate(ControllerObject[penLightDatas[ButtonCount + DataValue].itemIndex - 1], Right.transform.position, Right.transform.rotation, Right.transform.parent);
            if (penLightDatas[ButtonCount + DataValue].name == "Penlight")
            {
                RightData.transform.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
            for (int i = 0; i < Right.transform.childCount; i++)
            {
                Destroy(Right.transform.GetChild(i).gameObject);
            }
            while (RightData.transform.childCount != 0)
            {
                RightData.transform.GetChild(0).parent = Right.transform;
            }

            Destroy(RightData);
            //for (int i = 0; i < 2; i++)
            //{
            //    Right.transform.GetChild(i).GetComponent<MeshFilter>().mesh = ViewController.transform.GetChild(i).GetComponent<MeshFilter>().mesh;
            //    Right.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_EmissionColor", ViewController.transform.GetChild(i).GetComponent<Renderer>().material.GetColor("_EmissionColor"));
            //}
            Right.transform.parent.GetChild(0).GetComponent<LineRenderer>().material.SetColor("_EmissionColor", new Color(penLightDatas[ButtonCount + DataValue].RGB.R / 255f, penLightDatas[ButtonCount + DataValue].RGB.G / 255f, penLightDatas[ButtonCount + DataValue].RGB.B / 255f));
            Right.transform.parent.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EsmissionColor", new Color(penLightDatas[ButtonCount + DataValue].RGB.R / 255f, penLightDatas[ButtonCount + DataValue].RGB.G / 255f, penLightDatas[ButtonCount + DataValue].RGB.B / 255f));
        }
        if (GameObject.Find("Controller (left)").transform.childCount > 2)
        {
            Left = GameObject.Find("Controller (left)").transform.GetChild(2).gameObject;
            Left.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            GameObject LeftData = Instantiate(ControllerObject[penLightDatas[ButtonCount + DataValue].itemIndex - 1], Left.transform.position, Left.transform.rotation, Left.transform.parent);
            if (penLightDatas[ButtonCount + DataValue].name == "Penlight")
            {
                LeftData.transform.GetChild(1).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
            for (int i = 0; i < Left.transform.childCount; i++)
            {
                Destroy(Left.transform.GetChild(i).gameObject);
            }
            while (LeftData.transform.childCount != 0)
            {
                LeftData.transform.GetChild(0).parent = Left.transform;
            }

            Destroy(LeftData);
            //for (int i = 0; i < 2; i++)
            //{
            //    Left.transform.GetChild(i).GetComponent<MeshFilter>().mesh = ViewController.transform.GetChild(i).GetComponent<MeshFilter>().mesh;
            //    Left.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_EmissionColor", ViewController.transform.GetChild(i).GetComponent<Renderer>().material.GetColor("_EmissionColor"));
            //}
            Left.transform.parent.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(penLightDatas[ButtonCount + DataValue].RGB.R / 255f, penLightDatas[ButtonCount + DataValue].RGB.G / 255f, penLightDatas[ButtonCount + DataValue].RGB.B / 255f));
            Left.transform.parent.GetChild(0).GetComponent<LineRenderer>().material.SetColor("_EmissionColor", new Color(penLightDatas[ButtonCount + DataValue].RGB.R / 255f, penLightDatas[ButtonCount + DataValue].RGB.G / 255f, penLightDatas[ButtonCount + DataValue].RGB.B / 255f));
        }
    }

    public void PenLightName(GameObject data)
    {
        uimanager.SetPenLightName(data, penLightDatas[ButtonCount + DataValue].name, penLightDatas[ButtonCount + DataValue].description);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Load();
        uimanager = GameObject.Find("UIManager").GetComponent<UIManager>();
        ButtonCheck = 2;
        InitController();
        DataValue = PlayerPrefs.GetInt("StartValue");
        ButtonCount = PlayerPrefs.GetInt("StartCount");
        ChageController(ButtonCount + DataValue);
        uimanager.ColSelect(DataValue);
        uimanager.SetPenLightName(penLightDatas[DataValue + ButtonCount].name, penLightDatas[DataValue + ButtonCount].description);
    }

    // Update is called once per frame
    private void Update()
    {
        if (ListCount < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                ListObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = ListCount; i < 4; i++)
            {
                ListObject.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            ListObject.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = penLightDatas[ButtonCount + i].name;
            ListObject.transform.GetChild(i).GetChild(1).GetComponent<Text>().text = penLightDatas[ButtonCount + i].rarity;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("StartValue", DataValue);
        PlayerPrefs.SetInt("StartCount", ButtonCount);
        Save();
    }
}