using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;
    // �ִϸ����� �Ķ���� �ؽ���
    private int hitHash = Animator.StringToHash("RandomHit");

    private void Start()
    {
        // �ִϸ����� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        // ��Ʈ �ִϸ��̼� 2�� �� 1�� ����
        animator.SetInteger(hitHash, Random.Range(0, 2));
    }

    private void Update()
    {
        // �ִϸ��̼��� ������ ��Ʈ ����Ʈ ����
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(this.gameObject);
    }

}
