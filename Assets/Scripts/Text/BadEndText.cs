using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndText : MonoBehaviour
{
    // 대사 출력 텍스트
    [SerializeField]
    private Text text;
    // 출력할 대사
    [SerializeField, TextArea]
    private string badEndText;

    private void Start()
    {
        // 텍스트 컴포넌트 획득
        text = GetComponent<Text>();
        // 텍스트 대사 변경
        text.text = badEndText;
        // 베드엔드 사운드 재생
        AudioManager.Instance.BadEnd();
    }

}
