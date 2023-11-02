using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ȿ���� ��� �����
    [SerializeField]
    private GameObject subAudio;
    // ��� ������� ������ҽ� ���� ������ ����
    [SerializeField]
    private AudioSource[] subAudios;
    // �޴� ���� Ŭ��
    [SerializeField]
    private AudioClip[] menuClips;
    // ��ȭâ ���� Ŭ��
    [SerializeField]
    private AudioClip dialogClips;

    // ȿ���� ����� ����� �ε���
    private int audioIndex = 0;

    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action menuMove;
    public static Action menuSelection;
    public static Action dialogComFirm;

    private void Start()
    {
        // ������� �ִ� ������ҽ��� ���� ȹ��
        subAudios = subAudio.GetComponents<AudioSource>();
        // �׼Ǹ޼��忡 �޼��� ����
        menuMove = () => { MenuMoveSound(); };
        menuSelection = () => { MenuSelectionSound(); };
        dialogComFirm = () => { DialogComFirm(); };
    }

    private void MenuMoveSound()
    {
        // ��� ����� �ε��� �� ����
        audioIndex++;
        // ����� �ҽ��� ���� ���� Ŀ���� 0���� �ʱ�ȭ
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // �޴� �̵� ���� ���
        subAudios[audioIndex].PlayOneShot(menuClips[1]);
    }

    private void MenuSelectionSound()
    {
        // ��� ����� �ε��� �� ����
        audioIndex++;
        // ����� �ҽ��� ���� ���� Ŀ���� 0���� �ʱ�ȭ
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // �޴� ���� ���� ���
        subAudios[audioIndex].PlayOneShot(menuClips[0]);
    }

    private void DialogComFirm()
    {
        // ��� ����� �ε��� �� ����
        audioIndex++;
        // ����� �ҽ��� ���� ���� Ŀ���� 0���� �ʱ�ȭ
        if (audioIndex >= subAudios.Length) audioIndex = 0;
        // ��ȭâ ��� ���� ���
        subAudios[audioIndex].PlayOneShot(dialogClips);
    }

}
