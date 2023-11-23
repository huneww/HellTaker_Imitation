using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    // 다리 이동 속도
    [SerializeField]
    private float moveSpeed = 1f;
    // 이동할 오브젝트 메쉬
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
            // 이동할 오브젝트의 오프셋값 조정
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
    /// 다리 정지
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopRoutine()
    {
        JGBossGameManager.Instance.IsMove = false;
        JGBossAudioManager.Instance.MainAudioStop();
        float curTime = 0;
        float percent = 0;

        // 현재 오프셋값 저장
        Vector2 curBridgeOffset = bridgeMesh[0].material.mainTextureOffset;
        Vector2 curChainOffset = bridgeMesh[3].material.mainTextureOffset;
        // 현재 오프셋값에 따라 이동 위치 설정
        // 오프셋값의 절대값 저장
        float value = Mathf.Abs(curBridgeOffset.y);
        // 절대값의 소숫점을 버리고 저장
        float floor = Mathf.FloorToInt(value);
        // 절대값과 소숫점을 버린 숫자의 차이를 비교
        // 차이가 0.5 초과이면 절대값에 + 1을 한값을 저장
        // 차이가 0.5 이하이면 소숫점을 버린 값을 저장
        // 텍스처가 음수로 이동하기 때문에 계산후 값을 음수로 변경
        float changeY = -(value - floor > 0.5f ? floor + 1f : floor);

        // 오프셋의 y값을 일정값 변경 후 저장
        Vector2 bridgeTargetOffset = new Vector2(curBridgeOffset.x, changeY);
        Vector2 chainTargetOffset = new Vector2(curChainOffset.x, changeY);


        // 변경된 위치까지 이동
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
