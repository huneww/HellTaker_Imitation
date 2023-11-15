using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuciferCutScene : MonoBehaviour
{
    // �� ���
    [SerializeField]
    private GameObject[] backGrounds;
    // ���� ���̷���
    [SerializeField]
    private GameObject[] guards;
    // ����� �̹���
    [SerializeField]
    private GameObject demonImage;
    // ����� �̹��� ���� ��������Ʈ
    [SerializeField]
    private Sprite[] demonSprite;
    // ��� �ؽ�Ʈ
    [SerializeField]
    private Text dialogText;
    // ����
    [SerializeField]
    private GameObject booper;
    // ������ �޴�
    [SerializeField]
    private GameObject selectMenu;
    // �ι�° ������ �޴�
    [SerializeField]
    private GameObject secondSelectMenu;
    // ���� ���� �޴�
    [SerializeField]
    private GameObject goodEndMenu;
    // ���� ��� �ε���
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    // ��Ʈ�� ���
    [SerializeField, TextArea]
    private string[] text_before;
    // ù��° ������ ������ ���
    [SerializeField, TextArea]
    private string[] text_after;
    // �ι��� ������ ������ ���
    [SerializeField, TextArea]
    private string[] text_second_after;

    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action<bool> selectBad;
    public static Action<bool> selectGood;
    // ��忣��, ���ǿ��� �����ߴ��� Ȯ�� ����
    private bool isBad = false;
    private bool isSecondBad = false;
    private bool isGood = false;

    private void Awake()
    {
        // �׼� ���� �� ����
        selectBad = (value) => { SelectBad(value); };
        selectGood = (value) => { SelectGood(value); };
        curDialogTextIndex = 0;
    }

    private IEnumerator Start()
    {
        float movetime = 0;
        // �� ��� �̵�
        foreach (var back in backGrounds)
        {
            DialogBackGroundMove move = back.GetComponent<DialogBackGroundMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = movetime < move.MoveTime ? move.MoveTime : movetime;
        }
        yield return new WaitForSeconds(movetime);
        // ���� ���̷��� �̵�
        foreach (var guard in guards)
        {
            GuardMove move = guard.GetComponent<GuardMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = move.MoveTime;
        }
        yield return new WaitForSeconds(movetime + 1.5f);
        // ��� �ؽ�Ʈ ����
        dialogText.text = text_before[0];
        // ����� �̵�
        StartCoroutine(demonImage.GetComponent<LuciferSpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        // ���̾�αװ� ��Ȱ��ȭ ���¸� �޼��� ����
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance.IsDialog)
            {
                // ���� ��簡 ������
                if (curDialogTextIndex < text_before.Length - 1)
                {
                    // ���� ��� �ε����� �� ����
                    curDialogTextIndex++;
                    // ��� ����
                    dialogText.text = text_before[curDialogTextIndex];
                    // ���̾�α� ��� �Ϸ� ���� ���
                    AudioManager.Instance.DialogComFirm();
                }
                // ���� ��簡 text_second_after[1]�� ���ٸ�
                else if (dialogText.text.Contains(text_second_after[1]))
                {
                    // ��� ����
                    dialogText.text = text_second_after[2];
                    // ���̾�α� ��� �Ϸ� ���� ���
                    AudioManager.Instance.DialogComFirm();
                    isGood = true;
                    GameManager.Instance.IsDialog = false;
                    // ���� ���� UI Ȱ��ȭ
                    goodEndMenu.SetActive(true);
                    // ����� ��������Ʈ ����
                    demonImage.GetComponent<Image>().sprite = demonSprite[1];
                }
                // ���� �޴��� ����
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
            else if (isGood || isBad || isSecondBad)
            {
                if (isGood)
                {
                    // ���� ���� �޼��� ����
                    GameManager.Instance.GoodEnd();
                    // �ƾ� ��Ȱ��ȭ
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
                else if (isSecondBad)
                {
                    // ���� ���� �޼��� ����
                    GameManager.Instance.BadSecondEnd();
                }
            }
        }
    }

    /// <summary>
    /// ���� ���� ���ý�
    /// </summary>
    /// <param name="isSecond">����� é�Ϳ��� �ι�° ���������� ���忣�� ���� Ȯ�� ����</param>
    private void SelectBad(bool isSecond = false)
    {
        // ���� Ȱ��ȭ
        booper.SetActive(true);

        if (!isSecond)
        {
            // ���� �޴� ��Ȱ��ȭ
            selectMenu.SetActive(false);
            isBad = true;
            // ��� �ؽ�Ʈ ����
            dialogText.text = text_after[0];
        }
        else
        {
            // �ι�° ���� �޴� ��Ȱ��ȭ
            secondSelectMenu.SetActive(false);
            isSecondBad = true;
            // ��� �ؽ�Ʈ ����
            dialogText.text = text_second_after[0];
        }
    }

    /// <summary>
    /// ���� ���� ���ý�
    /// </summary>
    /// <param name="isSecond"> ����� é�Ϳ��� �ι�° ���������� ���ǿ��� ���� Ȯ�� ����</param>
    private void SelectGood(bool isSecond = false)
    {
        if (!isSecond)
        {
            // ���� �޴� ��Ȱ��ȭ
            selectMenu.SetActive(false);
            // �ι��� ���� �޴� Ȱ��ȭ
            secondSelectMenu.SetActive(true);
            // ��� �ؽ�Ʈ ����
            dialogText.text = text_after[1];
            // ����� �ִϸ����� ��Ȱ��ȭ
            demonImage.GetComponent<Animator>().enabled = false;
            // ����� �̹��� ����
            demonImage.GetComponent<Image>().sprite = demonSprite[0];
        }
        else
        {
            // ���� Ȱ��ȭ
            booper.SetActive(true);
            // �ι��� ���� �޴� ��Ȱ��ȭ
            secondSelectMenu.SetActive(false);
            // ��� �ؽ�Ʈ ����
            dialogText.text = text_second_after[1];
            GameManager.Instance.IsDialog = true;
            GameManager.Instance.IsSelect = false;
        }
    }
}
