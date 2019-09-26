using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMChanger : NoteBase
{
    private float changeBPM;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.curBPM = changeBPM;
        Destroy(gameObject);
    }
    public void SetChangeBPM(float value)
    {
        changeBPM = value;
    }
    private void OnDestroy()
    {
        NoteManager.instance.curNoteCount--;
    }
}
