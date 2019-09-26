using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MotionMemorize : MonoBehaviour
{
    private StreamWriter writer;
    private bool isStarted;

    // Start is called before the first frame update
    private void Start()
    {
        writer = new StreamWriter(Application.dataPath + @"\Motion" + Random.Range(0, 10) + ".txt");
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (SoundManager.instance.curMusic.isPlaying)
        {
            isStarted = true;
            writer.WriteLine(transform.position.x + "," + transform.position.y + "," + transform.position.z + "," + transform.rotation.eulerAngles.x + "," + transform.rotation.eulerAngles.y + "," + transform.rotation.eulerAngles.z);
        }
        if (Time.time > 10)
        {
            writer.Close();
            Destroy(gameObject);
        }
    }
}