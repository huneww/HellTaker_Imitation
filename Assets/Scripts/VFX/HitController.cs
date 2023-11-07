using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    private Animator animator;

    private int hitHash = Animator.StringToHash("RandomHit");

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(hitHash, Random.Range(0, 2));
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(this.gameObject);
    }

}
