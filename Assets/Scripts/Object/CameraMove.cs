using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private BoxCollider2D boxCollider;

    private float minBound;
    private float maxBound;
    private float halfHeight;

    private void Start()
    {
        minBound = boxCollider.bounds.min.y;
        maxBound = boxCollider.bounds.max.y;
        halfHeight = GetComponent<Camera>().orthographicSize;
    }

    private void Update()
    {
        if (target == null) return;

        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        float clampY = Mathf.Clamp(transform.position.y, minBound + halfHeight, maxBound - halfHeight);
        transform.position = new Vector3(transform.position.x, clampY, transform.position.z);

    }
}
