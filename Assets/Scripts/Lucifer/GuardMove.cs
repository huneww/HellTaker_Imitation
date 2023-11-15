using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardMove : MonoBehaviour
{
    // 이동 위치
    [SerializeField]
    private Vector3 targetPos;
    [SerializeField]
    // 이동 시간
    private float moveTime = 1f;
    public float MoveTime
    {
        get
        {
            return moveTime;
        }
    }

    private RectTransform rect;
    private Image image;
    
    public IEnumerator MoveCoroutine()
    {
        // 컴포넌트 획득
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        // 이동 위치로 이동
        Vector3 curPos = rect.localPosition;
        Color color = image.color;
        float curTime = 0;
        float percent = 0;

        // 가드 움직이는 사운드 재생
        AudioManager.Instance.LuciferGuardMove();

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rect.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(0, 255, percent);
            image.color = color;
            yield return null;
        }

        rect.localPosition = targetPos;
        color.a = 255;
        image.color = color;

    }

}
