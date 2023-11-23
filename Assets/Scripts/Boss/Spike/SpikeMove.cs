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

        // ������ũ �ִϸ��̼� Up���·� ����
        animator.SetTrigger("Up");
    }

    private void Update()
    {
        // �����ϼ� �ִ� ���°� �ƴ϶�� �޼��� ����
        if (!JGBossGameManager.Instance.IsMove) return;

        // ������ũ ���� �̵�
        Vector3 movePos = new Vector3(tr.position.x,
                                      tr.position.y + JGBossGameManager.Instance.MoveUpSpeed * Time.deltaTime,
                                      tr.position.z);
        tr.position = movePos;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ���� �ݶ��̴��� �浹�Ѵٸ�
        if (collider.gameObject.CompareTag("SpikeColliderUp"))
        {
            // ��ġ ����
            tr.localPosition = new Vector3(tr.position.x - 0.5f, -2f, tr.position.z);
            // Up �ִϸ��̼� ����
            animator.SetTrigger("Up");
        }
        // �Ʒ��� �ݶ��̴��� �浹�Ѵٸ�
        else if (collider.gameObject.CompareTag("SpikeColliderDown"))
        {
            // ���� ��ġ ����
            Vector3 movePos = new Vector3(tr.position.x - 0.5f, -1.5f, tr.position.z);
            // Down �ִϸ��̼� ����
            animator.SetTrigger("Down");
            // ��ġ ���� �ڷ�ƾ ����
            StartCoroutine(SpikeMoveDown(movePos, 0.5f));
        }
    }

    private IEnumerator SpikeMoveDown(Vector3 movePos, float delay)
    {
        yield return new WaitForSeconds(delay);
        // ��ġ ����
        tr.localPosition = movePos;
        // Up �ִϸ��̼� ����
        animator.SetTrigger("Up");
    }

}
