using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Transform NoteBunddle;
    public Transform LightBunddle;
    public Color32 curColor;
    public float beat;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator WingColorCalcul()
    {
        for (int i = 0; i < NoteBunddle.childCount; i++)
        {
            MeshRenderer m = NoteBunddle.GetChild(i).GetComponent<NoteBase>().wing.GetComponent<MeshRenderer>();
            m.material.color = curColor;
            m.material.SetColor("_EmissionColor", curColor);
            yield return null;
        }
    }

    private IEnumerator WingColorChange(Color32 col)
    {
        float r = col.r;
        float g = col.g;
        float b = col.b;
        for (int i = 0; i < NoteBunddle.childCount; i++)
        {
            MeshRenderer m = NoteBunddle.GetChild(i).GetComponent<NoteBase>().wing.GetComponent<MeshRenderer>();
            m.material.color = curColor;
            r = col.r * Time.deltaTime;
            g = col.g * Time.deltaTime;
            b = col.b * Time.deltaTime;
            Color32 appColor = new Color32((byte)r, (byte)g, (byte)b, 255);
            m.material.SetColor("_EmissionColor", m.material.GetColor("_EmissionColor") + appColor);
            yield return null;
        }
    }
}