using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    // 메뉴 기본 상태 색상
    private Color idelColor;
    [SerializeField]
    // 메뉴 하이라이트 상태 색상
    private Color hightLightColor;
    [SerializeField]
    // 현재 선택 중인 메뉴
    private int curSelectMenu = 0;
    [SerializeField]
    // 메뉴 이미지
    private Image[] imageChildMenus;
    [SerializeField]
    // 메뉴 텍스트
    private Text[] textChildMenus;

    private void Awake()
    {
        // 자식 객체에서 컴포넌트 획득
        imageChildMenus = GetComponentsInChildren<Image>();
        textChildMenus = GetComponentsInChildren<Text>();
    }

    private void OnEnable()
    {
        // 활성화시 하이라이트 설정
        HightLight();
    }

    private void Update()
    {
        // 비활성화 상태면 메서드 종료
        if (!gameObject.activeSelf) return;

        // 윗 키를 누르면
        if (Input.GetKeyDown(MenuManager.Instance.UpKey[0]) || Input.GetKeyDown(MenuManager.Instance.UpKey[1]))
        {
            // 0보다 크다면 선택 메뉴 인덱스 값 변경
            curSelectMenu = curSelectMenu > 0 ? curSelectMenu - 1 : curSelectMenu;
            // 하이라이트 변경
            HightLight();
        }
        // 아랫 키를 누르면
        else if (Input.GetKeyDown(MenuManager.Instance.DownKey[0]) || Input.GetKeyDown(MenuManager.Instance.DownKey[1]))
        {
            // 메뉴 크기보다 작으면 선택 메뉴 인덱스 값 변경
            curSelectMenu = curSelectMenu < imageChildMenus.Length - 1 ? curSelectMenu + 1 : curSelectMenu;
            // 하이라이트 변경
            HightLight();
        }

        // 엔터 키를 누르면
        if (Input.GetKeyDown(MenuManager.Instance.EnterKey))
        {
            // 현재 메뉴에 맞는 코드 실행
            MenuManager.Instance.SelectMenu(curSelectMenu);
        }
    }

    private void HightLight()
    {
        // 모든 메뉴 컬러 기본 상태로 변경
        for (int i = 0; i < textChildMenus.Length; i++)
        {
            imageChildMenus[i].GetComponent<Image>().color = idelColor;
            textChildMenus[i].GetComponentInChildren<Text>().color = Color.gray;
        }

        // 현재 선택 중인 메뉴만 하이라이트 컬러로 변경
        imageChildMenus[curSelectMenu].GetComponent<Image>().color = hightLightColor;
        textChildMenus[curSelectMenu].GetComponentInChildren<Text>().color = Color.white;
    }

}
