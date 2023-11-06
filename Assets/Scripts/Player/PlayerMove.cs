using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveXOffset = 1f;
    [SerializeField]
    private float moveYOffset = 1f;
    [SerializeField]
    private LayerMask wallLayer;

    private Animator animator;
    private Collider2D hitWall;

    private int clearHash = Animator.StringToHash("Clear");
    private int moveHash = Animator.StringToHash("Move");
    private int kickHash = Animator.StringToHash("Kick");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 checkPos = new Vector3(transform.position.x + moveXOffset, transform.position.y);
            hitWall = Physics2D.OverlapCircle(checkPos, 0.8f, wallLayer);
            Debug.Log(checkPos);
            if (hitWall == null)
                Debug.Log("Right Nothing");
            else
                Debug.Log("Right is Wall");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 checkPos = new Vector3(transform.position.x - moveXOffset, transform.position.y);
            hitWall = Physics2D.OverlapCircle(checkPos, 0.8f, wallLayer);
            Debug.Log(checkPos);
            if (hitWall == null)
                Debug.Log("Left Nothing");
            else
                Debug.Log("Left is Wall");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y + moveYOffset);
            hitWall = Physics2D.OverlapCircle(checkPos, 0.8f, wallLayer);
            Debug.Log(checkPos);
            if (hitWall == null)
                Debug.Log("Up Nothing");
            else
                Debug.Log("Up is Wall");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 checkPos = new Vector3(transform.position.x, transform.position.y - moveYOffset);
            hitWall = Physics2D.OverlapCircle(checkPos, 0.8f, wallLayer);
            Debug.Log(checkPos);
            if (hitWall == null)
                Debug.Log("Down Nothing");
            else
                Debug.Log("Down is Wall");
        }
        hitWall = null;
    }

}
