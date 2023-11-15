using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ��鸮�� ����
    [SerializeField]
    private float magnitude = 1f;
    // ���� �ð�
    [SerializeField]
    private float duration = 1f;

    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static System.Action cameraShake;

    Camera main;
    Vector3 curPos;

    private void Start()
    {
        // ���� ī�޶� ȹ��
        main = Camera.main;
        // ī�޶� ��ġ ����
        curPos = main.transform.position;
        // �׼� ���� �� ����
        cameraShake = () => { StartShake(); };
    }

    private void StartShake()
    {
        StartCoroutine(CameraShaker());
    }

    private IEnumerator CameraShaker()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            // ���� ��Ŭ ��ġ ���� ���� ��ġ�� ���� ���� ��ġ�� ����
            main.transform.position = Random.insideUnitSphere * magnitude + curPos;
            timer += Time.deltaTime;
        }

        //��ġ�� ���� ��ġ�� ����
        main.transform.position = curPos;
    }

}
