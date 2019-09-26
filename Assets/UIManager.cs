using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Valve.VR;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private int nScore;
    public Text tScore;
    public int nCombo;
    private int nBestCombo;
    public static int nMaxScore;
    public static int nMissCount;
    public Text tCombo;
    [SerializeField]
    private Canvas canvas;
    private bool HUD;
    private bool effter;
    private bool Mode;

    [SerializeField]
    private Sprite[] On;
    [SerializeField]
    private Sprite[] Off;
    [SerializeField]
    private Sprite selectImage;
    [SerializeField]
    private Sprite noSelectImage;
    [SerializeField]
    private Sprite[] ranksImage;
    [SerializeField]
    private Sprite[] musicsImage;

    private int musicIndex;
    public Button gSelectButton;
    public Point[] pointers = new Point[2];
    public UiController uiController;
    [SerializeField]
    private int pointerIndex;

    private int selectIndex;

    private string sPreSceneName;

    [SerializeField]
    private Sprite[] Date;
    [SerializeField]
    private Sprite[] Rarity;
    private void Awake()
    {
        sPreSceneName = SceneManager.GetActiveScene().name;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(this);
        HUD = true;
        effter = true;
       
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameObject musicList = canvas.transform.GetChild(1).GetChild(4).gameObject;
        for (int i = 0; i < musicList.transform.childCount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                musicList.transform.GetChild(i).GetChild(j).GetComponent<Text>().text = Json.song[i].GetData(j);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != sPreSceneName)
        {
            SteamVR_Behaviour_Pose[] poses = GameObject.FindObjectsOfType<SteamVR_Behaviour_Pose>();
            for (int i = 0; i < poses.Length; i++)
            {
                poses[i].enabled = false;
                poses[i].enabled = true;
                poses[i].GetComponent<TestController>().SetData();
            }
            sPreSceneName = SceneManager.GetActiveScene().name;
        }
    }

    public void ColSelect(int Index)
    {
        canvas.transform.GetChild(2).GetChild(0).GetChild(5).GetChild(selectIndex).GetComponent<Button>().image.sprite = noSelectImage;
        canvas.transform.GetChild(2).GetChild(0).GetChild(5).GetChild(Index).GetComponent<Button>().image.sprite = selectImage;
        selectIndex = Index;
    }

    public void MusicSelectImage(Image musicImage)
    {
      for(int i=0; i<  musicImage.transform.parent.childCount; i++)
        {
            musicImage.transform.parent.GetChild(i).GetComponent<Image>().sprite = noSelectImage;
        }
        musicImage.sprite = selectImage;
    }

    public void ButtonDisenble()
    {
        gSelectButton.interactable = false;
        Color a = new Color(1, 1, 1, 0.5f);
        gSelectButton.GetComponent<ButtonTransitioner>().m_NormalColor = a;
        gSelectButton.GetComponent<ButtonTransitioner>().Check = true;
        gSelectButton.image.color = a;
    }

    public void Buttonenble()
    {
        gSelectButton.interactable = true;
        gSelectButton.GetComponent<ButtonTransitioner>().m_NormalColor = Color.white;
        gSelectButton.GetComponent<ButtonTransitioner>().Check = false;
        gSelectButton.image.color = Color.white;
    }


    public void SceneLoad(int LoadIndex)
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(SceneChange(LoadIndex));
        
    }

    private IEnumerator SceneChange(int LoadIndex)
    {
        if(musicIndex == 0 && LoadIndex != 0)
        {
            LoadIndex = 2;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadIndex);
        asyncOperation.allowSceneActivation = false;
        while (true)
        {

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }
            Debug.Log(asyncOperation.progress);
            yield return null;
            
        }
        asyncOperation.allowSceneActivation = true;
       
        switch (LoadIndex)
        {
            case 0:
                pointers[pointerIndex].gameObject.SetActive(true);
                uiController.SetCamera(pointers[pointerIndex]);
                VRInputModule.m_camera = pointers[pointerIndex].GetComponent<Camera>();
                if (musicIndex != 0)
                {
                    tScore.text = string.Format(" {0:D6}", nScore);
                    tCombo.text = string.Format(" {0:D3}", nCombo);
                    canvas.transform.GetChild(6).gameObject.SetActive(true);
                    GameEnd(true);
                }
                else
                {
                    canvas.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case 1:
                nCombo = nScore = nBestCombo =  0;
                tScore.text = string.Format("Score : {0:D6}", nScore);
                tCombo.text = string.Format("x {0:D3}", nCombo);
                if (HUD == true)
                {
                    canvas.transform.GetChild(5).gameObject.SetActive(true);
                }
                pointers[pointerIndex].gameObject.SetActive(false);
                break;
            case 2:
                pointers[pointerIndex].gameObject.SetActive(false);
                canvas.transform.GetChild(7).gameObject.SetActive(true);
                break;
        }
       
    }

    public void MusicSelect(GameObject DisObject)
    {
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        DisObject.SetActive(false);
    }

    public void MainObject(int Value)
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(Value).gameObject.SetActive(false);
    }

    public void Collection()
    {
        canvas.transform.GetChild(2).gameObject.SetActive(true);
        canvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Setting()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void GetScore(int sc)
    {
        nScore += sc;
        tScore.text = string.Format("Score : {0:D6}", nScore);
    }

    public void OnButtonDown(GameObject ParentObject)
    {
        ParentObject.transform.GetChild(0).GetComponent<Image>().sprite = Off[0];
        ParentObject.transform.GetChild(1).GetComponent<Image>().sprite = On[1];
    }

    public void OffButtonDown(GameObject ParentObject)
    {
        ParentObject.transform.GetChild(0).GetComponent<Image>().sprite = Off[1];
        ParentObject.transform.GetChild(1).GetComponent<Image>().sprite = On[0];
    }

    public void ButtonDownList(int Value)
    {
        switch (Value)
        {
            case 1:
                HUD = true;
                break;

            case 2:
                effter = true;
                break;
        }
    }

    public void ButtonUpList(int Value)
    {
        switch (Value)
        {
            case 1:
                HUD = false;
                break;

            case 2:
                effter = false;
                break;
        }
    }

    public void PlayerPos(GameObject Slider)
    {
        GameObject.Find("GameObject").transform.position += new Vector3(0, Slider.GetComponent<Slider>().value) * Time.deltaTime;
    }

    public void GetCombo()
    {
        
        if(nCombo == nBestCombo)
        {
            nBestCombo++;
        }
        nCombo += 1;
        tCombo.text = string.Format("x {0:D3}", nCombo);
    }

    public void ResetCombo()
    {
        if(nCombo> nBestCombo)
        {
            nBestCombo = nCombo;
        }
        nCombo = 0;
        tCombo.text = string.Format("x {0:D3}", nCombo);
    }

    public void SetPointer(int Index, Point point)
    {
        pointers[Index] = point;
        if (point.gameObject.activeSelf)
        {
            uiController.SetCamera(point);
            VRInputModule.m_camera = point.GetComponent<Camera>();
            pointerIndex = Index;
        }
    }
    public void SetPointerIndex(int Index)
    {
        switch (Index)
        {
            case 0:
                pointerIndex = 1;
                break;
            case 1:
                pointerIndex = 0;
                break;
        }
        uiController.SetCamera(pointers[pointerIndex]);
    }
    public void SetPointerIndex(int Index, bool IndexData)
    {
        pointerIndex = Index;
        uiController.SetCamera(pointers[pointerIndex]);
    }
    public void SetPenLightName(string name, string ex)
    {
        canvas.transform.GetChild(2).GetChild(0).GetChild(6).GetChild(0).GetComponent<Text>().text = name;
        canvas.transform.GetChild(2).GetChild(0).GetChild(6).GetChild(1).GetComponent<Text>().text = ex;
    }

    public void SetPenLightName(GameObject data, string name, string ex)
    {
        data.transform.GetChild(0).GetComponent<Text>().text = name;
        data.transform.GetChild(1).GetComponent<Text>().text = ex;
    }

    public bool GetEffterCheck()
    {
        return effter;
    }

    public void GameEnd(bool EndList)
    {
        GameObject uiData = canvas.transform.GetChild(6).gameObject;
        uiData.SetActive(true);
        uiData = uiData.transform.GetChild(0).gameObject;
        uiData.transform.GetChild(0).GetComponent<Image>().sprite = musicsImage[musicIndex];
        uiData.transform.GetChild(4).GetComponent<Text>().text = tScore.text;
        uiData.transform.GetChild(6).GetComponent<Text>().text = tCombo.text;
        RankSystem();
        if (nScore > PlayerPrefs.GetInt(Json.song[musicIndex].Title + "Score"))
        {
            PlayerPrefs.SetInt(Json.song[musicIndex].Title + "Score", nScore);
        }
        if(nBestCombo > PlayerPrefs.GetInt(Json.song[musicIndex].Title + "Combo"))
        {
            PlayerPrefs.SetInt(Json.song[musicIndex].Title + "Combo", nBestCombo);
        }

        uiData.transform.GetChild(5).GetComponent<Text>().text = PlayerPrefs.GetInt(Json.song[musicIndex].Title + "Score").ToString();
        uiData.transform.GetChild(7).GetComponent<Text>().text = PlayerPrefs.GetInt(Json.song[musicIndex].Title + "Combo").ToString();
    }

    public void SetMusicData(int Index)
    {
        musicIndex = Index;
        GameObject musicSelect = canvas.transform.GetChild(1).GetChild(3).gameObject;
        GameObject gameEnd = canvas.transform.GetChild(6).GetChild(1).gameObject;
        Image musicImage = canvas.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Image>();
        for (int i = 0; i < 3; i++)
        {
            gameEnd.transform.GetChild(i).GetComponent<Text>().text = musicSelect.transform.GetChild(i).GetComponent<Text>().text = Json.song[musicIndex].GetData(i);
        }
        musicImage.sprite = musicsImage[Index];
        
    }

    public void RankSystem()
    {
        Image rankImage = canvas.transform.GetChild(6).GetChild(0).GetChild(1).GetComponent<Image>();
        if (nScore >= nMaxScore)
        {
            rankImage.sprite = ranksImage[0];
        }
        else if (nScore < nMaxScore && nScore >= nMaxScore * 0.95f)
        {
            rankImage.sprite = ranksImage[1];
        }
        else if (nScore < nMaxScore * 0.85f && nScore >= nMaxScore * 0.85f)
        {
            rankImage.sprite = ranksImage[2];
        }
        else if (nScore < nMaxScore * 0.85 && nScore > nMaxScore * 0.7f)
        {
            rankImage.sprite = ranksImage[3];
        }
        else if (nScore < nMaxScore * 0.7f && nScore > nMaxScore * 0.55f)
        {
            rankImage.sprite = ranksImage[4];
        }
        else
        {
            rankImage.sprite = ranksImage[5];
        }
    }

    public void ColButtonSprite(int Index)
    {
        Image DateImage = canvas.transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<Image>();
        Image RarityImage = canvas.transform.GetChild(2).GetChild(0).GetChild(4).GetComponent<Image>();
        switch (Index)

        {
            case 1:
            case 2:
                if(Index == 1)
                {
                    DateImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    DateImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                }
                DateImage.transform.GetChild(0).gameObject.SetActive(true);
                RarityImage.transform.GetChild(0).gameObject.SetActive(false);
                DateImage.sprite = Date[0];
                RarityImage.sprite = Rarity[1];
                break;
            case 3:
            case 4:
                if (Index == 3)
                {
                    RarityImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    RarityImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0,0);
                }
                DateImage.transform.GetChild(0).gameObject.SetActive(false);
                RarityImage.transform.GetChild(0).gameObject.SetActive(true);
                DateImage.sprite = Date[1];
                RarityImage.sprite = Rarity[0];
                break;
        }

    }
}