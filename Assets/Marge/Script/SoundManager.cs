using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource curMusic;
    private float cbpm;

    public float curBPM
    {
        get { return cbpm; }
        set
        {
            cbpm = value;
            curSPB = 60 / curBPM;
        }
    }

    private float cspb;

    public float curSPB
    {
        get { return cspb; }
        set
        {
            cspb = value;
            Generator.instance.SetNoteWingSpeed(value);
        }
    }

    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}