using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGBossAudioManager : MonoBehaviour
{
    // �̱���
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

    // ���� �����
    [SerializeField]
    private AudioSource mainAudio;
    // ���� �����
    [SerializeField]
    private AudioSource[] subAudios;
    // ������ ���� ����� �ε���
    [SerializeField]
    private int curAudioIndex = 0;
    // ��� �۵� ���� ����
    [SerializeField]
    private AudioClip machineStart;
    // ��� �۵� ���� ����
    [SerializeField]
    private AudioClip machineStop;
    // ü�� ��ũ ����
    [SerializeField]
    private AudioClip[] chainBlink;
    // ü�ο� �´� ����
    [SerializeField]
    private AudioClip[] playerHit;
    // �÷��̾� �״� ����
    [SerializeField]
    private AudioClip playerDead;
    // ü�� ������ ����
    [SerializeField]
    private AudioClip[] chainHit_L;
    [SerializeField]
    private AudioClip[] chainHit_R;
    // ü�� ������ ����
    [SerializeField]
    private AudioClip[] chainEnable;
    // ü�� �μ����� ����
    [SerializeField]
    private AudioClip chainBreak_L;
    [SerializeField]
    private AudioClip chainBreak_R;
    // ������Ʈ ���� ����
    [SerializeField]
    private AudioClip judgmentLanding;
    // ���̾�α� ���� ����
    [SerializeField]
    private AudioClip startDialog;
    // ���̾�α� ��� ����
    [SerializeField]
    private AudioClip textEnd;
    // ���ε� ü�� ��ȯ ����
    [SerializeField]
    private AudioClip bindingChainSummon;
    // ������Ʈ ���ε� ����
    [SerializeField]
    private AudioClip judgmentBinding;
    // ü�� �����̴� ����
    [SerializeField]
    private AudioClip[] bindingChainMove;
    // ���̾�α� ���� ����
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
