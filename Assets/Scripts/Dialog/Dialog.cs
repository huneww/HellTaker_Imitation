using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    [TextArea()]
    public string[] texts;
    public Sprite changesprite;
}
