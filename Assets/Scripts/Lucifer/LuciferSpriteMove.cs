using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuciferSpriteMove : MonoBehaviour
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

    // 각 컴포넌트
    private RectTransform rectTransform;
    private Image image;
    private Animator animator;

    // 애니메이션 파라미터 해쉬값
    private readonly int hashSwirl = Animator.StringToHash("Swirl");
    private readonly int hashIdle = Animator.StringToHash("Idle");

    private void Start()
    {
        // 각 컴포넌트 획득
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public IEnumerator MoveCoroutine()
    {
        // 루시퍼 이동 위치로 이동
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = rectTransform.localPosition;
        Color color = image.color;

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

        // 위치 이동 후
        yield return new WaitForSeconds(0.25f);
        // 잔 흔드는 애니메이션 실행
        animator.SetTrigger(hashSwirl);
        // 잔 흔드는 사운드 재생
        AudioManager.Instance.Swirl();
        yield return new WaitForSeconds(0.01f);
        // 잔 흔드는 애니메이션 보다 0.25초 대기 후
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.25f);
        // 기본 상태 애니메이션 실행
        animator.SetTrigger(hashIdle);
        // 이름, 대사, 부퍼 활성화
        foreach (var text in texts)
            text.SetActive(true);
        booper.SetActive(true);
        // 루시퍼 인트로 사운드 재생
        AudioManager.Instance.LuciferIntro();
    }
}
