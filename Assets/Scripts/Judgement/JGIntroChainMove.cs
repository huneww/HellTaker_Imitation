using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroChainMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    [SerializeField]
    private float moveTime = 1f;
    [SerializeField]
    private float pingPongDis = 5f;

    private RectTransform rect;
    private Image image;

    private IEnumerator Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = rect.localPosition;
        Vector3 pingPongPos = Vector3.zero;

        if (gameObject.name.Contains("R"))
        {
            pingPongPos.Set(targetPos.x + pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }
        else if (gameObject.name.Contains("L"))
        {
            pingPongPos.Set(targetPos.x - pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rect.localPosition = Vector3.Lerp(curPos, pingPongPos, percent);
            yield return null;
        }

        rect.localPosition = pingPongPos;
        curPos = pingPongPos;
        curTime = 0;
        percent = 0;
        Color color = image.color;

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / (moveTime * 1.25f);
            rect.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(1, 0.63f, percent);
            image.color = color;
            yield return null;
        }

        rect.localPosition = targetPos;
        color.a = 0.63f;
        image.color = color;
    }

}
