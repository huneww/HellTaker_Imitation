using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationSpeedRandom : MonoBehaviour
{
    // �ִϸ��̼� ������Ʈ
    private Animator animator;
    // �ִϸ��̼� ���ǵ� �ּҰ�
    [SerializeField]
    private float speedMin = 1.0f;
    // �ִϸ��̼� ���ǵ� �ִ밪
    [SerializeField]
    private float speedMax = 1.0f;
    // �ִϸ��̼� ���ǵ� �Ķ���� �ؽ���
    private int speedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        // �ִϸ����� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        // �ִϸ��̼� ���ǵ� ���� ����
        animator.SetFloat(speedHash, Random.Range(speedMin, speedMax));
    }

}
