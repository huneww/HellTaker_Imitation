using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePingPong : MonoBehaviour
{
    // x,y축 애니메이션 커브
    [SerializeField]
    private AnimationCurve xCurve;
    [SerializeField]
    private AnimationCurve yCurve;
    // 커브 시간
    [SerializeField]
    private float time = 1f;

    private RectTransform rectTR;

    // 다른 스크립트에서 접근을 우한 액션 변수
    public static Action cutScenePingPong;

    private void Start()
    {
        rectTR = GetComponent<RectTransform>();
        // 액션 변수 값 지정
        cutScenePingPong = () => { PingPong(); };
    }

    private void PingPong()
    {
        // 핑퐁 코루틴 실행
        StartCoroutine(PingPongCoroutine());
    }

    private IEnumerator PingPongCoroutine()
    {
        float curTime = 0f;
        float percent = 0f;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / time;
            // 애니메이션 커브의 에버레이트 값에 따라 로컬크기 변경
            rectTR.localScale = new Vector3(xCurve.Evaluate(percent), yCurve.Evaluate(percent), rectTR.localScale.z);
        }

        rectTR.localScale = new Vector3(xCurve.Evaluate(1), yCurve.Evaluate(1), rectTR.localScale.z);
    }

}
