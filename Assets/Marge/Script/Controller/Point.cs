using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Point : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot = null;

    public VRInputModule m_InputModule;

    private LineRenderer m_LineRender = null;

    GameObject RayObject = null;

    private void Awake()
    {
        m_LineRender = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        PointerEventData data = m_InputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPosion = transform.position + (transform.forward * targetLength);

        if (hit.collider != null)
        {
            endPosion = hit.point;
        }

        m_Dot.transform.position = endPosion;

        m_LineRender.SetPosition(0, transform.position);
        m_LineRender.SetPosition(1, endPosion);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward * length);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}