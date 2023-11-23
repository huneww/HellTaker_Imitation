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

    // 플레이어, 오브젝트 이동 오프셋 값
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

    // 플레이어 오브젝트
    private GameObject player;
    // 플레이어가 죽었는지 확인 변수
    private bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    // 플레이어가 열쇠를 가지고 있는지 확인 변수
    private bool haveKey;
    public bool HaveKey
    {
        get
        {
            return haveKey;
        }
    }
    // 현재 스테이지의 악마 오브젝트
    private GameObject demon;
    // 현재 스테이지
    private int curStage;
    public int CurStage
    {
        get
        {
            return curStage;
        }
    }

    [Space(10), Header("VFX")]
    // 이동시 먼지 이펙트
    [SerializeField]
    private GameObject dust;
    // 오브젝트 히트 이펙트
    [SerializeField]
    private GameObject hit;
    // 스켈레톤 죽을때 나오는 뼈 파티클
    [SerializeField]
    private GameObject bornParticle;
    // 플레이어 함정에 맞을때 나오는 피 파티클
    [SerializeField]
    private GameObject blood;
    // 챕터 클리어시 나오는 하트 파티클
    [SerializeField]
    private GameObject lovePlosion;
    // 잠긴 박스 해제시 나오는 파티클
    [SerializeField]
    private GameObject unLock;

    [Space(10), Header("UI")]
    // 챕터, 이동횟수 UI 부모 오브젝트
    [SerializeField]
    private GameObject uiCanvas;
    // 이동횟수 텍스트
    [SerializeField]
    private Text footCountText;
    // 현재 남은 이동횟수
    [SerializeField]
    private int curFootCount;
    // 현재 스테이지 텍스트
    [SerializeField]
    private Text chapterText;

    [Space(10), Header("Dead")]
    // 플레이어 사망시 나오는 애니메이션
    [SerializeField]
    private GameObject deadAnimation;
    // 사망시 나오는 애니메이션 배경
    [SerializeField]
    private GameObject deadBackGround;

    [Space(10), Header("Dialog")]
    // 현재 다이얼로그에 있는지 확인 변수
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
    // 다이얼로그 캔버스 오브젝트
    [SerializeField]
    private GameObject dialogCanvas;
    // 선택지 캔버스 오브젝트
    [SerializeField]
    private GameObject selectMenu;
    // 현재 선택지에 있는지 확인 변수
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
    // 잘못된 선택지 고를시 나오는 애니메이션
    [SerializeField]
    private GameObject badEndScene;
    // 루시퍼 스테이지 두번째 선택지에 잘못 고를시 나오는 애니메이션
    [SerializeField]
    private GameObject secondBadEndScene;

    private void Awake()
    {
        // 싱글톤 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        // 플레이어, 악마 오브젝트 획득
        player = GameObject.FindGameObjectWithTag("Player");
        demon = GameObject.FindGameObjectWithTag("Demon");
        // 이동횟수, 챕터 UI 초기화
        ChapterUITextReset();
    }

    private void Update()
    {
        // R키를 누를시
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 현재 스테이지 다시 로드
            StartCoroutine(StageChange(true, true));
        }

        // 베드 엔드가 나와있는 상태일경우
        if (badEndScene.activeSelf)
        {
            // 엔터, R키를 누를시 챕터 다시 로드
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R))
            {
                // 0은 1챕터이기때문에 8은 9챕터임
                // 9챕터에서는 다음 챕터로 이동
                if (curStage == 8)
                    StartCoroutine(StageChange(false));
                // 현재 챕터 다시 로드
                else
                    StartCoroutine(StageChange(true));
            }
        }
    }

    /// <summary>
    /// 먼지 이펙트 생성
    /// </summary>
    /// <param name="spawnPos">생성 위치</param>
    public void SpawnDust(Vector3 spawnPos)
    {
        // 먼지 이펙트 생성
        Instantiate(dust, spawnPos, Quaternion.identity);
    }

    /// <summary>
    /// 히트 이펙트 생성
    /// </summary>
    /// <param name="spawnPos">생성 위치</param>
    public void SpawnHit(Vector3 spawnPos)
    {
        // 방향은 랜덤으로 지정
        Quaternion quaternion = Quaternion.Euler(0, 0, Random.Range(0, 361));
        // 랜덤 방향으로 히트 이펙트 생성
        Instantiate(hit, spawnPos, quaternion);
    }

    /// <summary>
    /// 뼈 이펙트 생성
    /// </summary>
    /// <param name="spawnPos">생성 위치</param>
    public void SpawnBornParticle(Vector3 spawnPos)
    {
        // 뼈 이펙트 생성
        GameObject clone = Instantiate(bornParticle, spawnPos, Quaternion.identity);
        // 3초후 뼈 이펙트 제거
        Destroy(clone, 3.0f);
    }

    /// <summary>
    /// 출혈 이펙트 생성
    /// </summary>
    /// <param name="spawnPos">생성 위치</param>
    public void SpawnBlood(Vector3 spawnPos)
    {
        // 출혈 이펙트 생성
        Instantiate(blood, spawnPos, Quaternion.identity);
        // 출혈 이펙트는 트랩에의해서만 호출됨
        // 스파이크 사운드 재생
        AudioManager.Instance.SpikeHit();
        // 이동횟수 1회 추가 마이너스
        FootCountMinus(true);
        // 플레이어 색상 변경 코루틴 실행
        StartCoroutine(PlayerColorChange());
    }

    /// <summary>
    /// 현재 챕터에 맞게 이동횟수, 횟수 텍스트, 챕터, 챕터 텍스트 변경
    /// </summary>
    private void ChapterUITextReset()
    {
        // 현재 실행중인 씬 이름 획득
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        // 현재 실행중인 씬에 맞게 초기화
        switch (scene.name)
        {
            case "ChapterOne":
                chapterText.text = "Ⅰ";
                curFootCount = 23;
                curStage = 0;
                break;
            case "ChapterTwo":
                chapterText.text = "Ⅱ";
                curFootCount = 24;
                curStage = 1;
                break;
            case "ChapterThree":
                chapterText.text = "Ⅲ";
                curFootCount = 32;
                curStage = 2;
                break;
            case "ChapterFour":
                chapterText.text = "Ⅳ";
                curFootCount = 23;
                curStage = 3;
                break;
            case "ChapterFive":
                chapterText.text = "Ⅴ";
                curFootCount = 23;
                curStage = 4;
                break;
            case "ChapterSix":
                chapterText.text = "Ⅵ";
                curFootCount = 43;
                curStage = 5;
                break;
            case "ChapterSeven":
                chapterText.text = "Ⅶ";
                curFootCount = 32;
                curStage = 6;
                break;
            case "ChapterEight":
                chapterText.text = "Ⅷ";
                curFootCount = 12;
                curStage = 7;
                break;
            case "ChapterNine":
                chapterText.text = "Ⅸ";
                curFootCount = 33;
                curStage = 8;
                break;
        }
        footCountText.text = curFootCount.ToString();
    }

    /// <summary>
    /// 이동 횟수 차감
    /// </summary>
    /// <param name="isSpike">스파이크에 의해 실행 됐는지 확인 여부</param>
    public void FootCountMinus(bool isSpike = false)
    {
        // 이동 횟수가 0일대 움직일시
        if (curFootCount < 1 && !isDead)
        {
            Debug.Log("Player Dead");
            // 죽은 상태로 상태 변화
            isDead = true;
            // 플레이어 죽는 사운드 재생
            AudioManager.Instance.PlayerDead();
            // 이동횟수, 챕터 UI 비활성화
            uiCanvas.SetActive(false);
            // 플레이어 죽을시 나오는 애니메이션, 뒷 배경 생성
            Instantiate(deadBackGround, Vector3.zero, Quaternion.identity);
            Instantiate(deadAnimation, player.transform.position, Quaternion.identity);
            // 챕터 다시 로드
            StartCoroutine(StageChange(true));
            return;
        }

        // 이동횟수 차감
        curFootCount--;
        Debug.Log("FootMinus");
        // 이동횟수 텍스트 변경
        footCountText.text = curFootCount.ToString();
        // 텍스트 핑퐁을 위해 텍스트 비활성화 후 바로 활성화
        footCountText.gameObject.SetActive(false);
        footCountText.gameObject.SetActive(true);

        // 스파이크에 의해서 실행되면
        if (isSpike)
        {
            // 이동횟수 텍스트 색상 변경 코루틴 실행
            StartCoroutine(FootTextColor());
            // 카메라 흔들기
            CameraShake.cameraShake();
        }

        // 현재 이동 가능 횟수가 0이라면
        // 텍스트 0이 아닌 X로 변경
        if (curFootCount == 0)
            footCountText.text = "X";
    }

    /// <summary>
    /// 이동 가능 횟수 텍스트 색상 변경
    /// </summary>
    /// <returns></returns>
    private IEnumerator FootTextColor()
    {
        // 기본 색상 저장
        Color color = footCountText.color;
        // 빨간색으로 변경
        footCountText.color = Color.red;
        // 0.1초 대기 후
        yield return new WaitForSeconds(0.1f);
        // 원상태로 변경
        footCountText.color = color;
    }

    /// <summary>
    /// 플레이어가 골지점 도착
    /// </summary>
    public void Goal()
    {
        // 현재 죽은 상태라면 메서드 종료
        if (isDead) return;

        // 챕터, 이동횟수 UI 비활성화
        uiCanvas.SetActive(false);
        // 다이얼로그 UI 활성화
        dialogCanvas.SetActive(true);
        // 현재 다이얼로그에 있는 상태로 변환
        isDialog = true;
    }

    /// <summary>
    /// 선택지에서 베드엔드 선택시
    /// </summary>
    public void BadEnd()
    {
        // 다이얼로그 UI 비활성화
        dialogCanvas.SetActive(false);
        // 베드 엔드 UI 활성화
        badEndScene.SetActive(true);
        // 배경음 정지
        AudioManager.Instance.MainAudioStop();
    }

    /// <summary>
    /// 루시퍼 스테이지에서 두번째 선택지에서 베드엔드 선택시
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    public void BadSecondEnd()
    {
        // 두번쟤 선택지가 없다면
        if (secondBadEndScene == null) throw new System.Exception("Second Bad End Scene is Null");

        // 다이얼로그 UI 비활성화
        dialogCanvas.SetActive(false);
        // 두번째 베드 엔드 UI 활성화
        secondBadEndScene.SetActive(true);
        // 배경음 정지
        AudioManager.Instance.MainAudioStop();
    }

    /// <summary>
    /// 해피 엔드 선택시
    /// </summary>
    public void GoodEnd()
    {
        // 플레이어 클리어 애니메이션 실행
        player.GetComponent<Animator>().SetTrigger("Clear");
        // 성공 사운드 재생
        AudioManager.Instance.SuccesSound();
        // 성공시 나오는 파티클, 애니메이션 생성
        Instantiate(lovePlosion, demon.transform.position, Quaternion.identity);
    }

    ///// <summary>
    ///// 딜레이를 주고 스테이지 재시작
    ///// </summary>
    ///// <param name="delay">딜레이 시간</param>
    ///// <returns></returns>
    //private IEnumerator DelayStageChange(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    StartCoroutine(StageChange(false));
    //}

    /// <summary>
    /// 챕터 변경 코루틴
    /// </summary>
    /// <param name="isRestart">재식작이지 확인 여부</param>
    /// <param name="isPressR">R키를 눌러서 실행되는 확인 여부</param>
    /// <returns></returns>
    public IEnumerator StageChange(bool isRestart, bool isPressR = false)
    {
        // 현재 실행중인 씬 이름 확인
        Scene scene = SceneManager.GetActiveScene();
        string name = scene.name;
        // 배경음 정지
        AudioManager.Instance.MainAudioStop();

        // 재식작이라면
        if (isRestart)
        {
            // R키를 누른게 아니라면
            if (!isPressR)
                // 1초 대기후 실행
                yield return new WaitForSeconds(1.0f);
            Debug.Log("ReStart Stage");
            // 현재 씬 다시 로드
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        // 다음 챕터로 이동
        else
        {
            Debug.Log("Next Stage");
            // 현재 실행 중인 씬이름에 맞게 다음 챕터 실행
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
    /// 플레이어 컬러 변경
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerColorChange()
    {
        // 기존 색상 저장
        Color color = player.GetComponent<SpriteRenderer>().color;
        // 빨간색으로 변경
        player.GetComponent<SpriteRenderer>().color = Color.red;
        // 0.25초 대기 후
        yield return new WaitForSeconds(0.25f);
        // 기존 색상으로 변경
        player.GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// 열쇠 획드
    /// </summary>
    /// <param name="key">열쇠 오브젝트</param>
    public void GetKey(GameObject key)
    {
        // 열쇠를 가지고 있는 상태로 변경
        haveKey = true;
        // 열쇠 획득, 언락 이펙트는 동일
        // 열쇠 획득 이펙트 실행
        Instantiate(unLock, key.transform.position, Quaternion.identity);
        // 열쇠 획득 사운드 재생
        AudioManager.Instance.GetKey();
        // 열쇠 제거
        Destroy(key);
    }

    /// <summary>
    /// 잠긴 문 개방
    /// </summary>
    /// <param name="box">잠긴 문</param>
    public void UnLockBox(GameObject box)
    {
        // 열쇠 소비
        haveKey = false;
        // 언락 이펙트 실행
        Instantiate(unLock, box.transform.position, Quaternion.identity);
        // 문 열리는 사운드 재생
        AudioManager.Instance.DoorOpen();
        // 문 제거
        Destroy(box);
    }

}
