using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuciferCutScene : MonoBehaviour
{
    // 뒷 배경
    [SerializeField]
    private GameObject[] backGrounds;
    // 가드 스켈레톤
    [SerializeField]
    private GameObject[] guards;
    // 루시퍼 이미지
    [SerializeField]
    private GameObject demonImage;
    // 루시퍼 이미지 변경 스프라이트
    [SerializeField]
    private Sprite[] demonSprite;
    // 대사 텍스트
    [SerializeField]
    private Text dialogText;
    // 부퍼
    [SerializeField]
    private GameObject booper;
    // 선택지 메뉴
    [SerializeField]
    private GameObject selectMenu;
    // 두번째 선택지 메뉴
    [SerializeField]
    private GameObject secondSelectMenu;
    // 해피 엔드 메뉴
    [SerializeField]
    private GameObject goodEndMenu;
    // 현재 대사 인덱스
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    // 인트로 대사
    [SerializeField, TextArea]
    private string[] text_before;
    // 첫번째 선택지 선택후 대사
    [SerializeField, TextArea]
    private string[] text_after;
    // 두번쨰 선택지 선택후 대사
    [SerializeField, TextArea]
    private string[] text_second_after;

    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action<bool> selectBad;
    public static Action<bool> selectGood;
    // 배드엔드, 해피엔드 선택했는지 확인 변수
    private bool isBad = false;
    private bool isSecondBad = false;
    private bool isGood = false;

    private void Awake()
    {
        // 액션 변수 값 지정
        selectBad = (value) => { SelectBad(value); };
        selectGood = (value) => { SelectGood(value); };
        curDialogTextIndex = 0;
    }

    private IEnumerator Start()
    {
        float movetime = 0;
        // 뒷 배경 이동
        foreach (var back in backGrounds)
        {
            DialogBackGroundMove move = back.GetComponent<DialogBackGroundMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = movetime < move.MoveTime ? move.MoveTime : movetime;
        }
        yield return new WaitForSeconds(movetime);
        // 가드 스켈레톤 이동
        foreach (var guard in guards)
        {
            GuardMove move = guard.GetComponent<GuardMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = move.MoveTime;
        }
        yield return new WaitForSeconds(movetime + 1.5f);
        // 대사 텍스트 변경
        dialogText.text = text_before[0];
        // 루시퍼 이동
        StartCoroutine(demonImage.GetComponent<LuciferSpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        // 다이얼로그가 비활성화 상태면 메서드 종료
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance.IsDialog)
            {
                // 다음 대사가 있으면
                if (curDialogTextIndex < text_before.Length - 1)
                {
                    // 다음 대사 인덱스로 값 증가
                    curDialogTextIndex++;
                    // 대사 변경
                    dialogText.text = text_before[curDialogTextIndex];
                    // 다이얼로그 출력 완료 사운드 재생
                    AudioManager.Instance.DialogComFirm();
                }
                // 현재 대사가 text_second_after[1]과 같다면
                else if (dialogText.text.Contains(text_second_after[1]))
                {
                    // 대사 변경
                    dialogText.text = text_second_after[2];
                    // 다이얼로그 출력 완료 사운드 재생
                    AudioManager.Instance.DialogComFirm();
                    isGood = true;
                    GameManager.Instance.IsDialog = false;
                    // 해피 엔드 UI 활성화
                    goodEndMenu.SetActive(true);
                    // 루시퍼 스프라이트 변경
                    demonImage.GetComponent<Image>().sprite = demonSprite[1];
                }
                // 선택 메뉴로 변경
                else
                {
                    // 선택 메뉴 활성화
                    selectMenu.SetActive(true);
                    GameManager.Instance.IsDialog = false;
                    GameManager.Instance.IsSelect = true;
                    // 부퍼 비활성화
                    booper.SetActive(false);
                }
            }
            else if (isGood || isBad || isSecondBad)
            {
                if (isGood)
                {
                    // 해피 엔드 메서드 실행
                    GameManager.Instance.GoodEnd();
                    // 컷씬 비활성화
                    gameObject.SetActive(false);
                    Debug.Log("Good End");
                }
                else if (isBad)
                {
                    // 천국으로 가는 엔딩 씬을 따로 제작
                    if (GameManager.Instance.CurStage == 5)
                    {
                        Debug.Log("Go to Heaven");
                    }
                    else
                        // 베드 엔드 메서드 실행
                        GameManager.Instance.BadEnd();
                    Debug.Log("Bad End");
                }
                else if (isSecondBad)
                {
                    // 베드 엔드 메서드 실행
                    GameManager.Instance.BadSecondEnd();
                }
            }
        }
    }

    /// <summary>
    /// 베드 엔드 선택시
    /// </summary>
    /// <param name="isSecond">루시퍼 챕터에서 두번째 선택지에서 베드엔드 인지 확인 변수</param>
    private void SelectBad(bool isSecond = false)
    {
        // 부퍼 활성화
        booper.SetActive(true);

        if (!isSecond)
        {
            // 선택 메뉴 비활성화
            selectMenu.SetActive(false);
            isBad = true;
            // 대사 텍스트 변경
            dialogText.text = text_after[0];
        }
        else
        {
            // 두번째 선택 메뉴 비활성화
            secondSelectMenu.SetActive(false);
            isSecondBad = true;
            // 대사 텍스트 변경
            dialogText.text = text_second_after[0];
        }
    }

    /// <summary>
    /// 헤피 엔드 선택시
    /// </summary>
    /// <param name="isSecond"> 루시퍼 챕터에서 두번째 선택지에서 해피엔드 인지 확인 변수</param>
    private void SelectGood(bool isSecond = false)
    {
        if (!isSecond)
        {
            // 선택 메뉴 비활성화
            selectMenu.SetActive(false);
            // 두번쨰 선택 메뉴 활성화
            secondSelectMenu.SetActive(true);
            // 대사 텍스트 변경
            dialogText.text = text_after[1];
            // 루시퍼 애니메이터 비활성화
            demonImage.GetComponent<Animator>().enabled = false;
            // 루시퍼 이미지 변경
            demonImage.GetComponent<Image>().sprite = demonSprite[0];
        }
        else
        {
            // 부퍼 활성화
            booper.SetActive(true);
            // 두번쨰 선택 메뉴 비활성화
            secondSelectMenu.SetActive(false);
            // 대사 텍스트 변경
            dialogText.text = text_second_after[1];
            GameManager.Instance.IsDialog = true;
            GameManager.Instance.IsSelect = false;
        }
    }
}
