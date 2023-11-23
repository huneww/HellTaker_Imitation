using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JGBossGameManager : MonoBehaviour
{
    // 싱글톤
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
    // 오디오 매니저 싱글톤
    private static JGBossAudioManager audioInstance;

    public bool dontMove = false;

    [Space(10), Header("BackGround")]
    // 피스톤 애니메이터
    [SerializeField]
    private Animator[] up_Pistons;
    [SerializeField]
    private Animator[] down_Pistons;
    // 다리 작동 여부 확인 변수
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
    // 플레이어, 스파이크 위로 올라가는 속도
    [SerializeField]
    private float moveUpSpeed = 0.5f;
    public float MoveUpSpeed
    {
        get
        {
            return moveUpSpeed;
        }
    }
    // 스테이지 표시하는 휠 애니메이터
    [SerializeField]
    private Animator stageWheel;
    // 다리 정지시 소모되는 시간
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
    // 먼지 이펙트
    [SerializeField]
    private GameObject dust;
    [SerializeField]
    private GameObject landingDust;
    // 출혈 이펙트
    [SerializeField]
    private GameObject blood;
    // 히트 이펙트
    [SerializeField]
    private GameObject hit;
    // 성공 이펙트
    [SerializeField]
    private GameObject lovePlosion;

    [Space(10), Header("Health")]
    // 현재 체력
    [SerializeField]
    private int curHealth = 4;
    // 체력 표시 불꽃 판넬
    [SerializeField]
    private Image[] healthSinImages;
    // 체력 감소시 판넬 변경 스프라이트
    [SerializeField]
    private Sprite emptyhHealthSprite;
    // 현재 체력 표시 불꽃
    [SerializeField]
    private GameObject[] fires;

    [Space(10), Header("Dead")]
    // 죽을때 나오는 이펙트
    [SerializeField]
    private GameObject deadParticle;
    // 이펙트 뒷배경
    [SerializeField]
    private GameObject deadBackGround;
    // 현재 죽어있는지 확인 변수
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

    // 플레이어 오브젝트
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
        // 싱글톤 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        // 플레이어 오브젝트 획득
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator Start()
    {
        if (dontMove) yield break;

        // 오디오 인스턴스 값 지정
        audioInstance = JGBossAudioManager.Instance;

        // 0.5초 대기
        yield return new WaitForSeconds(1f);

        // 피스톤의 내려가는 애니메이션 실행
        foreach (var piston in up_Pistons)
            piston.SetTrigger("Down");
        // 기계 작동음 재생
        audioInstance.MachinStart();
        // 메인 오디오 재생
        audioInstance.MainAudioStart();

        yield return new WaitForSeconds(1.15f);

        // 다리 작동
        isMove = true;

        // 휠 회전
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
    /// 먼지 이펙트 소환
    /// </summary>
    /// <param name="spawnPos">소환 위치</param>
    public void SpawnDust(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(dust, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// 출혈 이펙트 소환
    /// </summary>
    /// <param name="spawnPos">소환 위치</param>
    public void SpawnBlood(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(blood, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// 히트 이펙트 소환
    /// </summary>
    /// <param name="spawnPos">소환 위치</param>
    public void SpawnHit(Vector3 spawnPos)
    {
        GameObject clone = Instantiate(hit, spawnPos, Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

    /// <summary>
    /// 저지먼트 랜딩 먼지 이펙트 소환
    /// </summary>
    /// <param name="spawnPos">소환 위치</param>
    /// <param name="parent">저지먼트 트랜스폼</param>
    public void SpawnLandingDust(Vector3 spawnPos, Transform parent)
    {
        GameObject clone = Instantiate(landingDust, spawnPos, Quaternion.identity);
        clone.transform.SetParent(parent);
    }

    /// <summary>
    /// 플레이어 컬러 체인지
    /// </summary>
    public void PlayerColorChange()
    {
        StartCoroutine(PlayerColorChangeCoroutine());
    }

    /// <summary>
    /// 체력 감소
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
    /// 플레이어 사망 시퀀스
    /// </summary>
    public void PlayerDead()
    {
        if (isDead) return;

        Debug.Log("Player Dead");
        // 플레이어 사망 이펙트, 배경 소환
        Instantiate(deadParticle, player.transform.position, Quaternion.identity);
        Instantiate(deadBackGround, this.transform.position, Quaternion.identity);
        // 플레이어 사망상태로 변경
        isDead = true;
        // 사망 이펙트 사운드 재생
        JGBossAudioManager.Instance.PlayerDead();
        // 메인 사운드 정지
        JGBossAudioManager.Instance.MainAudioStop();
        // 스테이지 재시작
        StartCoroutine(StageChange(true));
    }

    public void NextStage()
    {
        StartCoroutine(StageChange(false));
    }

    /// <summary>
    /// 스테이지 변경
    /// </summary>
    /// <param name="isRestart">다시 시작하는지 여부</param>
    /// <param name="isPreesR">R키를 눌러서 재식자하는지 여부</param>
    /// <returns></returns>
    private IEnumerator StageChange(bool isRestart, bool isPreesR = false)
    {
        // 현재 씬 이름 획득
        string name = SceneManager.GetActiveScene().name;

        // R키를 누른게 아니라면 일정시간 대기
        if (!isPreesR) yield return new WaitForSeconds(1.25f);

        // 현재 스테이지 재시작
        if (isRestart)
        {
            SceneChangeDoor.Instance.PlayCloseAnimation(name);
        }
        else
        {
            // 다음 스테이지 시작
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
    /// 플레이어 컬러 틴트
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerColorChangeCoroutine()
    {
        // 플레이어 스프라이트 랜더러 컴포넌트 획득
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();

        // 현재 플레이어의 색상이 빨간색이면 코루틴 종료
        if (playerSprite.color == Color.red) yield break;

        // 현재 색상 저장
        Color color = playerSprite.color;
        // 플레이어 색상 변경
        playerSprite.color = Color.red;
        // 일정 시간 대기
        yield return new WaitForSeconds(0.25f);
        // 원래 색상으로 변경
        playerSprite.color = color;
    }

    /// <summary>
    /// 다리 정지
    /// </summary>
    public void BridgeStop()
    {
        // 다리 정지 메서드 실행
        BridgeController.stop();
        // 메인 오디오 정지
        audioInstance.MachinStop();
        // 피스톤 애니메이션 실행
        foreach (var piston in down_Pistons)
            piston.SetTrigger("Up");
    }

    /// <summary>
    /// 체인 쉐이크
    /// </summary>
    /// <param name="obj">흔들 오브젝트</param>
    /// <param name="magnitued">강도</param>
    /// <param name="duration">지속시간</param>
    /// <param name="isLeft">왼쪽 체인인지 확인 여부</param>
    /// <returns></returns>
    public IEnumerator ChainShake(GameObject obj, float magnitued = 1f, float duration = 1f, bool isLeft = false)
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = Vector3.zero;

        // 왼쪽 체인이라면
        if (isLeft)
            // 왼쪽 체인 소환 위치 저장
            curPos = ChainSpanwer.Instance.SpawnPos_L;
        else
            // 오른쪽 체인 소환 위치 저장
            curPos = ChainSpanwer.Instance.SpawnPos_R;

        // 쉐이크
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / duration;
            obj.transform.position = new Vector3(curPos.x + Random.Range(-magnitued, magnitued), curPos.y + Random.Range(-magnitued, magnitued));
        }

        // 기존 위치로 위치 변경
        obj.transform.position = curPos;

    }

    public void SpawnLovePlosion()
    {
        GameObject clone = Instantiate(lovePlosion, new Vector3(0.53f, 4.1f), Quaternion.identity);
        clone.transform.SetParent(this.transform);
    }

}
