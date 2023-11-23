using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChainSpanwer : MonoBehaviour
{
    private static ChainSpanwer instance;
    public static ChainSpanwer Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [Space(10), Header("Attack Chain")]
    [SerializeField]
    private GameObject attackChain;
    [SerializeField]
    private GameObject attackChain_Horizontal;
    [SerializeField]
    private Vector2[] verticalSpawnPos;
    [SerializeField]
    private Vector2[] horizontalSpawnPos;
    [SerializeField]
    private Queue<GameObject> chainPool;

    private WaitForSeconds delayTime = new WaitForSeconds(0.65f);
    private Coroutine spawnChainCroutine;
    private Coroutine defensChainCoroutine;

    [Space(10), Header("Defend Chain")]
    [SerializeField]
    private GameObject[] defendChains;
    private Vector3 spawnPos_L;
    private Vector3 spawnPos_R;
    public Vector3 SpawnPos_L
    {
        get
        {
            return spawnPos_L;
        }
    }
    public Vector3 SpawnPos_R
    {
        get
        {
            return spawnPos_R;
        }
    }

    [SerializeField]
    private uint chain_L_Health = 5;
    public uint Chain_L_Health
    {
        get
        {
            return chain_L_Health;
        }
        set
        {
            chain_L_Health = value;
        }
    }
    [SerializeField]
    private uint chain_R_Health = 5;
    public uint Chain_R_Health
    {
        get
        {
            return chain_R_Health;
        }
        set
        {
            chain_R_Health = value;
        }
    }

    [Space(10), Header("Health UI")]
    [SerializeField]
    private GameObject healthUI;
    [SerializeField]
    private Slider healthSlider;

    [Space(10), Header("Judgement")]
    [SerializeField]
    private GameObject judgement;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private IEnumerator Start()
    {
        if (JGBossGameManager.Instance.dontMove) yield break;

        spawnPos_R = defendChains[0].transform.position;
        spawnPos_L = defendChains[1].transform.position;

        // ���� Ȱ��ȭ�� �̸� ����
        string name = SceneManager.GetActiveScene().name;

        // �����̴� �ƽ� ���� ����
        healthSlider.maxValue = chain_L_Health + chain_R_Health;
        // �����̴� ��� �ƽ��� ����
        healthSlider.value = healthSlider.maxValue;

        yield return new WaitForSeconds(3f);

        switch (name)
        {
            case "BossStage_1":
                spawnChainCroutine = StartCoroutine(StageOne());
                break;
            case "BossStage_2":
                spawnChainCroutine = StartCoroutine(StageTwo());
                break;
            case "BossStage_3":
                spawnChainCroutine = StartCoroutine(StageThere());
                break;
            case "BossStage_4":
                spawnChainCroutine = StartCoroutine(StageFour());
                break;
        }
    }

    private IEnumerator StageOne()
    {

        // ���ʿ��� ���������� ���ʴ�� ����
        foreach (var pos in verticalSpawnPos)
        {
            Instantiate(attackChain, pos, Quaternion.identity);
            yield return delayTime;
        }

        // �����ʿ��� �������� ���ʴ�� ����
        for (int i = verticalSpawnPos.Length - 2; i >= 0; i--)
        {
            Instantiate(attackChain, verticalSpawnPos[i], Quaternion.identity);
            yield return delayTime;
        }

        // ���ڷ� ����
        int cur = 0;
        for (int i = 0; i < 3;)
        {
            int next = (cur + 2) % verticalSpawnPos.Length;
            Instantiate(attackChain, verticalSpawnPos[cur], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[next], Quaternion.identity);
            cur = (next + 2) % verticalSpawnPos.Length;
            i++;
            yield return delayTime;
        }

        // �۾��� ���� �������� 2���� ����
        for (int i = 1; i <= verticalSpawnPos.Length / 2; i++)
        {
            Instantiate(attackChain, verticalSpawnPos[i - 1], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[verticalSpawnPos.Length - i], Quaternion.identity);
            yield return delayTime;
        }

        // ���� ���� �۾������� 2���� ����
        for (int i = verticalSpawnPos.Length / 2 - 1; i > 0; i--)
        {
            Instantiate(attackChain, verticalSpawnPos[i - 1], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[verticalSpawnPos.Length - i], Quaternion.identity);
            yield return delayTime;
        }

        // ���ʿ��� +2�� 3�� ���� 2��
        for (int i = 0; i < 2; i++)
        {
            int index = i;
            Instantiate(attackChain, verticalSpawnPos[index], Quaternion.identity);
            index += 2;
            Instantiate(attackChain, verticalSpawnPos[index], Quaternion.identity);
            index += 2;
            Instantiate(attackChain, verticalSpawnPos[index], Quaternion.identity);
            yield return delayTime;
        }

        // ���� ����� �ٸ� ����
        JGBossGameManager.Instance.BridgeStop();

        // �����ð� ���
        yield return new WaitForSeconds(JGBossGameManager.Instance.StopMoveTime - 0.15f);

        // �����ִ� ü�� ���� ����
        JGBossAudioManager.Instance.DefendChainEnable();
        // �����ִ� ü�� Ȱ��ȭ
        defendChains[0].SetActive(true);
        defendChains[1].SetActive(true);
        // ü�� ü�� UI Ȱ��ȭ
        healthUI.SetActive(true);
    }

    private IEnumerator StageTwo()
    {

        // *-**-*
        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        // -*--*-
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        // �ؿ��� ���� 2ĭ�� �پ� ��ȯ
        for (int i = 0; i < horizontalSpawnPos.Length; i += 2)
        {
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[i], attackChain_Horizontal.transform.rotation);

            yield return delayTime;
        }

        Instantiate(attackChain_Horizontal, horizontalSpawnPos[1], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain_Horizontal, horizontalSpawnPos[3], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        // **--**
        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        // --**--
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);

        yield return delayTime;

        // **--**
        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        // *-**-*
        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        // -*--*-
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        for (int i = 0; i < horizontalSpawnPos.Length; i++)
        {
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[i], attackChain_Horizontal.transform.rotation);
            yield return delayTime;
        }

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        // ���� ����� �ٸ� ����
        JGBossGameManager.Instance.BridgeStop();

        defensChainCoroutine = StartCoroutine(StageTwo_Defens());

        // �����ð� ���
        yield return new WaitForSeconds(JGBossGameManager.Instance.StopMoveTime - 0.15f);

        // �����ִ� ü�� ���� ����
        JGBossAudioManager.Instance.DefendChainEnable();
        // �����ִ� ü�� Ȱ��ȭ
        defendChains[0].SetActive(true);
        defendChains[1].SetActive(true);
        // ü�� ü�� UI Ȱ��ȭ
        healthUI.SetActive(true);
    }

    private IEnumerator StageTwo_Defens()
    {
        while (true)
        {
            Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

            yield return delayTime;

            Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

            yield return delayTime;

            Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);

            yield return delayTime;
        }
    }

    private IEnumerator StageThere()
    {
        // --**--
        // -*--*-
        // *----*
        for (int i = verticalSpawnPos.Length / 2 - 1; i >= 0; i--)
        {
            Instantiate(attackChain, verticalSpawnPos[i], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[verticalSpawnPos.Length - i - 1], Quaternion.identity);

            yield return delayTime;
        }

        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[Random.Range(1, 4)], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[2], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain_Horizontal, horizontalSpawnPos[2], attackChain_Horizontal.transform.rotation);

        yield return new WaitForSeconds(0.25f);

        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        // ���� ����� �ٸ� ����
        JGBossGameManager.Instance.BridgeStop();

        defensChainCoroutine = StartCoroutine(StageThere_Defens());

        // �����ð� ���
        yield return new WaitForSeconds(JGBossGameManager.Instance.StopMoveTime - 0.15f);

        // �����ִ� ü�� ���� ����
        JGBossAudioManager.Instance.DefendChainEnable();
        // �����ִ� ü�� Ȱ��ȭ
        defendChains[0].SetActive(true);
        defendChains[1].SetActive(true);
        // ü�� ü�� UI Ȱ��ȭ
        healthUI.SetActive(true);

    }

    private IEnumerator StageThere_Defens()
    {
        while (true)
        {
            Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);

            yield return delayTime;

            Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);

            yield return delayTime;

            Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);

            yield return delayTime;

            Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

            yield return delayTime;

        }
    }

    private IEnumerator StageFour()
    {
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[2], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[1], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[3], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        for (int i = 0; i < verticalSpawnPos.Length / 2; i++)
        {
            Instantiate(attackChain, verticalSpawnPos[i], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[verticalSpawnPos.Length - i - 1], Quaternion.identity);

            yield return delayTime;
        }

        for (int i = verticalSpawnPos.Length / 2 - 1; i >= 0; i--)
        {
            Instantiate(attackChain, verticalSpawnPos[i], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[verticalSpawnPos.Length - i - 1], Quaternion.identity);

            yield return delayTime;
        }

        Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[2], attackChain_Horizontal.transform.rotation);

        yield return delayTime;

        Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);

        yield return delayTime;

        Instantiate(attackChain_Horizontal, horizontalSpawnPos[1], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain_Horizontal, horizontalSpawnPos[3], attackChain_Horizontal.transform.rotation);
        Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
        Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

        yield return delayTime;

        // ���� ����� �ٸ� ����
        JGBossGameManager.Instance.BridgeStop();

        defensChainCoroutine = StartCoroutine(StageFour_Defens());

        // �����ð� ���
        yield return new WaitForSeconds(JGBossGameManager.Instance.StopMoveTime - 0.15f);

        // �����ִ� ü�� ���� ����
        JGBossAudioManager.Instance.DefendChainEnable();
        // �����ִ� ü�� Ȱ��ȭ
        defendChains[0].SetActive(true);
        defendChains[1].SetActive(true);
        // ü�� ü�� UI Ȱ��ȭ
        healthUI.SetActive(true);

    }

    private IEnumerator StageFour_Defens()
    {
        while (true)
        {
            Instantiate(attackChain, verticalSpawnPos[0], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[5], Quaternion.identity);

            yield return delayTime;

            Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);

            yield return delayTime;

            Instantiate(attackChain_Horizontal, horizontalSpawnPos[1], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[3], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain, verticalSpawnPos[1], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[4], Quaternion.identity);

            yield return delayTime;

            Instantiate(attackChain_Horizontal, horizontalSpawnPos[0], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain_Horizontal, horizontalSpawnPos[4], attackChain_Horizontal.transform.rotation);
            Instantiate(attackChain, verticalSpawnPos[2], Quaternion.identity);
            Instantiate(attackChain, verticalSpawnPos[3], Quaternion.identity);

            yield return delayTime;

        }
    }

    private void Update()
    {
        // �÷��̾ ������ ���� �ڷ�ƾ ����
        if (JGBossGameManager.Instance.IsDead)
        {
            Debug.Log("Spawn Stop");
            StopCoroutine(spawnChainCroutine);
            StopCoroutine(defensChainCoroutine);
        }

        // ü�� ü�� UI�� Ȱ��ȭ ���¸�
        if (healthUI.activeSelf)
        {
            // �ΰ��� ü�� ü���� �����̴��� �ݿ�
            healthSlider.value = chain_R_Health + chain_L_Health;

            // ü�� ���� �ı���
            if (healthSlider.value <= 0)
            {
                // ��� ���� �ڷ�ƾ ����
                StopCoroutine(defensChainCoroutine);
                judgement.SetActive(true);
                healthUI.SetActive(false);
            }
        }
    }
}   
