using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationSpeedRandom : MonoBehaviour
{
    // 애니메이션 컴포넌트
    private Animator animator;
    // 애니메이션 스피드 최소값
    [SerializeField]
    private float speedMin = 1.0f;
    // 애니메이션 스피드 최대값
    [SerializeField]
    private float speedMax = 1.0f;
    // 애니메이션 스피드 파라미터 해쉬값
    private int speedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        // 애니메이터 컴포넌트 획득
        animator = GetComponent<Animator>();
        // 애니메이션 스피드 랜덤 지정
        animator.SetFloat(speedHash, Random.Range(speedMin, speedMax));
    }

}
