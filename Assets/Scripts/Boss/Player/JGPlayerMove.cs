using System.Collections;
using UnityEngine;

public class JGPlayerMove : MonoBehaviour
{
    // 움직이는데 걸리는 시간
    [SerializeField]
    private float moveTIme = 1f;
    // 이동위치 오브젝트 확인용 콜라이더 반지름
    [SerializeField]
    private float circleRadius = 0.4f;
    // 충돌 감지 레이어
    [SerializeField]
    private LayerMask checkLayer;

    // 움직이는 오프셋 값
    [SerializeField]
    private float xMoveOffset = 1f;
    [SerializeField]
    private float yMoveOffset = 1f;

    private Animator animator;
    public Collider2D[] hit;
    private SpriteRenderer spriteRenderer;
    private Transform tr;

    private int clearHash = Animator.StringToHash("Clear");
    private int moveHash = Animator.StringToHash("Move");
    private int kickHash = Animator.StringToHash("Kick");

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        if (JGBossGameManager.Instance.IsMove)
        {
            Vector3 movePos = new Vector3(tr.position.x, tr.position.y + JGBossGameManager.Instance.MoveUpSpeed * Time.deltaTime, tr.position.z);
            tr.position = movePos;
        }

        Move();
    }

    private void Move()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 플레이어 오른쪽 위치를 저장
            Vector3 checkPos = new Vector3(tr.position.x + xMoveOffset, tr.position.y);
            // 오른쪽에 오브젝트가 있는지 확인
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Right);
            }
            else
            {
                // 캐릭터 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Right));
                // 캐릭터 스프라이트 반전
                spriteRenderer.flipX = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 플레이어 왼쪽 위치를 저장
            Vector3 checkPos = new Vector3(tr.position.x - xMoveOffset, tr.position.y);
            // 왼쪽에 오브젝트가 있는지 확인
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Left);
            }
            else
            {
                // 캐릭터 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Left));
                // 캐릭터 스프라이트 반전
                spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 플레이어 위쪽 위치를 저장
            Vector3 checkPos = new Vector3(tr.position.x, tr.position.y + yMoveOffset);
            // 위쪽에 오브젝트가 있는지 확인
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Up);
            }
            else
            {
                // 캐릭터 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Up));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 플레이어 아랫쪽 위치를 저장
            Vector3 checkPos = new Vector3(tr.position.x, tr.position.y - yMoveOffset);
            // 아랫쪽에 오브젝트가 있는지 확인
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // 충돌 오브젝트 확인
                CheckImpactObject(MoveDirection.Down);
            }
            else
            {
                // 캐릭터 이동
                StartCoroutine(MoveCoroutine(MoveDirection.Down));
            }
        }
    }

    private void CheckImpactObject(MoveDirection dircetion)
    {
        // 오브젝트가 없다면 메서드 종료
        if (hit == null) return;

        // 충돌한 오브젝트 순차적으로 검사
        foreach (var obj in hit)
        {
            // HitChain오브젝트라면
            if (obj.transform.CompareTag("HitChain"))
            {
                // 현재 플레이어 위치 정보 저장
                Vector3 spawnPos = JGBossGameManager.Instance.Player.transform.position;
                // 킥 애니메이션 실행
                animator.SetTrigger(kickHash);

                // 오브젝트 이름에 L이 포함되어있으면
                if (obj.gameObject.name.Contains("L"))
                {
                    // 스폰 위치 변경
                    spawnPos.Set(spawnPos.x - 1f, spawnPos.y, spawnPos.z);
                    // 히트 이펙트 소환
                    JGBossGameManager.Instance.SpawnHit(spawnPos);
                    // 체인 쉐이크 메서드 실행
                    StartCoroutine(JGBossGameManager.Instance.ChainShake(obj.gameObject, 0.1f, 0.25f, true));
                    // 맞은 체인 체력 차감
                    ChainSpanwer.Instance.Chain_L_Health--;

                    // 체인의 체력이 0이하면
                    if (ChainSpanwer.Instance.Chain_L_Health <= 0)
                    {
                        // Broke 애니메이션 실행
                        obj.GetComponent<Animator>().SetTrigger("Broke");
                        // 부서지는 사운드 재생
                        JGBossAudioManager.Instance.ChainBreak_L();
                        // 체인 콜라이더 비활성화
                        obj.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        // Hit 애니메이션 실행
                        obj.GetComponent<Animator>().SetTrigger("Hit");
                        // 체인 Hit 사운드 재생
                        ChainHitSound(true);
                    }
                }
                // 오브젝트 이름에 R이 포함되어있으면
                else if (obj.gameObject.name.Contains("R"))
                {
                    // 스폰 위치 변경
                    spawnPos.Set(spawnPos.x + 1f, spawnPos.y, spawnPos.z);
                    // 히트 이펙트 소환
                    JGBossGameManager.Instance.SpawnHit(spawnPos);
                    // 체인 쉐이크 메서드 실행
                    StartCoroutine(JGBossGameManager.Instance.ChainShake(obj.gameObject, 0.1f, 0.25f));
                    // 맞은 체인 체력 차감
                    ChainSpanwer.Instance.Chain_R_Health--;

                    // 체인의 체력이 0이하면
                    if (ChainSpanwer.Instance.Chain_R_Health <= 0)
                    {
                        // Broke 애니메이션 실행
                        obj.GetComponent<Animator>().SetTrigger("Broke");
                        // 부서지는 사운드 재생
                        JGBossAudioManager.Instance.ChainBreak_R();
                        // 체인 콜라이더 비활성화
                        obj.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        // Hit 애니메이션 실행
                        obj.GetComponent<Animator>().SetTrigger("Hit");
                        // 체인 Hit 사운드 재생
                        ChainHitSound(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 체인 공격 사운드
    /// </summary>
    /// <param name="isLeft">왼쪽에있는 체인인지 확인 여부</param>
    private void ChainHitSound(bool isLeft)
    {
        // 왼쪽에 있는 체인이라면
        if (isLeft)
            // 왼쪽에서 들리는 사운드 재생
            JGBossAudioManager.Instance.ChainHit_L();
        else
            // 오른쪽에서 들리는 사운드 재생
            JGBossAudioManager.Instance.ChainHit_R();
    }

    private IEnumerator MoveCoroutine(MoveDirection direction)
    {
        // 이동 애니메이션 실행
        animator.SetTrigger(moveHash);
        // 먼지 이펙트 소환
        JGBossGameManager.Instance.SpawnDust(transform.position);

        float curTime = 0;
        float percent = 0;
        // 현재 위치
        Vector3 curPos = tr.position;
        // 이동 위치
        Vector3 targetPos = Vector3.zero;
        // 이동 방향에 맞게 이동 위치 변경
        switch (direction)
        {
            case MoveDirection.Right:
                targetPos = new Vector3(curPos.x + xMoveOffset, curPos.y);
                break;
            case MoveDirection.Left:
                targetPos = new Vector3(curPos.x - xMoveOffset, curPos.y);
                break;
            case MoveDirection.Up:
                targetPos = new Vector3(curPos.x, Mathf.Round(curPos.y) + yMoveOffset);
                break;
            case MoveDirection.Down:
                targetPos = new Vector3(curPos.x, Mathf.Round(curPos.y) - yMoveOffset);
                break;
        }

        // X축은 소숫점을 전부 버림
        targetPos = new Vector3(Mathf.Round(targetPos.x), targetPos.y);

        // 플레이어 이동
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTIme;
            tr.position = Vector3.Lerp(curPos, targetPos, percent);
        }

        tr.position = targetPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 특정 위치로 이동하면 플레이어 사망
        if (collision.transform.CompareTag("DeathZone") && !JGBossGameManager.Instance.IsDead)
        {
            JGBossGameManager.Instance.IsDead = true;
            JGBossGameManager.Instance.PlayerDead();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + xMoveOffset, transform.position.y), 0.4f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - xMoveOffset, transform.position.y), 0.4f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + yMoveOffset), 0.4f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - yMoveOffset), 0.4f);
    }

}
