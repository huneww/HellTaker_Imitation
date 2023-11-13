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
    private GameObject player;
    private bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    private bool haveKey;
    public bool HaveKey
    {
        get
        {
            return haveKey;
        }
    }
    private GameObject demon;
    private int curStage;
    public int CurStage
    {
        get
        {
            return curStage;
        }
    }

    [Space(10), Header("VFX")]
    [SerializeField]
    private GameObject dust;
    [SerializeField]
    private GameObject hit;
    [SerializeField]
    private GameObject bornParticle;
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private GameObject lovePlosion;
    [SerializeField]
    private GameObject unLock;

    [Space(10), Header("UI")]
    [SerializeField]
    private GameObject uiCanvas;
    [SerializeField]
    private Text footCountText;
    [SerializeField]
    private int curFootCount;
    [SerializeField]
    private Text chapterText;

    [Space(10), Header("Dead")]
    [SerializeField]
    private GameObject deadAnimation;
    [SerializeField]
    private GameObject deadBackGround;

    [Space(10), Header("Dialog")]
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
    [SerializeField]
    private GameObject dialogCanvas;
    [SerializeField]
    private GameObject selectMenu;
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
    [SerializeField]
    private GameObject badEndScene;
    [SerializeField]
    private GameObject secondBadEndScene;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        demon = GameObject.FindGameObjectWithTag("Demon");
        ChapterUITextReset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(StageChange(true, true));
        }

        if (badEndScene.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (curStage == 8)
                    StartCoroutine(StageChange(false));
                else
                    StartCoroutine(StageChange(true));
            }
        }
    }

    public void SpawnDust(Vector3 spawnPos)
    {
        Instantiate(dust, spawnPos, Quaternion.identity);
    }

    public void SpawnHit(Vector3 spawnPos)
    {
        Quaternion quaternion = Quaternion.Euler(0, 0, Random.Range(0, 361));
        Instantiate(hit, spawnPos, quaternion);
    }

    public void SpawnBornParticle(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(bornParticle, spawnPos, Quaternion.identity);
        Destroy(clone, 3.0f);
    }

    public void SpawnBlood(Vector3 spawnPos)
    {
        Instantiate(blood, spawnPos, Quaternion.identity);
        AudioManager.Instance.SpikeHit();
        FootCountMinus(true);
        StartCoroutine(PlayerColorChange());
    }

    private void ChapterUITextReset()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        switch (scene.name)
        {
            case "ChapterOne":
                chapterText.text = "グ";
                curFootCount = 23;
                curStage = 0;
                break;
            case "ChapterTwo":
                chapterText.text = "ケ";
                curFootCount = 24;
                curStage = 1;
                break;
            case "ChapterThree":
                chapterText.text = "ゲ";
                curFootCount = 32;
                curStage = 2;
                break;
            case "ChapterFour":
                chapterText.text = "コ";
                curFootCount = 23;
                curStage = 3;
                break;
            case "ChapterFive":
                chapterText.text = "ゴ";
                curFootCount = 23;
                curStage = 4;
                break;
            case "ChapterSix":
                chapterText.text = "サ";
                curFootCount = 43;
                curStage = 5;
                break;
            case "ChapterSeven":
                chapterText.text = "ザ";
                curFootCount = 32;
                curStage = 6;
                break;
            case "ChapterEight":
                chapterText.text = "シ";
                curFootCount = 12;
                curStage = 7;
                break;
            case "ChapterNine":
                chapterText.text = "ジ";
                curFootCount = 33;
                curStage = 8;
                break;
        }
        footCountText.text = curFootCount.ToString();
    }

    public void FootCountMinus(bool isSpike = false)
    {
        if (curFootCount < 1 && !isDead)
        {
            Debug.Log("Player Dead");
            isDead = true;
            AudioManager.Instance.PlayerDead();
            uiCanvas.SetActive(false);
            Instantiate(deadBackGround, Vector3.zero, Quaternion.identity);
            Instantiate(deadAnimation, player.transform.position, Quaternion.identity);
            StartCoroutine(StageChange(true));
            return;
        }
        curFootCount--;
        Debug.Log("FootMinus");
        footCountText.text = curFootCount.ToString();
        footCountText.gameObject.SetActive(false);
        footCountText.gameObject.SetActive(true);

        if (isSpike)
        {
            StartCoroutine(FootTextColor());
            CameraShake.cameraShake();
        }

        if (curFootCount == 0)
            footCountText.text = "X";
    }

    private IEnumerator FootTextColor()
    {
        footCountText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        footCountText.color = Color.white;
    }

    public void Goal()
    {
        if (isDead) return;
        uiCanvas.SetActive(false);
        dialogCanvas.SetActive(true);
        isDialog = true;
    }

    public void BadEnd()
    {
        dialogCanvas.SetActive(false);
        badEndScene.SetActive(true);
        AudioManager.Instance.MainAudioStop();
    }

    public void BadSecondEnd()
    {
        if (secondBadEndScene == null) throw new System.Exception("Second Bad End Scene is Null");

        dialogCanvas.SetActive(false);
        secondBadEndScene.SetActive(true);
        AudioManager.Instance.MainAudioStop();
    }

    public void GoodEnd()
    {
        player.GetComponent<Animator>().SetTrigger("Clear");
        AudioManager.Instance.SuccesSound();
        Instantiate(lovePlosion, demon.transform.position, Quaternion.identity);
    }

    private IEnumerator DelayStageChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StageChange(false));
    }

    public IEnumerator StageChange(bool isRestart, bool isPressR = false)
    {
        Scene scene = SceneManager.GetActiveScene();
        string name = scene.name;
        AudioManager.Instance.MainAudioStop();
        if (isRestart)
        {
            if (!isPressR)
                yield return new WaitForSeconds(1.0f);
            Debug.Log("ReStart Stage");
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        else
        {
            Debug.Log("Next Stage");
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
                case "ChapterTen":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChapterEX");
                    break;
                case "ChapterEX":
                    SceneChangeDoor.Instance.PlayCloseAnimation("MainMenu");
                    break;
            }
        }

        yield return new WaitForSeconds(2f);
    }

    private IEnumerator PlayerColorChange()
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.25f);
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void GetKey(GameObject key)
    {
        haveKey = true;
        Instantiate(unLock, key.transform.position, Quaternion.identity);
        AudioManager.Instance.GetKey();
        Destroy(key);
    }

    public void UnLockBox(GameObject box)
    {
        haveKey = false;
        Instantiate(unLock, box.transform.position, Quaternion.identity);
        AudioManager.Instance.DoorOpen();
        Destroy(box);
    }

}
