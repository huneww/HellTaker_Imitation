using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JGBossGameManager : MonoBehaviour
{
    // �̱���
    private static JGBossGameManager instance;
    public static JGBossGameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    // ����� �Ŵ��� �̱���
    private static JGBossAudioManager audioInstance;

    public bool dontMove = false;

    [Space(10), Header("BackGround")]
    // �ǽ��� �ִϸ�����
    [SerializeField]
    private Animator[] up_Pistons;
    [SerializeField]
    private Animator[] down_Pistons;
    // �ٸ� �۵� ���� Ȯ�� ����
    [SerializeField]
    private bool isMove = false;
    public bool IsMove
    {
        get
        {
            return isMove;
        }
        set
        {
            isMove = value;
        }
    }
    // �÷��̾�, ������ũ ���� �ö󰡴� �ӵ�
    [SerializeField]
    private float moveUpSpeed = 0.5f;
    public float MoveUpSpeed
    {
        get
        {
            return moveUpSpeed;
        }
    }
    // �������� ǥ���ϴ� �� �ִϸ�����
    [SerializeField]
    private Animator stageWheel;
    // �ٸ� ������ �Ҹ�Ǵ� �ð�
    [SerializeField]
    private float stopMoveTime = 1f;
    public float StopMoveTime
    {
        get
        {
            return stopMoveTime;
        }
    }

    [Space(10), Header("Effect")]
    // ���� ����Ʈ
    [SerializeField]
    private GameObject dust;
    [SerializeField]
    private GameObject landingDust;
    // ���� ����Ʈ
    [SerializeField]
    private GameObject blood;
    // ��Ʈ ����Ʈ
    [SerializeField]
    private GameObject hit;
    // ���� ����Ʈ
    [SerializeField]
    private GameObject lovePlosion;

    [Space(10), Header("Health")]
    // ���� ü��
    [SerializeField]
    private int curHealth = 4;
    // ü�� ǥ�� �Ҳ� �ǳ�
    [SerializeField]
    private Image[] healthSinImages;
    // ü�� ���ҽ� �ǳ� ���� ��������Ʈ
    [SerializeField]
    private Sprite emptyhHealthSprite;
    // ���� ü�� ǥ�� �Ҳ�
    [SerializeField]
    private GameObject[] fires;

    [Space(10), Header("Dead")]
    // ������ ������ ����Ʈ
    [SerializeField]
    private GameObject deadParticle;
    // ����Ʈ �޹��
    [SerializeField]
    private GameObject deadBackGround;
    // ���� �׾��ִ��� Ȯ�� ����
    private bool isDead = false;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }

    // �÷��̾� ������Ʈ
    private GameObject player;
    public GameObject Player
    {
        get
        {
            return player;
        }
    }

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        // �÷��̾� ������Ʈ ȹ��
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator Start()
    {
        if (dontMove) yield break;

        // ����� �ν��Ͻ� �� ����
        audioInstance = JGBossAudioManager.Instance;

        // 0.5�� ���
        yield return new WaitForSeconds(1f);

        // �ǽ����� �������� �ִϸ��̼� ����
        foreach (var piston in up_Pistons)
            piston.SetTrigger("Down");
        // ��� �۵��� ���
        audioInstance.MachinStart();
        // ���� ����� ���
        audioInstance.MainAudioStart();

        yield return new WaitForSeconds(1.15f);

        // �ٸ� �۵�
        isMove = true;

        // �� ȸ��
        if (SceneManager.GetActiveScene().name.Contains("BossStage_1"))
            stageWheel.SetTrigger("One");
        else if (SceneManager.GetActiveScene().name.Contains("BossStage_2"))
            stageWheel.SetTrigger("Two");
        else if (SceneManager.GetActiveScene().name.Contains("BossStage_3"))
            stageWheel.SetTrigger("There");
        else if (SceneManager.GetActiveScene().name.Contains("BossStage_4"))
            stageWheel.SetTrigger("Four");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StageChange(true, true);
        }
        if (dontMove && Input.GetKeyDown(KeyCode.Space))
        {
            JGBossDialogManager.Instance.StartDialog();
        }
    }

    /// <summary>
    /// ���� ����Ʈ ��ȯ
    /// </summary>
    /// <param name="spawnPos">��ȯ ��ġ</param>
    public void SpawnDust(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(dust, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// ���� ����Ʈ ��ȯ
    /// </summary>
    /// <param name="spawnPos">��ȯ ��ġ</param>
    public void SpawnBlood(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(blood, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// ��Ʈ ����Ʈ ��ȯ
    /// </summary>
    /// <param name="spawnPos">��ȯ ��ġ</param>
    public void SpawnHit(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(hit, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// ������Ʈ ���� ���� ����Ʈ ��ȯ
    /// </summary>
    /// <param name="spawnPos">��ȯ ��ġ</param>
    /// <param name="parent">������Ʈ Ʈ������</param>
    public void SpawnLandingDust(Vector3 spawnPos, Transform parent)
    {
        GameObject clone = Instantiate(landingDust, spawnPos, Quaternion.identity);
        clone.transform.SetParent(parent);
    }

    /// <summary>
    /// �÷��̾� �÷� ü����
    /// </summary>
    public void PlayerColorChange()
    {
        StartCoroutine(PlayerColorChangeCoroutine());
    }

    /// <summary>
    /// ü�� ����
    /// </summary>
    public void HealthMinus()
    {
        curHealth--;
        if (curHealth <= 0)
        {
            PlayerDead();
            return;
        }
        healthSinImages[3 - curHealth].sprite = emptyhHealthSprite;
        Destroy(fires[3 - curHealth]);
    }

    /// <summary>
    /// �÷��̾� ��� ������
    /// </summary>
    public void PlayerDead()
    {
        if (isDead) return;

        Debug.Log("Player Dead");
        // �÷��̾� ��� ����Ʈ, ��� ��ȯ
        Instantiate(deadParticle, player.transform.position, Quaternion.identity);
        Instantiate(deadBackGround, this.transform.position, Quaternion.identity);
        // �÷��̾� ������·� ����
        isDead = true;
        // ��� ����Ʈ ���� ���
        JGBossAudioManager.Instance.PlayerDead();
        // ���� ���� ����
        JGBossAudioManager.Instance.MainAudioStop();
        // �������� �����
        StartCoroutine(StageChange(true));
    }

    public void NextStage()
    {
        StartCoroutine(StageChange(false));
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="isRestart">�ٽ� �����ϴ��� ����</param>
    /// <param name="isPreesR">RŰ�� ������ ������ϴ��� ����</param>
    /// <returns></returns>
    private IEnumerator StageChange(bool isRestart, bool isPreesR = false)
    {
        // ���� �� �̸� ȹ��
        string name = SceneManager.GetActiveScene().name;

        // RŰ�� ������ �ƴ϶�� �����ð� ���
        if (!isPreesR) yield return new WaitForSeconds(1.25f);

        // ���� �������� �����
        if (isRestart)
        {
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        else
        {
            // ���� �������� ����
            switch(name)
            {
                case "BossStage_1":
                    SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_2");
                    break;
                case "BossStage_2":
                    SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_3");
                    break;
                case "BossStage_3":
                    SceneChangeDoor.Instance.PlayCloseAnimation("BossStage_4");
                    break;
                case "BossStage_4":
                    SceneChangeDoor.Instance.PlayCloseAnimation("MainMenu");
                    break;
            }
        }
    }

    /// <summary>
    /// �÷��̾� �÷� ƾƮ
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerColorChangeCoroutine()
    {
        // �÷��̾� ��������Ʈ ������ ������Ʈ ȹ��
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        // ���� �÷��̾��� ������ �������̸� �ڷ�ƾ ����
        if (playerSprite.color == Color.red) yield break;

        // ���� ���� ����
        Color color = playerSprite.color;
        // �÷��̾� ���� ����
        playerSprite.color = Color.red;
        // ���� �ð� ���
        yield return new WaitForSeconds(0.25f);
        // ���� �������� ����
        playerSprite.color = color;
    }

    /// <summary>
    /// �ٸ� ����
    /// </summary>
    public void BridgeStop()
    {
        // �ٸ� ���� �޼��� ����
        BridgeController.stop();
        // ���� ����� ����
        audioInstance.MachinStop();
        // �ǽ��� �ִϸ��̼� ����
        foreach (var piston in down_Pistons)
            piston.SetTrigger("Up");
    }

    /// <summary>
    /// ü�� ����ũ
    /// </summary>
    /// <param name="obj">��� ������Ʈ</param>
    /// <param name="magnitued">����</param>
    /// <param name="duration">���ӽð�</param>
    /// <param name="isLeft">���� ü������ Ȯ�� ����</param>
    /// <returns></returns>
    public IEnumerator ChainShake(GameObject obj, float magnitued = 1f, float duration = 1f, bool isLeft = false)
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = Vector3.zero;

        // ���� ü���̶��
        if (isLeft)
            // ���� ü�� ��ȯ ��ġ ����
            curPos = ChainSpanwer.Instance.SpawnPos_L;
        else
            // ������ ü�� ��ȯ ��ġ ����
            curPos = ChainSpanwer.Instance.SpawnPos_R;

        // ����ũ
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / duration;
            obj.transform.position = new Vector3(curPos.x + Random.Range(-magnitued, magnitued), curPos.y + Random.Range(-magnitued, magnitued));
        }

        // ���� ��ġ�� ��ġ ����
        obj.transform.position = curPos;

    }

    public void SpawnLovePlosion()
    {
        GameObject clone = Instantiate(lovePlosion, new Vector3(0.53f, 4.1f), Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

}
