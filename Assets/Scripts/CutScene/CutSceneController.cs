using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    // �� ���
    [SerializeField]
    private GameObject[] backGrounds;
    // �Ǹ� �̹���
    [SerializeField]
    private GameObject demonImage;
    // �Ǹ� �̹��� ���� ��������Ʈ
    [SerializeField]
    private Sprite demonChangeSprite;
    // ��� �ؽ�Ʈ
    [SerializeField]
    private Text dialogText;
    // ����
    [SerializeField]
    private GameObject booper;
    // ���� �޴�
    [SerializeField]
    private GameObject selectMenu;
    // ���� ����
    [SerializeField]
    private GameObject goodEndMenu;
    // ���� ��� �ε���
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    // ���� �޴� ���� �ؽ�Ʈ
    [SerializeField, TextArea]
    private string[] text_before;
    // ���� �޴� ������ �ؽ�Ʈ
    [SerializeField, TextArea]
    private string[] text_after;

    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action selectBad;
    public static Action selectGood;
    // ������ ���� Ȯ�� ����
    private bool isBad = false;
    private bool isGood = false;

    private void Awake()
    {
        // �׼� ���� �� ����
        selectBad = () => { SelectBad(); };
        selectGood = () => { SelectGood(); };
        curDialogTextIndex = 0;
    }

    private IEnumerator Start()
    {
        float movetime = 0;
        // ��� ��ġ �̵�
        foreach (var back in backGrounds)
        {
            DialogBackGroundMove move = back.GetComponent<DialogBackGroundMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = movetime < move.MoveTime ? move.MoveTime : movetime;
        }
        yield return new WaitForSeconds(movetime);
        // ��� �ؽ�Ʈ ����
        dialogText.text = text_before[0];
        // �Ǹ� �̹��� �̵�
        StartCoroutine(demonImage.GetComponent<SpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance.IsDialog)
            {
                // ���� ��簡 �ִٸ�
                if (curDialogTextIndex < text_before.Length - 1)
                {
                    // �ε����� ����
                    curDialogTextIndex++;
                    // ��� �ؽ�Ʈ ����
                    dialogText.text = text_before[curDialogTextIndex];
                    // ���̾�α� ��� �Ϸ� ���� ���
                    AudioManager.Instance.DialogComFirm();
                }
                // é�� 9������ �ٷ� ���� ���� ����
                else if (GameManager.Instance.CurStage == 8)
                {
                    GameManager.Instance.BadEnd();
                }
                else
                {
                    // ���� �޴� Ȱ��ȭ
                    selectMenu.SetActive(true);
                    GameManager.Instance.IsDialog = false;
                    GameManager.Instance.IsSelect = true;
                    // ���� ��Ȱ��ȭ
                    booper.SetActive(false);
                }
            }
            else if (isGood || isBad)
            {
                if (isGood)
                {
                    // ���� ���� �޼��� ����
                    GameManager.Instance.GoodEnd();
                    // ������Ʈ ��Ȱ��ȭ
                    gameObject.SetActive(false);
                    Debug.Log("Good End");
                }
                else if (isBad)
                {
                    // õ������ ���� ���� ���� ���� ����
                    if (GameManager.Instance.CurStage == 5)
                    {
                        Debug.Log("Go to Heaven");
                    }
                    else
                        // ���� ���� �޼��� ����
                        GameManager.Instance.BadEnd();
                    Debug.Log("Bad End");
                }
            }
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void SelectBad()
    {
        booper.SetActive(true);
        selectMenu.SetActive(false);
        isBad = true;
        dialogText.text = text_after[0];
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void SelectGood()
    {
        AudioManager.Instance.GoodEnd();
        demonImage.GetComponent<Image>().sprite = demonChangeSprite;
        selectMenu.SetActive(false);
        goodEndMenu.SetActive(true);
        isGood = true;
        dialogText.text = text_after[1];
    }

}
