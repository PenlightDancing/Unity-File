using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutoreal : MonoBehaviour
{
    public Sprite[] tutorealImage;
    int imageIndex = 0;
    float timer;
    bool[] take = new bool[6];
    GameObject tutorealObject;
    Image tutorealUIImage;
    // Start is called before the first frame update
    void Start()
    {
        tutorealObject = GameObject.Find("Canvas").transform.GetChild(7).gameObject;
        tutorealUIImage = tutorealObject.transform.GetChild(0).GetComponent<Image>();
        tutorealUIImage.sprite = tutorealImage[0];
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (take[0] == false)
        {
            OneTake(timer);
        }
        else if (take[1] == false)
        {
            TwoTake(timer);
        }
        else if (take[2] == false)
        {
            ThreeTake(timer);
        }
        else if (take[3] == false)
        {
            FourTake(timer);
        }
        else if(take[4] == false)
        {
            FiveTake(timer);
        }
        else if(take[5]== false)
        {
            SixTake(timer);
        }
    }

    void OneTake(float t)
    {
        if (t >= 29.032f)
        {
            take[0] = true;
            tutorealObject.SetActive(false);
            tutorealUIImage.sprite = tutorealImage[5];
        }
        else if (t >= 23.225f)
        {
            tutorealUIImage.sprite = tutorealImage[4];
        }
        else if (t >= 15.483f)
        {
            tutorealUIImage.sprite = tutorealImage[3];
        }
        else if (t >= 7.741f)
        {
            tutorealUIImage.sprite = tutorealImage[2];
        }
        else if (t >= 3.87f)
        {
            tutorealUIImage.sprite = tutorealImage[1];
        }
        else if (t >= 0.12f)
        {
            tutorealObject.SetActive(true);

        }
    }

    void TwoTake(float t)
    {
        if (t >= 67.741f)
        {
            take[1] = true;
            tutorealObject.SetActive(false);
            tutorealUIImage.sprite = tutorealImage[9];
        }
        else if (t >= 61.935f)
        {
            tutorealUIImage.sprite = tutorealImage[8];
        }
        else if (t >= 54.193f)
        {
            tutorealUIImage.sprite = tutorealImage[7];
        }
        else if (t >= 50.322f)
        {
            tutorealUIImage.sprite = tutorealImage[6];
        }
        else if (t >= 46.451f)
        {
            tutorealObject.SetActive(true);
        }
    }

    void ThreeTake(float t)
    {
        if (t >= 102.58f)
        {
            take[2] = true;
            tutorealObject.SetActive(false);
            tutorealUIImage.sprite = tutorealImage[13];
        }
        else if (t >= 100.645f)
        {
            tutorealUIImage.sprite = tutorealImage[12];
        }
        else if (t >= 92.903f)
        {
            tutorealUIImage.sprite = tutorealImage[11];
        }
        else if (t >= 89.032f)
        {
            tutorealUIImage.sprite = tutorealImage[10];
        }
        else if (t >= 85.161)
        {
            tutorealObject.SetActive(true);
        }
    }

    void FourTake(float t)
    {
        if (t >= 131.612f)
        {
            take[3] = true;
            tutorealObject.SetActive(false);
            tutorealUIImage.sprite = tutorealImage[17];
        }
        else if (t >= 125.806f)
        {
            tutorealUIImage.sprite = tutorealImage[16];
        }
        else if (t >= 118.064f)
        {
            tutorealUIImage.sprite = tutorealImage[15];
        }
        else if (t >= 114.193)
        {
            tutorealUIImage.sprite = tutorealImage[14];

        }
        else if (t >= 110.322)
        {
            tutorealObject.SetActive(true);
        }
    }

    void FiveTake(float t)
    {
        if (t >= 162.58f)
        {
            take[4] = true;
            tutorealObject.SetActive(false);
            tutorealUIImage.sprite = tutorealImage[21];
        }
        else if (t >= 156.774f)
        {
            tutorealUIImage.sprite = tutorealImage[20];
        }
        else if (t >= 149.032f)
        {
            tutorealUIImage.sprite = tutorealImage[19];
        }
        else if (t >= 145.161f)
        {
            tutorealUIImage.sprite = tutorealImage[18];
        }
        else if (t >= 141.29f)
        {
            tutorealObject.SetActive(true);
        }
    }

    void SixTake(float t)
    {
        if (t >= 180.000f)
        {
            take[5] = true;
            tutorealObject.SetActive(false);
            UIManager.instance.SceneLoad(0);
        }
        else if (t >= 176.129f)
        {
            tutorealUIImage.sprite = tutorealImage[22];
        }
        else if (t >= 172.258f)
        {
            tutorealObject.SetActive(true);
        }
    }
}
