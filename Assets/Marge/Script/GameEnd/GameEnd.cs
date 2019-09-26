using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public bool isDone;
    IEnumerator iClearChecker;

    // Start is called before the first frame update
    void Start()
    {
        isDone = false;
        iClearChecker = ClearChecker();
        StartCoroutine(iClearChecker);
    }
    private void OnDestroy()
    {
        StopCoroutine(iClearChecker);
        iClearChecker = ClearChecker();
    }
    IEnumerator ClearChecker()
    {
        Debug.Log("Test");
        float t = SoundManager.instance.curMusic.time;
        while (true)
        {
            if (true/*일시정지 여부 확인후 일시정지 아닐때*/)
            {
                t += Time.deltaTime;
            }
            if (t > SoundManager.instance.curMusic.clip.length/*현재 재생중인 AudioSource.clip.length*/)
            {
                isDone = true;
                UIManager.instance.SceneLoad(0);
                break;
            }
            yield return null;
        }
    }
}
