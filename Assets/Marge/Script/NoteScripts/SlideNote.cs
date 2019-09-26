using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideNote : NoteBase
{
    public bool leftTrigger;
    public bool rightTrigger;

    public float rotate;
    Valve.VR.InteractionSystem.Hand hand;
    // Start is called before the first frame update
    private void Start()
    {
        noteKind = NoteKind.Slider;
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
        wing = transform.GetChild(0).gameObject;
        noteCore = transform.GetChild(1).gameObject;
        noteCore.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", applyColor);
        noteCore.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", applyColor);
        // noteCore.transform.Rotate(0, 0, rotate);
        // wing.transform.Rotate(0, 0, rotate);
        transform.LookAt(Generator.instance.transform.position);
        transform.GetChild(2).GetComponent<NoteLeftTrigger>().SetParent(this);
        //transform.GetChild(2).transform.Rotate(0, 0, rotate);

        transform.GetChild(3).GetComponent<NoteRightTrigger>().SetParent(this);
        transform.Rotate(0, 0, rotate);
        //transform.GetChild(3).transform.Rotate(0, 0, rotate);
    }

    // Update is called once per frame
    private void Update()
    {
        if (leftTrigger && rightTrigger)
        {
            Debug.Log("SliceNote Success");
            hand.TriggerHapticPulse(ushort.MaxValue);
            Destroy(gameObject);
            UIManager.instance.GetCombo();
            UIManager.instance.GetScore(300 + (UIManager.instance.nCombo - 1) * 3);
        }
        wing.transform.localScale -= Vector3.one * (sizePerSec * Time.deltaTime);
        NoteLineControl();
        if (wing.transform.localScale.x < 0.35f)
        {
            UIManager.instance.ResetCombo();
            Destroy(gameObject);
        }
    }

    public void Sethand(Valve.VR.InteractionSystem.Hand Value)
    {
        hand = Value;
    }
}