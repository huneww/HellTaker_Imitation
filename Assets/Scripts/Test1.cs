using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public GameObject test;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(test.activeSelf);
            test.SetActive(!test.activeSelf);
            Debug.Log(test.activeSelf);
        }
    }
}
