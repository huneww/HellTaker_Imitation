using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // 싱글톤 패턴
    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // 메뉴 조작키 설정
    [HideInInspector]
    public KeyCode[] UpKey = { KeyCode.W, KeyCode.UpArrow };
    [HideInInspector]
    public KeyCode[] DownKey = { KeyCode.S, KeyCode.DownArrow };
    [HideInInspector]
    public KeyCode[] RightKey = { KeyCode.D, KeyCode.RightArrow };
    [HideInInspector]
    public KeyCode[] LeftKey = { KeyCode.A, KeyCode.LeftArrow };
    [HideInInspector]
    public KeyCode EnterKey = KeyCode.Return;
    [HideInInspector]
    public KeyCode ESCKey = KeyCode.Escape;

    [Space(10), Header("Text")]
    [SerializeField]
    // 텍스트와 부퍼 부모 오브젝트
    private GameObject textBooperGroup;
    [SerializeField]
    // 텍스트 저장 변수
    private Text text;
    [SerializeField]
    // 이름 텍스트 저장 변수
    private Text nameText;
    [SerializeField, TextArea]
    // 출력할 텍스트 목록
    private string[] textList;

    [Space(10), Header("Menu")]
    [SerializeField]
    // 메인 메뉴 그룹 오브젝트
    private GameObject menuGroup;
    [SerializeField]
    // 챕터 선택 그룹 오브젝트
    private GameObject selectMenuGroup;
    [Space(10), Header("BackGround")]
    [SerializeField]
    private GameObject bakcGround;
    [Space(10), Header("CutScene")]
    [SerializeField]
    private GameObject cutSceneGroup;

    private void Awake()
    {
        // 싱글톤 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        // 첫 번쨰 대사 출력
        StartCoroutine(TextBooperActive(0));
    }

    private void Update()
    {
        // 텍스트 변경 메서드
        TextChange();
    }

    private void TextChange()
    {
        // 텍스트가 활성화 되어있을때만 실행
        if (text.gameObject.activeSelf)
        {
            // 첫 번째 대사에서 엔터키 입력시
            if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[0]))
            {
                // 부퍼 애니메이션 실행
                BooperController.booperActive();
                // 베엘제붑 위치 이동
                MoveBeel.move();
                // 베엘제붑 이름 텍스트 활성화
                nameText.gameObject.SetActive(true);
                // 텍스트만 다음 대사로 변경후 핑퐁 실행
                StartCoroutine(TextBooperActive(1, false));
            }
            // 두 번째 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[1]))
            {
                // 텍스트, 부퍼의 부모 오브젝트 비활성화
                textBooperGroup.SetActive(false);
                // 메뉴 오브젝트 활성화
                menuGroup.gameObject.SetActive(true);
                // 텍스트를 빈칸으로 변경
                text.text = "";
            }
            // 컷씬 첫 번쨰 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[2]))
            {
                // 다음 데사 출력
                StartCoroutine(TextBooperActive(3, false));
            }
            // 컷씬 두 번째 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[3]))
            {
                // 다음 데사 출력
                StartCoroutine(TextBooperActive(4, false));
                // 베엘제붑 비활성화
                MoveBeel.active(false);
            }
            // 컷씬 세 번째 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[4]))
            {
                // 다음 데사 출력
                StartCoroutine(TextBooperActive(5, false));
                // 베엘제붑 이름 텍스트 비활성화
                nameText.gameObject.SetActive(false);
                // 뒷 배경 비활성화
                bakcGround.SetActive(false);
                // 컷 씬 그룹 활성화
                cutSceneGroup.SetActive(true);
            }
            // 컷씬 네 번째 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[5]))
            {
                // 다음 데사 출력
                StartCoroutine(TextBooperActive(6, false));
                // 컷씬 이미지 변경
                CutSceneChange.ChangeImage();
            }
            // 컷씬 다섯 번째 대사에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[6]))
            {
                // 다음 데사 출력
                StartCoroutine(TextBooperActive(7, false));
                // 컷씬 이미지 변경
                CutSceneChange.ChangeImage();
            }
            // 마지막 컷씬 에서 엔터키 입력시
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[7]))
            {
                // 1챕터 시작
                StartStage(1);
                Debug.Log("Start 1Chapter");
            }
            // 종료 메세지와 같을때
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[textList.Length - 1]))
            {
                // 게임 종료
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// 텍스트, 부퍼 활성화
    /// </summary>
    /// <param name="textIndex">변경할 텍스트 인덱스</param>
    /// <param name="groupActive">그룹으로 텍스트와 부퍼를 둘다 활성화 실킬시</param>
    /// <returns></returns>
    private IEnumerator TextBooperActive(int textIndex, bool groupActive = true)
    {
        // 텍스트 리스트의 텍스트로 변경
        text.text = textList[textIndex];

        // 첫 번째 대화를 출력할려면 0.5초 후에 출력
        if (textIndex == 0)
            yield return new WaitForSeconds(0.5f);

        // groupActive가 참이면
        if (groupActive)
            // 텍스트와 부퍼를 활성화
            // 텍스트가 비활성화면 부퍼도 비활성화 상태이기 때문
            textBooperGroup.SetActive(true);
        else
        {
            // 텍스트 핑퐁 스크립트 작동
            // OnEnable() 이벤트 메서드에 발동하도록 설정해놓음
            text.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
        // 다이얼로그 출력 사운드 재생
        MenuAudioManager.DialogComfirm();
    }

    /// <summary>
    /// 선택 메뉴에 따라 코드 실행
    /// </summary>
    /// <param name="menuIndex">선택한 메뉴 인덱스</param>
    public void SelectMenu(int menuIndex)
    {
        switch (menuIndex)
        {
            // 게임 시작
            case 0:
                // 첫 스테이지 실행
                StartStage(0);
                break;
            // 챕터 선택 메뉴
            case 1:
                // 메뉴 비활성화
                menuGroup.SetActive(false);
                // 챕터 선택 메뉴 활성화
                selectMenuGroup.SetActive(true);
                break;
            // 게임 종료
            case 2:
                // 게임 종료 텍스트로 변경
                StartCoroutine(TextBooperActive(textList.Length - 1));
                // 메뉴 오브젝트 비활성화
                menuGroup.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// 챕터 선택에서 메뉴로 전환
    /// </summary>
    public void ReturnToMenu()
    {
        // 챕터 선택 메뉴 비활성화
        selectMenuGroup.SetActive(false);
        // 메뉴 활성화
        menuGroup.SetActive(true);
    }

    /// <summary>
    /// 챕터 실행
    /// </summary>
    /// <param name="stage">선택한 챕터 씬 로드</param>
    public void StartStage(int stage)
    {
        Debug.Log("Start : " + stage + "stage");
        switch (stage)
        {
            case 0:
                // 챕터 선택에서 아닌 게임 시작으로 1스테이지 시작시
                // 컷 씬을 불러온다
                CutScene();
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
        }
    }

    private void CutScene()
    {
        // 메뉴, 챕터 선택 비활성화
        menuGroup.SetActive(false);
        selectMenuGroup.SetActive(false);
        // 텍스트 변경
        StartCoroutine(TextBooperActive(2));
    }

}
