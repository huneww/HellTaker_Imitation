using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 타겟 오브젝트
    [SerializeField]
    private Transform target;
    // 바운드 콜라이더
    [SerializeField]
    private BoxCollider2D boxCollider;

    private float minBound;
    private float maxBound;
    private float halfHeight;

    private void Start()
    {
        // 바운드 Y축의 최대, 최소 값 획득
        minBound = boxCollider.bounds.min.y;
        maxBound = boxCollider.bounds.max.y;
        // 카메라 orthographic크기 획득
        halfHeight = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        // 타겟이 없다면 메서드 종료
        if (target == null) return;

        // 타겟의 y축위치로 카메라 이동
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        // 카메라가 일정 위치를 벗어나지 못하도록 조정
        float clampY = Mathf.Clamp(transform.position.y, minBound + halfHeight, maxBound - halfHeight);
        // 조정된 위치를 카메라에 위치로 변경
        transform.position = new Vector3(transform.position.x, clampY, transform.position.z);

    }
}
