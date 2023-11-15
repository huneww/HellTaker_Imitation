using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBackGroundMove : MonoBehaviour
{
    // 이동 위치
    [SerializeField]
    private Vector3 targetPos;
    // 이동 시간
    [SerializeField]
    private float moveTime;
    public float MoveTime
    {
        get
        {
            return moveTime;
        }
    }

    private RectTransform rectTransform;
    private Vector3 curPos;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        curPos = rectTransform.localPosition;
    }

    public IEnumerator MoveCoroutine()
    {
        // 이동 위치로 이동
        float curTime = 0;
        float percent = 0;

        while (percent < 1.0f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rectTransform.localPosition = Vector3.Lerp(curPos, targetPos, percent);
        }

        rectTransform.localPosition = targetPos;
    }

}
