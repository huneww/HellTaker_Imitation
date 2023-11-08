using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBackGroundMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;
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

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public IEnumerator MoveCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = rectTransform.localPosition;

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
