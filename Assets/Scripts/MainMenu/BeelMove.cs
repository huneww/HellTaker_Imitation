using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeelMove : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 1f;
    [SerializeField]
    private Vector3 targetPos = Vector3.zero;

    private void Start()
    {
        // ���θ޴� ��ũ��Ʈ�� ��������Ʈ�� �޼��� �߰�
        MainMenuManager.moveToCenter += Move;
    }

    private void OnDestroy()
    {
        // �ı���� ��������Ʈ���� �޼��� ����
        MainMenuManager.moveToCenter -= Move;
    }

    public void Move()
    {
        // �ڷ�ƾ ȣ��
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = transform.position;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            transform.position = Vector3.Lerp(curPos, targetPos, percent);
        }
        transform.position = targetPos;
    }

}
