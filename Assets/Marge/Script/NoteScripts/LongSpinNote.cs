using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSpinNote : NoteBase
{
    public Queue<GameObject> nextPos = new Queue<GameObject>();
    public bool isStartNote;

    public bool RightHandle;

    public bool LeftHandle;

    public bool isChecking;

    private bool spinNow;
    private float rot;
    private float curRot;

    public int minSpin;

    [HideInInspector]
    public float unitBeat;

    private GameObject wing2;

    // Start is called before the first frame update
    private void Start()
    {
        wing = transform.GetChild(0).gameObject;
        wing2 = transform.GetChild(1).gameObject;
        noteCore = transform.GetChild(2).gameObject;
        StartCoroutine(SpinNote());
        noteKind = NoteKind.Long;
        Debug.Log("LS " + nextPos.Count + " , " + unitBeat + " at " + noteTime);
    }

    // Update is called once per frame
    private void Update()
    {
        if (spinNow)
        {
            if (RightHandle&&LeftHandle)          //돌리는거 체크
            {
                rot += 200;
                RightHandle = LeftHandle = false;
                UIManager.instance.GetCombo();
                UIManager.instance.GetScore(30);
            }
            curRot = (float)System.Math.Truncate((rot * Time.deltaTime) * 100) / 100;
            rot -= curRot * 3;
            noteCore.transform.Rotate(0, 0, curRot * 3);
        }
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
        wing2.transform.parent = noteCore.transform;
        if (isStartNote)
        {
            spinNow = true;
            StartCoroutine(MovePosition());
        }
    }

    private IEnumerator MovePosition()
    {
        do
        {
            GameObject peakObj = nextPos.Dequeue();
            Vector3 dir = (peakObj.transform.position - noteCore.transform.position).normalized;
            float dis = Vector3.Distance(noteCore.transform.position, peakObj.transform.position);
            float moveSpeed = dis / unitBeat;
            float t = Time.time;
            while (t + unitBeat > Time.time)
            {
                noteCore.transform.position += dir * moveSpeed * Time.deltaTime;
                noteCore.transform.LookAt(Generator.instance.transform.position);
                Debug.DrawLine(noteCore.transform.position, peakObj.transform.position, Color.red);
                yield return null;
            }
            Destroy(peakObj);
        } while (nextPos.Count > 0);
        //한 중간노트 끝
        //롱노트 중간 판정

        Destroy(gameObject, 0.1f);
    }
}