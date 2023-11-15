using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BooperController : MonoBehaviour
{
    // 애니메이터 컴포넌트
    private Animator animator;
    // 애니메이션 파라미터 해쉬 값
    private readonly int activeHash = Animator.StringToHash("BooperActive");
    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action booperActive;

    private void Start()
    {
        // 애니메이터 컴포넌트 획득
        animator = GetComponent<Animator>();
        // 액션 변수 값 지정
        booperActive = () => { BooperActive(); };
    }

    private void BooperActive()
    {
        // 부퍼 애니메이션 활성화
        animator.SetTrigger(activeHash);
    }

}
