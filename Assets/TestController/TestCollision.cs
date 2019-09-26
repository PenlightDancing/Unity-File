using UnityEngine;
using Valve.VR.InteractionSystem;

public class TestCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Note>().NoteList == List.Shot)
        {
            Debug.Log("TEst");
            Hand Test = transform.parent.GetComponent<Hand>();
            Test.TriggerHapticPulse(ushort.MaxValue);
        }
    }
}