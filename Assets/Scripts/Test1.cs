using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public GameObject group;

    public Transform[] findobjs;

    private void Start()
    {
        findobjs = group.GetComponentsInChildren<Transform>();
    }
}
