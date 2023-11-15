using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;
    // 애니메이터 파라미터 해쉬값
    private int hitHash = Animator.StringToHash("RandomHit");

    private void Start()
    {
        // 애니메이터 컴포넌트 획득
        animator = GetComponent<Animator>();
        // 히트 애니메이션 2개 중 1개 실행
        animator.SetInteger(hitHash, Random.Range(0, 2));
    }

    private void Update()
    {
        // 애니메이션이 끝나면 히트 이펙트 제거
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(this.gameObject);
    }

}
