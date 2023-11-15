using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    // 흔들리는 세기
    [SerializeField]
    private float magnitude = 1f;
    // 지속 시간
    [SerializeField]
    private float duration = 1f;

    Vector3 curPos;

    private void Start()
    {
        // 현재 위치 저장
        curPos = transform.position;
    }

    public IEnumerator ObjectShake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            // 랜덤 서클 위치 값 획득
            Vector3 randomPos = Random.insideUnitSphere * magnitude;
            // z값을 임의의 값으로 변경
            randomPos.Set(randomPos.x, randomPos.y, 0);
            // 위치를 기존 위치에 랜덤 위치를 더한 값으로 변경
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        // 위치를 기존 위치로 변경
        transform.position = curPos;
    }

}
