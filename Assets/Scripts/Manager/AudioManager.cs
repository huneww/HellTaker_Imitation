using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // 싱글톤 패턴
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // 메인 배경음 오디오
    [SerializeField]
    private AudioSource mainAudio;
    // 효과음 오디오
    [SerializeField]
    private AudioSource[] subAudios;
    // 효과음 오디오 인덱스
    private int audioIndex;

    // 플레이어 이동 사운드
    [SerializeField]
    private AudioClip playerMove;
    // 플레이어 죽는 사운드
    [SerializeField]
    private AudioClip playerDead;
    // 스켈레톤 차는 사운드
    [SerializeField]
    private AudioClip[] enemyKickSound;
    // 스켈레톤 죽는 사운드
    [SerializeField]
    private AudioClip[] enemyDieSound;
    // 스톤 차는 사운드
    [SerializeField]
    private AudioClip[] stonKickSound;
    // 스톤 이동 사운드
    [SerializeField]
    private AudioClip[] stonMoveSound;
    // 다이얼로그 출력 사운드
    [SerializeField]
    private AudioClip dialogComFirmSound;
    // 다이얼로그에서 실패시 나오는 사운드
    [SerializeField]
    private AudioClip badEnd;
    // 다이얼로그에서 성공시 나오는 사운드
    [SerializeField]
    private AudioClip goodEnd;
    // 다이얼로그 선택 메뉴 이동 사운드
    [SerializeField]
    private AudioClip dialogMoveSound;
    // 다이얼로그 선택 메뉴 결정 사운드
    [SerializeField]
    private AudioClip dialogSelectSound;
    // 스파이크 히트 사운드
    [SerializeField]
    private AudioClip[] spikeHitSound;
    // 파티클 사운드
    [SerializeField]
    private AudioClip[] succesSound;
    // 열쇠 획득 사운드
    [SerializeField]
    private AudioClip getKeySound;
    // 잠긴문 차는 사운드
    [SerializeField]
    private AudioClip[] doorKickSound;
    // 문 열리는 사운드
    [SerializeField]
    private AudioClip doorOpenSound;
    // 루시퍼 와인잔 돌리는 사운드
    [SerializeField]
    private AudioClip luciferSwirlSound;
    // 루시퍼 인트로 사운드
    [SerializeField]
    private AudioClip luciferIntroSound;
    // 루시퍼 가드 움직이는 사운드
    [SerializeField]
    private AudioClip luciferGuardMoveSound;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// 오디오 인덱스 변경
    /// </summary>
    private void IndexChange()
    {
        // 오디오 인덱스 변경
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    /// <summary>
    /// 오디오 클립 재생
    /// </summary>
    /// <param name="clip">재생할 오디오 클립</param>
    private void AudioPlay(AudioClip clip)
    {
        // 클립 재생
        subAudios[audioIndex].PlayOneShot(clip);
        // 오디오 인덱스 변경
        IndexChange();
    }

    public void PlayerMove()
    {
        AudioPlay(playerMove);
    }

    public void PlayerDead()
    {
        AudioPlay(playerDead);
    }

    public void EnemyKick()
    {
        AudioPlay(enemyKickSound[Random.Range(0, enemyKickSound.Length)]);
    }

    public void EnemyDie()
    {
        AudioPlay(enemyDieSound[Random.Range(0, enemyDieSound.Length)]);
    }

    public void StoneKick()
    {
        AudioPlay(stonKickSound[Random.Range(0, stonKickSound.Length)]);
    }

    public void StoneMove()
    {
        AudioPlay(stonMoveSound[Random.Range(0, stonMoveSound.Length)]);
    }

    public void DialogComFirm()
    {
        AudioPlay(dialogComFirmSound);
    }

    public void BadEnd()
    {
        AudioPlay(badEnd);
    }

    public void GoodEnd()
    {
        AudioPlay(goodEnd);
    }

    public void DialogMove()
    {
        AudioPlay(dialogMoveSound);
    }

    public void DialogSelect()
    {
        AudioPlay(dialogSelectSound);
    }

    public void SpikeHit()
    {
        AudioPlay(spikeHitSound[Random.Range(0, spikeHitSound.Length)]);
    }

    public void SuccesSound()
    {
        AudioPlay(succesSound[Random.Range(0, succesSound.Length)]);
    }

    public void DoorKick()
    {
        AudioPlay(doorKickSound[Random.Range(0, doorKickSound.Length)]);
    }

    public void DoorOpen()
    {
        AudioPlay(doorOpenSound);
    }

    public void GetKey()
    {
        AudioPlay(getKeySound);
    }

    public void Swirl()
    {
        AudioPlay(luciferSwirlSound);
    }

    public void LuciferIntro()
    {
        AudioPlay(luciferIntroSound);
    }

    public void LuciferGuardMove()
    {
        AudioPlay(luciferGuardMoveSound);
    }

    public void MainAudioStop()
    {
        mainAudio.Stop();
    }

    public void MainAudioPlay()
    {
        mainAudio.Play();
    }

}
