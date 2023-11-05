using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BooperController : MonoBehaviour
{
    private Animator animator;
    private readonly int activeHash = Animator.StringToHash("BooperActive");
    public static Action booperActive;

    private void Start()
    {
        animator = GetComponent<Animator>();
        booperActive = () => { BooperActive(); };
    }

    private void BooperActive()
    {
        animator.SetTrigger(activeHash);
    }

}
