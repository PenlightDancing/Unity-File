using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionReader : MonoBehaviour
{
    public TextAsset text;
    private string[] str;
    private int count;

    // Start is called before the first frame update
    private void Start()
    {
        str = text.text.Split('\n');
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        string[] k = str[count].Split(',');
        float[] tf = new float[6];
        for (int i = 0; i < 6; i++)
        {
            tf[i] = float.Parse(k[i]);
        }
        transform.position = new Vector3(tf[0], tf[1], tf[2]);
        transform.rotation = Quaternion.Euler(new Vector3(tf[3], tf[4], tf[5]));
        count++;
    }
}