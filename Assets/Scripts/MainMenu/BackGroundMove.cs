using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    // 배경 스크롤 속도
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
        // 배경 화면 스크롤
        pos += (Time.deltaTime * moveSpeed) * -1;

        mesh.material.mainTextureOffset = new Vector2(pos, 0);
        
    }

}
