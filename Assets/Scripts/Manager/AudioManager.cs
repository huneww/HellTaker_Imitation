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
    // 다이얼로그에서 잘못선택시 나오는 사운드
    [SerializeField]
    private AudioClip badEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void PlayerMove()
    {
        subAudios[audioIndex].PlayOneShot(playerMove);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void PlayerDead()
    {
        subAudios[audioIndex].PlayOneShot(playerDead);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void EnemyKick()
    {
        subAudios[audioIndex].PlayOneShot(enemyKickSound[Random.Range(0, enemyKickSound.Length)]);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void EnemyDie()
    {
        subAudios[audioIndex].PlayOneShot(enemyDieSound[Random.Range(0, enemyDieSound.Length)]);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void StoneKick()
    {
        subAudios[audioIndex].PlayOneShot(stonKickSound[Random.Range(0, stonKickSound.Length)]);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void StoneMove()
    {
        subAudios[audioIndex].PlayOneShot(stonMoveSound[Random.Range(0, stonMoveSound.Length)]);
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    public void BadEnd()
    {
        subAudios[audioIndex].PlayOneShot(badEnd);
        audioIndex = (audioIndex + 1) % subAudios.Length;
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
