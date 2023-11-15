using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpriteRandom : MonoBehaviour
{
    // 변경할 스프라이트
    [SerializeField]
    private Sprite[] stoneSprites;

    private void Start()
    {
       // 시작시 벽돌 스프라이트 랜덤 변경
       GetComponent<SpriteRenderer>().sprite = stoneSprites[Random.Range(0, stoneSprites.Length)];
    }
}
