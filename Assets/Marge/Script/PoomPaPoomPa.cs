using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoomPaPoomPa : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetPoomPaSpeed(float f)
    {
        animator.speed = f;
    }

}
