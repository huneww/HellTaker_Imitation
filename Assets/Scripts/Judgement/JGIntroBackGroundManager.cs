using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class JGIntroBackGroundManager : MonoBehaviour
{
    private static JGIntroBackGroundManager instance;
    public static JGIntroBackGroundManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [Space(10), Header("BackGround")]
    // 첫번째 뒷 배경
    [SerializeField]
    private GameObject firstBack;
    // 밝은 뒷 배경
    // 인트로 애니메이션 실행시 잠깐 나왔다가 사라짐
    [SerializeField]
    private GameObject lightHell;
    // 두번째 뒷 배경
    [SerializeField]
    private GameObject secondBack;
    // 첫번째 배경에서 두번째 배경으로 넘어가는 시간
    [SerializeField]
    private float backMoveTime = 1f;
    // 첫번째 배경 이동 위치
    [SerializeField]
    private Vector3 firstTarget;
    // 두번쟤 배경 이동 위치
    [SerializeField]
    private Vector3 secondTarget;

    [Space(10), Header("Chain")]
    // 처음 내려올 체인
    [SerializeField]
    private GameObject firstChain;
    // 두번째로 내려올 체인
    [SerializeField]
    private GameObject secondChain;
    // 서번째로 내려올 체인
    [SerializeField]
    private GameObject thirdChain;

    [Space(10), Header("Arm")]
    // 저지먼트 팔
    [SerializeField]
    private GameObject[] arms;
    // 팔 애니메이션 파라미터 해쉬값
    private int armMoveHash = Animator.StringToHash("Move");

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public IEnumerator IntroCoroutine()
    {
        // 밝은 뒷 배경 페이드 아웃
        LightHellFadeOut();
        // 일정시간 대기
        yield return new WaitForSeconds(1.7f);
        // 팔 애니메이션 실행
        foreach (var arm in arms)
        {
            arm.GetComponent<Animator>().SetTrigger(armMoveHash);
        }
        // 일정시간 대기
        yield return new WaitForSeconds(2f);
        // 뒷 배경 애니메이션 재생
        StartCoroutine(BackGroundMove());
    }

    private IEnumerator BackGroundMove()
    {
        float curTime = 0;
        float percent = 0;

        // 첫번째 배경 현재 위치 저장
        Vector3 firstPos = firstBack.GetComponent<RectTransform>().localPosition;
        // 두번째 배경 현재 위치 저장
        Vector3 secondPos = secondBack.GetComponent<RectTransform>().localPosition;

        // 배경 위치 이동
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / backMoveTime;
            firstBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(firstPos, firstTarget, percent);
            secondBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(secondPos, secondTarget, percent);
            yield return null;
        }

        // 첫번째 배경 비활성화
        firstBack.SetActive(false);
        // 두번째 배경 이동위치로 위치 변경
        secondBack.GetComponent<RectTransform>().localPosition = secondTarget;

        // 대기시간 생성
        WaitForSeconds chainDelay = new WaitForSeconds(0.1f);
        // 첫번째 체인 활성화
        firstChain.SetActive(true);
        yield return chainDelay;
        // 두번째 체인 활성화
        secondChain.SetActive(true);
        yield return chainDelay;
        // 세번째 체인 활성화
        thirdChain.SetActive(true);
    }

    private void LightHellFadeOut()
    {
        // 밝은 뒷 배경 활성화
        lightHell.SetActive(true);
        // 뒷 배경 페이드 아웃
        StartCoroutine(lightHell.GetComponent<FadeOutBackGround>().FadeOut());
    }

}
