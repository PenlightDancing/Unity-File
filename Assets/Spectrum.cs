using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spectrum : MonoBehaviour
{
    const int BarCount = 64;
    public List<GameObject> spectrumBars;         //GameObject 막대 생성
    [Header("플레이어로부터 얼마나 떨어질지 반지름")]
    public float radius;
    [Header("스펙트럼 민감도")]
    public float sensitive;
    public GameObject spectrumBar = null;        // 노란색 막대 선언

    private void Start()
    {
        for (int i = 0; i < BarCount; i++)
        {
            GameObject obj = Instantiate(spectrumBar,transform);
            obj.transform.Rotate(0, (float)i / BarCount * 360, 0);
            obj.transform.Translate(Vector3.forward * radius);
            spectrumBars.Add(obj);                                                  
        }
    }

    private void Update()
    {
        float[] SpectrumData = AudioListener.GetSpectrumData(2048, 0, FFTWindow.Hamming);          // 스펙트럼데이터 배열에 오디오가 듣고있는 스펙트럼데이터를 대입
        for (int i = 0; i < spectrumBars.Count; i++)
        {
            Vector3 FirstScale = spectrumBars[i].transform.localScale;                                    // 처음 막대기 스케일을 변수로 생성
            FirstScale.y =-1*( 0.02f + SpectrumData[i] * 30*sensitive);                                            // 막대기 y를 스펙트럼데이터에 맞게 늘림
            spectrumBars[i].transform.localScale = Vector3.MoveTowards(spectrumBars[i].transform.localScale, FirstScale, 0.1f);     // 스펙트럼데이터에 맞게 늘어난 스케일을 처음스케일로 0.1의 속도만큼 바꿈
        }
    }
}