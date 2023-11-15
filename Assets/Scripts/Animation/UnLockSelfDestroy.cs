using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockSelfDestroy : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;

    private void Start()
    {
        // �ִϸ����� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �ִϸ��̼��� ������
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            // ������Ʈ ����
            Destroy(this.gameObject);
        }
    }

}
