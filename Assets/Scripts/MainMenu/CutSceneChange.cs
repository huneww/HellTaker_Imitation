using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneChange : MonoBehaviour
{
    [SerializeField]
    // 컷씬 이미지
    private Sprite[] sprites;
    // 컷씬 UI
    private Image image;
    // 현재 나오고있는 스프라이트 인덱스
    private int curImage = -1;
    // 다른 스크립트에서 접근을 위한 액션 변수
    public static Action ChangeImage;

    private void Start()
    {
        // 액션 메서드 연결
        ChangeImage = () => { ChangeCutScene(); };
        // 이미지 컴포넌트 획득
        image = GetComponent<Image>();
    }

    private void ChangeCutScene()
    {
        // 인덱스값 증가
        curImage++;
        // 인덱스값이 컷씬 이미지의 크기보다 크다면
        // 오류 메세지 출력
        if (curImage >= sprites.Length) throw new Exception("sprites overFloor");
        // 컷씬 이미지 변경
        else
        {
            CutScenePingPong.cutScenePingPong();
            image.sprite = sprites[curImage];
        }
        
    }

}
