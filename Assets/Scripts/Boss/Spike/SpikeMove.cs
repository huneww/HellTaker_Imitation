using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{
    private Transform tr;
    private Animator animator;

    private void Start()
    {
        tr = transform;
        animator = GetComponent<Animator>();

        // 스파이크 애니메이션 Up상태로 변경
        animator.SetTrigger("Up");
    }

    private void Update()
    {
        // 움직일수 있는 상태가 아니라면 메서드 종료
        if (!JGBossGameManager.Instance.IsMove) return;

        // 스파이크 위로 이동
        Vector3 movePos = new Vector3(tr.position.x,
                                      tr.position.y + JGBossGameManager.Instance.MoveUpSpeed * Time.deltaTime,
                                      tr.position.z);
        tr.position = movePos;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 윗쪽 콜라이더에 충돌한다면
        if (collider.gameObject.CompareTag("SpikeColliderUp"))
        {
            // 위치 변경
            tr.localPosition = new Vector3(tr.position.x - 0.5f, -2f, tr.position.z);
            // Up 애니메이션 실행
            animator.SetTrigger("Up");
        }
        // 아랫쪽 콜라이더에 충돌한다면
        else if (collider.gameObject.CompareTag("SpikeColliderDown"))
        {
            // 변경 위치 저장
            Vector3 movePos = new Vector3(tr.position.x - 0.5f, -1.5f, tr.position.z);
            // Down 애니메이션 실행
            animator.SetTrigger("Down");
            // 위치 변경 코루틴 실행
            StartCoroutine(SpikeMoveDown(movePos, 0.5f));
        }
    }

    private IEnumerator SpikeMoveDown(Vector3 movePos, float delay)
    {
        yield return new WaitForSeconds(delay);
        // 위치 변경
        tr.localPosition = movePos;
        // Up 애니메이션 실행
        animator.SetTrigger("Up");
    }

}
