using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAnimationController : MonoBehaviour
{
    // ����Ʈ ������Ʈ�� �ִϸ�����
    [SerializeField]
    private Animator maskAnimator;
    // �ذ� �Ӹ� ������Ʈ�� �ִϸ�����
    [SerializeField]
    private Animator skullHeadAnimator;

    /// <summary>
    /// ó�� é�� ���۽� �ٸ� ���� �ִϸ��̼�
    /// </summary>
    public void UpAnimationStart()
    {
        maskAnimator.SetTrigger("Move");
        skullHeadAnimator.SetTrigger("Up");
    }

    /// <summary>
    /// é�� Ŭ����� �ٸ� ���� ���� �ִϸ��̼�
    /// </summary>
    public void DownAnimationStart()
    {
        maskAnimator.SetTrigger("Move");
        skullHeadAnimator.SetTrigger("Down");
    }

}
