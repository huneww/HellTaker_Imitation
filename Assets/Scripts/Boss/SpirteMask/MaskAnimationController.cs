using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAnimationController : MonoBehaviour
{
    // 마스트 오브젝트의 애니메이터
    [SerializeField]
    private Animator maskAnimator;
    // 해골 머리 오브젝트의 애니메이터
    [SerializeField]
    private Animator skullHeadAnimator;

    /// <summary>
    /// 처음 챕터 시작시 다리 가동 애니메이션
    /// </summary>
    public void UpAnimationStart()
    {
        maskAnimator.SetTrigger("Move");
        skullHeadAnimator.SetTrigger("Up");
    }

    /// <summary>
    /// 챕터 클리어시 다리 가동 중지 애니메이션
    /// </summary>
    public void DownAnimationStart()
    {
        maskAnimator.SetTrigger("Move");
        skullHeadAnimator.SetTrigger("Down");
    }

}
