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
    [SerializeField]
    private int curIndex;

    private void Start()
    {
        ChangeColor();
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
        switch (GameManager.Instance.CurStage)
        {
            case 0:
                if (curIndex == 0)
                {
                    CutSceneController.selectBad();
                }
                else if (curIndex == 1)
                {
                    CutSceneController.selectGood();
                }
                break;
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
