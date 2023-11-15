using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePlosionController : MonoBehaviour
{
    // 하트 파티클
    [SerializeField]
    private ParticleSystem particle_Heart;
    // 별 파티클
    [SerializeField]
    private ParticleSystem particle_Star;
    // 애니메이터 컴포넌트
    private Animator animator;
    // 중복 실행 방지 변수
    private bool isPlay = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 애니메이션이 방출 애니메이션이라면
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ReleaseLovePlosion"))
        {
            // 아직 파티클이 실행이 되지 않았으면
            if (!isPlay)
            {
                // 파티클 실행
                particle_Heart.Play();
                particle_Star.Play();
                // 변수값 변경
                isPlay = true;
            }
            // 애니메이션이 끝났다면
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                // 다음 스테이지로 넘어가는 코루틴 실행
                StartCoroutine(StartStageChange());
            }
        }
    }

    private IEnumerator StartStageChange()
    {
        // 일정시간 대기
        yield return new WaitForSeconds(0.5f);
        // 게임 매니저의 스테이지 변경 메서드 실행
        StartCoroutine(GameManager.Instance.StageChange(false));
        // 오브젝트 삭제
        Destroy(this.gameObject);
    }

}
