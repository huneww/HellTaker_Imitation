using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    // ��鸮�� ����
    [SerializeField]
    private float magnitude = 1f;
    // ���� �ð�
    [SerializeField]
    private float duration = 1f;

    private Vector3 curPos;

    private void Start()
    {
        // ���� ��ġ ����
        curPos = transform.position;
    }

    public IEnumerator Shake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            // ���� ��Ŭ ��ġ���� ȹ��
            Vector2 pos = Random.insideUnitCircle * magnitude;
            // z���� ���� ������Ʈ�� ������ ����
            Vector3 randomPos = new Vector3(pos.x, pos.y, curPos.z);
            // ���� ������Ʈ�� ���� ��ġ�� ���� ��ġ�� ���� ������ ����
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        // ��ġ�� ���������� ����
        transform.position = curPos;
        Debug.Log("Object Shake");
    }

}
