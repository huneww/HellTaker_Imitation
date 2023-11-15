using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockSelfDestroy : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;

    private void Start()
    {
        // 애니메이터 컴포넌트 획득
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 애니메이션이 끝나면
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // 오브젝트 제거
            Destroy(this.gameObject);
        }
    }

}
