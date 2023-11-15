using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGIntroAudioManager : MonoBehaviour
{
    private static JGIntroAudioManager instance;
    public static JGIntroAudioManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // ȿ���� ��� �����
    [SerializeField]
    private AudioSource[] subAudios;

    // ��� ����� �ε���
    private int audioIndex;

    // ��Ʈ�� �ִϸ��̼� ����
    [SerializeField]
    private AudioClip introSound;
    // ��� ��� �Ϸ� ����
    [SerializeField]
    private AudioClip dialogComfirm;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void IndexChange()
    {
        audioIndex = (audioIndex + 1) % subAudios.Length;
    }

    private void AudioPlay(AudioClip clip)
    {
        subAudios[audioIndex].PlayOneShot(clip);
        IndexChange();
    }

    public void JGIntro()
    {
        AudioPlay(introSound);
    }

    public void JGIntroDialogComfirm()
    {
        AudioPlay(dialogComfirm);
    }

}
