using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroChainMove : MonoBehaviour
{
    // 이동 위치
    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    // 이동 시간
    [SerializeField]
    private float moveTime = 1f;
    // 핑퐁 거리
    [SerializeField]
    private float pingPongDis = 5f;

    private RectTransform rect;
    private Image image;

    private IEnumerator Start()
    {
        // 각 컴포넌트 획득
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        float curTime = 0;
        float percent = 0;

        // 현재 위치 저장
        Vector3 curPos = rect.localPosition;
        Vector3 pingPongPos = Vector3.zero;

        // 오른쪽 체인이라면
        if (gameObject.name.Contains("R"))
        {
            // x축은 증가, y축은 감소
            pingPongPos.Set(targetPos.x + pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }
        // 왼쪽 체인이라면
        else if (gameObject.name.Contains("L"))
        {
            // x,y축 둘다 감소
            pingPongPos.Set(targetPos.x - pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }

        // 체인 핑퐁위치까지 이동
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rect.localPosition = Vector3.Lerp(curPos, pingPongPos, percent);
            yield return null;
        }

        // 현재위치 핑퐁위치로 변경
        rect.localPosition = pingPongPos;
        curPos = pingPongPos;
        // 현재시간, 퍼센트 변수 초기화
        curTime = 0;
        percent = 0;
        // 이미지 색상 저장
        Color color = image.color;

        // 원래 이동 위치로 이동
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            // 원래 이동시간보다 조금더 걸리게 설정
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
