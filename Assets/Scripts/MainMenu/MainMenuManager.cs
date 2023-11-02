using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance;
    public static MainMenuManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    // 키 설정
    [SerializeField]
    private KeyCode enterKey = KeyCode.Return;
    public KeyCode EnterKey
    {
        get
        {
            return enterKey;
        }
    }
    [SerializeField]
    private KeyCode[] upKey = { KeyCode.W, KeyCode.UpArrow };
    public KeyCode UpKey_1
    {
        get
        {
            return upKey[0];
        }
    }
    public KeyCode UpKey_2
    {
        get
        {
            return upKey[1];
        }
    }
    [SerializeField]
    private KeyCode[] downKey = { KeyCode.S, KeyCode.DownArrow };
    public KeyCode DownKey_1
    {
        get
        {
            return downKey[0];
        }
    }
    public KeyCode DownKey_2
    {
        get
        {
            return downKey[1];
        }
    }
    [SerializeField]
    private KeyCode[] rightKey = { KeyCode.D, KeyCode.RightArrow };
    public KeyCode RightKey_1
    {
        get
        {
            return rightKey[0];
        }
    }
    public KeyCode RightKey_2
    {
        get
        {
            return rightKey[1];
        }
    }
    [SerializeField]
    private KeyCode[] leftKey = { KeyCode.A, KeyCode.LeftArrow };
    public KeyCode LeftKey_1
    {
        get
        {
            return leftKey[0];
        }
    }
    public KeyCode LeftKey_2
    {
        get
        {
            return leftKey[1];
        }
    }

    [Space(30), Header("텍스트")]
    [SerializeField, TextArea]
    private string[] textList;
    [SerializeField]
    private Text text;

    [Space(30), Header("부퍼")]
    [SerializeField]
    private GameObject booper;
    private Animator booperAnimator;
    private int clickHash = Animator.StringToHash("Click");

    public delegate void BeelMoveToCenter();
    [SerializeField]
    public static BeelMoveToCenter moveToCenter;
    [Space(30), Header("베엘제붑")]
    [SerializeField]
    private GameObject beelNameText;

    [Space(30), Header("메뉴")]
    public GameObject mainMenuGroup;

    [Space(30), Header("쳅터 선택")]
    [SerializeField]
    public GameObject chapterParent;
    [SerializeField]
    private GameObject[] chapters;
    [SerializeField]
    private Color chapterIdelColor;
    [SerializeField]
    private Color chapterhighLightColor;
    [SerializeField]
    private Color EXChapterIdelColor;
    [SerializeField]
    private Color EXChapterHightLightColor;
    [SerializeField]
    private Text chapterInfoText;

    private void Awake()
    {
        // 인스턴스 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        // 부퍼 애니메이터 획득
        booperAnimator = booper.GetComponent<Animator>();
        // 첫 대사 활성화
        StartCoroutine(TextPingPong(0));
    }

    private void Update()
    {
        // 첫 대사에서 엔터키를 누르면
        if (text.text.Equals(textList[0]) && Input.GetKeyDown(enterKey))
        {
            // 다음 대사를 출력
            StartCoroutine(TextPingPong(1));
            // 부퍼 애니메이션 실행
            booperAnimator.SetTrigger(clickHash);
            // 베엘제붑 이름 텍스트 활성화
            beelNameText.SetActive(true);
            // 베엘제붑 스프라이트 이동
            moveToCenter();
        }
        // 두 번째 대사에서 엔터키를 누르면
        else if (text.text.Equals(textList[1]) && Input.GetKeyDown(enterKey))
        {
            // 대사 텍스트 비활성화
            text.gameObject.SetActive(false);
            // 부퍼 비활성화
            booper.SetActive(false);
            // 메인 메뉴 UI 활성화
            mainMenuGroup.gameObject.SetActive(true);
        }
    }

    private void SetText(int index)
    {
        text.text = textList[index];
    }

    private IEnumerator TextPingPong(int index)
    {
        // 게임을 초기에 실행하면
        if (index == 0)
        {
            // 0.5초 기다린 후 실행
            yield return new WaitForSeconds(0.5f);
            // 부퍼 애니메이션 재생
            booper.SetActive(true);
        }
        // 대사 출력 완료 사운드 재생
        SoundManager.dialogComFirm();
        // 텍스트 오브젝트를 비활성화
        text.gameObject.SetActive(false);
        // 텍스트 문자 변경
        SetText(index);
        // 텍스트 오브젝트를 활성화
        text.gameObject.SetActive(true);
        yield return null;
    }

    public GameObject GetChapter(int index)
    {
        return chapters[index];
    }

    public void SelectChapter(int index)
    {
        chapterParent.GetComponent<CanvasGroup>().alpha = 1;
        for (int i = 0; i < chapters.Length; i++)
        {
            if (i == 10)
            {
                chapters[i].GetComponent<Image>().color = EXChapterIdelColor;
                chapters[i].GetComponentInChildren<Text>().color = EXChapterIdelColor;
            }
            else
            {
                chapters[i].GetComponent<Image>().color = chapterIdelColor;
                chapters[i].GetComponentInChildren<Text>().color = chapterIdelColor;
            }
        }

        if (index == 10)
        {
            chapters[index].GetComponent<Image>().color = EXChapterHightLightColor;
            chapters[index].GetComponentInChildren<Text>().color = EXChapterHightLightColor;
        }
        else
        {
            chapters[index].GetComponent<Image>().color = chapterhighLightColor;
            chapters[index].GetComponentInChildren<Text>().color = chapterhighLightColor;
        }
        
        switch (index)
        {
            case 0:
                chapterInfoText.text = "Ⅰ장 피곤한 악마";
                break;
            case 1:
                chapterInfoText.text = "Ⅱ장 음란한 악마";
                break;
            case 2:
                chapterInfoText.text = "Ⅲ장 세쌍둥이 악마";
                break;
            case 3:
                chapterInfoText.text = "Ⅳ장 시큰둥한 악마";
                break;
            case 4:
                chapterInfoText.text = "Ⅴ장 상스러운 악마";
                break;
            case 5:
                chapterInfoText.text = "Ⅵ장 호기심 많은 천사";
                break;
            case 6:
                chapterInfoText.text = "Ⅶ장 끝내주는 악마";
                break;
            case 7:
                chapterInfoText.text = "Ⅷ장 지옥의 CEO";
                break;
            case 8:
                chapterInfoText.text = "Ⅸ장 고위 기소관";
                break;
            case 9:
                chapterInfoText.text = "에필로그";
                break;
            case 10:
                break;

        }
    }

    /// <summary>
    /// 스테이즈 씬 로드
    /// </summary>
    /// <param name="stage">로드할 씬</param>
    public void STARTSTAGE(int stage)
    {
        switch (stage)
        {
            case 1:

                break;
            case 2:

                break;
        }
    }

    /// <summary>
    /// 게임 종료 메서드
    /// </summary>
    public void EXIT()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

}
