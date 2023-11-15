using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroTextManager : MonoBehaviour
{
    // 페이드 아웃되는 배경
    [SerializeField]
    private GameObject fadeBackGround;
    // 대사 텍스트
    [SerializeField]
    private Text text;
    // 이름 텍스트
    [SerializeField]
    private Text nameText;
    // 부퍼
    [SerializeField]
    private GameObject booper;
    // 인트로 대사
    [SerializeField, TextArea]
    private string[] introText;
    // 악마 이름
    [SerializeField, TextArea]
    private string demonName;

    [Space(10), Header("Image")]
    // 악마 이미지
    [SerializeField]
    private GameObject demonImage;

    private void Start()
    {
        // 대사 텍스트 변경
        text.text = introText[0];
        // 대사 출력 완료 사운드 재생
        JGIntroAudioManager.Instance.JGIntroDialogComfirm();
    }

    private void Update()
    {
        TextChange();
    }

    private void TextChange()
    {
        // 엔터 입력시
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 첫번째 대사에서
            if (text.text.Contains(introText[0]))
            {
                // 대사 변경하면서 텍스트 핑퐁
                TextPingPong(introText[1]);
                // 이름 텍스트 활성화
                nameText.gameObject.SetActive(true);
                // 배경 페이드 아웃
                StartCoroutine(fadeBackGround.GetComponent<FadeOutBackGround>().FadeOut());
                // 대사 출력 완료 사운드 재생
                JGIntroAudioManager.Instance.JGIntroDialogComfirm();
            }
            // 두번쟤 대사에서
            else if (text.text.Contains(introText[1]))
            {
                // 세번쨰 대사로 변경하면서 텍스트 핑퐁
                TextPingPong(introText[2]);
                // 대사 출력 완료 사운드 재생
                JGIntroAudioManager.Instance.JGIntroDialogComfirm();
            }
            // 세번쨰 대사에서
            else if (text.text.Contains(introText[2]))
            {
                // 대사, 이름 텍스트, 부퍼 비활성화
                text.gameObject.SetActive(false);
                nameText.gameObject.SetActive(false);
                booper.SetActive(false);
                // 인트로 애니메이션 실행
                StartCoroutine(StartIntro());
                // 인트로 사운드 재생
                JGIntroAudioManager.Instance.JGIntro();
            }
            // 네번째 대사에서
            else if (text.text.Contains(introText[3]))
            {
                // 보스 챕터로 씬 로드
                SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_1");
            }
        }
    }

    private IEnumerator StartIntro()
    {
        // 배경 애니메이션이 전부 실행되면
        yield return StartCoroutine(JGIntroBackGroundManager.Instance.IntroCoroutine());
        // 대사 텍스트 변경
        text.text = introText[3];
        // 악마 이름 변경
        nameText.text = demonName;
        // 다이얼로그 오브젝트 활성화
        DialogEnable(true);
        // 악마 이미지 활성화
        demonImage.SetActive(true);
    }

    /// <summary>
    /// 이름, 대사, 부퍼 활성화, 비활성화
    /// </summary>
    /// <param name="active"></param>
    private void DialogEnable(bool active)
    {
        text.gameObject.SetActive(active);
        nameText.gameObject.SetActive(active);
        booper.SetActive(active);
    }

    /// <summary>
    /// 텍스트 변경후 텍스트 핑퐁
    /// </summary>
    /// <param name="changeText">변경할 텍스트</param>
    private void TextPingPong(string changeText)
    {
        text.gameObject.SetActive(false);
        text.text = changeText;
        text.gameObject.SetActive(true);
    }

}
