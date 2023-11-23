using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JGBossDialogManager : MonoBehaviour
{
    private static JGBossDialogManager instance;
    public static JGBossDialogManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    [Header("UI Group")]
    // �÷��̿� �ʿ��� UI
    [SerializeField]
    private GameObject playUI;
    // ���̾�α׿� �ʿ��� UI
    [SerializeField]
    private GameObject dialogUI;

    [Space(10), Header("Move Back")]
    // ���� �ö� �̹���
    [SerializeField]
    private RectTransform move_Up;
    // �Ʒ��� ������ �̹���
    [SerializeField]
    private RectTransform move_Down;
    // �� ��� �̵� �ð�
    [SerializeField]
    private float backMoveTime;

    [Space(10), Header("Demon Image")]
    // �Ǹ� �̹��� Ʈ������
    [SerializeField]
    private RectTransform demonTransform;
    // �Ǹ� �̹���
    [SerializeField]
    private Image demonImage;
    // �̹��� �̵� �ð�
    [SerializeField]
    private float imageMoveTime;
    // �̹��� ���İ� ���� Ŀ��
    [SerializeField]
    private AnimationCurve colorAlpha;
    // �ִϸ��̼� �̹���
    [SerializeField]
    private GameObject demonImageAnimation;
    // �ִϸ��̼� ��
    [SerializeField]
    private GameObject demonArm;
    // ���ε� ü��
    [SerializeField]
    private GameObject[] bindingChains;

    [Space(10), Header("Name, Dialog, Booper")]
    // ���̾�α� �̸� �ؽ�Ʈ
    [SerializeField]
    private Text demonName;
    // ���̾�α� ��� �ؽ�Ʈ
    [SerializeField]
    private Text dialog;
    // ���̾�α� ���� ������Ʈ
    [SerializeField]
    private GameObject booper;

    [Space(10), Header("Dialog List")]
    // ���̾�α� ����Ʈ
    public Dialog[] dialogList;
    // ���� ����Ʈ �ε���
    [SerializeField]
    private int dialogListIndex = 0;
    // ����Ʈ �ȿ��ִ� �ؽ�Ʈ �ε���
    [SerializeField]
    private int dialogListTextIndex = 0;

    [Space(10), Header("Select Menu")]
    // ���� �޴�
    [SerializeField]
    private GameObject selectMenu;
    // �޴� �̹���
    [SerializeField]
    private Image[] menuImages;
    // �⺻ ���� ����
    [SerializeField]
    private Color idle;
    // ���� ����
    [SerializeField]
    private Color hightLight;

    [Space(10), Header("Success")]
    [SerializeField]
    private GameObject success;

    private string sceneName;

    /// <summary>
    /// ���̾�α� ����
    /// </summary>
    public void StartDialog()
    {
        // �÷��� UI ��Ȱ��ȭ
        playUI.SetActive(false);
        // ���̾�α� UI Ȱ��ȭ
        dialogUI.SetActive(true);
        // ���̾�α� ���� �ڷ�ƾ ����
        StartCoroutine(DialogStartCoroutine());
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        SelecetMenu();
        DialogController();
    }

    private void DialogController()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (sceneName.Contains("BossStage_3"))
            {
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.NextStage();
                    }
                    else
                    {
                        if (demonImageAnimation != null && demonImageAnimation.activeSelf)
                        {
                            Destroy(demonImageAnimation);
                        }

                        if (dialogListIndex == 4 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                        }
                        else if (dialogListIndex == 5 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "�˾�. �׷��� ���� �̷��� �����Ѱ���.";
                            menuImages[1].GetComponentInChildren<Text>().text = "�� ���鵵 �� ������ ��µ�!";
                        }
                        else
                        {
                            if (selectMenu.activeSelf)
                            {
                                booper.SetActive(true);
                                selectMenu.SetActive(false);
                            }

                            // ����Ʈ�� ���� ���, �̹����� ����
                            NextDialog();
                        }
                        // �ؽ�Ʈ ��� �Ϸ� ����
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // ���� �ؽ�Ʈ�� ����
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // �ؽ�Ʈ ��� �Ϸ� ����
                    JGBossAudioManager.Instance.TextEnd();
                }

                if (bindingChains[0] != null)
                {
                    if (bindingChains[0].activeSelf)
                    {
                        Destroy(demonArm);
                        demonImageAnimation.GetComponent<Animator>().SetTrigger("Binding");
                        Animator[] clones = bindingChains[0].GetComponentsInChildren<Animator>();
                        JGBossAudioManager.Instance.JGBinding();
                        if (clones[0].gameObject.name.Contains("Chain_Back_1"))
                        {
                            clones[0].SetTrigger("Binding_1");
                            clones[1].SetTrigger("Binding_2");
                        }
                        else if (clones[0].gameObject.name.Contains("Chain_Back_2"))
                        {
                            clones[0].SetTrigger("Binding_2");
                            clones[1].SetTrigger("Binding_1");
                        }
                        clones = bindingChains[1].GetComponentsInChildren<Animator>();
                        if (clones[0].gameObject.name.Contains("Chain_Front_1"))
                        {
                            clones[0].SetTrigger("Binding_1");
                            clones[1].SetTrigger("Binding_2");
                        }
                        else if (clones[0].gameObject.name.Contains("Chain_Front_2"))
                        {
                            clones[0].SetTrigger("Binding_2");
                            clones[1].SetTrigger("Binding_1");
                        }

                        Destroy(bindingChains[0], 0.5f);
                        Destroy(bindingChains[1], 0.5f);
                    }

                    if (!bindingChains[0].activeSelf)
                    {
                        bindingChains[0].SetActive(true);
                        bindingChains[1].SetActive(true);
                        JGBossAudioManager.Instance.SummonBindChain();
                    }
                }
            }
            else if (sceneName.Contains("BossStage_4"))
            {
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.SpawnLovePlosion();
                        JGBossAudioManager.Instance.LovePlosion();
                        dialogUI.SetActive(false);
                    }
                    else
                    {
                        if (dialogListIndex == 0 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "�ʸ� ���� ���� ���� �� ���� ��ȸ�Ǵ� ��.";
                            menuImages[1].GetComponentInChildren<Text>().text = "�ʿ��� ���� ���� �� �־ �˸� ���� ��ġ�� �ִٰ� ������.";
                        }
                        else if (dialogListIndex == 1 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "�Դٰ�, �̷��� ���� ���� �� ������.";
                            menuImages[1].GetComponentInChildren<Text>().text = "���� �Ʊ� �Ӹ� ��Ÿ�� �����ٰ� �ֱ� �߾���?";
                        }
                        else if (dialogListIndex == 5 && !selectMenu.activeSelf)
                        {
                            selectMenu.SetActive(true);
                            booper.SetActive(false);
                            menuImages[0].GetComponentInChildren<Text>().text = "�װ� ���� �ٸ� �� ������ �ʾ�.";
                            menuImages[1].GetComponentInChildren<Text>().text = "�� �� ����. �̹� �� �Ϸ��� �� ������ �� �ִٴϱ�.";
                        }
                        else
                        {
                            booper.SetActive(true);
                            if (selectMenu.activeSelf)
                            {
                                selectMenu.SetActive(false);
                            }

                            if (success.activeSelf)
                            {
                                success.SetActive(false);
                            }

                            // ����Ʈ�� ���� ���, �̹����� ����
                            NextDialog();

                            if (dialogListIndex == 6)
                            {
                                booper.SetActive(false);
                                success.SetActive(true);
                                JGBossAudioManager.Instance.DialogSuccess();
                            }
                        }
                        // �ؽ�Ʈ ��� �Ϸ� ����
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // ���� �ؽ�Ʈ�� ����
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // �ؽ�Ʈ ��� �Ϸ� ����
                    JGBossAudioManager.Instance.TextEnd();
                }
            }
            else
            {
                // �ؽ�Ʈ �ε����� ����� ���̺��� ũ�ٸ�
                if (dialogListTextIndex + 1 >= dialogList[dialogListIndex].texts.Length)
                {
                    // ���̾�α� ����Ʈ�� ���̻� ���ٸ� ���� �������� ����
                    if (dialogListIndex + 1 >= dialogList.Length)
                    {
                        JGBossGameManager.Instance.NextStage();
                    }
                    else
                    {
                        // ����Ʈ�� ���� ���, �̹����� ����
                        NextDialog();
                        // �ؽ�Ʈ ��� �Ϸ� ����
                        JGBossAudioManager.Instance.TextEnd();
                    }
                }
                else
                {
                    // ���� �ؽ�Ʈ�� ����
                    dialog.gameObject.SetActive(false);
                    dialogListTextIndex++;
                    dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
                    dialog.gameObject.SetActive(true);
                    // �ؽ�Ʈ ��� �Ϸ� ����
                    JGBossAudioManager.Instance.TextEnd();
                }
            }
        }
    }

    private void SelecetMenu()
    {
        if (!selectMenu.activeSelf || selectMenu == null) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            menuImages[0].color = hightLight;
            menuImages[1].color = idle;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            menuImages[0].color = idle;
            menuImages[1].color = hightLight;
        }
    }

    /// <summary>
    /// ���̾�α� ����Ʈ �������� ����
    /// </summary>
    private void NextDialog()
    {
        // �ؽ�Ʈ �ε��� 0���� �ʱ�ȭ
        dialogListTextIndex = 0;
        // ����Ʈ �ε��� ����
        dialogListIndex++;
        // �Ǹ� �̹��� ����
        demonImage.sprite = dialogList[dialogListIndex].changesprite;
        // �Ǹ� �̹��� ������ �ڵ� ����
        demonImage.SetNativeSize();
        // �̸� ����
        demonName.text = dialogList[dialogListIndex].name;
        // ��� ����
        dialog.text = dialogList[dialogListIndex].texts[dialogListTextIndex];
        // ��� �ؽ�Ʈ ����
        dialog.gameObject.SetActive(false);
        dialog.gameObject.SetActive(true);
        // �̹��� �ٽ� �̵�
        StartCoroutine(DemonImageMove(new Vector3(1500, 202), new Vector3(0, 202), imageMoveTime));
    }

    private IEnumerator DialogStartCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        // �� ��� ���� ��ġ, ��ǥ ��ġ ����
        Vector3 up_Target = new Vector3(0, 810);
        Vector3 up_Cur = move_Up.localPosition;
        Vector3 down_Target = new Vector3(0, -810);
        Vector3 down_Cur = move_Down.localPosition;

        // �� ��� ��ǥ ��ġ�� �̵�
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / backMoveTime;
            // up : 810, down : -810
            move_Up.localPosition = Vector3.Lerp(up_Cur, up_Target, percent);
            move_Down.localPosition = Vector3.Lerp(down_Cur, down_Target, percent);
        }

        move_Up.localPosition = up_Target;
        move_Down.localPosition = down_Target;

        // �̹��� ���� ��ġ, ��ǥ ��ġ ����
        Vector3 image_Target = new Vector3(0, 202);
        Vector3 image_Cur = demonTransform.localPosition;

        if (!SceneManager.GetActiveScene().name.Contains("BossStage_3"))
        {
            demonName.text = dialogList[0].name;
            dialog.text = dialogList[0].texts[dialogListTextIndex];
            demonImage.sprite = dialogList[0].changesprite;
            demonImage.SetNativeSize();
        }

        // �̹��� �̵�
        yield return StartCoroutine(DemonImageMove(image_Cur, image_Target, imageMoveTime));

        if (SceneManager.GetActiveScene().name.Contains("BossStage_3"))
        {
            Color color = demonImage.color;
            color.a = 0;
            demonImage.color = color;
        }

        // ���̾�α� ��� ���� ���
        JGBossAudioManager.Instance.StartDialog();
        // �Ǹ� �̸�, ���̾�α� �ؽ�Ʈ, ���� Ȱ��ȭ
        demonName.gameObject.SetActive(true);
        dialog.gameObject.SetActive(true);
        booper.SetActive(true);
    }

    private IEnumerator StageThereDialogStartcoroutine()
    {
        yield return null;
    }

    private IEnumerator DemonImageMove(Vector3 curPos, Vector3 targetPos, float moveTime)
    {
        float curTime = 0;
        float percent = 0;
        Color color = demonImage.color;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            demonTransform.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(0, 1, colorAlpha.Evaluate(percent));
            demonImage.color = color;
            if (demonImageAnimation != null)
            {
                demonImageAnimation.GetComponent<RectTransform>().localPosition = Vector3.Lerp(curPos, targetPos, percent);
                demonImageAnimation.GetComponent<Image>().color = color;
            }
        }

        demonTransform.localPosition = targetPos;
        color.a = 1;
        demonImage.color = color;
        if (demonImageAnimation != null)
        {
            demonImageAnimation.GetComponent<RectTransform>().localPosition = targetPos;
            demonImageAnimation.GetComponent<Image>().color = color;
            demonArm.SetActive(true);
        }
    }

}
