using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 겹친다면
        if (collision.transform.CompareTag("Player"))
        {
            // 현재 올라와있는 상태라면
            if (isOn)
            {
                // 플레이어 출혈 이펙트 생성
                GameManager.Instance.SpawnBlood(transform.position);
            }
        }
    }

}
