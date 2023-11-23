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
    /// Ư�� �ð����� �ݶ��̴� Ȱ��ȭ, ���� ���
    /// </summary>
    public void ColliderEnable()
    {
        boxCollider.enabled = true;
        JGBossAudioManager.Instance.ChainBlink();
    }

    /// <summary>
    /// ü�� ����
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹�ϸ�
        if (collision.transform.CompareTag("Player"))
        {
            // �÷��̾� ��Ʈ ���� ���
            JGBossAudioManager.Instance.PlayerHit();
            // ���� ����Ʈ ��ȯ
            JGBossGameManager.Instance.SpawnBlood(JGBossGameManager.Instance.Player.transform.position);
            // �÷��̾� ���� ����
            JGBossGameManager.Instance.PlayerColorChange();
            // �÷��̾� ü�� ����
            JGBossGameManager.Instance.HealthMinus();
            // ī�޶� ����ũ
            CameraShake.cameraShake();
        }
    }

}
