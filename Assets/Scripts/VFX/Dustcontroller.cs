using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dustcontroller : MonoBehaviour
{
    private Animator animator;

    private int random = Animator.StringToHash("RandomDust");

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(random, Random.Range(0, 3));
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(this.gameObject);
    }

}
