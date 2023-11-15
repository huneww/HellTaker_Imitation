using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeel : MonoBehaviour
{
    [SerializeField]
    // 이동 위치
    private Vector3 targetPos;
    [SerializeField]
    // 이동 시간
    private float moveTime;
    // 현재 위치
    private Vector2 curPos;
    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action move;
    public static Action<bool> active;

    private void Start()
    {
        // 액션 메서드 연결
        move = () => { Move(); };
        active = (value) => { SetActive(value); };
        // 현재 위치 저장
        curPos = transform.position;
    }

    private void Move()
    {
        // 무브 코루틴 실행
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        // 벨 위치 이동
        float curTime = 0;
        float percent = 0;

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            transform.position = Vector3.Lerp(curPos, targetPos, percent);
            yield return null;
        }
        transform.position = targetPos;
    }

    private void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

}
