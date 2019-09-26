using System.Collections;
using UnityEngine;

public class generator : MonoBehaviour
{
    [SerializeField]
    private Vector3[] pos = new Vector3[91];

    [SerializeField]
    private GameObject node;

    [SerializeField]
    private LineRenderer line;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Generator());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [ContextMenu("SetPosition")]
    private void SetPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pos[i] = transform.GetChild(i).GetComponent<MeshRenderer>().bounds.center;
        }
    }

    private IEnumerator Generator()
    {
        GameObject preobject = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            node.transform.position = pos[i];
            if (preobject != null)
            {
                LineRenderer _line = Instantiate(line, preobject.transform);
                _line.SetPosition(0, preobject.transform.position);
                _line.SetPosition(1, node.transform.position);
            }
            preobject = Instantiate(node);
            yield return new WaitForSeconds(1f);
        }
    }
}