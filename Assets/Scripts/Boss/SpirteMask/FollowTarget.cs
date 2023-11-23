using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // 따라갈 타겟
    [SerializeField]
    private Transform target;

    private void Update()
    {
        // 타겟의 y축값만 받아서 이동
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }

}
