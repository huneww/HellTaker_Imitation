using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPingPong : MonoBehaviour
{
    // x,y�� ������ �ð����� �ϴ��� Ȯ�� ����
    public bool sameTime;
    // x,y�� �̵� �ð�
    public float xTime = 1f;
    public float yTime = 1f;
    // x,y�� �ִϸ��̼� Ŀ��
    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    // �ؽ�Ʈ ��ƮƮ������
    private RectTransform tr;

    private void OnEnable()
    {
        // Ȱ��ȭ �ɶ� ���� �ڷ�ƾ ����
        StartCoroutine(Visible());
    }

    private void Awake()
    {
        // �ڷ�ƾ �������� ��ƮƮ������ ������Ʈ ȹ��
        tr = GetComponent<RectTransform>();
    }

    private IEnumerator Visible()
    {
        // ��� �ð� ���� ����
        float curtime = 0f;
        // xTime �� yTime�� ū ���� ����
        float maxTime = xTime >= yTime ? xTime : yTime;

        while (curtime < maxTime)
        {
            curtime += Time.deltaTime;
            // Ŀ���� time�� ���ļ� ����
            float xSize = xCurve.Evaluate(curtime / xTime);
            float ySize = yCurve.Evaluate(curtime / yTime);
            // Ŀ������ ȹ���� ���� ����� ����
            tr.localScale = new Vector3(xSize, ySize, 1);
            yield return null;
        }

        // ����� Ŀ���� �� ������ ������ ����
        tr.localScale = new Vector3(xCurve.Evaluate(1), yCurve.Evaluate(1), 1);

        yield return null;
    }
}