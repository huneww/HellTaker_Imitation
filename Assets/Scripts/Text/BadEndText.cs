using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadEndText : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField, TextArea]
    private string badEndText;

    private void Start()
    {
        text = GetComponent<Text>();
        Debug.Log(GameManager.Instance.CurStage);
        text.text = badEndText;
        AudioManager.Instance.BadEnd();
    }

}
