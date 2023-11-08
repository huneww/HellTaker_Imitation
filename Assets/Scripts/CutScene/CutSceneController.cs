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

    [Space(10), Header("Pandemonica")]
    [SerializeField, TextArea]
    private string[] pande_text_before;
    [SerializeField, TextArea]
    private string[] pande_text_select;
    [SerializeField, TextArea]
    private string[] pande_text_after;

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
        switch (GameManager.Instance.CurStage)
        {
            case 1:
                dialogText.text = pande_text_before[0];
                break;

        }
        StartCoroutine(demonImage.GetComponent<SpriteMove>().MoveCoroutine());
    }

    private void Update()
    {
        if (!dialogText.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (GameManager.Instance.CurStage)
            {
                case 0:
                    if (GameManager.Instance.IsDialog)
                    {
                        if (curDialogTextIndex < pande_text_before.Length - 1)
                        {
                            curDialogTextIndex++;
                            dialogText.text = pande_text_before[curDialogTextIndex];
                            AudioManager.Instance.DialogComFirm();
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
                            Debug.Log("Good End");
                        }
                        else if (isBad)
                        {
                            GameManager.Instance.BadEnd();
                            Debug.Log("Bad End");
                        }
                    }
                    break;
            }
        }
    }

    private void SelectBad()
    {
        booper.SetActive(true);
        selectMenu.SetActive(false);
        isBad = true;
        dialogText.text = pande_text_after[0];
    }

    private void SelectGood()
    {
        AudioManager.Instance.GoodEnd();
        demonImage.GetComponent<Image>().sprite = demonChangeSprite;
        selectMenu.SetActive(false);
        goodEndMenu.SetActive(true);
        isGood = true;
        dialogText.text = pande_text_after[1];
    }

}
