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

    // 효과음 출력 오디오
    [SerializeField]
    private AudioSource[] subAudios;

    // 출력 오디오 인덱스
    private int audioIndex;

    // 인트로 애니메이션 사운드
    [SerializeField]
    private AudioClip introSound;
    // 대사 출력 완료 사운드
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
