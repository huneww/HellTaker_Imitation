using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Space(10), Header("VFX")]
    [SerializeField]
    private GameObject dust;
    [SerializeField]
    private GameObject hit;
    [SerializeField]
    private GameObject bornParticle;

    [Space(10), Header("UI")]
    [SerializeField]
    private GameObject canvas;
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
                chapterText.text = "��";
                curFootCount = 23;
                break;
            case "ChpaterTwo":
                chapterText.text = "��";
                curFootCount = 24;
                break;
            case "ChpaterThree":
                chapterText.text = "��";
                curFootCount = 32;
                break;
            case "ChpaterFour":
                chapterText.text = "��";
                curFootCount = 23;
                break;
            case "ChpaterFive":
                chapterText.text = "��";
                curFootCount = 23;
                break;
            case "ChapterSix":
                chapterText.text = "��";
                curFootCount = 43;
                break;
            case "ChapterSeven":
                chapterText.text = "��";
                curFootCount = 32;
                break;
            case "ChapterEight":
                chapterText.text = "��";
                curFootCount = 12;
                break;
            case "ChatperNine":
                chapterText.text = "��";
                curFootCount = 33;
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
            canvas.SetActive(false);
            Instantiate(deadBackGround, Vector3.zero, Quaternion.identity);
            Instantiate(deadAnimation, player.transform.position, Quaternion.identity);
            StartCoroutine(StageChange(true));
            return;
        }
        curFootCount--;
        footCountText.text = curFootCount.ToString();
    }

    private IEnumerator StageChange(bool isRestart)
    {
        Scene scene = SceneManager.GetActiveScene();
        string name = scene.name;
        AudioManager.Instance.MainAudioStop();
        if (isRestart)
        {
            yield return new WaitForSeconds(1.0f);
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        else
        {
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
