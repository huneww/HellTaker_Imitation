using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField]
    // 서브 오디오 컴포넌트
    private AudioSource[] audioSources;
    // 오디오 클립
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

    // 재생할 오디오 배열의 인덱스
    private int curAudioIndex = 0;

    // 다른 스크립트에서 접근을 위한 액션 메서드
    public static Action DialogComfirm;
    public static Action MenuMove;
    public static Action MenuSelect;
    public static Action ChapterMove;
    public static Action ChapterSelect;

    private void Start()
    {
        // 액션 메서드 연결
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
    /// 다이얼로그 출력 사운드
    /// </summary>
    private void PlayDialogComfirm()
    {
        audioSources[curAudioIndex].PlayOneShot(dialogComfirm);
        ChangeIndex();
    }

    /// <summary>
    /// 메뉴 변경 사운드
    /// </summary>
    private void PlayMenuMove()
    {
        audioSources[curAudioIndex].PlayOneShot(menuMove);
        ChangeIndex();
    }

    /// <summary>
    /// 메뉴 선택 사운드
    /// </summary>
    private void PlayMenuSelect()
    {
        audioSources[curAudioIndex].PlayOneShot(menuSelect);
        ChangeIndex();
    }

    /// <summary>
    /// 챕터 변경 사운드
    /// </summary>
    private void PlayChapterMove()
    {
        audioSources[curAudioIndex].PlayOneShot(chapterMove);
        ChangeIndex();
    }

    /// <summary>
    /// 챕터 선택 사운드
    /// </summary>
    private void PlayChapterSelect()
    {
        audioSources[curAudioIndex].PlayOneShot(chapterSelect);
        ChangeIndex();
    }

}
