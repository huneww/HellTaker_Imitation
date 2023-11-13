using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    [SerializeField]
    private float magnitude = 1f;
    [SerializeField]
    private float duration = 1f;

    private Vector3 curPos;

    private void Start()
    {
        curPos = transform.position;
    }

    public IEnumerator Shake()
    {
        float timer = 0;

        while (timer <= duration)
        {
            yield return null;
            Vector2 pos = Random.insideUnitCircle * magnitude;
            Vector3 randomPos = new Vector3(pos.x, pos.y, curPos.z);
            transform.position = randomPos + curPos;
            timer += Time.deltaTime;
        }

        transform.position = curPos;
        Debug.Log("Object Shake");
    }

}
