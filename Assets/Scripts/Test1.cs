using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position = new Vector3(transform.position.x + 1, transform.position.y);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.position = new Vector3(transform.position.x - 1, transform.position.y);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            transform.position = new Vector3(transform.position.x, transform.position.y + 1);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            transform.position = new Vector3(transform.position.x, transform.position.y - 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("UpDownSpike"))
        {
            //Debug.Log("Player in UpDownSpike");
        }
    }
}
