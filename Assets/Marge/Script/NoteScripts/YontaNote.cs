using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YontaNote : NoteBase
{
    public List<float> yontaTimes = new List<float>();
    [SerializeField] List<GameObject> yontaCountPoint = new List<GameObject>();
    public Queue<GameObject> targetWingQueue = new Queue<GameObject>();
    public Queue<float> sizeSecs = new Queue<float>();
    int yontacount;
    public int YonTaCount
    {
        get { return yontacount; }
        set
        {
            yontacount = value;
            for (int i = 0; i < 4; i++)
            {
                yontaCountPoint[i].SetActive(false);
            }
            if (value > 0)
                yontaCountPoint[value - 1].SetActive(true);
        }
    }

    GameObject curWing;
    // Start is called before the first frame update
    void Start()
    {
        yontaCountPoint[YonTaCount - 1].SetActive(true);
        noteCore = transform.GetChild(0).gameObject;
        transform.LookAt(Generator.instance.transform.position);
        Color changeColor = Color.magenta;
        switch (noteHands)
        {
            case Valve.VR.SteamVR_Input_Sources.LeftHand:
                changeColor = new Color(255f / 255f, 104f / 255f, 104f / 255f);
                break;

            case Valve.VR.SteamVR_Input_Sources.RightHand:
                changeColor = new Color(104f/255f,184f/255f,255f/255f);
                break;

            default:
                Debug.LogWarning(noteTime);
                break;
        }
        noteCore.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", changeColor);
        noteKind = NoteKind.Yonta;
        curWing = targetWingQueue.Dequeue();
        sizePerSec = sizeSecs.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        curWing.transform.localScale -= Vector3.one * (sizePerSec * Time.deltaTime);
        if (curWing.transform.localScale.x < 0.35)
        {
            Destroy(curWing);
            if (targetWingQueue.Count > 0)
            {
                curWing = targetWingQueue.Dequeue();
                sizePerSec = sizeSecs.Dequeue();
                YonTaCount--;
            }
            else
            {
                Destroy(gameObject);
                UIManager.instance.ResetCombo();
            }
        }

        NoteLineControl();

    }
   
}
