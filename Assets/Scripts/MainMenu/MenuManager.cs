using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // �̱��� ����
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

    // �޴� ����Ű ����
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
    // �ؽ�Ʈ�� ���� �θ� ������Ʈ
    private GameObject textBooperGroup;
    [SerializeField]
    // �ؽ�Ʈ ���� ����
    private Text text;
    [SerializeField]
    // �̸� �ؽ�Ʈ ���� ����
    private Text nameText;
    [SerializeField, TextArea]
    // ����� �ؽ�Ʈ ���
    private string[] textList;

    [Space(10), Header("Menu")]
    [SerializeField]
    // ���� �޴� �׷� ������Ʈ
    private GameObject menuGroup;
    [SerializeField]
    // é�� ���� �׷� ������Ʈ
    private GameObject selectMenuGroup;
    [Space(10), Header("BackGround")]
    [SerializeField]
    private GameObject bakcGround;
    [Space(10), Header("CutScene")]
    [SerializeField]
    private GameObject cutSceneGroup;

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        // ù ���� ��� ���
        StartCoroutine(TextBooperActive(0));
    }

    private void Update()
    {
        // �ؽ�Ʈ ���� �޼���
        TextChange();
    }

    private void TextChange()
    {
        // �ؽ�Ʈ�� Ȱ��ȭ �Ǿ��������� ����
        if (text.gameObject.activeSelf)
        {
            // ù ��° ��翡�� ����Ű �Է½�
            if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[0]))
            {
                // ���� �ִϸ��̼� ����
                BooperController.booperActive();
                // �������� ��ġ �̵�
                MoveBeel.move();
                // �������� �̸� �ؽ�Ʈ Ȱ��ȭ
                nameText.gameObject.SetActive(true);
                // �ؽ�Ʈ�� ���� ���� ������ ���� ����
                StartCoroutine(TextBooperActive(1, false));
            }
            // �� ��° ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[1]))
            {
                // �ؽ�Ʈ, ������ �θ� ������Ʈ ��Ȱ��ȭ
                textBooperGroup.SetActive(false);
                // �޴� ������Ʈ Ȱ��ȭ
                menuGroup.gameObject.SetActive(true);
                // �ؽ�Ʈ�� ��ĭ���� ����
                text.text = "";
            }
            // �ƾ� ù ���� ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[2]))
            {
                // ���� ���� ���
                StartCoroutine(TextBooperActive(3, false));
            }
            // �ƾ� �� ��° ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[3]))
            {
                // ���� ���� ���
                StartCoroutine(TextBooperActive(4, false));
                // �������� ��Ȱ��ȭ
                MoveBeel.active(false);
            }
            // �ƾ� �� ��° ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[4]))
            {
                // ���� ���� ���
                StartCoroutine(TextBooperActive(5, false));
                // �������� �̸� �ؽ�Ʈ ��Ȱ��ȭ
                nameText.gameObject.SetActive(false);
                // �� ��� ��Ȱ��ȭ
                bakcGround.SetActive(false);
                // �� �� �׷� Ȱ��ȭ
                cutSceneGroup.SetActive(true);
            }
            // �ƾ� �� ��° ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[5]))
            {
                // ���� ���� ���
                StartCoroutine(TextBooperActive(6, false));
                // �ƾ� �̹��� ����
                CutSceneChange.ChangeImage();
            }
            // �ƾ� �ټ� ��° ��翡�� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[6]))
            {
                // ���� ���� ���
                StartCoroutine(TextBooperActive(7, false));
                // �ƾ� �̹��� ����
                CutSceneChange.ChangeImage();
            }
            // ������ �ƾ� ���� ����Ű �Է½�
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[7]))
            {
                // 1é�� ����
                StartStage(1);
                Debug.Log("Start 1Chapter");
            }
            // ���� �޼����� ������
            else if (Input.GetKeyDown(EnterKey) && text.text.Equals(textList[textList.Length - 1]))
            {
                // ���� ����
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// �ؽ�Ʈ, ���� Ȱ��ȭ
    /// </summary>
    /// <param name="textIndex">������ �ؽ�Ʈ �ε���</param>
    /// <param name="groupActive">�׷����� �ؽ�Ʈ�� ���۸� �Ѵ� Ȱ��ȭ ��ų��</param>
    /// <returns></returns>
    private IEnumerator TextBooperActive(int textIndex, bool groupActive = true)
    {
        // �ؽ�Ʈ ����Ʈ�� �ؽ�Ʈ�� ����
        text.text = textList[textIndex];

        // ù ��° ��ȭ�� ����ҷ��� 0.5�� �Ŀ� ���
        if (textIndex == 0)
            yield return new WaitForSeconds(0.5f);

        // groupActive�� ���̸�
        if (groupActive)
            // �ؽ�Ʈ�� ���۸� Ȱ��ȭ
            // �ؽ�Ʈ�� ��Ȱ��ȭ�� ���۵� ��Ȱ��ȭ �����̱� ����
            textBooperGroup.SetActive(true);
        else
        {
            // �ؽ�Ʈ ���� ��ũ��Ʈ �۵�
            // OnEnable() �̺�Ʈ �޼��忡 �ߵ��ϵ��� �����س���
            text.gameObject.SetActive(false);
            text.gameObject.SetActive(true);
        }
        // ���̾�α� ��� ���� ���
        MenuAudioManager.DialogComfirm();
    }

    /// <summary>
    /// ���� �޴��� ���� �ڵ� ����
    /// </summary>
    /// <param name="menuIndex">������ �޴� �ε���</param>
    public void SelectMenu(int menuIndex)
    {
        switch (menuIndex)
        {
            // ���� ����
            case 0:
                // ù �������� ����
                StartStage(0);
                break;
            // é�� ���� �޴�
            case 1:
                // �޴� ��Ȱ��ȭ
                menuGroup.SetActive(false);
                // é�� ���� �޴� Ȱ��ȭ
                selectMenuGroup.SetActive(true);
                break;
            // ���� ����
            case 2:
                // ���� ���� �ؽ�Ʈ�� ����
                StartCoroutine(TextBooperActive(textList.Length - 1));
                // �޴� ������Ʈ ��Ȱ��ȭ
                menuGroup.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// é�� ���ÿ��� �޴��� ��ȯ
    /// </summary>
    public void ReturnToMenu()
    {
        // é�� ���� �޴� ��Ȱ��ȭ
        selectMenuGroup.SetActive(false);
        // �޴� Ȱ��ȭ
        menuGroup.SetActive(true);
    }

    /// <summary>
    /// é�� ����
    /// </summary>
    /// <param name="stage">������ é�� �� �ε�</param>
    public void StartStage(int stage)
    {
        Debug.Log("Start : " + stage + "stage");
        switch (stage)
        {
            case 0:
                // é�� ���ÿ��� �ƴ� ���� �������� 1�������� ���۽�
                // �� ���� �ҷ��´�
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
        // �޴�, é�� ���� ��Ȱ��ȭ
        menuGroup.SetActive(false);
        selectMenuGroup.SetActive(false);
        // �ؽ�Ʈ ����
        StartCoroutine(TextBooperActive(2));
    }

}
