using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // ���� Ÿ��
    [SerializeField]
    private Transform target;

    private void Update()
    {
        // Ÿ���� y�ప�� �޾Ƽ� �̵�
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }

}
