using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLeftTrigger : MonoBehaviour
{
    private SlideNote note;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void SetParent(SlideNote sn)
    {
        note = sn;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void colider()
    {
        if (!note.rightTrigger)
        {
            note.leftTrigger = true;
        }
        if (note.rightTrigger)
        {
            Debug.Log("Slice Note Fail");
        }
    }
}