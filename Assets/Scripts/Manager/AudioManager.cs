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
    // ���̾�α׿��� �߸����ý� ������ ����
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
