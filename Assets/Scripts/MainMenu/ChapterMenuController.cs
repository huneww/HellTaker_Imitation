using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterMenuController : MonoBehaviour
{
    [SerializeField]
    // �Ϲ� �������� �⺻ ���� �÷�
    private Color idelColor;
    [SerializeField]
    // �Ϲ� �������� ���̶���Ʈ ���� �÷�
    private Color hightLightColor;
    [SerializeField]
    // ����Ʈ�� �������� �⺻ ���� �÷�
    private Color exIdelColor;
    [SerializeField]
    // ����Ʈ�� �������� ���̶���Ʈ ���� �÷�
    private Color exHightLightColor;
    [SerializeField]
    // ���� ���� ���� �������� �ε���
    private int curSelectMenu = 0;
    [SerializeField]
    // �������� �̹���
    private Image[] imageChildes;
    [SerializeField]
    // �������� �ؽ�Ʈ
    private Text[] textChildes;

    private void Awake()
    {
        // �ڽ� ��ü���� ������Ʈ ȹ��
        imageChildes = GetComponentsInChildren<Image>();
        textChildes = GetComponentsInChildren<Text>();
    }

    private void OnEnable()
    {
        // Ȱ��ȭ�� ���̶���Ʈ ����
        HightLight();
    }

    private void Update()
    {
        // ��Ȱ��ȭ ���¸� �޼��� ����
        if (!gameObject.activeSelf) return;

        // ������ Ű�� ������
        if (Input.GetKeyDown(MenuManager.Instance.RightKey[0]) || Input.GetKeyDown(MenuManager.Instance.RightKey[1]))
        {
            // �������� ���� �۴ٸ� �ε��� �� ����
            curSelectMenu = curSelectMenu < imageChildes.Length - 1 ? curSelectMenu + 1 : curSelectMenu;
            // ���̶���Ʈ ����
            HightLight();
            // �̵� ���Ʈ ���
            MenuAudioManager.ChapterMove();
        }
        // ���� Ű�� ������
        else if (Input.GetKeyDown(MenuManager.Instance.LeftKey[0]) || Input.GetKeyDown(MenuManager.Instance.LeftKey[1]))
        {
            //0���� ũ�ٸ� ������ �� ����
            curSelectMenu = curSelectMenu > 0 ? curSelectMenu - 1 : curSelectMenu;
            // ���̶���Ʈ ����
            HightLight();
            // �̵� ���Ʈ ���
            MenuAudioManager.ChapterMove();
        }
        // ���� Ű�� ������
        else if (Input.GetKeyDown(MenuManager.Instance.EnterKey))
        {
            // ���� �������� �ε��� + 1���� �Ű������� �� �ε�
            MenuManager.Instance.StartStage(curSelectMenu + 1);
            // ���� ���� ���
            MenuAudioManager.ChapterSelect();
        }
        // ESC Ű�� ������
        else if (Input.GetKeyDown(MenuManager.Instance.ESCKey))
        {
            // �ٽ� �޴�
            MenuManager.Instance.ReturnToMenu();
            // ���� ���� ���
            MenuAudioManager.ChapterSelect();
        }
    }

    private void HightLight()
    {
        // ��� �������� ���� �⺻ ���·� ����
        for (int i = 0; i < imageChildes.Length; i++)
        {
            // ����Ʈ�� �������� ���
            if (i == imageChildes.Length - 1)
            {
                // ����Ʈ�� �÷��� ����
                imageChildes[i].color = exIdelColor;
                textChildes[i].color = exIdelColor;
            }
            else
            {
                // �Ϲ� �������� �÷��� ����
                imageChildes[i].color = idelColor;
                textChildes[i].color = Color.gray;
            }
        }

        // ���� ������ ���������� ����Ʈ�� �������� ���
        if (curSelectMenu == imageChildes.Length - 1)
        {
            // ����Ʈ�� �������� �÷� ����
            imageChildes[curSelectMenu].color = exHightLightColor;
            textChildes[curSelectMenu].color = exHightLightColor;
        }
        else
        {
            // �Ϲ� �������� �÷��� ����
            imageChildes[curSelectMenu].color = hightLightColor;
            textChildes[curSelectMenu].color = Color.white;
        }
    }

}
