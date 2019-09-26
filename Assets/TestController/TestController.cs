using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public SteamVR_Input_Sources ControllerList;
    public SteamVR_Action_Boolean spawn;

    [SerializeField]
    private Hand Test;

    public ushort TestInt;

    private GameObject Point;
    private Point pointer;
    UIManager uiManger;
    int pointerIndex;
    
    // Start is called before the first frame update
    private void Start()
    {
        uiManger = GameObject.FindObjectOfType<UIManager>();
        Test = this.GetComponent<Hand>();
        Point = transform.GetChild(0).gameObject;
        switch (transform.name)
        {
            case "Controller (left)":
                pointer = transform.parent.GetChild(1).GetChild(0).GetComponent<Point>();
                pointerIndex = 1;
                break;

            case "Controller (right)":
                pointer = transform.parent.GetChild(0).GetChild(0).GetComponent<Point>();
                pointerIndex = 0;
                break;
        }

        SetData();
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawn.GetStateDown(ControllerList))
        {
            if (Point.activeSelf == false)
            {
                VRInputModule.m_camera = Point.GetComponent<Camera>();
                pointer.gameObject.SetActive(false);
                Point.SetActive(true);
                uiManger.SetPointerIndex(pointerIndex);
                // Point.GetComponent<Material>().SetColor("_EmissionColor", pointer.GetComponent<Material>().GetColor("_EmissionColor"));
            }
        }
    }
    public void SetData()
    {
        uiManger.SetPointer(pointerIndex, pointer);
        if (pointer.gameObject.activeSelf)
        {
            VRInputModule.m_camera = pointer.GetComponent<Camera>(); 
        }
    }
}