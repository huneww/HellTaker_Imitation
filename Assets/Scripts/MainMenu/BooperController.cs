using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BooperController : MonoBehaviour
{
    // �ִϸ����� ������Ʈ
    private Animator animator;
    // �ִϸ��̼� �Ķ���� �ؽ� ��
    private readonly int activeHash = Animator.StringToHash("BooperActive");
    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action booperActive;

    private void Start()
    {
        // �ִϸ����� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        // �׼� ���� �� ����
        booperActive = () => { BooperActive(); };
    }

    private void BooperActive()
    {
        // ���� �ִϸ��̼� Ȱ��ȭ
        animator.SetTrigger(activeHash);
    }

}
