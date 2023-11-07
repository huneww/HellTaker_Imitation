using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum MoveDirection
{
    Right,
    Left,
    Up,
    Down
}


public class PlayerMove : MonoBehaviour
{
    // 움직이는데 걸리는 시간
    [SerializeField]
    private float moveTime = 1f;
    // 이동위치 오브젝트 확인용 콜라이더 반지름
    [SerializeField]
    private float circleRadius = 0.4f;
    // 충돌 감지 레이어
    [SerializeField]
    private LayerMask checkLayer;

    private Animator animator;
    private Collider2D hit;
    private SpriteRenderer spriteRenderer;
    private Transform tr;
    
    // 애니메이터 파라미터 해쉬 값
    private int clearHash = Animator.StringToHash("Clear");
    private int moveHash = Animator.StringToHash("Move");
    private int kickHash = Animator.StringToHash("Kick");

    // 현재 움직이는지 확인 여부
    private bool isMove = false;

    private void Start()
    {
        // 각 컴포넌트 획득
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 플레이어의 오른쪽 위치를 저장
            Vector3 checkPos = new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y + 0.25f);
            // 오른쪽 위치에 오브젝트 확인
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // 오브젝트가 없고, 이동이 가능하면
            if (hit == null)
            {
                // 방향을 매개변수로 위치 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Right));
                // 스프라이트 반전
                spriteRenderer.flipX = false;
            }
            else
            {
                // 방향을 매개변수로 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 플레이어의 왼쪽 위치를 저장
            Vector3 checkPos = new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y + 0.25f);
            // 왼쪽 위치에 오브젝트 확인
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);
            // 스프라이트 반전
            spriteRenderer.flipX = true;

            // 오브젝트가 없고, 이동이 가능하면
            if (hit == null)
            {
                // 방향을 매개변수로 위치 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Left));
                // 스프라이트 반전
                spriteRenderer.flipX = true;
            }
            else
            {
                // 방향을 매개변수로 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 플레이어의 위쪽 위치를 저장
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset + 0.25f);
            // 위쪽 위치에 오브젝트 확인
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // 오브젝트가 없고, 이동이 가능하면
            if (hit == null)
                // 방향을 매개변수로 위치 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Up));
            else
            {
                // 방향을 매개변수로 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 플레이어 아랫쪽 위치를 저장
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset + 0.25f);
            // 아랫쪽 위치에 오브젝트 확인
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // 오브젝트가 없고, 이동이 가능하면
            if (hit == null)
                // 방향을 매개변수로 위치 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Down));
            else
            {
                // 방향을 매개변수로 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Down);
            }
        }
    }

    /// <summary>
    /// 방향의 오브젝트 확인
    /// </summary>
    /// <param name="direction">이동 방향</param>
    private void CheckImpactObject(MoveDirection direction)
    {
        // 이동 방향에 오브젝트가 없다면 메서드 종료
        if (hit == null) return;

        // 오브젝트의 태그가 Skeleton, Stone이면
        if (hit.transform.CompareTag("Skeleton") || hit.transform.CompareTag("Stone"))
        {
            ObjectMove move = hit.transform.GetComponent<ObjectMove>();
            move.Move(direction);
            Vector3 spawnPos = hit.transform.position;
            GameManager.Instance.SpawnHit(new Vector3(spawnPos.x, spawnPos.y + 0.25f, spawnPos.z));
            // 킥 애니메이션 실행
            animator.SetTrigger(kickHash);
            // 이동 횟수 감소
            GameManager.Instance.FootCountMinus();
        }
        // 오브젝트의 태그가 Wall이면
        else if (hit.transform.CompareTag("Wall"))
        {
        }
    }

    /// <summary>
    /// 플레이어 위치 이동 코루틴
    /// </summary>
    /// <param name="direction">이동 방향</param>
    /// <returns></returns>
    private IEnumerator MoveCoroutine(MoveDirection direction)
    {
        // 이동 사운드 재생
        AudioManager.Instance.PlayerMove();
        // 이동 애니메이션 실행
        animator.SetTrigger(moveHash);
        // 현재 위치에서 y축을 조금 올려 먼지 생성
        GameManager.Instance.SpawnDust(new Vector3(tr.position.x, tr.position.y + 0.25f, tr.position.z));
        // 이동 횟수 감소
        GameManager.Instance.FootCountMinus();

        // 이동 불가로 변경
        isMove = true;
        float curTime = 0;
        float percent = 0;
        // 현재 위치 저장
        Vector3 curPos = tr.position;
        // 목표 위치 저장 변수
        Vector3 targetPos = Vector3.zero;

        // 방향에 따라 목표 위치 저장
        switch (direction)
        {
            // 오른쪽
            case MoveDirection.Right:
                // 현재 위치에서 오프셋 값만큼 변경
                targetPos = new Vector3(curPos.x + GameManager.Instance.MoveXOffset, curPos.y);
                break;
            // 왼쪽
            case MoveDirection.Left:
                // 현재 위치에서 오프셋 값만큼 변경
                targetPos = new Vector3(curPos.x - GameManager.Instance.MoveXOffset, curPos.y);
                break;
            // 위쪽
            case MoveDirection.Up:
                // 현재 위치에서 오프셋 값만큼 변경
                targetPos = new Vector3(curPos.x, curPos.y + GameManager.Instance.MoveYOffset);
                break;
            // 아랫쪽
            case MoveDirection.Down:
                // 현재 위치에서 오프셋 값만큼 변경
                targetPos = new Vector3(curPos.x, curPos.y - GameManager.Instance.MoveYOffset);
                break;
        }

        // 플레이어 이동
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            tr.position = Vector3.Lerp(curPos, targetPos, percent);
        }

        // 플레이어 목표 위치로 변경
        tr.position = targetPos;
        // 이동이 가능하도록 변경
        isMove = false;
    }

    // 이동 방향 오브젝트 체크 가시화
    private void OnDrawGizmosSelected()
    {
        if (GameManager.Instance == null)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y + 0.25f), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y + 0.25f), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset + 0.25f), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset + 0.25f), circleRadius);
    }

}
