using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort : MonoBehaviour
{
    public GameObject sort;

    // Start is called before the first frame update
    private void Start()
    {
    }

    [ContextMenu("t")]
    private void t()
    {
        for (int i = 1; i <= 320; i++)
        {
            GameObject go = GameObject.Find(i.ToString());
            go.transform.parent = sort.transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}