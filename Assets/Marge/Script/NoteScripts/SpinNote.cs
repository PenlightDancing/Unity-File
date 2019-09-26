using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinNote : NoteBase
{
    public float stayBeat;

    private bool spinNow;
    public int minSpin;
    private float rot;
    private float curRot;
    private float r;

    private int spinCount =0;
    //컨트롤러 구분 불값
    public bool RightHandle;

    public bool LeftHandle;

    // Start is called before the first frame update
    private void Start()
    {
        coreindex = 2;
        wing = transform.GetChild(0).gameObject;
        noteCore = transform.GetChild(1).gameObject;
        transform.LookAt(Generator.instance.transform.position);
        StartCoroutine(NoteWingControl());
        noteKind = NoteKind.Spin;
    }

    // Update is called once per frame
    private void Update()
    {
        if (spinNow)
        {
            if (RightHandle && LeftHandle)          //돌리는거 체크
            {
                rot += 200;
                RightHandle = LeftHandle = false;
                UIManager.instance.GetCombo();
                UIManager.instance.GetScore(30+(UIManager.instance.nCombo-1)*3);
                spinCount++;
            }
            curRot = (float)System.Math.Truncate((rot * Time.deltaTime) * 100) / 100;
            rot -= curRot * 3;
            transform.Rotate(0, 0, curRot * 3);
        }
        NoteLineControl();
    }

    private IEnumerator NoteWingControl()
    {
        float t = Time.time;
        while (wing.transform.localScale.x >= 0.35)
        {
            wing.transform.localScale -= Vector3.one * sizePerSec * Time.deltaTime;
            yield return null;
        }
        spinNow = true;
        Destroy(wing.gameObject);
        StartCoroutine(NoteSpinControl());
    }

    private IEnumerator NoteSpinControl()
    {
        float t = 0;
        while (noteCore.transform.localScale.x >= 0.5f)
        {
            noteCore.transform.localScale -= Vector3.one * (0.5f / (stayBeat * SoundManager.instance.curSPB)) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        if(spinCount<minSpin)
        {
            UIManager.instance.ResetCombo();
        }
    }
}