using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModelChage : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.GetChild(0).GetChild(1).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}