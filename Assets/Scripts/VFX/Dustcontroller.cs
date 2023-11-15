using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dustcontroller : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;
    // �ִϸ��̼� �Ķ���� �ؽ���
    private int random = Animator.StringToHash("RandomDust");

    private void Start()
    {
        // �ִϸ����� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        // ���� �ִϸ��̼� 3�� �� 1�� ����
        animator.SetInteger(random, Random.Range(0, 3));
    }

    private void Update()
    {
        // �ִϸ��̼� ������ ���� ����Ʈ ����
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            Destroy(this.gameObject);
    }

}
