using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField]
    // ���� ����� ������Ʈ
    private AudioSource[] audioSources;
    // ����� Ŭ��
    [SerializeField]
    private AudioClip dialogComfirm;
    [SerializeField]
    private AudioClip menuMove;
    [SerializeField]
    private AudioClip menuSelect;
    [SerializeField]
    private AudioClip chapterMove;
    [SerializeField]
    private AudioClip chapterSelect;

    // ����� ����� �迭�� �ε���
    private int curAudioIndex = 0;

    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� �޼���
    public static Action DialogComfirm;
    public static Action MenuMove;
    public static Action MenuSelect;
    public static Action ChapterMove;
    public static Action ChapterSelect;

    private void Start()
    {
        // �׼� �޼��� ����
        DialogComfirm = () => { PlayDialogComfirm(); };
        MenuMove = () => { PlayMenuMove(); };
        MenuSelect = () => { PlayMenuSelect(); };
        ChapterMove = () => { PlayChapterMove(); };
        ChapterSelect = () => { PlayChapterSelect(); };
    }

    private void ChangeIndex()
    {
        curAudioIndex++;
        if (curAudioIndex >= audioSources.Length)
            curAudioIndex = 0;
    }

    /// <summary>
    /// ���̾�α� ��� ����
    /// </summary>
    private void PlayDialogComfirm()
    {
        audioSources[curAudioIndex].PlayOneShot(dialogComfirm);
        ChangeIndex();
    }

    /// <summary>
    /// �޴� ���� ����
    /// </summary>
    private void PlayMenuMove()
    {
        audioSources[curAudioIndex].PlayOneShot(menuMove);
        ChangeIndex();
    }

    /// <summary>
    /// �޴� ���� ����
    /// </summary>
    private void PlayMenuSelect()
    {
        audioSources[curAudioIndex].PlayOneShot(menuSelect);
        ChangeIndex();
    }

    /// <summary>
    /// é�� ���� ����
    /// </summary>
    private void PlayChapterMove()
    {
        audioSources[curAudioIndex].PlayOneShot(chapterMove);
        ChangeIndex();
    }

    /// <summary>
    /// é�� ���� ����
    /// </summary>
    private void PlayChapterSelect()
    {
        audioSources[curAudioIndex].PlayOneShot(chapterSelect);
        ChangeIndex();
    }

}
