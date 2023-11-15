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
    // ù��° �� ���
    [SerializeField]
    private GameObject firstBack;
    // ���� �� ���
    // ��Ʈ�� �ִϸ��̼� ����� ��� ���Դٰ� �����
    [SerializeField]
    private GameObject lightHell;
    // �ι�° �� ���
    [SerializeField]
    private GameObject secondBack;
    // ù��° ��濡�� �ι�° ������� �Ѿ�� �ð�
    [SerializeField]
    private float backMoveTime = 1f;
    // ù��° ��� �̵� ��ġ
    [SerializeField]
    private Vector3 firstTarget;
    // �ι��� ��� �̵� ��ġ
    [SerializeField]
    private Vector3 secondTarget;

    [Space(10), Header("Chain")]
    // ó�� ������ ü��
    [SerializeField]
    private GameObject firstChain;
    // �ι�°�� ������ ü��
    [SerializeField]
    private GameObject secondChain;
    // ����°�� ������ ü��
    [SerializeField]
    private GameObject thirdChain;

    [Space(10), Header("Arm")]
    // ������Ʈ ��
    [SerializeField]
    private GameObject[] arms;
    // �� �ִϸ��̼� �Ķ���� �ؽ���
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
        // ���� �� ��� ���̵� �ƿ�
        LightHellFadeOut();
        // �����ð� ���
        yield return new WaitForSeconds(1.7f);
        // �� �ִϸ��̼� ����
        foreach (var arm in arms)
        {
            arm.GetComponent<Animator>().SetTrigger(armMoveHash);
        }
        // �����ð� ���
        yield return new WaitForSeconds(2f);
        // �� ��� �ִϸ��̼� ���
        StartCoroutine(BackGroundMove());
    }

    private IEnumerator BackGroundMove()
    {
        float curTime = 0;
        float percent = 0;

        // ù��° ��� ���� ��ġ ����
        Vector3 firstPos = firstBack.GetComponent<RectTransform>().localPosition;
        // �ι�° ��� ���� ��ġ ����
        Vector3 secondPos = secondBack.GetComponent<RectTransform>().localPosition;

        // ��� ��ġ �̵�
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / backMoveTime;
            firstBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(firstPos, firstTarget, percent);
            secondBack.GetComponent<RectTransform>().localPosition = Vector3.Lerp(secondPos, secondTarget, percent);
            yield return null;
        }

        // ù��° ��� ��Ȱ��ȭ
        firstBack.SetActive(false);
        // �ι�° ��� �̵���ġ�� ��ġ ����
        secondBack.GetComponent<RectTransform>().localPosition = secondTarget;

        // ���ð� ����
        WaitForSeconds chainDelay = new WaitForSeconds(0.1f);
        // ù��° ü�� Ȱ��ȭ
        firstChain.SetActive(true);
        yield return chainDelay;
        // �ι�° ü�� Ȱ��ȭ
        secondChain.SetActive(true);
        yield return chainDelay;
        // ����° ü�� Ȱ��ȭ
        thirdChain.SetActive(true);
    }

    private void LightHellFadeOut()
    {
        // ���� �� ��� Ȱ��ȭ
        lightHell.SetActive(true);
        // �� ��� ���̵� �ƿ�
        StartCoroutine(lightHell.GetComponent<FadeOutBackGround>().FadeOut());
    }

}
