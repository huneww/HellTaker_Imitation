using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Ÿ�� ������Ʈ
    [SerializeField]
    private Transform target;
    // �ٿ�� �ݶ��̴�
    [SerializeField]
    private BoxCollider2D boxCollider;

    private float minBound;
    private float maxBound;
    private float halfHeight;

    private void Start()
    {
        // �ٿ�� Y���� �ִ�, �ּ� �� ȹ��
        minBound = boxCollider.bounds.min.y;
        maxBound = boxCollider.bounds.max.y;
        // ī�޶� orthographicũ�� ȹ��
        halfHeight = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        // Ÿ���� ���ٸ� �޼��� ����
        if (target == null) return;

        // Ÿ���� y����ġ�� ī�޶� �̵�
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        // ī�޶� ���� ��ġ�� ����� ���ϵ��� ����
        float clampY = Mathf.Clamp(transform.position.y, minBound + halfHeight, maxBound - halfHeight);
        // ������ ��ġ�� ī�޶� ��ġ�� ����
        transform.position = new Vector3(transform.position.x, clampY, transform.position.z);

    }
}
