using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    // �޴� �⺻ ���� ����
    private Color idelColor;
    [SerializeField]
    // �޴� ���̶���Ʈ ���� ����
    private Color hightLightColor;
    [SerializeField]
    // ���� ���� ���� �޴�
    private int curSelectMenu = 0;
    [SerializeField]
    // �޴� �̹���
    private Image[] imageChildMenus;
    [SerializeField]
    // �޴� �ؽ�Ʈ
    private Text[] textChildMenus;

    private void Awake()
    {
        // �ڽ� ��ü���� ������Ʈ ȹ��
        imageChildMenus = GetComponentsInChildren<Image>();
        textChildMenus = GetComponentsInChildren<Text>();
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

        // �� Ű�� ������
        if (Input.GetKeyDown(MenuManager.Instance.UpKey[0]) || Input.GetKeyDown(MenuManager.Instance.UpKey[1]))
        {
            // 0���� ũ�ٸ� ���� �޴� �ε��� �� ����
            curSelectMenu = curSelectMenu > 0 ? curSelectMenu - 1 : curSelectMenu;
            // ���̶���Ʈ ����
            HightLight();
        }
        // �Ʒ� Ű�� ������
        else if (Input.GetKeyDown(MenuManager.Instance.DownKey[0]) || Input.GetKeyDown(MenuManager.Instance.DownKey[1]))
        {
            // �޴� ũ�⺸�� ������ ���� �޴� �ε��� �� ����
            curSelectMenu = curSelectMenu < imageChildMenus.Length - 1 ? curSelectMenu + 1 : curSelectMenu;
            // ���̶���Ʈ ����
            HightLight();
        }

        // ���� Ű�� ������
        if (Input.GetKeyDown(MenuManager.Instance.EnterKey))
        {
            // ���� �޴��� �´� �ڵ� ����
            MenuManager.Instance.SelectMenu(curSelectMenu);
        }
    }

    private void HightLight()
    {
        // ��� �޴� �÷� �⺻ ���·� ����
        for (int i = 0; i < textChildMenus.Length; i++)
        {
            imageChildMenus[i].GetComponent<Image>().color = idelColor;
            textChildMenus[i].GetComponentInChildren<Text>().color = Color.gray;
        }

        // ���� ���� ���� �޴��� ���̶���Ʈ �÷��� ����
        imageChildMenus[curSelectMenu].GetComponent<Image>().color = hightLightColor;
        textChildMenus[curSelectMenu].GetComponentInChildren<Text>().color = Color.white;
    }

}
