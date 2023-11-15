using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // �̱��� ����
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

    // ���� ����� �����
    [SerializeField]
    private AudioSource mainAudio;
    // ȿ���� �����
    [SerializeField]
    private AudioSource[] subAudios;
    // ȿ���� ����� �ε���
    private int audioIndex;

    // �÷��̾� �̵� ����
    [SerializeField]
    private AudioClip playerMove;
    // �÷��̾� �״� ����
    [SerializeField]
    private AudioClip playerDead;
    // ���̷��� ���� ����
    [SerializeField]
    private AudioClip[] enemyKickSound;
    // ���̷��� �״� ����
    [SerializeField]
    private AudioClip[] enemyDieSound;
    // ���� ���� ����
    [SerializeField]
    private AudioClip[] stonKickSound;
    // ���� �̵� ����
    [SerializeField]
    private AudioClip[] stonMoveSound;
    // ���̾�α� ��� ����
    [SerializeField]
    private AudioClip dialogComFirmSound;
    // ���̾�α׿��� ���н� ������ ����
    [SerializeField]
    private AudioClip badEnd;
    // ���̾�α׿��� ������ ������ ����
    [SerializeField]
    private AudioClip goodEnd;
    // ���̾�α� ���� �޴� �̵� ����
    [SerializeField]
    private AudioClip dialogMoveSound;
    // ���̾�α� ���� �޴� ���� ����
    [SerializeField]
    private AudioClip dialogSelectSound;
    // ������ũ ��Ʈ ����
    [SerializeField]
    private AudioClip[] spikeHitSound;
    // ��ƼŬ ����
    [SerializeField]
    private AudioClip[] succesSound;
    // ���� ȹ�� ����
    [SerializeField]
    private AudioClip getKeySound;
    // ��乮 ���� ����
    [SerializeField]
    private AudioClip[] doorKickSound;
    // �� ������ ����
    [SerializeField]
    private AudioClip doorOpenSound;
    // ����� ������ ������ ����
    [SerializeField]
    private AudioClip luciferSwirlSound;
    // ����� ��Ʈ�� ����
    [SerializeField]
    private AudioClip luciferIntroSound;
    // ����� ���� �����̴� ����
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
    /// ����� �ε��� ����
    /// </summary>
    private void IndexChange()
    {
        // ����� �ε��� ����
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    /// <summary>
    /// ����� Ŭ�� ���
    /// </summary>
    /// <param name="clip">����� ����� Ŭ��</param>
    private void AudioPlay(AudioClip clip)
    {
        // Ŭ�� ���
        subAudios[audioIndex].PlayOneShot(clip);
        // ����� �ε��� ����
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
