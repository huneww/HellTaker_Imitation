using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroTextManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeBackGround;

    [SerializeField]
    private Text text;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private GameObject booper;

    [SerializeField, TextArea]
    private string[] introText;

    [SerializeField, TextArea]
    private string answerText;
    [SerializeField, TextArea]
    private string demonName;

    [Space(10), Header("Image")]
    [SerializeField]
    private GameObject demonImage;

    private void Start()
    {
        text.text = introText[0];
    }

    private void Update()
    {
        TextChange();
    }

    private void TextChange()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (text.text.Contains(introText[0]))
            {
                TextPingPong(introText[1]);
                nameText.gameObject.SetActive(true);
                StartCoroutine(fadeBackGround.GetComponent<FadeOutBackGround>().FadeOut());
            }
            else if (text.text.Contains(introText[1]))
            {
                TextPingPong(introText[2]);
            }
            else if (text.text.Contains(introText[2]))
            {
                text.gameObject.SetActive(false);
                nameText.gameObject.SetActive(false);
                booper.SetActive(false);
                StartCoroutine(StartIntro());
                JGIntroAudioManager.Instance.JGIntro();
            }
            else if (text.text.Contains(answerText))
            {
                SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_1");
            }
        }
    }

    private IEnumerator StartIntro()
    {
        yield return StartCoroutine(JGIntroBackGroundManager.Instance.IntroCoroutine());
        text.text = answerText;
        nameText.text = demonName;
        DialogEnable(true);
        demonImage.SetActive(true);
    }

    private void DialogEnable(bool active)
    {
        text.gameObject.SetActive(active);
        nameText.gameObject.SetActive(active);
        booper.SetActive(active);
    }

    private void TextPingPong(string changeText)
    {
        text.gameObject.SetActive(false);
        text.text = changeText;
        text.gameObject.SetActive(true);
    }

}
