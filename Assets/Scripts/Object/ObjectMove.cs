using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // ������Ʈ �̵� �ð�
    [SerializeField]
    private float moveTime = 1f;
    // �浹 ������ ���̾�
    [SerializeField]
    private LayerMask checkLayer;

    // ��ǥ �̵� ��ġ
    private Vector3 targetPos;
    // ���̸��� �ִϸ�����
    private Animator animator;
    // ��������Ʈ ������
    private SpriteRenderer spriteRenderer;
    // �ִϸ����� �Ķ���� �ؽ� ��
    private int hitHash = Animator.StringToHash("Hit");

    private void Start()
    {
        // ������Ʈ�� ���̷����̸� �ִϸ����� ������Ʈ ȹ��
        if (gameObject.CompareTag("Skeleton"))
            animator = GetComponent<Animator>();
        // ��������Ʈ ������ ������Ʈ ȹ��
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(MoveDirection direction)
    {
        // �ִϸ����Ͱ� �ִ��� Ȯ��
        if (animator != null)
            // ��Ʈ �ִϸ��̼� ����
            animator.SetTrigger(hitHash);

        // ������ ������Ʈ�� �����̶��
        // ���� ���� �Ҹ� ���
        if (gameObject.CompareTag("Stone"))
            AudioManager.Instance.StoneKick();

        // ���⿡ ���� ��ǥ ��ġ�� ����
        switch (direction)
        {
            case MoveDirection.Right:
                targetPos = new Vector3(transform.position.x + GameManager.Instance.MoveXOffset, transform.position.y);
                // ���̷����� ��������Ʈ ���� �� ����
                if (gameObject.CompareTag("Skeleton")) spriteRenderer.flipX = true;
                break;
            case MoveDirection.Left:
                targetPos = new Vector3(transform.position.x - GameManager.Instance.MoveXOffset, transform.position.y);
                // ���̷����� ��������Ʈ ���� �� ����
                if (gameObject.CompareTag("Skeleton")) spriteRenderer.flipX = false;
                break;
            case MoveDirection.Up:
                targetPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.MoveYOffset);
                break;
            case MoveDirection.Down:
                targetPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.MoveYOffset);
                break;
        }

        // ��ǥ ��ġ�� ������Ʈ�� �ִ��� Ȯ��
        // ��ǥ ��ġ�� ������Ʈ�� �ִٸ�
        if (CheckTargetPosObj())
        {
            Debug.Log("Behind is Wall");
            // ������Ʈ�� ���̷����̶��
            if (gameObject.CompareTag("Skeleton"))
            {
                Dead();
            }
        }
        else
        {
            // ������Ʈ ��ǥ ��ġ�� �̵�
            StartCoroutine(MoveCoroutine());
        }
    }

    private void Dead()
    {
        // ���̷��� �״� ���� ���
        AudioManager.Instance.EnemyDie();
        // ���̷��� �� ��ƼŬ ����
        GameManager.Instance.SpawnBornParticle(transform.position);
        // ���̷��� ������Ʈ ����
        Destroy(this.gameObject);
    }

    private IEnumerator MoveCoroutine()
    {
        // �̵��� ������Ʈ�� ���̷����̶�� ���̷��� ���� �Ҹ� ���
        if (gameObject.CompareTag("Skeleton")) AudioManager.Instance.EnemyKick();
        // �̵��� ������Ʈ�� �����̶�� ���� �̵� �Ҹ� ���
        // ������ �̵����� �ȴ��� ���� �Ҹ��� �����
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
    /// ��ǥ ��ġ�� ������Ʈ�� �ִ��� Ȯ�� �޼���
    /// </summary>
    /// <returns>������ ��, ������ ����</returns>
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
    /// ������ƮȮ�� OverlapCircle ����ȭ
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
