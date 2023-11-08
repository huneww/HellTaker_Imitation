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
        ChapterUITextReset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(StageChange(true, true));
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

    private void ChapterUITextReset()
    {
        Scene scene = SceneManager.GetActiveScene();
        chapterText.text = scene.name;
        switch (scene.name)
        {
            case "ChapterOne":
                chapterText.text = "グ";
                curFootCount = 23;
                curStage = 0;
                break;
            case "ChpaterTwo":
                chapterText.text = "ケ";
                curFootCount = 24;
                curStage = 1;
                break;
            case "ChpaterThree":
                chapterText.text = "ゲ";
                curFootCount = 32;
                curStage = 2;
                break;
            case "ChpaterFour":
                chapterText.text = "コ";
                curFootCount = 23;
                curStage = 3;
                break;
            case "ChpaterFive":
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
            case "ChatperNine":
                chapterText.text = "ジ";
                curFootCount = 33;
                curStage = 8;
                break;
        }
        footCountText.text = curFootCount.ToString();
    }

    public void FootCountMinus()
    {
        if (curFootCount < 1)
        {
            Debug.Log("Player Dead");
            AudioManager.Instance.PlayerDead();
            uiCanvas.SetActive(false);
            Instantiate(deadBackGround, Vector3.zero, Quaternion.identity);
            Instantiate(deadAnimation, player.transform.position, Quaternion.identity);
            StartCoroutine(StageChange(true));
            return;
        }
        curFootCount--;
        footCountText.text = curFootCount.ToString();
    }

    public void Goal()
    {
        uiCanvas.SetActive(false);
        dialogCanvas.SetActive(true);
        isDialog = true;
    }

    public void BadEnd()
    {
        dialogCanvas.SetActive(false);
        badEndScene.SetActive(true);
    }

    public void GoodEnd()
    {
        StartCoroutine(StageChange(false));
    }

    private IEnumerator StageChange(bool isRestart, bool isPressR = false)
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
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterTwo");
                    break;
                case "ChpaterTwo":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterThree");
                    break;
                case "ChpaterThree":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterFour");
                    break;
                case "ChpaterFour":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterFive");
                    break;
                case "ChpaterFive":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterSix");
                    break;
                case "ChapterSix":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterSeven");
                    break;
                case "ChapterSeven":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterEight");
                    break;
                case "ChapterEight":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterNine");
                    break;
                case "ChatperNine":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterTen");
                    break;
                case "ChapterTen":
                    SceneChangeDoor.Instance.PlayCloseAnimation("ChpaterEX");
                    break;
                case "ChapterEX":
                    SceneChangeDoor.Instance.PlayCloseAnimation("MainMenu");
                    break;
            }
        }

        yield return new WaitForSeconds(2f);
    }

}
