using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndText : MonoBehaviour
{
    // ��� ��� �ؽ�Ʈ
    [SerializeField]
    private Text text;
    // ����� ���
    [SerializeField, TextArea]
    private string badEndText;

    private void Start()
    {
        // �ؽ�Ʈ ������Ʈ ȹ��
        text = GetComponent<Text>();
        // �ؽ�Ʈ ��� ����
        text.text = badEndText;
        // ���忣�� ���� ���
        AudioManager.Instance.BadEnd();
    }

}
