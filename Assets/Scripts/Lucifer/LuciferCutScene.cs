using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuciferCutScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backGrounds;
    [SerializeField]
    private GameObject[] guards;
    [SerializeField]
    private GameObject demonImage;
    [SerializeField]
    private Sprite[] demonSprite;
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private GameObject booper;
    [SerializeField]
    private GameObject selectMenu;
    [SerializeField]
    private GameObject secondSelectMenu;
    [SerializeField]
    private GameObject goodEndMenu;
    [SerializeField]
    private int curDialogTextIndex = 0;

    [Space(10)]
    [SerializeField, TextArea]
    private string[] text_before;
    [SerializeField, TextArea]
    private string[] text_after;
    [SerializeField, TextArea]
    private string[] text_second_after;

    public static Action<bool> selectBad;
    public static Action<bool> selectGood;
    private bool isBad = false;
    private bool isSecondBad = false;
    private bool isGood = false;

    private void Awake()
    {
        selectBad = (value) => { SelectBad(value); };
        selectGood = (value) => { SelectGood(value); };
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
        foreach (var guard in guards)
        {
            GuardMove move = guard.GetComponent<GuardMove>();
            StartCoroutine(move.MoveCoroutine());
            movetime = move.MoveTime;
        }
        yield return new WaitForSeconds(movetime + 1.5f);
        dialogText.text = text_before[0];
        StartCoroutine(demonImage.GetComponent<LuciferSpriteMove>().MoveCoroutine());
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
                else if (dialogText.text.Contains(text_second_after[1]))
                {
                    dialogText.text = text_second_after[2];
                    AudioManager.Instance.DialogComFirm();
                    isGood = true;
                    GameManager.Instance.IsDialog = false;
                    goodEndMenu.SetActive(true);
                    demonImage.GetComponent<Image>().sprite = demonSprite[1];
                }
                else
                {
                    selectMenu.SetActive(true);
                    GameManager.Instance.IsDialog = false;
                    GameManager.Instance.IsSelect = true;
                    booper.SetActive(false);
                }
            }
            else if (isGood || isBad || isSecondBad)
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
                else if (isSecondBad)
                {
                    GameManager.Instance.BadSecondEnd();
                }
            }
        }
    }

    private void SelectBad(bool isSecond = false)
    {
        if (!isSecond)
        {
            booper.SetActive(true);
            selectMenu.SetActive(false);
            isBad = true;
            dialogText.text = text_after[0];
        }
        else
        {
            booper.SetActive(true);
            secondSelectMenu.SetActive(false);
            isSecondBad = true;
            dialogText.text = text_second_after[0];
        }
    }

    private void SelectGood(bool isSecond = false)
    {
        if (!isSecond)
        {
            selectMenu.SetActive(false);
            secondSelectMenu.SetActive(true);
            dialogText.text = text_after[1];
            demonImage.GetComponent<Animator>().enabled = false;
            demonImage.GetComponent<Image>().sprite = demonSprite[0];
        }
        else
        {
            booper.SetActive(true);
            secondSelectMenu.SetActive(false);
            dialogText.text = text_second_after[1];
            GameManager.Instance.IsDialog = true;
            GameManager.Instance.IsSelect = false;
            //demonImage.GetComponent<Image>().sprite = demonSprite[1];
            //AudioManager.Instance.GoodEnd();
        }
    }
}
