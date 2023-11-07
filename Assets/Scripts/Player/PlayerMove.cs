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
    // �����̴µ� �ɸ��� �ð�
    [SerializeField]
    private float moveTime = 1f;
    // �̵���ġ ������Ʈ Ȯ�ο� �ݶ��̴� ������
    [SerializeField]
    private float circleRadius = 0.4f;
    // �浹 ���� ���̾�
    [SerializeField]
    private LayerMask checkLayer;

    private Animator animator;
    private Collider2D hit;
    private SpriteRenderer spriteRenderer;
    private Transform tr;
    
    // �ִϸ����� �Ķ���� �ؽ� ��
    private int clearHash = Animator.StringToHash("Clear");
    private int moveHash = Animator.StringToHash("Move");
    private int kickHash = Animator.StringToHash("Kick");

    // ���� �����̴��� Ȯ�� ����
    private bool isMove = false;

    private void Start()
    {
        // �� ������Ʈ ȹ��
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
            // �÷��̾��� ������ ��ġ�� ����
            Vector3 checkPos = new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y + 0.25f);
            // ������ ��ġ�� ������Ʈ Ȯ��
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // ������Ʈ�� ����, �̵��� �����ϸ�
            if (hit == null)
            {
                // ������ �Ű������� ��ġ �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Right));
                // ��������Ʈ ����
                spriteRenderer.flipX = false;
            }
            else
            {
                // ������ �Ű������� �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �÷��̾��� ���� ��ġ�� ����
            Vector3 checkPos = new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y + 0.25f);
            // ���� ��ġ�� ������Ʈ Ȯ��
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);
            // ��������Ʈ ����
            spriteRenderer.flipX = true;

            // ������Ʈ�� ����, �̵��� �����ϸ�
            if (hit == null)
            {
                // ������ �Ű������� ��ġ �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Left));
                // ��������Ʈ ����
                spriteRenderer.flipX = true;
            }
            else
            {
                // ������ �Ű������� �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �÷��̾��� ���� ��ġ�� ����
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset + 0.25f);
            // ���� ��ġ�� ������Ʈ Ȯ��
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // ������Ʈ�� ����, �̵��� �����ϸ�
            if (hit == null)
                // ������ �Ű������� ��ġ �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Up));
            else
            {
                // ������ �Ű������� �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // �÷��̾� �Ʒ��� ��ġ�� ����
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset + 0.25f);
            // �Ʒ��� ��ġ�� ������Ʈ Ȯ��
            hit = Physics2D.OverlapCircle(checkPos, circleRadius, checkLayer);

            // ������Ʈ�� ����, �̵��� �����ϸ�
            if (hit == null)
                // ������ �Ű������� ��ġ �̵�
                StartCoroutine(MoveCoroutine(MoveDirection.Down));
            else
            {
                // ������ �Ű������� �浹 ������Ʈ Ȯ��
                CheckImpactObject(MoveDirection.Down);
            }
        }
    }

    /// <summary>
    /// ������ ������Ʈ Ȯ��
    /// </summary>
    /// <param name="direction">�̵� ����</param>
    private void CheckImpactObject(MoveDirection direction)
    {
        // �̵� ���⿡ ������Ʈ�� ���ٸ� �޼��� ����
        if (hit == null) return;

        // ������Ʈ�� �±װ� Skeleton, Stone�̸�
        if (hit.transform.CompareTag("Skeleton") || hit.transform.CompareTag("Stone"))
        {
            ObjectMove move = hit.transform.GetComponent<ObjectMove>();
            move.Move(direction);
            Vector3 spawnPos = hit.transform.position;
            GameManager.Instance.SpawnHit(new Vector3(spawnPos.x, spawnPos.y + 0.25f, spawnPos.z));
            // ű �ִϸ��̼� ����
            animator.SetTrigger(kickHash);
            // �̵� Ƚ�� ����
            GameManager.Instance.FootCountMinus();
        }
        // ������Ʈ�� �±װ� Wall�̸�
        else if (hit.transform.CompareTag("Wall"))
        {
        }
    }

    /// <summary>
    /// �÷��̾� ��ġ �̵� �ڷ�ƾ
    /// </summary>
    /// <param name="direction">�̵� ����</param>
    /// <returns></returns>
    private IEnumerator MoveCoroutine(MoveDirection direction)
    {
        // �̵� ���� ���
        AudioManager.Instance.PlayerMove();
        // �̵� �ִϸ��̼� ����
        animator.SetTrigger(moveHash);
        // ���� ��ġ���� y���� ���� �÷� ���� ����
        GameManager.Instance.SpawnDust(new Vector3(tr.position.x, tr.position.y + 0.25f, tr.position.z));
        // �̵� Ƚ�� ����
        GameManager.Instance.FootCountMinus();

        // �̵� �Ұ��� ����
        isMove = true;
        float curTime = 0;
        float percent = 0;
        // ���� ��ġ ����
        Vector3 curPos = tr.position;
        // ��ǥ ��ġ ���� ����
        Vector3 targetPos = Vector3.zero;

        // ���⿡ ���� ��ǥ ��ġ ����
        switch (direction)
        {
            // ������
            case MoveDirection.Right:
                // ���� ��ġ���� ������ ����ŭ ����
                targetPos = new Vector3(curPos.x + GameManager.Instance.MoveXOffset, curPos.y);
                break;
            // ����
            case MoveDirection.Left:
                // ���� ��ġ���� ������ ����ŭ ����
                targetPos = new Vector3(curPos.x - GameManager.Instance.MoveXOffset, curPos.y);
                break;
            // ����
            case MoveDirection.Up:
                // ���� ��ġ���� ������ ����ŭ ����
                targetPos = new Vector3(curPos.x, curPos.y + GameManager.Instance.MoveYOffset);
                break;
            // �Ʒ���
            case MoveDirection.Down:
                // ���� ��ġ���� ������ ����ŭ ����
                targetPos = new Vector3(curPos.x, curPos.y - GameManager.Instance.MoveYOffset);
                break;
        }

        // �÷��̾� �̵�
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            tr.position = Vector3.Lerp(curPos, targetPos, percent);
        }

        // �÷��̾� ��ǥ ��ġ�� ����
        tr.position = targetPos;
        // �̵��� �����ϵ��� ����
        isMove = false;
    }

    // �̵� ���� ������Ʈ üũ ����ȭ
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
