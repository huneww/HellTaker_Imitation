using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBox : MonoBehaviour
{
    [SerializeField]
    private float magnitude = 1f;
    [SerializeField]
    private float duration = 1f;

    Vector3 curPos;

    private void Start()
    {
        curPos = transform.position;
    }

    public IEnumerator ObjectShake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            Vector3 randomPos = Random.insideUnitSphere * magnitude;
            randomPos.Set(randomPos.x, randomPos.y, 0);
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        transform.position = curPos;
    }

}
