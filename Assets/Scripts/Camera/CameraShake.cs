using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // 흔들리는 세기
    [SerializeField]
    private float magnitude = 1f;
    // 지속 시간
    [SerializeField]
    private float duration = 1f;

    // 다른 스크립트에서 접근을 위한 액션 변수
    public static System.Action cameraShake;

    Camera main;
    Vector3 curPos;

    private void Start()
    {
        // 메인 카메라 획득
        main = Camera.main;
        // 카메라 위치 저장
        curPos = main.transform.position;
        // 액션 변수 값 지정
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
            // 랜덤 서클 위치 값에 기존 위치를 더한 값을 위치로 저장
            main.transform.position = Random.insideUnitSphere * magnitude + curPos;
            timer += Time.deltaTime;
        }

        //위치를 기존 위치로 변경
        main.transform.position = curPos;
    }

}
