using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tttttttttt : MonoBehaviour
{
    public Transform tf;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [ContextMenu("Set All Light")]
    private void sex()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject light = transform.GetChild(i).GetChild(0).GetChild(0).gameObject;
            light.transform.LookAt(tf);
        }
    }
}