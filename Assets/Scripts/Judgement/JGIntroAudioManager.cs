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

    [SerializeField]
    private AudioSource[] subAudios;
    private int audioIndex;
    [SerializeField]
    private AudioClip introSound;

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
    }

    public void JGIntro()
    {
        AudioPlay(introSound);
    }

}
