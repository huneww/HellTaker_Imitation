using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private bool isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� ��ģ�ٸ�
        if (collision.transform.CompareTag("Player"))
        {
            // ���� �ö���ִ� ���¶��
            if (isOn)
            {
                // �÷��̾� ���� ����Ʈ ����
                GameManager.Instance.SpawnBlood(transform.position);
            }
        }
    }

}
