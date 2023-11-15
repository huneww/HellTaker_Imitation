using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutBackGround : MonoBehaviour
{
    // 페이드 시간
    [SerializeField]
    private float fadeTime = 1f;

    public IEnumerator FadeOut()
    {
        float curTime = 0;
        float percent = 0;

        // 이미지 컴포넌트 획득
        Image image = GetComponent<Image>();
        // 현재 컬러 저장
        Color color = image.color;

        // 알파값 조정
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, percent);
            image.color = color;
        }

        // 알파값 0으로 변경
        color.a = 0;
        image.color = color;

        // 오브젝트 삭제
        Destroy(this.gameObject, 0.25f);
    }
}
