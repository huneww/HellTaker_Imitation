using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMenuController : MonoBehaviour
{
    [SerializeField]
    // 일반 스테이지 기본 상태 컬러
    private Color idelColor;
    [SerializeField]
    // 일반 스테이지 하이라이트 상태 컬러
    private Color hightLightColor;
    [SerializeField]
    // 엑스트라 스테이지 기본 상태 컬러
    private Color exIdelColor;
    [SerializeField]
    // 엑스트라 스테이지 하이라이트 상태 컬러
    private Color exHightLightColor;
    [SerializeField]
    // 현재 선택 중인 스테이지 인덱스
    private int curSelectMenu = 0;
    [SerializeField]
    // 스테이지 이미지
    private Image[] imageChildes;
    [SerializeField]
    // 스테이지 텍스트
    private Text[] textChildes;

    private void Awake()
    {
        // 자식 객체에서 컴포넌트 획득
        imageChildes = GetComponentsInChildren<Image>();
        textChildes = GetComponentsInChildren<Text>();
    }

    private void OnEnable()
    {
        // 활성화시 하이라이트 설정
        HightLight();
    }

    private void Update()
    {
        // 비활성화 상태면 메서드 조욜
        if (!gameObject.activeSelf) return;

        // 오른쪽 키를 누르면
        if (Input.GetKeyDown(MenuManager.Instance.RightKey[0]) || Input.GetKeyDown(MenuManager.Instance.RightKey[1]))
        {
            // 스테이지 보다 작다면 인덱스 값 변경
            curSelectMenu = curSelectMenu < imageChildes.Length - 1 ? curSelectMenu + 1 : curSelectMenu;
            // 하이라이트 변경
            HightLight();
            // 이동 사운트 재생
            MenuAudioManager.ChapterMove();
        }
        // 왼쪽 키를 누르면
        else if (Input.GetKeyDown(MenuManager.Instance.LeftKey[0]) || Input.GetKeyDown(MenuManager.Instance.LeftKey[1]))
        {
            //0보다 크다면 엔덱스 값 변경
            curSelectMenu = curSelectMenu > 0 ? curSelectMenu - 1 : curSelectMenu;
            // 하이라이트 변경
            HightLight();
            // 이동 사운트 재생
            MenuAudioManager.ChapterMove();
        }
        // 엔터 키를 누르면
        else if (Input.GetKeyDown(MenuManager.Instance.EnterKey))
        {
            // 현재 선택중인 인덱스 + 1값을 매개변수로 씬 로드
            MenuManager.Instance.StartStage(curSelectMenu + 1);
            // 선택 사운드 재생
            MenuAudioManager.ChapterSelect();
        }
        // ESC 키를 누르면
        else if (Input.GetKeyDown(MenuManager.Instance.ESCKey))
        {
            // 다시 메뉴
            MenuManager.Instance.ReturnToMenu();
            // 선택 사운드 재생
            MenuAudioManager.ChapterSelect();
        }
    }

    private void HightLight()
    {
        // 모든 스테이지 색상 기본 상태로 변경
        for (int i = 0; i < imageChildes.Length; i++)
        {
            // 엑스트라 스테이지 경우
            if (i == imageChildes.Length - 1)
            {
                // 엑스트라 컬러로 변경
                imageChildes[i].color = exIdelColor;
                textChildes[i].color = exIdelColor;
            }
            else
            {
                // 일반 스테이지 컬러로 변경
                imageChildes[i].color = idelColor;
                textChildes[i].color = Color.gray;
            }
        }

        // 현재 강조할 스테이지가 엑스트라 스테이지 경우
        if (curSelectMenu == imageChildes.Length - 1)
        {
            // 엑스트라 스테이지 컬로 변경
            imageChildes[curSelectMenu].color = exHightLightColor;
            textChildes[curSelectMenu].color = exHightLightColor;
        }
        else
        {
            // 일반 스테이지 컬러로 변경
            imageChildes[curSelectMenu].color = hightLightColor;
            textChildes[curSelectMenu].color = Color.white;
        }
    }

}
