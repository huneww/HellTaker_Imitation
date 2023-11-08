using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneChange : MonoBehaviour
{
    [SerializeField]
    // �ƾ� �̹���
    private Sprite[] sprites;
    // �ƾ� UI
    private Image image;
    // ���� �������ִ� ��������Ʈ �ε���
    private int curImage = -1;
    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action ChangeImage;

    private void Start()
    {
        // �׼� �޼��� ����
        ChangeImage = () => { ChangeCutScene(); };
        // �̹��� ������Ʈ ȹ��
        image = GetComponent<Image>();
    }

    private void ChangeCutScene()
    {
        // �ε����� ����
        curImage++;
        // �ε������� �ƾ� �̹����� ũ�⺸�� ũ�ٸ�
        // ���� �޼��� ���
        if (curImage >= sprites.Length) throw new Exception("sprites overFloor");
        // �ƾ� �̹��� ����
        else
        {
            CutScenePingPong.cutScenePingPong();
            image.sprite = sprites[curImage];
        }
        
    }

}
