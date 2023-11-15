using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;
    // 애니메이션 파라미터 해쉬값
    private int random = Animator.StringToHash("RandomBlood");

    private void Start()
    {
        // 애니메이터 컴포넌트 획득
        animator = GetComponent<Animator>();
        // 출혈 애니메이션 3개 중 1개 실행
        animator.SetInteger(random, Random.Range(0, 3));
    }

    private void Update()
    {
        // 애니메이션 끝나면 출혈 이펙트 삭제
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

}
