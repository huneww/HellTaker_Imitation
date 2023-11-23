using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGLanding : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f;

    // �ִϸ��̼ǿ��� ������ ���� �޼���
    public void StartDialog()
    {
        // ���̾�α� ���� �޼��� ����
        JGBossDialogManager.Instance.StartDialog();
    }

    // ���� ���� ���
    public void LandingSound()
    {
        // ���� ����
        JGBossAudioManager.Instance.JGLanding();

        // ���� ����Ʈ ����
        int count = Random.Range(3, 5);
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = Random.insideUnitCircle * radius + (Vector2)transform.position;
            spawnPos.Set(spawnPos.x, spawnPos.y - 0.5f, spawnPos.z);
            JGBossGameManager.Instance.SpawnLandingDust(spawnPos, this.transform);
        }

        // ī�޶� ����ũ
        CameraShake.cameraShake();
    }

}
