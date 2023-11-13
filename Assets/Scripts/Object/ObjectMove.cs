using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // 오브젝트 이동 시간
    [SerializeField]
    private float moveTime = 1f;
    // 충돌 감지할 레이어
    [SerializeField]
    private LayerMask checkLayer;

    // 목표 이동 위치
    private Vector3 targetPos;
    // 스켈리톤 애니메이터
    private Animator animator;
    // 스프라이트 랜더러
    private SpriteRenderer spriteRenderer;
    // 애니메이터 파라미터 해쉬 값
    private int hitHash = Animator.StringToHash("Hit");

    private void Start()
    {
        // 오브젝트가 스켈레톤이면 애니메이터 컴포넌트 획득
        if (gameObject.CompareTag("Skeleton"))
            animator = GetComponent<Animator>();
        // 스프라이트 랜더러 컴포넌트 획득
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(MoveDirection direction)
    {
        // 애니메이터가 있는지 확인
        if (animator != null)
            // 히트 애니메이션 실행
            animator.SetTrigger(hitHash);

        // 차려는 오브젝트가 스톤이라면
        // 스톤 차는 소리 재생
        if (gameObject.CompareTag("Stone"))
            AudioManager.Instance.StoneKick();

        // 방향에 따라 목표 위치값 변경
        switch (direction)
        {
            case MoveDirection.Right:
                targetPos = new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y);
                // 스켈레톤은 스프라이트 반전 값 변경
                if (gameObject.CompareTag("Skeleton")) spriteRenderer.flipX = true;
                break;
            case MoveDirection.Left:
                targetPos = new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y);
                // 스켈레톤은 스프라이트 반전 값 변경
                if (gameObject.CompareTag("Skeleton")) spriteRenderer.flipX = false;
                break;
            case MoveDirection.Up:
                targetPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset);
                break;
            case MoveDirection.Down:
                targetPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset);
                break;
        }

        // 목표 위치에 오브젝트가 있는지 확인
        // 목표 위치에 오브젝트가 있다면
        if (CheckTargetPosObj())
        {
            Debug.Log("Behind is Wall");
            // 오브젝트가 스켈레톤이라면
            if (gameObject.CompareTag("Skeleton"))
            {
                Dead();
            }
        }
        else
        {
            // 오브젝트 목표 위치로 이동
            StartCoroutine(MoveCoroutine());
        }
    }

    private void Dead()
    {
        // 스켈레톤 죽는 사운드 재생
        AudioManager.Instance.EnemyDie();
        // 스켈레톤 뼈 파티클 생성
        GameManager.Instance.SpawnBornParticle(transform.position);
        // 스켈레톤 오브젝트 제거
        Destroy(this.gameObject);
    }

    private IEnumerator MoveCoroutine()
    {
        // 이동할 오브젝트가 스켈레톤이라면 스켈레톤 차는 소리 재생
        if (gameObject.CompareTag("Skeleton")) AudioManager.Instance.EnemyKick();
        // 이동할 오브젝트가 스톤이라면 스톤 이동 소리 재생
        // 스톤은 이동하지 안더라도 차는 소리는 재생됨
        else if (gameObject.CompareTag("Stone")) AudioManager.Instance.StoneMove();

        float curTime = 0;
        float percent = 0;
        Vector3 curPos = transform.position;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            transform.position = Vector3.Lerp(curPos, targetPos, percent);
        }

        transform.position = targetPos;

    }

    /// <summary>
    /// 목표 위치에 오브젝트가 있는지 확인 메서드
    /// </summary>
    /// <returns>있으면 참, 없으면 거짓</returns>
    private bool CheckTargetPosObj()
    {
        Vector3 checkPos = targetPos;
        Collider2D check = Physics2D.OverlapCircle(checkPos, 0.3f, checkLayer);
        if (check != null)
            return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Spike") || collision.transform.CompareTag("UpDownSpike"))
        {
            Dead();
        }
    }

    /// <summary>
    /// 오브젝트확인 OverlapCircle 가시화
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (GameManager.Instance == null)
            return;

        Gizmos.DrawWireSphere(new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y), 0.3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y), 0.3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset), 0.3f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset), 0.3f);
    }

}
