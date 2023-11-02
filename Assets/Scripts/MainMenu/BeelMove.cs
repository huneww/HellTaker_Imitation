using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeelMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 1f;
    [SerializeField]
    private Vector3 targetPos = Vector3.zero;

    private void Start()
    {
        // 메인메뉴 스크립트의 델리게이트에 메서드 추가
        MainMenuManager.moveToCenter += Move;
    }

    private void OnDestroy()
    {
        // 파괴댈시 델리게이트에서 메서드 제거
        MainMenuManager.moveToCenter -= Move;
    }

    public void Move()
    {
        // 코루틴 호출
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = transform.position;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            transform.position = Vector3.Lerp(curPos, targetPos, percent);
        }
        transform.position = targetPos;
    }

}
