using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutBackGround : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;

    public IEnumerator FadeOut()
    {
        float curTime = 0;
        float percent = 0;
        Image image = GetComponent<Image>();
        Color color = image.color;

        while (percent < 1f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / fadeTime;
            color.a = Mathf.Lerp(1, 0, percent);
            image.color = color;
        }

        color.a = 0;
        image.color = color;

        Destroy(this.gameObject, 0.25f);
    }
}
