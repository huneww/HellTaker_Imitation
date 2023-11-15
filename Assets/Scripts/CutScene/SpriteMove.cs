using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpriteMove : MonoBehaviour
{
    // 이동 위치
    [SerializeField]
    private Vector3 targetPos;
    // 이동 시간
    [SerializeField]
    private float moveTime = 1f;
    // 이름, 대사 텍스트
    [SerializeField]
    private GameObject[] texts;
    // 부퍼
    [SerializeField]
    private GameObject booper;

    private RectTransform rectTransform;
    private Image image;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 이름, 대사 부퍼 활성화
    /// </summary>
    private void TextActive()
    {
        foreach (var text in texts)
            text.SetActive(true);
        booper.SetActive(true);
    }

    public IEnumerator MoveCoroutine()
    {
        // 현재 9챕터라면 이름, 대사, 부퍼만 활성화
        if (GameManager.Instance.CurStage == 8)
        {
            TextActive();
            yield break;
        }

        float curTime = 0;
        float percent = 0;

        // 현재 위치 저장
        Vector3 curPos = rectTransform.localPosition;
        // 현재 색상 저아
        Color color = image.color;

        // 이동 위치로 이동
        while (percent < 1.0f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rectTransform.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(0, 255, percent);
            image.color = color;
        }

        rectTransform.localPosition = targetPos;
        color.a = 255;
        image.color = color;
        // 이름, 대사, 부퍼 활성화
        TextActive();
        // 다이얼로그 출력 완료 사운드 재생
        AudioManager.Instance.DialogComFirm();
    }

}
