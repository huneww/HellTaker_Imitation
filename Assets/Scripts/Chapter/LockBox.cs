using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    // ��鸮�� ����
    [SerializeField]
    private float magnitude = 1f;
    // ���� �ð�
    [SerializeField]
    private float duration = 1f;

    Vector3 curPos;

    private void Start()
    {
        // ���� ��ġ ����
        curPos = transform.position;
    }

    public IEnumerator ObjectShake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            // ���� ��Ŭ ��ġ �� ȹ��
            Vector3 randomPos = Random.insideUnitSphere * magnitude;
            // z���� ������ ������ ����
            randomPos.Set(randomPos.x, randomPos.y, 0);
            // ��ġ�� ���� ��ġ�� ���� ��ġ�� ���� ������ ����
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        // ��ġ�� ���� ��ġ�� ����
        transform.position = curPos;
    }

}
