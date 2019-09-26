using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : NoteBase
{
    public Queue<GameObject> nextPos = new Queue<GameObject>();

    public bool isStartNote;

    public bool isChecking;
    public bool isFristEnter = false;
    public bool isLastEnter = true;
    [HideInInspector]
    public float unitBeat;

    // Start is called before the first frame update
    private void Start()
    {
        wing = transform.GetChild(0).gameObject;
        noteCore = transform.GetChild(1).gameObject;
        if (isStartNote)
        {
            Color applyColor = Color.magenta;
            switch (noteHands)
            {
                case Valve.VR.SteamVR_Input_Sources.LeftHand:
                    applyColor = new Color(255f / 255f, 104f / 255f, 104f / 255f);
                    break;

                case Valve.VR.SteamVR_Input_Sources.RightHand:
                    applyColor = new Color(104f / 255f, 184f / 255f, 255f / 255f);
                    break;

                default:
                    Debug.LogWarning(noteTime);
                    break;
            }
            noteCore.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", applyColor);
        }

        StartCoroutine(SpinNote());
        noteKind = NoteKind.Long;
    }

    // Update is called once per frame
    private void Update()
    {
        NoteLineControl();
    }

    private IEnumerator SpinNote()
    {
        while (wing.transform.localScale.x >= 0.35)
        {
            wing.transform.localScale -= Vector3.one * sizePerSec * Time.deltaTime;
            yield return null;
        }
        wing.transform.parent = noteCore.transform;


        if (isStartNote)
        {
            StartCoroutine(MovePosition());
        }
    }

    private IEnumerator MovePosition()
    {
        isFristEnter = true;
        while (nextPos.Count > 0)
        {
            GameObject peakObj = nextPos.Dequeue();
            Vector3 dir = (peakObj.transform.position - noteCore.transform.position).normalized;
            float dis = Vector3.Distance(noteCore.transform.position, peakObj.transform.position);
            float moveSpeed = dis / unitBeat;
            //Debug.Log(unitBeat);
            //Debug.Log(moveSpeed);
            float t = Time.time;
            while (t + unitBeat > Time.time)
            {
                noteCore.transform.position += dir * moveSpeed * Time.deltaTime;
                noteCore.transform.LookAt(Generator.instance.transform.position);
                //  Debug.DrawLine(noteCore.transform.position, peakObj.transform.position);
                yield return null;
            }
            Destroy(peakObj);
            if (isChecking)
            {
                UIManager.instance.GetCombo();
                UIManager.instance.GetScore(20);
            }
            else
            {
                UIManager.instance.ResetCombo();
            }
        }
        //한 중간노트 끝
        //롱노트 중간 판정
        isLastEnter = false;
        Destroy(gameObject, 0.1f);
    }
}