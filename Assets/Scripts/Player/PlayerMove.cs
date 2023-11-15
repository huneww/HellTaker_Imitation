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

    private void Start()
    {
        // �� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        // �����ϼ� �ִ� ���°� �ƴ϶�� �޼��� ����
        if (GameManager.Instance.IsDialog || GameManager.Instance.IsSelect || GameManager.Instance.IsDead)
            return;

        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // �÷��̾��� ������ ��ġ�� ����
            Vector3 checkPos = new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y);
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
            Vector3 checkPos = new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y);
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
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset);
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
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset);
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
            GameManager.Instance.SpawnHit(hit.transform.position);
            // ű �ִϸ��̼� ����
            animator.SetTrigger(kickHash);
            // �̵� Ƚ�� ����
            GameManager.Instance.FootCountMinus();
        }
        // ������Ʈ�� �±װ� LockBox�̸�
        else if (hit.transform.CompareTag("LockBox"))
        {
            Debug.Log("Lock Box");
            // Ű�� ���� ����
            if (GameManager.Instance.HaveKey)
            {
                GameManager.Instance.UnLockBox(hit.gameObject);
                // ������ �Ű������� ��ġ �̵�
                StartCoroutine(MoveCoroutine(direction));
            }
            // Ű�� ���� ���� ����
            else
            {
                animator.SetTrigger(kickHash);
                AudioManager.Instance.DoorKick();
                GameManager.Instance.FootCountMinus();
                StartCoroutine(hit.transform.GetComponent<LockBox>().ObjectShake());
            }

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
        GameManager.Instance.SpawnDust(transform.position);
        // �̵� Ƚ�� ����
        GameManager.Instance.FootCountMinus();

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Goal"))
        {
            GameManager.Instance.Goal();
        }
        else if (collision.transform.CompareTag("Key"))
        {
            GameManager.Instance.GetKey(collision.gameObject);
        }
    }

    // �̵� ���� ������Ʈ üũ ����ȭ
    private void OnDrawGizmosSelected()
    {
        if (GameManager.Instance == null)
            return;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset), circleRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset), circleRadius);
    }

}
