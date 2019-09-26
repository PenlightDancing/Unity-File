using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenLight_Lighter : MonoBehaviour
{
    public LineRenderer line;
    public Transform linePos;
    private Vector3 prevPos;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        line.SetPosition(0, linePos.position);
        line.SetPosition(1, prevPos);
    }

    private void LateUpdate()
    {
        prevPos = linePos.position;
    }
}