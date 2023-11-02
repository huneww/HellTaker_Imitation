using UnityEngine;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{
    [Space(30)]
    [SerializeField]
    // 현재 선택 중인 메뉴
    private int curSelectionMenu = 0;
    [SerializeField]
    // 메뉴 UI
    private Image[] menuObjects;
    [SerializeField]
    // 메뉴 기본 상태 컬러
    private Color idelColor;
    [SerializeField]
    // 메뉴 선택 상태 컬러
    private Color hightLightColor;
    [SerializeField]
    private GameObject SlectionChapterParent;
    [SerializeField]
    // 쳅터 선택 메뉴 인덱스
    private int curSlectionChapter = 0;

    private void Start()
    {
        // 메뉴 UI 부모 오브젝트에서 메뉴 UI이 Image컴포넌트 획득
        menuObjects = MainMenuManager.Instance.mainMenuGroup.GetComponentsInChildren<Image>();
        // 메뉴 UI의 첫번째를 선택 컬러로 변경
        menuObjects[0].color = hightLightColor;
    }

    private void Update()
    {
        // 메뉴 UI 부모 오브젝트가 비활성화 상태면 메서드 종료
        if (MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha > 0)
        {
            // 위로 올라가는 키를 누르면
            if (Input.GetKeyDown(MainMenuManager.Instance.UpKey_1) || Input.GetKeyDown(MainMenuManager.Instance.UpKey_2))
            {
                // 현재 선택중인 메뉴 변수에 -1
                // 0보다 작으면 현재 값을 저장
                curSelectionMenu = curSelectionMenu > 0 ? curSelectionMenu - 1 : curSelectionMenu;
                // 메뉴 이동 사운드 재생
                SoundManager.menuMove();
                // 메뉴 하이라이트 변경
                MenuHightLight();
            }
            // 아래로 내려가는 키를 누르면
            else if (Input.GetKeyDown(MainMenuManager.Instance.DownKey_1) || Input.GetKeyDown(MainMenuManager.Instance.DownKey_2))
            {
                // 현재 선택중인 메뉴 변수에 +1
                // 메뉴의 갯수 보다 많으면 현재 값을 저장
                curSelectionMenu = curSelectionMenu < menuObjects.Length - 1 ? curSelectionMenu + 1 : curSelectionMenu;
                // 메뉴 이동 사운드 재생
                SoundManager.menuMove();
                // 메뉴 하이라이트 변경
                MenuHightLight();
            }
            // 메뉴 선택
            if (Input.GetKeyDown(MainMenuManager.Instance.EnterKey))
            {
                // 메뉴 선택 사운드 재생
                SoundManager.menuSelection();
                // 메뉴에 맞는 코드 실행
                EnterMenu(curSelectionMenu);
            }
        }
        // 챕터 선택 메뉴가 활성화 상태면
        else if (MainMenuManager.Instance.chapterParent.GetComponent<CanvasGroup>().alpha > 0)
        {
            if (Input.GetKeyDown(MainMenuManager.Instance.RightKey_1) || Input.GetKeyDown(MainMenuManager.Instance.RightKey_2))
            {
                curSlectionChapter = curSlectionChapter < 10 ? curSlectionChapter + 1 : 0;
                SoundManager.menuMove();
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
            }
            else if (Input.GetKeyDown(MainMenuManager.Instance.LeftKey_1) || Input.GetKeyDown(MainMenuManager.Instance.LeftKey_2))
            {
                curSlectionChapter = curSlectionChapter > 0 ? curSlectionChapter - 1 : 10;
                SoundManager.menuMove();
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
            }

            if (Input.GetKeyDown(MainMenuManager.Instance.EnterKey))
            {
                SoundManager.menuSelection();
                MainMenuManager.Instance.STARTSTAGE(curSlectionChapter);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenuManager.Instance.chapterParent.GetComponent<CanvasGroup>().alpha = 0;
                MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }

    private void MenuHightLight()
    {
        // 강조할 오브젝트의 색상이 강조 색상과 동일하면 메서드 종료
        if (menuObjects[curSelectionMenu].color == hightLightColor) return;

        // 메뉴 오브젝트를 전부 기본 색상으로 변경
        foreach (var menu in menuObjects)
            menu.color = idelColor;
        // 강조할 메뉴 색상 변경
        menuObjects[curSelectionMenu].color = hightLightColor;
    }

    private void EnterMenu(int stage)
    {
        switch (stage)
        {
            case 0:
                // 첫 번째 스테이지 실행
                MainMenuManager.Instance.STARTSTAGE(1);
                break;
            case 1:
                MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha = 0;
                // 선택한 스테이지 실행
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
                break;
            case 2:
                // 게임 종료
                MainMenuManager.Instance.EXIT();
                break;
        }
    }

}
