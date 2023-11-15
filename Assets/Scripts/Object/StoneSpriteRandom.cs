using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpriteRandom : MonoBehaviour
{
    // ������ ��������Ʈ
    [SerializeField]
    private Sprite[] stoneSprites;

    private void Start()
    {
       // ���۽� ���� ��������Ʈ ���� ����
       GetComponent<SpriteRenderer>().sprite = stoneSprites[Random.Range(0, stoneSprites.Length)];
    }
}
