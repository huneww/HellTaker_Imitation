using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSelectMenu : MonoBehaviour
{
    // 기본 상태 색상
    [SerializeField]
    private Color IdleColor;
    // 하이라이트 상태 색상
    [SerializeField]
    private Color HighlightColor;
    // 텍스트 이미지
    [SerializeField]
    private Image[] textImages;
    // 텍스트
    [SerializeField]
    private Text[] texts;
    // 선택 메뉴 텍스트
    [SerializeField, TextArea]
    private string[] text_select;
    // 현재 선택중인 텍스트 인덱스
    [SerializeField]
    private int curIndex;

    private void Start()
    {
        // 색상 초기화
        ChangeColor();
        // 선택 메뉴 텍스트 변경
        texts[0].text = text_select[0];
        texts[1].text = text_select[1];
    }

    private void Update()
    {
        // 선택 메뉴에 있는 상태가 아니면 메서드 종료
        if (!GameManager.Instance.IsSelect)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 현재 인덱스 감소
            curIndex = curIndex > 0 ? --curIndex : curIndex;
            Debug.Log(curIndex);
            // 색상 변경
            ChangeColor();
            // 메뉴 이동 사운드 재생
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 현재 인덱스 증가
            curIndex = curIndex < textImages.Length - 1 ? ++curIndex : curIndex;
            Debug.Log(curIndex);
            // 색상 변경
            ChangeColor();
            // 메뉴 이동 사운드 재생
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // 메뉴 선택 사운드 재생
            AudioManager.Instance.DialogSelect();
            // 선택 메뉴에 맞는 코드 실행
            SelectMenu();
        }
    }

    /// <summary>
    /// 선택한 메뉴에 따라 코드 실행
    /// </summary>
    private void SelectMenu()
    {
        // 특정 챕터는 첫번째 선택지가 해피 엔드
        if (GameManager.Instance.CurStage == 2 || GameManager.Instance.CurStage == 3 ||
            GameManager.Instance.CurStage == 4 || GameManager.Instance.CurStage == 5)
        {
            if (curIndex == 0)
            {
                CutSceneController.selectGood();
            }
            else if (curIndex == 1)
            {
                CutSceneController.selectBad();
            }
        }
        // 저스티스 챕터는 둘다 해피 엔드
        else if (GameManager.Instance.CurStage == 6)
        {
            CutSceneController.selectGood();
        }
        else
        {
            if (curIndex == 0)
            {
                // 루시퍼 챕터이고
                if (GameManager.Instance.CurStage == 7)
                {
                    // 현재 선택 메뉴가 두번째라면
                    if (gameObject.CompareTag("SecondSelect"))
                        LuciferCutScene.selectBad(true);
                    else
                        LuciferCutScene.selectBad(false);
                }
                else
                    CutSceneController.selectBad();
            }
            else if (curIndex == 1)
            {
                // 루시퍼 챕터이고
                if (GameManager.Instance.CurStage == 7)
                {
                    // 현재 선택 메뉴가 두번째라면
                    if (gameObject.CompareTag("SecondSelect"))
                        LuciferCutScene.selectGood(true);
                    else
                        LuciferCutScene.selectGood(false);
                }
                else
                    CutSceneController.selectGood();
            }
        }
    }

    /// <summary>
    /// 선택 메뉴 색상 변경
    /// </summary>
    private void ChangeColor()
    {
        // 이미지, 텍스트 색상 기본상태로 변경
        foreach (var image in textImages)
            image.color = IdleColor;
        foreach (var text in texts)
            text.color = Color.gray;

        // 선택한 이미ㅣㅈ, 텍스트 색상 하이라이트로 변경
        textImages[curIndex].color = HighlightColor;
        texts[curIndex].color = Color.white;
    }

}
