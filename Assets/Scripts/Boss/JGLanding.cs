using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGLanding : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f;

    // 애니메이션에서 접근을 위한 메서드
    public void StartDialog()
    {
        // 다이얼로그 시작 메서드 실행
        JGBossDialogManager.Instance.StartDialog();
    }

    // 랜딩 사운드 재생
    public void LandingSound()
    {
        // 랜딩 사운드
        JGBossAudioManager.Instance.JGLanding();

        // 먼지 이펙트 스폰
        int count = Random.Range(3, 5);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Random.insideUnitCircle * radius + (Vector2)transform.position;
            spawnPos.Set(spawnPos.x, spawnPos.y - 0.5f, spawnPos.z);
            JGBossGameManager.Instance.SpawnLandingDust(spawnPos, this.transform);
        }

        // 카메라 쉐이크
        CameraShake.cameraShake();
    }

}
