using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteKind
{
    Normal, Long, Spin, LongSpin, Slider,Yonta
}

public class NoteBase : MonoBehaviour
{
    public GameObject wing;
    public GameObject noteCore;
    public NoteKind noteKind;
    public Valve.VR.SteamVR_Input_Sources noteHands;
    public float noteTime;
    public GameObject line;
    public float speed;
    protected int coreindex = 1;
    public float sizePerSec;

    public GameObject nextNote;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    protected void Spin()
    {
        wing.transform.Rotate(0, speed * Time.deltaTime, 0);
        wing.transform.localScale -= Vector3.one * sizePerSec * Time.deltaTime;
        if (wing.transform.localScale.x < 0.5)
        {
            Destroy(gameObject);
        }
    }

    protected void NoteLineControl()
    {
        if (nextNote != null)
            line.SetActive(nextNote.active);
    }

    public Valve.VR.SteamVR_Input_Sources GetHandType()
    {
        return noteHands;
    }

    protected void OnDestroy()
    {
    }
}