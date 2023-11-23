using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JGBossDialogManager : MonoBehaviour
{
    private static JGBossDialogManager instance;
    public static JGBossDialogManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        // 싱글톤 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    [Header("UI Group")]
    // 플레이에 필요한 UI
    [SerializeField]
    private GameObject playUI;
    // 다이얼로그에 필요한 UI
    [SerializeField]
    private GameObject dialogUI;

    [Space(10), Header("Move Back")]
    // 위로 올라갈 이미지
    [SerializeField]
    private RectTransform move_Up;
    // 아래로 내려갈 이미지
    [SerializeField]
    private RectTransform move_Down;
    // 뒷 배경 이동 시간
    [SerializeField]
    private float backMoveTime;

    [Space(10), Header("Demon Image")]
    // 악마 이미지 트랜스폼
    [SerializeField]
    private RectTransform demonTransform;
    // 악마 이미지
    [SerializeField]
    private Image demonImage;
    // 이미지 이동 시간
    [SerializeField]
    private float imageMoveTime;
    // 이미지 알파값 조절 커브
    [SerializeField]
    private AnimationCurve colorAlpha;
    // 애니메이션 이미지
    [SerializeField]
    private GameObject demonImageAnimation;
    // 애니메이션 팔
    [SerializeField]
    private GameObject demonArm;
    // 바인딩 체인
    [SerializeField]
    private GameObject[] bindingChains;

    [Space(10), Header("Name, Dialog, Booper")]
    // 다이얼로그 이름 텍스트
    [SerializeField]
    private Text demonName;
    // 다이얼로그 대사 텍스트
    [SerializeField]
    private Text dialog;
    // 다이얼로그 부퍼 오브젝트
    [SerializeField]
    private GameObject booper;

    [Space(10), Header("Dialog List")]
    // 다이얼로그 리스트
    public Dialog[] dialogList;
    // 현재 리스트 인덱스
    [SerializeField]
    private int dialogListIndex = 0;
    // 리스트 안에있는 텍스트 인덱스
    [SerializeField]
    private int dialogListTextIndex = 0;

    [Space(10), Header("Select Menu")]
    // 선택 메뉴
    [SerializeField]
    private GameObject selectMenu;
    // 메뉴 이미지
    [SerializeField]
    private Image[] menuImages;
    // 기본 상태 색상
    [SerializeField]
    private Color idle;
    // 선택 색상
    [SerializeField]
    private Color hightLight;

    [Space(10), Header("Success")]
    [SerializeField]
    private GameObject success;

    private string sceneName;

    /// <summary>
    /// 다이얼로그 시작
    /// </summary>
    public void StartDialog()
    {
        // 플레이 UI 비활성화
        playUI.SetActive(false);
        // 다이얼로그 UI 활성화
        dialogUI.SetActive(true);
        // 다이얼로그 시작 코루틴 실행
        StartCoroutine(DialogStartCoroutine());
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        SelecetMenu();
        DialogController();
    }

    private void DialogController()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (sceneName.Contains("BossStage_3"))
            {
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.NextStage();
                    }
                    else
                    {
                        if (demonImageAnimation != null && demonImageAnimation.activeSelf)
                        {
                            Destroy(demonImageAnimation);
                        }

                        if (dialogListIndex == 4 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                        }
                        else if (dialogListIndex == 5 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "알아. 그래서 지금 이렇게 솔직한거지.";
                            menuImages[1].GetComponentInChildren<Text>().text = "그 손톱도 꽤 마음에 드는데!";
                        }
                        else
                        {
                            if (selectMenu.activeSelf)
                            {
                                booper.SetActive(true);
                                selectMenu.SetActive(false);
                            }

                            // 리스트의 다음 대사, 이미지로 변경
                            NextDialog();
                        }
                        // 텍스트 출력 완료 사운드
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // 다음 텍스트로 변경
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // 텍스트 출력 완료 사운드
                    JGBossAudioManager.Instance.TextEnd();
                }

                if (bindingChains[0] != null)
                {
                    if (bindingChains[0].activeSelf)
                    {
                        Destroy(demonArm);
                        demonImageAnimation.GetComponent<Animator>().SetTrigger("Binding");
                        Animator[] clones = bindingChains[0].GetComponentsInChildren<Animator>();
                        JGBossAudioManager.Instance.JGBinding();
                        if (clones[0].gameObject.name.Contains("Chain_Back_1"))
                        {
                            clones[0].SetTrigger("Binding_1");
                            clones[1].SetTrigger("Binding_2");
                        }
                        else if (clones[0].gameObject.name.Contains("Chain_Back_2"))
                        {
                            clones[0].SetTrigger("Binding_2");
                            clones[1].SetTrigger("Binding_1");
                        }
                        clones = bindingChains[1].GetComponentsInChildren<Animator>();
                        if (clones[0].gameObject.name.Contains("Chain_Front_1"))
                        {
                            clones[0].SetTrigger("Binding_1");
                            clones[1].SetTrigger("Binding_2");
                        }
                        else if (clones[0].gameObject.name.Contains("Chain_Front_2"))
                        {
                            clones[0].SetTrigger("Binding_2");
                            clones[1].SetTrigger("Binding_1");
                        }

                        Destroy(bindingChains[0], 0.5f);
                        Destroy(bindingChains[1], 0.5f);
                    }

                    if (!bindingChains[0].activeSelf)
                    {
                        bindingChains[0].SetActive(true);
                        bindingChains[1].SetActive(true);
                        JGBossAudioManager.Instance.SummonBindChain();
                    }
                }
            }
            else if (sceneName.Contains("BossStage_4"))
            {
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.SpawnLovePlosion();
                        JGBossAudioManager.Instance.LovePlosion();
                        dialogUI.SetActive(false);
                    }
                    else
                    {
                        if (dialogListIndex == 0 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "너를 일찍 보지 못한 게 가장 후회되는 군.";
                            menuImages[1].GetComponentInChildren<Text>().text = "너에게 벌을 받을 수 있어서 죄를 지을 가치가 있다고 생각해.";
                        }
                        else if (dialogListIndex == 1 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "게다가, 이렇게 예쁜 눈이 또 있을까.";
                            menuImages[1].GetComponentInChildren<Text>().text = "내가 아까 머리 스타일 멋지다고 애기 했었나?";
                        }
                        else if (dialogListIndex == 5 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "그것 말고 다른 건 원하지 않아.";
                            menuImages[1].GetComponentInChildren<Text>().text = "내 말 들어봐. 이미 내 하렘에 더 끔찍한 게 있다니까.";
                        }
                        else
                        {
                            booper.SetActive(true);
                            if (selectMenu.activeSelf)
                            {
                                selectMenu.SetActive(false);
                            }

                            if (success.activeSelf)
                            {
                                success.SetActive(false);
                            }

                            // 리스트의 다음 대사, 이미지로 변경
                            NextDialog();

                            if (dialogListIndex == 6)
                            {
                                booper.SetActive(false);
                                success.SetActive(true);
                                JGBossAudioManager.Instance.DialogSuccess();
                            }
                        }
                        // 텍스트 출력 완료 사운드
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // 다음 텍스트로 변경
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // 텍스트 출력 완료 사운드
                    JGBossAudioManager.Instance.TextEnd();
                }
            }
            else
            {
                // 텍스트 인덱스가 대사의 길이보다 크다면
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    // 다이얼로그 리스트가 더이상 없다면 다음 스테이지 실행
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.NextStage();
                    }
                    else
                    {
                        // 리스트의 다음 대사, 이미지로 변경
                        NextDialog();
                        // 텍스트 출력 완료 사운드
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // 다음 텍스트로 변경
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // 텍스트 출력 완료 사운드
                    JGBossAudioManager.Instance.TextEnd();
                }
            }
        }
    }

    private void SelecetMenu()
    {
        if (!selectMenu.activeSelf || selectMenu == null) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuImages[0].color = hightLight;
            menuImages[1].color = idle;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuImages[0].color = idle;
            menuImages[1].color = hightLight;
        }
    }

    /// <summary>
    /// 다이얼로그 리스트 다음으로 변경
    /// </summary>
    private void NextDialog()
    {
        // 텍스트 인덱스 0으로 초기화
        dialogListTextIndex = 0;
        // 리스트 인덱스 증가
        dialogListIndex++;
        // 악마 이미지 변경
        demonImage.sprite = dialogList[dialogListIndex].changesprite;
        // 악마 이미지 사이즈 자동 맞춤
        demonImage.SetNativeSize();
        // 이름 변경
        demonName.text = dialogList[dialogListIndex].name;
        // 대사 변경
        dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
        // 대사 텍스트 핑퐁
        dialog.gameObject.SetActive(false);
        dialog.gameObject.SetActive(true);
        // 이미지 다시 이동
        StartCoroutine(DemonImageMove(new Vector3(1500, 202), new Vector3(0, 202), imageMoveTime));
    }

    private IEnumerator DialogStartCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        // 뒷 배경 기존 위치, 목표 위치 저장
        Vector3 up_Target = new Vector3(0, 810);
        Vector3 up_Cur = move_Up.localPosition;
        Vector3 down_Target = new Vector3(0, -810);
        Vector3 down_Cur = move_Down.localPosition;

        // 뒷 배경 목표 위치로 이동
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / backMoveTime;
            // up : 810, down : -810
            move_Up.localPosition = Vector3.Lerp(up_Cur, up_Target, percent);
            move_Down.localPosition = Vector3.Lerp(down_Cur, down_Target, percent);
        }

        move_Up.localPosition = up_Target;
        move_Down.localPosition = down_Target;

        // 이미지 기존 위치, 목표 위치 저장
        Vector3 image_Target = new Vector3(0, 202);
        Vector3 image_Cur = demonTransform.localPosition;

        if (!SceneManager.GetActiveScene().name.Contains("BossStage_3"))
        {
            demonName.text = dialogList[0].name;
            dialog.text = dialogList[0].texts[dialogListTextIndex];
            demonImage.sprite = dialogList[0].changesprite;
            demonImage.SetNativeSize();
        }

        // 이미지 이동
        yield return StartCoroutine(DemonImageMove(image_Cur, image_Target, imageMoveTime));

        if (SceneManager.GetActiveScene().name.Contains("BossStage_3"))
        {
            Color color = demonImage.color;
            color.a = 0;
            demonImage.color = color;
        }

        // 다이얼로그 출력 사운드 재생
        JGBossAudioManager.Instance.StartDialog();
        // 악마 이름, 다이얼로그 텍스트, 부퍼 활성화
        demonName.gameObject.SetActive(true);
        dialog.gameObject.SetActive(true);
        booper.SetActive(true);
    }

    private IEnumerator StageThereDialogStartcoroutine()
    {
        yield return null;
    }

    private IEnumerator DemonImageMove(Vector3 curPos, Vector3 targetPos, float moveTime)
    {
        float curTime = 0;
        float percent = 0;
        Color color = demonImage.color;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            demonTransform.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(0, 1, colorAlpha.Evaluate(percent));
            demonImage.color = color;
            if (demonImageAnimation != null)
            {
                demonImageAnimation.GetComponent<RectTransform>().localPosition = Vector3.Lerp(curPos, targetPos, percent);
                demonImageAnimation.GetComponent<Image>().color = color;
            }
        }

        demonTransform.localPosition = targetPos;
        color.a = 1;
        demonImage.color = color;
        if (demonImageAnimation != null)
        {
            demonImageAnimation.GetComponent<RectTransform>().localPosition = targetPos;
            demonImageAnimation.GetComponent<Image>().color = color;
            demonArm.SetActive(true);
        }
    }

}
