using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Canvas canvas = null;
    Camera camera;
    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    private void Update()
    {
        canvas.worldCamera = camera;
    }
    public void SetCamera(Point point)
    {
        camera = point.GetComponent<Camera>();
    }
}