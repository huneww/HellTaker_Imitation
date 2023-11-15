using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeel : MonoBehaviour
{
    [SerializeField]
    // �̵� ��ġ
    private Vector3 targetPos;
    [SerializeField]
    // �̵� �ð�
    private float moveTime;
    // ���� ��ġ
    private Vector2 curPos;
    // �ٸ� ��ũ��Ʈ���� ������ ���� �׼� ����
    public static Action move;
    public static Action<bool> active;

    private void Start()
    {
        // �׼� �޼��� ����
        move = () => { Move(); };
        active = (value) => { SetActive(value); };
        // ���� ��ġ ����
        curPos = transform.position;
    }

    private void Move()
    {
        // ���� �ڷ�ƾ ����
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        // �� ��ġ �̵�
        float curTime = 0;
        float percent = 0;

        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            transform.position = Vector3.Lerp(curPos, targetPos, percent);
            yield return null;
        }
        transform.position = targetPos;
    }

    private void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

}
