using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JGIntroChainMove : MonoBehaviour
{
    // �̵� ��ġ
    [SerializeField]
    private Vector3 targetPos = Vector3.zero;
    // �̵� �ð�
    [SerializeField]
    private float moveTime = 1f;
    // ���� �Ÿ�
    [SerializeField]
    private float pingPongDis = 5f;

    private RectTransform rect;
    private Image image;

    private IEnumerator Start()
    {
        // �� ������Ʈ ȹ��
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        float curTime = 0;
        float percent = 0;

        // ���� ��ġ ����
        Vector3 curPos = rect.localPosition;
        Vector3 pingPongPos = Vector3.zero;

        // ������ ü���̶��
        if (gameObject.name.Contains("R"))
        {
            // x���� ����, y���� ����
            pingPongPos.Set(targetPos.x + pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }
        // ���� ü���̶��
        else if (gameObject.name.Contains("L"))
        {
            // x,y�� �Ѵ� ����
            pingPongPos.Set(targetPos.x - pingPongDis, targetPos.y - pingPongDis, targetPos.z);
        }

        // ü�� ������ġ���� �̵�
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            percent = curTime / moveTime;
            rect.localPosition = Vector3.Lerp(curPos, pingPongPos, percent);
            yield return null;
        }

        // ������ġ ������ġ�� ����
        rect.localPosition = pingPongPos;
        curPos = pingPongPos;
        // ����ð�, �ۼ�Ʈ ���� �ʱ�ȭ
        curTime = 0;
        percent = 0;
        // �̹��� ���� ����
        Color color = image.color;

        // ���� �̵� ��ġ�� �̵�
        while (percent < 1f)
        {
            curTime += Time.deltaTime;
            // ���� �̵��ð����� ���ݴ� �ɸ��� ����
            percent = curTime / (moveTime * 1.25f);
            rect.localPosition = Vector3.Lerp(curPos, targetPos, percent);
            color.a = Mathf.Lerp(1, 0.63f, percent);
            image.color = color;
            yield return null;
        }

        rect.localPosition = targetPos;
        color.a = 0.63f;
        image.color = color;
    }

}
