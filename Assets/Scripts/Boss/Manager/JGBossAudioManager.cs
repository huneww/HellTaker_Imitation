using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGBossAudioManager : MonoBehaviour
{
    // 싱글톤
    private static JGBossAudioManager instance;
    public static JGBossAudioManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // 메인 오디오
    [SerializeField]
    private AudioSource mainAudio;
    // 서브 오디오
    [SerializeField]
    private AudioSource[] subAudios;
    // 실행할 서브 오디오 인덱스
    [SerializeField]
    private int curAudioIndex = 0;
    // 기계 작동 시작 사운드
    [SerializeField]
    private AudioClip machineStart;
    // 기계 작동 중지 사운드
    [SerializeField]
    private AudioClip machineStop;
    // 체인 블링크 사운드
    [SerializeField]
    private AudioClip[] chainBlink;
    // 체인에 맞는 사운드
    [SerializeField]
    private AudioClip[] playerHit;
    // 플레이어 죽는 사운드
    [SerializeField]
    private AudioClip playerDead;
    // 체인 때리는 사운드
    [SerializeField]
    private AudioClip[] chainHit_L;
    [SerializeField]
    private AudioClip[] chainHit_R;
    // 체인 나오는 사운드
    [SerializeField]
    private AudioClip[] chainEnable;
    // 체인 부서지는 사운드
    [SerializeField]
    private AudioClip chainBreak_L;
    [SerializeField]
    private AudioClip chainBreak_R;
    // 저지먼트 착지 사운드
    [SerializeField]
    private AudioClip judgmentLanding;
    // 다이얼로그 시작 사운드
    [SerializeField]
    private AudioClip startDialog;
    // 다이얼로그 출력 사운드
    [SerializeField]
    private AudioClip textEnd;
    // 바인딩 체인 소환 사운드
    [SerializeField]
    private AudioClip bindingChainSummon;
    // 저지먼트 바인딩 사운드
    [SerializeField]
    private AudioClip judgmentBinding;
    // 체인 움직이는 사운드
    [SerializeField]
    private AudioClip[] bindingChainMove;
    // 다이얼로그 성공 사운드
    [SerializeField]
    private AudioClip dialogSuccess;
    [SerializeField]
    private AudioClip[] lovePlosion;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void IndexChange()
    {
        curAudioIndex = (curAudioIndex + 1) % subAudios.Length;
    }

    private void AudioPlay(AudioClip clip)
    {
        subAudios[curAudioIndex].PlayOneShot(clip);
        IndexChange();
    }

    public void MainAudioStart()
    {
        mainAudio.Play();
    }

    public void MainAudioStop()
    {
        mainAudio.Stop();
    }

    public void MachinStart()
    {
        AudioPlay(machineStart);
    }

    public void MachinStop()
    {
        AudioPlay(machineStop);
    }

    public void ChainBlink()
    {
        AudioPlay(chainBlink[Random.Range(0, chainBlink.Length)]);
    }

    public void PlayerHit()
    {
        AudioPlay(playerHit[Random.Range(0, playerHit.Length)]);
    }

    public void PlayerDead()
    {
        AudioPlay(playerDead);
    }

    public void ChainHit_L()
    {
        AudioPlay(chainHit_L[Random.Range(0, chainHit_L.Length)]);
    }

    public void ChainHit_R()
    {
        AudioPlay(chainHit_R[Random.Range(0, chainHit_R.Length)]);
    }

    public void DefendChainEnable()
    {
        AudioPlay(chainEnable[Random.Range(0, chainEnable.Length)]);
    }

    public void ChainBreak_L()
    {
        AudioPlay(chainBreak_L);
    }

    public void ChainBreak_R()
    {
        AudioPlay(chainBreak_R);
    }

    public void JGLanding()
    {
        AudioPlay(judgmentLanding);
    }

    public void StartDialog()
    {
        AudioPlay(startDialog);
    }

    public void TextEnd()
    {
        AudioPlay(textEnd);
    }

    public void SummonBindChain()
    {
        AudioPlay(bindingChainSummon);
    }

    public void JGBinding()
    {
        AudioPlay(judgmentBinding);
    }

    public void BindingChainMove()
    {
        AudioPlay(bindingChainMove[Random.Range(0, bindingChainMove.Length)]);
    }

    public void DialogSuccess()
    {
        AudioPlay(dialogSuccess);
    }

    public void LovePlosion()
    {
        AudioPlay(lovePlosion[Random.Range(0, lovePlosion.Length)]);
    }

}
