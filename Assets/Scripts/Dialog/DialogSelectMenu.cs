using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSelectMenu : MonoBehaviour
{
    [SerializeField]
    private Color IdleColor;
    [SerializeField]
    private Color HighlightColor;
    [SerializeField]
    private Image[] textImages;
    [SerializeField]
    private Text[] texts;
    [SerializeField, TextArea]
    private string[] text_select;
    [SerializeField]
    private int curIndex;

    private void Start()
    {
        ChangeColor();
        texts[0].text = text_select[0];
        texts[1].text = text_select[1];
    }

    private void Update()
    {
        if (!GameManager.Instance.IsSelect)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            curIndex = curIndex > 0 ? --curIndex : curIndex;
            Debug.Log(curIndex);
            ChangeColor();
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            curIndex = curIndex < textImages.Length - 1 ? ++curIndex : curIndex;
            Debug.Log(curIndex);
            ChangeColor();
            AudioManager.Instance.DialogMove();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            AudioManager.Instance.DialogSelect();
            SelectMenu();
        }
    }

    private void SelectMenu()
    {
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
        else if (GameManager.Instance.CurStage == 6)
        {
            CutSceneController.selectGood();
        }
        else
        {
            if (curIndex == 0)
            {
                if (GameManager.Instance.CurStage == 7)
                {
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
                if (GameManager.Instance.CurStage == 7)
                {
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

    private void ChangeColor()
    {
        foreach (var image in textImages)
            image.color = IdleColor;
        foreach (var text in texts)
            text.color = Color.gray;

        textImages[curIndex].color = HighlightColor;
        texts[curIndex].color = Color.white;
    }

}
