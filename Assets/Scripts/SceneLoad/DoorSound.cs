using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    // ����� �ҽ�
    private AudioSource audioSource;
    [SerializeField]
    // ������ ����
    private AudioClip closeSound;
    [SerializeField]
    // ������ ����
    private AudioClip openSound;

    private void Start()
    {
        // ����� �ҽ� ������Ʈ ȹ��
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ������ ���� ���
    /// �ִϸ����� �̺�Ʈ�� ����
    /// </summary>
    public void CloseSound()
    {
        audioSource.PlayOneShot(closeSound);
    }

    /// <summary>
    /// ������ ���� ���
    /// �ִϸ����� �̺�Ʈ�� ����
    /// </summary>
    public void OpenSound()
    {
        audioSource.PlayOneShot(openSound);
    }

    /// <summary>
    /// �� ������Ʈ ��Ȱ��ȭ
    /// �ִϸ����� �̺�Ʈ�� ����
    /// </summary>
    public void DoorDisalbe()
    {
        gameObject.SetActive(false);
    }

}
