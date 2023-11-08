using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpriteMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetPos;
    [SerializeField]
    private float moveTime = 1f;
    [SerializeField]
    private GameObject[] texts;
    [SerializeField]
    private GameObject booper;

    private RectTransform rectTransform;
    private Image image;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public IEnumerator MoveCoroutine()
    {
        float curTime = 0;
        float percent = 0;
        Vector3 curPos = rectTransform.localPosition;
        Color color = image.color;

        while (percent < 1.0f)
        {
            yield return null;
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rectTransform.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(0, 255, percent);
            image.color = color;
        }

        rectTransform.localPosition = targetPos;
        color.a = 255;
        image.color = color;
        foreach (var text in texts)
            text.SetActive(true);
        booper.SetActive(true);
        AudioManager.Instance.DialogComFirm();
    }

}
