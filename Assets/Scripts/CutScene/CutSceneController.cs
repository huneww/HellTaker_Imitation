using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backGrounds;
    [SerializeField]
    private GameObject demonImage;
    [SerializeField]
    private Sprite demonChangeSprite;
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private GameObject booper;
    [SerializeField]
    private GameObject selectMenu;
    [SerializeField]
    private GameObject goodEndMenu;
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    [SerializeField, TextArea]
    private string[] text_before;
    [SerializeField, TextArea]
    private string[] text_after;

    public static Action selectBad;
    public static Action selectGood;
    private bool isBad = false;
    private bool isGood = false;

    private void Awake()
    {
        selectBad = () => { SelectBad(); };
        selectGood = () => { SelectGood(); };
        curDialogTextIndex = 0;
    }

    private IEnumerator Start()
    {
        float movetime = 0;
        foreach (var back in backGrounds)
        {
            DialogBackGroundMove move = back.GetComponent<DialogBackGroundMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = movetime < move.MoveTime ? move.MoveTime : movetime;
        }
        yield return new WaitForSeconds(movetime);
        dialogText.text = text_before[0];
        StartCoroutine(demonImage.GetComponent<SpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance.IsDialog)
            {
                if (curDialogTextIndex < text_before.Length - 1)
                {
                    curDialogTextIndex++;
                    dialogText.text = text_before[curDialogTextIndex];
                    AudioManager.Instance.DialogComFirm();
                }
                else if (GameManager.Instance.CurStage == 8)
                {
                    GameManager.Instance.BadEnd();
                }
                else
                {
                    selectMenu.SetActive(true);
                    GameManager.Instance.IsDialog = false;
                    GameManager.Instance.IsSelect = true;
                    booper.SetActive(false);
                }
            }
            else if (isGood || isBad)
            {
                if (isGood)
                {
                    GameManager.Instance.GoodEnd();
                    gameObject.SetActive(false);
                    Debug.Log("Good End");
                }
                else if (isBad)
                {
                    // 천국으로 가는 엔딩 씬을 따로 제작
                    if (GameManager.Instance.CurStage == 5)
                    {
                        Debug.Log("Go to Heaven");
                    }
                    else
                        GameManager.Instance.BadEnd();
                    Debug.Log("Bad End");
                }
            }
        }
    }

    private void SelectBad()
    {
        booper.SetActive(true);
        selectMenu.SetActive(false);
        isBad = true;
        dialogText.text = text_after[0];
    }

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
