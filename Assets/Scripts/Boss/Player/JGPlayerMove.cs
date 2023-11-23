using System.Collections;
using UnityEngine;

public class JGPlayerMove : MonoBehaviour
{
    // �����̴µ� �ɸ��� �ð�
    [SerializeField]
    private float moveTIme = 1f;
    // �̵���ġ ������Ʈ Ȯ�ο� �ݶ��̴� ������
    [SerializeField]
    private float circleRadius = 0.4f;
    // �浹 ���� ���̾�
    [SerializeField]
    private LayerMask checkLayer;

    // �����̴� ������ ��
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
            // �÷��̾� ������ ��ġ�� ����
            Vector3 checkPos = new Vector3(tr.position.x + xMoveOffset, tr.position.y);
            // �����ʿ� ������Ʈ�� �ִ��� Ȯ��
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Right);
            }
            else
            {
                // ĳ���� �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Right));
                // ĳ���� ��������Ʈ ����
                spriteRenderer.flipX = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �÷��̾� ���� ��ġ�� ����
            Vector3 checkPos = new Vector3(tr.position.x - xMoveOffset, tr.position.y);
            // ���ʿ� ������Ʈ�� �ִ��� Ȯ��
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Left);
            }
            else
            {
                // ĳ���� �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Left));
                // ĳ���� ��������Ʈ ����
                spriteRenderer.flipX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �÷��̾� ���� ��ġ�� ����
            Vector3 checkPos = new Vector3(tr.position.x, tr.position.y + yMoveOffset);
            // ���ʿ� ������Ʈ�� �ִ��� Ȯ��
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Up);
            }
            else
            {
                // ĳ���� �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Up));
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // �÷��̾� �Ʒ��� ��ġ�� ����
            Vector3 checkPos = new Vector3(tr.position.x, tr.position.y - yMoveOffset);
            // �Ʒ��ʿ� ������Ʈ�� �ִ��� Ȯ��
            hit = Physics2D.OverlapCircleAll(checkPos, circleRadius, checkLayer);

            if (hit.Length > 0)
            {
                // �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Down);
            }
            else
            {
                // ĳ���� �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Down));
            }
        }
    }

    private void CheckImpactObject(MoveDirection dircetion)
    {
        // ������Ʈ�� ���ٸ� �޼��� ����
        if (hit == null) return;

        // �浹�� ������Ʈ ���������� �˻�
        foreach (var obj in hit)
        {
            // HitChain������Ʈ���
            if (obj.transform.CompareTag("HitChain"))
            {
                // ���� �÷��̾� ��ġ ���� ����
                Vector3 spawnPos = JGBossGameManager.Instance.Player.transform.position;
                // ű �ִϸ��̼� ����
                animator.SetTrigger(kickHash);

                // ������Ʈ �̸��� L�� ���ԵǾ�������
                if (obj.gameObject.name.Contains("L"))
                {
                    // ���� ��ġ ����
                    spawnPos.Set(spawnPos.x - 1f, spawnPos.y, spawnPos.z);
                    // ��Ʈ ����Ʈ ��ȯ
                    JGBossGameManager.Instance.SpawnHit(spawnPos);
                    // ü�� ����ũ �޼��� ����
                    StartCoroutine(JGBossGameManager.Instance.ChainShake(obj.gameObject, 0.1f, 0.25f, true));
                    // ���� ü�� ü�� ����
                    ChainSpanwer.Instance.Chain_L_Health--;

                    // ü���� ü���� 0���ϸ�
                    if (ChainSpanwer.Instance.Chain_L_Health <= 0)
                    {
                        // Broke �ִϸ��̼� ����
                        obj.GetComponent<Animator>().SetTrigger("Broke");
                        // �μ����� ���� ���
                        JGBossAudioManager.Instance.ChainBreak_L();
                        // ü�� �ݶ��̴� ��Ȱ��ȭ
                        obj.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        // Hit �ִϸ��̼� ����
                        obj.GetComponent<Animator>().SetTrigger("Hit");
                        // ü�� Hit ���� ���
                        ChainHitSound(true);
                    }
                }
                // ������Ʈ �̸��� R�� ���ԵǾ�������
                else if (obj.gameObject.name.Contains("R"))
                {
                    // ���� ��ġ ����
                    spawnPos.Set(spawnPos.x + 1f, spawnPos.y, spawnPos.z);
                    // ��Ʈ ����Ʈ ��ȯ
                    JGBossGameManager.Instance.SpawnHit(spawnPos);
                    // ü�� ����ũ �޼��� ����
                    StartCoroutine(JGBossGameManager.Instance.ChainShake(obj.gameObject, 0.1f, 0.25f));
                    // ���� ü�� ü�� ����
                    ChainSpanwer.Instance.Chain_R_Health--;

                    // ü���� ü���� 0���ϸ�
                    if (ChainSpanwer.Instance.Chain_R_Health <= 0)
                    {
                        // Broke �ִϸ��̼� ����
                        obj.GetComponent<Animator>().SetTrigger("Broke");
                        // �μ����� ���� ���
                        JGBossAudioManager.Instance.ChainBreak_R();
                        // ü�� �ݶ��̴� ��Ȱ��ȭ
                        obj.GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    else
                    {
                        // Hit �ִϸ��̼� ����
                        obj.GetComponent<Animator>().SetTrigger("Hit");
                        // ü�� Hit ���� ���
                        ChainHitSound(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ü�� ���� ����
    /// </summary>
    /// <param name="isLeft">���ʿ��ִ� ü������ Ȯ�� ����</param>
    private void ChainHitSound(bool isLeft)
    {
        // ���ʿ� �ִ� ü���̶��
        if (isLeft)
            // ���ʿ��� �鸮�� ���� ���
            JGBossAudioManager.Instance.ChainHit_L();
        else
            // �����ʿ��� �鸮�� ���� ���
            JGBossAudioManager.Instance.ChainHit_R();
    }

    private IEnumerator MoveCoroutine(MoveDirection direction)
    {
        // �̵� �ִϸ��̼� ����
        animator.SetTrigger(moveHash);
        // ���� ����Ʈ ��ȯ
        JGBossGameManager.Instance.SpawnDust(transform.position);

        float curTime = 0;
        float percent = 0;
        // ���� ��ġ
        Vector3 curPos = tr.position;
        // �̵� ��ġ
        Vector3 targetPos = Vector3.zero;
        // �̵� ���⿡ �°� �̵� ��ġ ����
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

        // X���� �Ҽ����� ���� ����
        targetPos = new Vector3(Mathf.Round(targetPos.x), targetPos.y);

        // �÷��̾� �̵�
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
        // Ư�� ��ġ�� �̵��ϸ� �÷��̾� ���
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
