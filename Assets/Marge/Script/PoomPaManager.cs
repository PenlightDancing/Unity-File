using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoomPaManager : MonoBehaviour
{
    public float bpm;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<PoomPaPoomPa>().SetPoomPaSpeed((bpm/60)/3);
        }

    }
    
}
