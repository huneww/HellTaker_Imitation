using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    // ��� ��ũ�� �ӵ�
    [SerializeField]
    private float moveSpeed;

    private MeshRenderer mesh;
    private float pos;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // ��� ȭ�� ��ũ��
        pos += (Time.deltaTime * moveSpeed) * -1;

        mesh.material.mainTextureOffset = new Vector2(pos, 0);
        
    }

}
