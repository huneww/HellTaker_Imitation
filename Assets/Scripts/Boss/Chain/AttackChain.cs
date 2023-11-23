using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChain : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// 특정 시간때에 콜라이더 활성화, 사운드 재생
    /// </summary>
    public void ColliderEnable()
    {
        boxCollider.enabled = true;
        JGBossAudioManager.Instance.ChainBlink();
    }

    /// <summary>
    /// 체인 제거
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면
        if (collision.transform.CompareTag("Player"))
        {
            // 플레이어 히트 사운드 재생
            JGBossAudioManager.Instance.PlayerHit();
            // 출혈 이펙트 소환
            JGBossGameManager.Instance.SpawnBlood(JGBossGameManager.Instance.Player.transform.position);
            // 플레이어 색상 변경
            JGBossGameManager.Instance.PlayerColorChange();
            // 플레이어 체력 감소
            JGBossGameManager.Instance.HealthMinus();
            // 카메라 쉐이크
            CameraShake.cameraShake();
        }
    }

}
