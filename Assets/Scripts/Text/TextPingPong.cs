using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPingPong : MonoBehaviour
{
    // x,y축 동일한 시간동안 하는지 확인 변수
    public bool sameTime;
    // x,y축 이동 시간
    public float xTime = 1f;
    public float yTime = 1f;
    // x,y축 애니메이션 커브
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    // 텍스트 렉트트랜스폼
    private RectTransform tr;

    private void OnEnable()
    {
        // 활성화 될때 마다 코루틴 실행
        StartCoroutine(Visible());
    }

    private void Awake()
    {
        // 코루틴 실행전에 렉트트랜스폼 컴포넌트 획득
        tr = GetComponent<RectTransform>();
    }

    private IEnumerator Visible()
    {
        // 경과 시간 저장 변수
        float curtime = 0f;
        // xTime 와 yTime중 큰 값을 선택
        float maxTime = xTime >= yTime ? xTime : yTime;

        while (curtime < maxTime)
        {
            curtime += Time.deltaTime;
            // 커프를 time에 걸쳐서 진행
            float xSize = xCurve.Evaluate(curtime / xTime);
            float ySize = yCurve.Evaluate(curtime / yTime);
            // 커프에서 획득한 값을 사이즈에 저장
            tr.localScale = new Vector3(xSize, ySize, 1);
            yield return null;
        }

        // 사이즈를 커브의 맨 마지막 값으로 지정
        tr.localScale = new Vector3(xCurve.Evaluate(1), yCurve.Evaluate(1), 1);

        yield return null;
    }
}