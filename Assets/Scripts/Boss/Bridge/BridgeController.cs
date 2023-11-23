using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    // �ٸ� �̵� �ӵ�
    [SerializeField]
    private float moveSpeed = 1f;
    // �̵��� ������Ʈ �޽�
    [SerializeField]
    private MeshRenderer[] bridgeMesh;

    private float bridgePos;
    private float chainPos;

    public static System.Action stop;

    private void Start()
    {
        stop = () => { Stop(); };
    }

    private void Update()
    {
        if (!JGBossGameManager.Instance.IsMove) return;

        bridgePos += Time.deltaTime * moveSpeed;
        chainPos += Time.deltaTime * moveSpeed * 2f;

        foreach (var bridge in bridgeMesh)
        {
            // �̵��� ������Ʈ�� �����°� ����
            if (bridge.gameObject.name.Contains("Bridge"))
            {
                bridge.material.mainTextureOffset = new Vector2(0, bridgePos);
            }
            else
            {
                bridge.material.mainTextureOffset = new Vector2(0, chainPos);
            }
        }
    }

    private void Stop()
    {
        StartCoroutine(StopRoutine());
    }

    /// <summary>
    /// �ٸ� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopRoutine()
    {
        JGBossGameManager.Instance.IsMove = false;
        JGBossAudioManager.Instance.MainAudioStop();
        float curTime = 0;
        float percent = 0;

        // ���� �����°� ����
        Vector2 curBridgeOffset = bridgeMesh[0].material.mainTextureOffset;
        Vector2 curChainOffset = bridgeMesh[3].material.mainTextureOffset;
        // ���� �����°��� ���� �̵� ��ġ ����
        // �����°��� ���밪 ����
        float value = Mathf.Abs(curBridgeOffset.y);
        // ���밪�� �Ҽ����� ������ ����
        float floor = Mathf.FloorToInt(value);
        // ���밪�� �Ҽ����� ���� ������ ���̸� ��
        // ���̰� 0.5 �ʰ��̸� ���밪�� + 1�� �Ѱ��� ����
        // ���̰� 0.5 �����̸� �Ҽ����� ���� ���� ����
        // �ؽ�ó�� ������ �̵��ϱ� ������ ����� ���� ������ ����
        float changeY = -(value - floor > 0.5f ? floor + 1f : floor);

        // �������� y���� ������ ���� �� ����
        Vector2 bridgeTargetOffset = new Vector2(curBridgeOffset.x, changeY);
        Vector2 chainTargetOffset = new Vector2(curChainOffset.x, changeY);


        // ����� ��ġ���� �̵�
        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / JGBossGameManager.Instance.StopMoveTime;
            foreach (var bridge in bridgeMesh)
            {
                if (bridge.gameObject.name.Contains("Bridge"))
                    bridge.material.mainTextureOffset = Vector2.Lerp(curBridgeOffset, bridgeTargetOffset, percent);
                else
                    bridge.material.mainTextureOffset = Vector2.Lerp(curChainOffset, chainTargetOffset, percent);
            }
        }

        bridgePos = bridgeTargetOffset.y;
        chainPos = chainTargetOffset.y;

    }

}
