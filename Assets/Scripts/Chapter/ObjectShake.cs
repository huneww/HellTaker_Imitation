using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    // 흔들리는 정도
    [SerializeField]
    private float magnitude = 1f;
    // 지속 시간
    [SerializeField]
    private float duration = 1f;

    private Vector3 curPos;

    private void Start()
    {
        // 현재 위치 저장
        curPos = transform.position;
    }

    public IEnumerator Shake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            // 랜덤 서클 위치값을 획득
            Vector2 pos = Random.insideUnitCircle * magnitude;
            // z축은 현재 오브젝트의 값으로 변경
            Vector3 randomPos = new Vector3(pos.x, pos.y, curPos.z);
            // 현재 오브젝트에 기존 위치에 랜덤 위치를 더한 값으로 저장
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        // 위치를 기존위리로 변경
        transform.position = curPos;
        Debug.Log("Object Shake");
    }

}
