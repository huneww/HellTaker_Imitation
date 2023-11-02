using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 효과음 출력 오디오
    [SerializeField]
    private GameObject subAudio;
    // 출력 오디오의 오디오소스 전부 저장할 변수
    [SerializeField]
    private AudioSource[] subAudios;
    // 메뉴 사운드 클립
    [SerializeField]
    private AudioClip[] menuClips;
    // 대화창 사운드 클립
    [SerializeField]
    private AudioClip dialogClips;

    // 효과음 출력할 오디오 인덱스
    private int audioIndex = 0;

    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action menuMove;
    public static Action menuSelection;
    public static Action dialogComFirm;

    private void Start()
    {
        // 오디오에 있는 오디오소스를 전부 획득
        subAudios = subAudio.GetComponents<AudioSource>();
        // 액션메서드에 메서드 연결
        menuMove = () => { MenuMoveSound(); };
        menuSelection = () => { MenuSelectionSound(); };
        dialogComFirm = () => { DialogComFirm(); };
    }

    private void MenuMoveSound()
    {
        // 출력 오디오 인덱스 값 증가
        audioIndex++;
        // 오디오 소스의 갯수 보다 커지면 0으로 초기화
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // 메뉴 이동 사운드 재생
        subAudios[audioIndex].PlayOneShot(menuClips[1]);
    }

    private void MenuSelectionSound()
    {
        // 출력 오디오 인덱스 값 증가
        audioIndex++;
        // 오디오 소스의 갯수 보다 커지면 0으로 초기화
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // 메뉴 선택 사운드 재생
        subAudios[audioIndex].PlayOneShot(menuClips[0]);
    }

    private void DialogComFirm()
    {
        // 출력 오디오 인덱스 값 증가
        audioIndex++;
        // 오디오 소스의 갯수 보다 커지면 0으로 초기화
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // 대화창 출력 사운드 재생
        subAudios[audioIndex].PlayOneShot(dialogClips);
    }

}
