using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNote : NoteBase
{
    // Start is called before the first frame update
    private void Start()
    {
        wing = transform.GetChild(0).gameObject;
        noteCore = transform.GetChild(1).gameObject;
        transform.LookAt(Generator.instance.transform.position);
        Color changeColor = Color.magenta;
        switch (noteHands)
        {
            case Valve.VR.SteamVR_Input_Sources.LeftHand:
                changeColor = new Color(255f / 255f, 104f / 255f, 104f / 255f);
                break;

            case Valve.VR.SteamVR_Input_Sources.RightHand:
                changeColor = new Color(104f / 255f, 184f / 255f, 255f / 255f);
                break;

            default:
                Debug.LogWarning(noteTime);
                break;
        }
        noteCore.transform.GetChild(1).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", changeColor);
        noteKind = NoteKind.Normal;
    }

    // Update is called once per frame
    private void Update()
    {
        wing.transform.Rotate(0, 0, speed * Time.deltaTime);
        wing.transform.localScale -= Vector3.one * (sizePerSec * Time.deltaTime);
        if (wing.transform.localScale.x < 0.35)
        {
            Destroy(gameObject);
            UIManager.instance.ResetCombo();
        }
        NoteLineControl();
    }

    private void OnDestroy()
    {
        base.OnDestroy();
    }
}