using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePingPong : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve xCurve;
    [SerializeField]
    private AnimationCurve yCurve;
    [SerializeField]
    private float time = 1f;

    private RectTransform rectTR;

    public static Action cutScenePingPong;

    private void Start()
    {
        rectTR = GetComponent<RectTransform>();
        cutScenePingPong = () => { PingPong(); };
    }

    private void PingPong()
    {
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
            rectTR.localScale = new Vector3(xCurve.Evaluate(percent), yCurve.Evaluate(percent), rectTR.localScale.z);
        }

        rectTR.localScale = new Vector3(xCurve.Evaluate(1), yCurve.Evaluate(1), rectTR.localScale.z);
    }

}
