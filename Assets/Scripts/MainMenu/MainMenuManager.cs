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

    // Ű ����
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

    [Space(30), Header("�ؽ�Ʈ")]
    [SerializeField, TextArea]
    private string[] textList;
    [SerializeField]
    private Text text;

    [Space(30), Header("����")]
    [SerializeField]
    private GameObject booper;
    private Animator booperAnimator;
    private int clickHash = Animator.StringToHash("Click");

    public delegate void BeelMoveToCenter();
    [SerializeField]
    public static BeelMoveToCenter moveToCenter;
    [Space(30), Header("��������")]
    [SerializeField]
    private GameObject beelNameText;

    [Space(30), Header("�޴�")]
    public GameObject mainMenuGroup;

    [Space(30), Header("���� ����")]
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
        // �ν��Ͻ� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        // ���� �ִϸ����� ȹ��
        booperAnimator = booper.GetComponent<Animator>();
        // ù ��� Ȱ��ȭ
        StartCoroutine(TextPingPong(0));
    }

    private void Update()
    {
        // ù ��翡�� ����Ű�� ������
        if (text.text.Equals(textList[0]) && Input.GetKeyDown(enterKey))
        {
            // ���� ��縦 ���
            StartCoroutine(TextPingPong(1));
            // ���� �ִϸ��̼� ����
            booperAnimator.SetTrigger(clickHash);
            // �������� �̸� �ؽ�Ʈ Ȱ��ȭ
            beelNameText.SetActive(true);
            // �������� ��������Ʈ �̵�
            moveToCenter();
        }
        // �� ��° ��翡�� ����Ű�� ������
        else if (text.text.Equals(textList[1]) && Input.GetKeyDown(enterKey))
        {
            // ��� �ؽ�Ʈ ��Ȱ��ȭ
            text.gameObject.SetActive(false);
            // ���� ��Ȱ��ȭ
            booper.SetActive(false);
            // ���� �޴� UI Ȱ��ȭ
            mainMenuGroup.gameObject.SetActive(true);
        }
    }

    private void SetText(int index)
    {
        text.text = textList[index];
    }

    private IEnumerator TextPingPong(int index)
    {
        // ������ �ʱ⿡ �����ϸ�
        if (index == 0)
        {
            // 0.5�� ��ٸ� �� ����
            yield return new WaitForSeconds(0.5f);
            // ���� �ִϸ��̼� ���
            booper.SetActive(true);
        }
        // ��� ��� �Ϸ� ���� ���
        SoundManager.dialogComFirm();
        // �ؽ�Ʈ ������Ʈ�� ��Ȱ��ȭ
        text.gameObject.SetActive(false);
        // �ؽ�Ʈ ���� ����
        SetText(index);
        // �ؽ�Ʈ ������Ʈ�� Ȱ��ȭ
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
                chapterInfoText.text = "���� �ǰ��� �Ǹ�";
                break;
            case 1:
                chapterInfoText.text = "���� ������ �Ǹ�";
                break;
            case 2:
                chapterInfoText.text = "���� ���ֵ��� �Ǹ�";
                break;
            case 3:
                chapterInfoText.text = "���� ��ū���� �Ǹ�";
                break;
            case 4:
                chapterInfoText.text = "���� �󽺷��� �Ǹ�";
                break;
            case 5:
                chapterInfoText.text = "���� ȣ��� ���� õ��";
                break;
            case 6:
                chapterInfoText.text = "���� �����ִ� �Ǹ�";
                break;
            case 7:
                chapterInfoText.text = "���� ������ CEO";
                break;
            case 8:
                chapterInfoText.text = "���� ���� ��Ұ�";
                break;
            case 9:
                chapterInfoText.text = "���ʷα�";
                break;
            case 10:
                break;

        }
    }

    /// <summary>
    /// �������� �� �ε�
    /// </summary>
    /// <param name="stage">�ε��� ��</param>
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
    /// ���� ���� �޼���
    /// </summary>
    public void EXIT()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

}
