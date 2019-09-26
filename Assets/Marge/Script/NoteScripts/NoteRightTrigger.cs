using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRightTrigger : MonoBehaviour
{
    private SlideNote note;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetParent(SlideNote sn)
    {
        note = sn;
    }

    public void colider()
    {
        if (note.leftTrigger)
        {
            note.rightTrigger = true;
        }
        if (!note.leftTrigger)
        {
            Debug.Log("Slice Note Fail");
        }
    }
}