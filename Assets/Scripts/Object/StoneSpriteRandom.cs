using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpriteRandom : MonoBehaviour
{
    [SerializeField]
    private Sprite[] stoneSprites;

    private void Start()
    {
       GetComponent<SpriteRenderer>().sprite = stoneSprites[Random.Range(0, stoneSprites.Length)];
    }
}
