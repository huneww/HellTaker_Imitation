using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class JGIntroBackGroundManager : MonoBehaviour
{
    private static JGIntroBackGroundManager instance;
    public static JGIntroBackGroundManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [Space(10), Header("BackGround")]
    [SerializeField]
    private GameObject firstBack;
    [SerializeField]
    private GameObject lightHell;
    [SerializeField]
    private GameObject secondBack;
    [SerializeField]
    private float backMoveTime = 1f;
    [SerializeField]
    private Vector3 firstTarget;
    [SerializeField]
    private Vector3 secondTarget;

    [Space(10), Header("Chain")]
    [SerializeField]
    private GameObject firstChain;
    [SerializeField]
    private GameObject secondChain;
    [SerializeField]
    private GameObject thirdChain;

    [Space(10), Header("Arm")]
    [SerializeField]
    private GameObject[] arms;
    private int armMoveHash = Animator.StringToHash("Move");

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public IEnumerator IntroCoroutine()
    {
        LightHellFadeOut();
        yield return new WaitForSeconds(1.7f);
        foreach (var arm in arms)
        {
            arm.GetComponent<Animator>().SetTrigger(armMoveHash);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(BackGroundMove());
    }

    private IEnumerator BackGroundMove()
    {
        float curTime = 0;
        float percent = 0;
        Vector3 firstPos = firstBack.GetComponent<RectTransform>().localPosition;
        Vector3 secondPos = secondBack.GetComponent<RectTransform>().localPosition;

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / backMoveTime;
            firstBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(firstPos, firstTarget, percent);
            secondBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(secondPos, secondTarget, percent);
            yield return null;
        }

        firstBack.SetActive(false);
        secondBack.GetComponent<RectTransform>().localPosition = secondTarget;

        WaitForSeconds chainDelay = new WaitForSeconds(0.1f);
        firstChain.SetActive(true);
        yield return chainDelay;
        secondChain.SetActive(true);
        yield return chainDelay;
        thirdChain.SetActive(true);
    }

    private void LightHellFadeOut()
    {
        lightHell.SetActive(true);
        StartCoroutine(lightHell.GetComponent<FadeOutBackGround>().FadeOut());
    }

}
