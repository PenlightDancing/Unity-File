using UnityEngine;
using Valve.VR;

public class Note : MonoBehaviour
{
    public List NoteList;
    private bool RightCheck;
    private bool LeftCheck;

    public int CheckData;

    // Start is called before the first frame update
    private void Start()
    {
        if (NoteList == List.Spin)
        {
            CheckData = 10;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (NoteList == List.Spin)
        {
            PhysicsTest[] Controller = GameObject.FindObjectsOfType<PhysicsTest>();

            if (LeftCheck && RightCheck)
            {
                CheckData--;
                LeftCheck = false;
                RightCheck = false;
                for (int i = 0; i < Controller.LongLength; i++)
                {
                    Controller[i].SetData(false);
                }
            }
        }
    }

    public void SetHandlessData(SteamVR_Input_Sources Handles)
    {
        if (Handles == SteamVR_Input_Sources.LeftHand)
        {
            LeftCheck = true;
        }
        else if (Handles == SteamVR_Input_Sources.RightHand)
        {
            RightCheck = true;
        }
    }
}

public enum List
{
    Shot, Long, Spin
};