using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationSpeedRandom : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float speedMin = 1.0f;
    [SerializeField]
    private float speedMax = 1.0f;

    private int speedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat(speedHash, Random.Range(speedMin, speedMax));
    }

}
