using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StageDemon
{
    Pandemonica = 1,
    Modeus,
    Cerberus,
    Malina,
    Zdrda,
    Azazel,
    Justice,
    Lucifer,
    Judgement
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // �÷��̾�, ������Ʈ �̵� ������ ��
    [SerializeField]
    private float moveXOffset = 1f;
    public float MoveXOffset
    {
        get
        {
            return moveXOffset;
        }
    }
    [SerializeField]
    private float moveYOffset = 1f;
    public float MoveYOffset
    {
        get
        {
            return moveYOffset;
        }
    }

    // �÷��̾� ������Ʈ
    private GameObject player;
    // �÷��̾ �׾����� Ȯ�� ����
    private bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    // �÷��̾ ���踦 ������ �ִ��� Ȯ�� ����
    private bool haveKey;
    public bool HaveKey
    {
        get
        {
            return haveKey;
        }
    }
    // ���� ���������� �Ǹ� ������Ʈ
    private GameObject demon;
    // ���� ��������
    private int curStage;
    public int CurStage
    {
        get
        {
            return curStage;
        }
    }

    [Space(10), Header("VFX")]
    // �̵��� ���� ����Ʈ
    [SerializeField]
    private GameObject dust;
    // ������Ʈ ��Ʈ ����Ʈ
    [SerializeField]
    private GameObject hit;
    // ���̷��� ������ ������ �� ��ƼŬ
    [SerializeField]
    private GameObject bornParticle;
    // �÷��̾� ������ ������ ������ �� ��ƼŬ
    [SerializeField]
    private GameObject blood;
    // é�� Ŭ����� ������ ��Ʈ ��ƼŬ
    [SerializeField]
    private GameObject lovePlosion;
    // ��� �ڽ� ������ ������ ��ƼŬ
    [SerializeField]
    private GameObject unLock;

    [Space(10), Header("UI")]
    // é��, �̵�Ƚ�� UI �θ� ������Ʈ
    [SerializeField]
    private GameObject uiCanvas;
    // �̵�Ƚ�� �ؽ�Ʈ
    [SerializeField]
    private Text footCountText;
    // ���� ���� �̵�Ƚ��
    [SerializeField]
    private int curFootCount;
    // ���� �������� �ؽ�Ʈ
    [SerializeField]
    private Text chapterText;

    [Space(10), Header("Dead")]
    // �÷��̾� ����� ������ �ִϸ��̼�
    [SerializeField]
    private GameObject deadAnimation;
    // ����� ������ �ִϸ��̼� ���
    [SerializeField]
    private GameObject deadBackGround;

    [Space(10), Header("Dialog")]
    // ���� ���̾�α׿� �ִ��� Ȯ�� ����
    [SerializeField]
    private bool isDialog = false;
    public bool IsDialog
    {
        get
        {
            return isDialog;
        }
        set
        {
            isDialog = value;
        }
    }
    // ���̾�α� ĵ���� ������Ʈ
    [SerializeField]
    private GameObject dialogCanvas;
    // ������ ĵ���� ������Ʈ
    [SerializeField]
    private GameObject selectMenu;
    // ���� �������� �ִ��� Ȯ�� ����
    [SerializeField]
    private bool isSelect = false;
    public bool IsSelect
    {
        get
        {
            return isSelect;
        }
        set
        {
            isSelect = value;
        }
    }
    // �߸��� ������ ���� ������ �ִϸ��̼�
    [SerializeField]
    private GameObject badEndScene;
    // ����� �������� �ι�° �������� �߸� ���� ������ �ִϸ��̼�
    [SerializeField]
    private GameObject secondBadEndScene;

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        // �÷��̾�, �Ǹ� ������Ʈ ȹ��
        player = GameObject.FindGameObjectWithTag("Player");
        demon = GameObject.FindGameObjectWithTag("Demon");
        // �̵�Ƚ��, é�� UI �ʱ�ȭ
        ChapterUITextReset();
    }

    private void Update()
    {
        // RŰ�� ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            // ���� �������� �ٽ� �ε�
            StartCoroutine(StageChange(true, true));
        }

        // ���� ���尡 �����ִ� �����ϰ��
        if (badEndScene.activeSelf)
        {
            // ����, RŰ�� ������ é�� �ٽ� �ε�
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R))
            {
                // 0�� 1é���̱⶧���� 8�� 9é����
                // 9é�Ϳ����� ���� é�ͷ� �̵�
                if (curStage == 8)
                    StartCoroutine(StageChange(false));
                // ���� é�� �ٽ� �ε�
                else
                    StartCoroutine(StageChange(true));
            }
        }
    }

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    /// <param name="spawnPos">���� ��ġ</param>
    public void SpawnDust(Vector3 spawnPos)
    {
        // ���� ����Ʈ ����
        Instantiate(dust, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// ��Ʈ ����Ʈ ����
    /// </summary>
    /// <param name="spawnPos">���� ��ġ</param>
    public void SpawnHit(Vector3 spawnPos)
    {
        // ������ �������� ����
        Quaternion quaternion = Quaternion.Euler(0, 0, Random.Range(0, 361));
        // ���� �������� ��Ʈ ����Ʈ ����
        Instantiate(hit, spawnPos, quaternion);
    }

    /// <summary>
    /// �� ����Ʈ ����
    /// </summary>
    /// <param name="spawnPos">���� ��ġ</param>
    public void SpawnBornParticle(Vector3 spawnPos)
    {
        // �� ����Ʈ ����
        GameObject clone = Instantiate(bornParticle, spawnPos, Quaternion.identity);
        // 3���� �� ����Ʈ ����
        Destroy(clone, 3.0f);
    }

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    /// <param name="spawnPos">���� ��ġ</param>
    public void SpawnBlood(Vector3 spawnPos)
    {
        // ���� ����Ʈ ����
        Instantiate(blood, spawnPos, Quaternion.identity);
        // ���� ����Ʈ�� Ʈ�������ؼ��� ȣ���
        // ������ũ ���� ���
        AudioManager.Instance.SpikeHit();
        // �̵�Ƚ�� 1ȸ �߰� ���̳ʽ�
        FootCountMinus(true);
        // �÷��̾� ���� ���� �ڷ�ƾ ����
        StartCoroutine(PlayerColorChange());
    }

    /// <summary>
    /// ���� é�Ϳ� �°� �̵�Ƚ��, Ƚ�� �ؽ�Ʈ, é��, é�� �ؽ�Ʈ ����
    /// </summary>
    private void ChapterUITextReset()
    {
        // ���� �������� �� �̸� ȹ��
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        // ���� �������� ���� �°� �ʱ�ȭ
        switch (scene.name)
        {
            case "ChapterOne":
                chapterText.text = "��";
                curFootCount = 23;
                curStage = 0;
                break;
            case "ChapterTwo":
                chapterText.text = "��";
                curFootCount = 24;
                curStage = 1;
                break;
            case "ChapterThree":
                chapterText.text = "��";
                curFootCount = 32;
                curStage = 2;
                break;
            case "ChapterFour":
                chapterText.text = "��";
                curFootCount = 23;
                curStage = 3;
                break;
            case "ChapterFive":
                chapterText.text = "��";
                curFootCount = 23;
                curStage = 4;
                break;
            case "ChapterSix":
                chapterText.text = "��";
                curFootCount = 43;
                curStage = 5;
                break;
            case "ChapterSeven":
                chapterText.text = "��";
                curFootCount = 32;
                curStage = 6;
                break;
            case "ChapterEight":
                chapterText.text = "��";
                curFootCount = 12;
                curStage = 7;
                break;
            case "ChapterNine":
                chapterText.text = "��";
                curFootCount = 33;
                curStage = 8;
                break;
        }
        footCountText.text = curFootCount.ToString();
    }

    /// <summary>
    /// �̵� Ƚ�� ����
    /// </summary>
    /// <param name="isSpike">������ũ�� ���� ���� �ƴ��� Ȯ�� ����</param>
    public void FootCountMinus(bool isSpike = false)
    {
        // �̵� Ƚ���� 0�ϴ� �����Ͻ�
        if (curFootCount < 1 && !isDead)
        {
            Debug.Log("Player Dead");
            // ���� ���·� ���� ��ȭ
            isDead = true;
            // �÷��̾� �״� ���� ���
            AudioManager.Instance.PlayerDead();
            // �̵�Ƚ��, é�� UI ��Ȱ��ȭ
            uiCanvas.SetActive(false);
            // �÷��̾� ������ ������ �ִϸ��̼�, �� ��� ����
            Instantiate(deadBackGround, Vector3.zero, Quaternion.identity);
            Instantiate(deadAnimation, player.transform.position, Quaternion.identity);
            // é�� �ٽ� �ε�
            StartCoroutine(StageChange(true));
            return;
        }

        // �̵�Ƚ�� ����
        curFootCount--;
        Debug.Log("FootMinus");
        // �̵�Ƚ�� �ؽ�Ʈ ����
        footCountText.text = curFootCount.ToString();
        // �ؽ�Ʈ ������ ���� �ؽ�Ʈ ��Ȱ��ȭ �� �ٷ� Ȱ��ȭ
        footCountText.gameObject.SetActive(false);
        footCountText.gameObject.SetActive(true);

        // ������ũ�� ���ؼ� ����Ǹ�
        if (isSpike)
        {
            // �̵�Ƚ�� �ؽ�Ʈ ���� ���� �ڷ�ƾ ����
            StartCoroutine(FootTextColor());
            // ī�޶� ����
            CameraShake.cameraShake();
        }

        // ���� �̵� ���� Ƚ���� 0�̶��
        // �ؽ�Ʈ 0�� �ƴ� X�� ����
        if (curFootCount == 0)
            footCountText.text = "X";
    }

    /// <summary>
    /// �̵� ���� Ƚ�� �ؽ�Ʈ ���� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator FootTextColor()
    {
        // �⺻ ���� ����
        Color color = footCountText.color;
        // ���������� ����
        footCountText.color = Color.red;
        // 0.1�� ��� ��
        yield return new WaitForSeconds(0.1f);
        // �����·� ����
        footCountText.color = color;
    }

    /// <summary>
    /// �÷��̾ ������ ����
    /// </summary>
    public void Goal()
    {
        // ���� ���� ���¶�� �޼��� ����
        if (isDead) return;

        // é��, �̵�Ƚ�� UI ��Ȱ��ȭ
        uiCanvas.SetActive(false);
        // ���̾�α� UI Ȱ��ȭ
        dialogCanvas.SetActive(true);
        // ���� ���̾�α׿� �ִ� ���·� ��ȯ
        isDialog = true;
    }

    /// <summary>
    /// ���������� ���忣�� ���ý�
    /// </summary>
    public void BadEnd()
    {
        // ���̾�α� UI ��Ȱ��ȭ
        dialogCanvas.SetActive(false);
        // ���� ���� UI Ȱ��ȭ
        badEndScene.SetActive(true);
        // ����� ����
        AudioManager.Instance.MainAudioStop();
    }

    /// <summary>
    /// ����� ������������ �ι�° ���������� ���忣�� ���ý�
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    public void BadSecondEnd()
    {
        // �ι��� �������� ���ٸ�
        if (secondBadEndScene == null) throw new System.Exception("Second Bad End Scene is Null");

        // ���̾�α� UI ��Ȱ��ȭ
        dialogCanvas.SetActive(false);
        // �ι�° ���� ���� UI Ȱ��ȭ
        secondBadEndScene.SetActive(true);
        // ����� ����
        AudioManager.Instance.MainAudioStop();
    }

    /// <summary>
    /// ���� ���� ���ý�
    /// </summary>
    public void GoodEnd()
    {
        // �÷��̾� Ŭ���� �ִϸ��̼� ����
        player.GetComponent<Animator>().SetTrigger("Clear");
        // ���� ���� ���
        AudioManager.Instance.SuccesSound();
        // ������ ������ ��ƼŬ, �ִϸ��̼� ����
        Instantiate(lovePlosion, demon.transform.position, Quaternion.identity);
    }

    ///// <summary>
    ///// �����̸� �ְ� �������� �����
    ///// </summary>
    ///// <param name="delay">������ �ð�</param>
    ///// <returns></returns>
    //private IEnumerator DelayStageChange(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    StartCoroutine(StageChange(false));
    //}

    /// <summary>
    /// é�� ���� �ڷ�ƾ
    /// </summary>
    /// <param name="isRestart">��������� Ȯ�� ����</param>
    /// <param name="isPressR">RŰ�� ������ ����Ǵ� Ȯ�� ����</param>
    /// <returns></returns>
    public IEnumerator StageChange(bool isRestart, bool isPressR = false)
    {
        // ���� �������� �� �̸� Ȯ��
        Scene scene = SceneManager.GetActiveScene();
        string name = scene.name;
        // ����� ����
        AudioManager.Instance.MainAudioStop();

        // ������̶��
        if (isRestart)
        {
            // RŰ�� ������ �ƴ϶��
            if (!isPressR)
                // 1�� ����� ����
                yield return new WaitForSeconds(1.0f);
            Debug.Log("ReStart Stage");
            // ���� �� �ٽ� �ε�
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        // ���� é�ͷ� �̵�
        else
        {
            Debug.Log("Next Stage");
            // ���� ���� ���� ���̸��� �°� ���� é�� ����
            switch(name)
            {
                case "ChapterOne":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterTwo");
                    break;
                case "ChapterTwo":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterThree");
                    break;
                case "ChapterThree":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterFour");
                    break;
                case "ChapterFour":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterFive");
                    break;
                case "ChapterFive":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterSix");
                    break;
                case "ChapterSix":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterSeven");
                    break;
                case "ChapterSeven":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterEight");
                    break;
                case "ChapterEight":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterNine");
                    break;
                case "ChapterNine":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterTen");
                    break;
            }
        }

        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// �÷��̾� �÷� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerColorChange()
    {
        // ���� ���� ����
        Color color = player.GetComponent<SpriteRenderer>().color;
        // ���������� ����
        player.GetComponent<SpriteRenderer>().color = Color.red;
        // 0.25�� ��� ��
        yield return new WaitForSeconds(0.25f);
        // ���� �������� ����
        player.GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// ���� ȹ��
    /// </summary>
    /// <param name="key">���� ������Ʈ</param>
    public void GetKey(GameObject key)
    {
        // ���踦 ������ �ִ� ���·� ����
        haveKey = true;
        // ���� ȹ��, ��� ����Ʈ�� ����
        // ���� ȹ�� ����Ʈ ����
        Instantiate(unLock, key.transform.position, Quaternion.identity);
        // ���� ȹ�� ���� ���
        AudioManager.Instance.GetKey();
        // ���� ����
        Destroy(key);
    }

    /// <summary>
    /// ��� �� ����
    /// </summary>
    /// <param name="box">��� ��</param>
    public void UnLockBox(GameObject box)
    {
        // ���� �Һ�
        haveKey = false;
        // ��� ����Ʈ ����
        Instantiate(unLock, box.transform.position, Quaternion.identity);
        // �� ������ ���� ���
        AudioManager.Instance.DoorOpen();
        // �� ����
        Destroy(box);
    }

}
