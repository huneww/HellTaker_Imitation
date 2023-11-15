using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    // 뒷 배경
    [SerializeField]
    private GameObject[] backGrounds;
    // 악마 이미지
    [SerializeField]
    private GameObject demonImage;
    // 악마 이미지 변경 스프라이트
    [SerializeField]
    private Sprite demonChangeSprite;
    // 대사 텍스트
    [SerializeField]
    private Text dialogText;
    // 부퍼
    [SerializeField]
    private GameObject booper;
    // 선택 메뉴
    [SerializeField]
    private GameObject selectMenu;
    // 해피 엔드
    [SerializeField]
    private GameObject goodEndMenu;
    // 현재 대사 인덱스
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    // 선택 메뉴 이전 텍스트
    [SerializeField, TextArea]
    private string[] text_before;
    // 선택 메뉴 선택후 텍스트
    [SerializeField, TextArea]
    private string[] text_after;

    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action selectBad;
    public static Action selectGood;
    // 선택지 엔딩 확인 변수
    private bool isBad = false;
    private bool isGood = false;

    private void Awake()
    {
        // 액션 변수 값 지정
        selectBad = () => { SelectBad(); };
        selectGood = () => { SelectGood(); };
        curDialogTextIndex = 0;
    }

    private IEnumerator Start()
    {
        float movetime = 0;
        // 배경 위치 이동
        foreach (var back in backGrounds)
        {
            DialogBackGroundMove move = back.GetComponent<DialogBackGroundMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = movetime < move.MoveTime ? move.MoveTime : movetime;
        }
        yield return new WaitForSeconds(movetime);
        // 대사 텍스트 변경
        dialogText.text = text_before[0];
        // 악마 이미지 이동
        StartCoroutine(demonImage.GetComponent<SpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance.IsDialog)
            {
                // 다음 대사가 있다면
                if (curDialogTextIndex < text_before.Length - 1)
                {
                    // 인덱스값 증가
                    curDialogTextIndex++;
                    // 대사 텍스트 변경
                    dialogText.text = text_before[curDialogTextIndex];
                    // 다이얼로그 출력 완료 사운드 재생
                    AudioManager.Instance.DialogComFirm();
                }
                // 챕터 9에서는 바로 베드 엔드 실행
                else if (GameManager.Instance.CurStage == 8)
                {
                    GameManager.Instance.BadEnd();
                }
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
            else if (isGood || isBad)
            {
                if (isGood)
                {
                    // 해피 엔드 메서드 실행
                    GameManager.Instance.GoodEnd();
                    // 오브젝트 비활성화
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
            }
        }
    }

    /// <summary>
    /// 베드 엔드
    /// </summary>
    private void SelectBad()
    {
        booper.SetActive(true);
        selectMenu.SetActive(false);
        isBad = true;
        dialogText.text = text_after[0];
    }

    /// <summary>
    /// 해피 엔드
    /// </summary>
    private void SelectGood()
    {
        AudioManager.Instance.GoodEnd();
        demonImage.GetComponent<Image>().sprite = demonChangeSprite;
        selectMenu.SetActive(false);
        goodEndMenu.SetActive(true);
        isGood = true;
        dialogText.text = text_after[1];
    }

}
