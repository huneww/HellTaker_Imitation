using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSelectMenu : MonoBehaviour
{
    // �⺻ ���� ����
    [SerializeField]
    private Color IdleColor;
    // ���̶���Ʈ ���� ����
    [SerializeField]
    private Color HighlightColor;
    // �ؽ�Ʈ �̹���
    [SerializeField]
    private Image[] textImages;
    // �ؽ�Ʈ
    [SerializeField]
    private Text[] texts;
    // ���� �޴� �ؽ�Ʈ
    [SerializeField, TextArea]
    private string[] text_select;
    // ���� �������� �ؽ�Ʈ �ε���
    [SerializeField]
    private int curIndex;

    private void Start()
    {
        // ���� �ʱ�ȭ
        ChangeColor();
        // ���� �޴� �ؽ�Ʈ ����
        texts[0].text = text_select[0];
        texts[1].text = text_select[1];
    }

    private void Update()
    {
        // ���� �޴��� �ִ� ���°� �ƴϸ� �޼��� ����
        if (!GameManager.Instance.IsSelect)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ���� �ε��� ����
            curIndex = curIndex > 0 ? --curIndex : curIndex;
            Debug.Log(curIndex);
            // ���� ����
            ChangeColor();
            // �޴� �̵� ���� ���
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ���� �ε��� ����
            curIndex = curIndex < textImages.Length - 1 ? ++curIndex : curIndex;
            Debug.Log(curIndex);
            // ���� ����
            ChangeColor();
            // �޴� �̵� ���� ���
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // �޴� ���� ���� ���
            AudioManager.Instance.DialogSelect();
            // ���� �޴��� �´� �ڵ� ����
            SelectMenu();
        }
    }

    /// <summary>
    /// ������ �޴��� ���� �ڵ� ����
    /// </summary>
    private void SelectMenu()
    {
        // Ư�� é�ʹ� ù��° �������� ���� ����
        if (GameManager.Instance.CurStage == 2 || GameManager.Instance.CurStage == 3 ||
            GameManager.Instance.CurStage == 4 || GameManager.Instance.CurStage == 5)
        {
            if (curIndex == 0)
            {
                CutSceneController.selectGood();
            }
            else if (curIndex == 1)
            {
                CutSceneController.selectBad();
            }
        }
        // ����Ƽ�� é�ʹ� �Ѵ� ���� ����
        else if (GameManager.Instance.CurStage == 6)
        {
            CutSceneController.selectGood();
        }
        else
        {
            if (curIndex == 0)
            {
                // ����� é���̰�
                if (GameManager.Instance.CurStage == 7)
                {
                    // ���� ���� �޴��� �ι�°���
                    if (gameObject.CompareTag("SecondSelect"))
                        LuciferCutScene.selectBad(true);
                    else
                        LuciferCutScene.selectBad(false);
                }
                else
                    CutSceneController.selectBad();
            }
            else if (curIndex == 1)
            {
                // ����� é���̰�
                if (GameManager.Instance.CurStage == 7)
                {
                    // ���� ���� �޴��� �ι�°���
                    if (gameObject.CompareTag("SecondSelect"))
                        LuciferCutScene.selectGood(true);
                    else
                        LuciferCutScene.selectGood(false);
                }
                else
                    CutSceneController.selectGood();
            }
        }
    }

    /// <summary>
    /// ���� �޴� ���� ����
    /// </summary>
    private void ChangeColor()
    {
        // �̹���, �ؽ�Ʈ ���� �⺻���·� ����
        foreach (var image in textImages)
            image.color = IdleColor;
        foreach (var text in texts)
            text.color = Color.gray;

        // ������ �̹̤Ӥ�, �ؽ�Ʈ ���� ���̶���Ʈ�� ����
        textImages[curIndex].color = HighlightColor;
        texts[curIndex].color = Color.white;
    }

}
