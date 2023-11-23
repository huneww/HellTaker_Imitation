using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpDownSpike : MonoBehaviour
{
    // 현재 올라와있는지 확인 변수
    [SerializeField]
    private bool isUp;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Start()
    {
        // 각 컴포넌트 획득
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // 콜라이더 isUp값에 따라 변경
        boxCollider.enabled = isUp;
        // 애니메이션 변경
        animator.SetBool("isUp", isUp);
    }

    public void Update()
    {

        // 활살표 입력시
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            // 스파이크 상태 변경
            SpikeChange();
    }

    public void SpikeChange()
    {
        isUp = !isUp;
        boxCollider.enabled = isUp;
        animator.SetBool("isUp", isUp);

        // 현재 애니메이션이 올라오거나, 내려가는 애니메이션이면
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Down"))
        {
            // 1에서 현재 애니메이션 진행도를 뺀 값을 시작 시간으로 설정하여 애니메이션 실행
            animator.Play(isUp ? "Spike_Up" : "Spike_Down", -1, 1f - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
        {
            // 올라오는, 내려가는 애니메이션 실행
            animator.SetTrigger(isUp ? "Up" : "Down");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 안으로 들어온다면
        if (collision.transform.CompareTag("Player"))
        {
            // 현재 올라와있는지 확인
            if (isUp)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Boss"))
                {
                    Debug.Log("Blood");
                }
                else
                {
                    // 플레이어 출혈 이펙트 생성
                    GameManager.Instance.SpawnBlood(transform.position);
                }
                
            }
        }
    }


}
