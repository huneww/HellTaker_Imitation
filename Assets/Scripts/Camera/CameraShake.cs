using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float magnitude = 1f;
    [SerializeField]
    private float duration = 1f;

    public static System.Action cameraShake;

    Camera main;
    Vector3 curPos;

    private void Start()
    {
        main = Camera.main;
        curPos = main.transform.position;
        cameraShake = () => { StartShake(); };
    }

    private void StartShake()
    {
        StartCoroutine(CameraShaker());
    }

    private IEnumerator CameraShaker()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            main.transform.position = Random.insideUnitSphere * magnitude + curPos;
            timer += Time.deltaTime;
        }

        main.transform.position = curPos;
    }

}
