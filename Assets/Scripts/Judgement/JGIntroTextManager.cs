using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroTextManager : MonoBehaviour
{
    // ���̵� �ƿ��Ǵ� ���
    [SerializeField]
    private GameObject fadeBackGround;
    // ��� �ؽ�Ʈ
    [SerializeField]
    private Text text;
    // �̸� �ؽ�Ʈ
    [SerializeField]
    private Text nameText;
    // ����
    [SerializeField]
    private GameObject booper;
    // ��Ʈ�� ���
    [SerializeField, TextArea]
    private string[] introText;
    // �Ǹ� �̸�
    [SerializeField, TextArea]
    private string demonName;

    [Space(10), Header("Image")]
    // �Ǹ� �̹���
    [SerializeField]
    private GameObject demonImage;

    private void Start()
    {
        // ��� �ؽ�Ʈ ����
        text.text = introText[0];
        // ��� ��� �Ϸ� ���� ���
        JGIntroAudioManager.Instance.JGIntroDialogComfirm();
    }

    private void Update()
    {
        TextChange();
    }

    private void TextChange()
    {
        // ���� �Է½�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ù��° ��翡��
            if (text.text.Contains(introText[0]))
            {
                // ��� �����ϸ鼭 �ؽ�Ʈ ����
                TextPingPong(introText[1]);
                // �̸� �ؽ�Ʈ Ȱ��ȭ
                nameText.gameObject.SetActive(true);
                // ��� ���̵� �ƿ�
                StartCoroutine(fadeBackGround.GetComponent<FadeOutBackGround>().FadeOut());
                // ��� ��� �Ϸ� ���� ���
                JGIntroAudioManager.Instance.JGIntroDialogComfirm();
            }
            // �ι��� ��翡��
            else if (text.text.Contains(introText[1]))
            {
                // ������ ���� �����ϸ鼭 �ؽ�Ʈ ����
                TextPingPong(introText[2]);
                // ��� ��� �Ϸ� ���� ���
                JGIntroAudioManager.Instance.JGIntroDialogComfirm();
            }
            // ������ ��翡��
            else if (text.text.Contains(introText[2]))
            {
                // ���, �̸� �ؽ�Ʈ, ���� ��Ȱ��ȭ
                text.gameObject.SetActive(false);
                nameText.gameObject.SetActive(false);
                booper.SetActive(false);
                // ��Ʈ�� �ִϸ��̼� ����
                StartCoroutine(StartIntro());
                // ��Ʈ�� ���� ���
                JGIntroAudioManager.Instance.JGIntro();
            }
            // �׹�° ��翡��
            else if (text.text.Contains(introText[3]))
            {
                // ���� é�ͷ� �� �ε�
                SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_1");
            }
        }
    }

    private IEnumerator StartIntro()
    {
        // ��� �ִϸ��̼��� ���� ����Ǹ�
        yield return StartCoroutine(JGIntroBackGroundManager.Instance.IntroCoroutine());
        // ��� �ؽ�Ʈ ����
        text.text = introText[3];
        // �Ǹ� �̸� ����
        nameText.text = demonName;
        // ���̾�α� ������Ʈ Ȱ��ȭ
        DialogEnable(true);
        // �Ǹ� �̹��� Ȱ��ȭ
        demonImage.SetActive(true);
    }

    /// <summary>
    /// �̸�, ���, ���� Ȱ��ȭ, ��Ȱ��ȭ
    /// </summary>
    /// <param name="active"></param>
    private void DialogEnable(bool active)
    {
        text.gameObject.SetActive(active);
        nameText.gameObject.SetActive(active);
        booper.SetActive(active);
    }

    /// <summary>
    /// �ؽ�Ʈ ������ �ؽ�Ʈ ����
    /// </summary>
    /// <param name="changeText">������ �ؽ�Ʈ</param>
    private void TextPingPong(string changeText)
    {
        text.gameObject.SetActive(false);
        text.text = changeText;
        text.gameObject.SetActive(true);
    }

}
