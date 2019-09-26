using UnityEngine;

public class NoteOrigin : MonoBehaviour
{
    [SerializeField]
    private Transform trg;

    // Start is called before the first frame update
    private void Start()
    {
        trg = GameObject.Find("Camera").transform;
        transform.LookAt(trg);
        transform.Rotate(-90, 0, 0);
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}