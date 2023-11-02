using UnityEngine;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{
    [Space(30)]
    [SerializeField]
    // ���� ���� ���� �޴�
    private int curSelectionMenu = 0;
    [SerializeField]
    // �޴� UI
    private Image[] menuObjects;
    [SerializeField]
    // �޴� �⺻ ���� �÷�
    private Color idelColor;
    [SerializeField]
    // �޴� ���� ���� �÷�
    private Color hightLightColor;
    [SerializeField]
    private GameObject SlectionChapterParent;
    [SerializeField]
    // ���� ���� �޴� �ε���
    private int curSlectionChapter = 0;

    private void Start()
    {
        // �޴� UI �θ� ������Ʈ���� �޴� UI�� Image������Ʈ ȹ��
        menuObjects = MainMenuManager.Instance.mainMenuGroup.GetComponentsInChildren<Image>();
        // �޴� UI�� ù��°�� ���� �÷��� ����
        menuObjects[0].color = hightLightColor;
    }

    private void Update()
    {
        // �޴� UI �θ� ������Ʈ�� ��Ȱ��ȭ ���¸� �޼��� ����
        if (MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha > 0)
        {
            // ���� �ö󰡴� Ű�� ������
            if (Input.GetKeyDown(MainMenuManager.Instance.UpKey_1) || Input.GetKeyDown(MainMenuManager.Instance.UpKey_2))
            {
                // ���� �������� �޴� ������ -1
                // 0���� ������ ���� ���� ����
                curSelectionMenu = curSelectionMenu > 0 ? curSelectionMenu - 1 : curSelectionMenu;
                // �޴� �̵� ���� ���
                SoundManager.menuMove();
                // �޴� ���̶���Ʈ ����
                MenuHightLight();
            }
            // �Ʒ��� �������� Ű�� ������
            else if (Input.GetKeyDown(MainMenuManager.Instance.DownKey_1) || Input.GetKeyDown(MainMenuManager.Instance.DownKey_2))
            {
                // ���� �������� �޴� ������ +1
                // �޴��� ���� ���� ������ ���� ���� ����
                curSelectionMenu = curSelectionMenu < menuObjects.Length - 1 ? curSelectionMenu + 1 : curSelectionMenu;
                // �޴� �̵� ���� ���
                SoundManager.menuMove();
                // �޴� ���̶���Ʈ ����
                MenuHightLight();
            }
            // �޴� ����
            if (Input.GetKeyDown(MainMenuManager.Instance.EnterKey))
            {
                // �޴� ���� ���� ���
                SoundManager.menuSelection();
                // �޴��� �´� �ڵ� ����
                EnterMenu(curSelectionMenu);
            }
        }
        // é�� ���� �޴��� Ȱ��ȭ ���¸�
        else if (MainMenuManager.Instance.chapterParent.GetComponent<CanvasGroup>().alpha > 0)
        {
            if (Input.GetKeyDown(MainMenuManager.Instance.RightKey_1) || Input.GetKeyDown(MainMenuManager.Instance.RightKey_2))
            {
                curSlectionChapter = curSlectionChapter < 10 ? curSlectionChapter + 1 : 0;
                SoundManager.menuMove();
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
            }
            else if (Input.GetKeyDown(MainMenuManager.Instance.LeftKey_1) || Input.GetKeyDown(MainMenuManager.Instance.LeftKey_2))
            {
                curSlectionChapter = curSlectionChapter > 0 ? curSlectionChapter - 1 : 10;
                SoundManager.menuMove();
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
            }

            if (Input.GetKeyDown(MainMenuManager.Instance.EnterKey))
            {
                SoundManager.menuSelection();
                MainMenuManager.Instance.STARTSTAGE(curSlectionChapter);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenuManager.Instance.chapterParent.GetComponent<CanvasGroup>().alpha = 0;
                MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }

    private void MenuHightLight()
    {
        // ������ ������Ʈ�� ������ ���� ����� �����ϸ� �޼��� ����
        if (menuObjects[curSelectionMenu].color == hightLightColor) return;

        // �޴� ������Ʈ�� ���� �⺻ �������� ����
        foreach (var menu in menuObjects)
            menu.color = idelColor;
        // ������ �޴� ���� ����
        menuObjects[curSelectionMenu].color = hightLightColor;
    }

    private void EnterMenu(int stage)
    {
        switch (stage)
        {
            case 0:
                // ù ��° �������� ����
                MainMenuManager.Instance.STARTSTAGE(1);
                break;
            case 1:
                MainMenuManager.Instance.mainMenuGroup.GetComponent<CanvasGroup>().alpha = 0;
                // ������ �������� ����
                MainMenuManager.Instance.SelectChapter(curSlectionChapter);
                break;
            case 2:
                // ���� ����
                MainMenuManager.Instance.EXIT();
                break;
        }
    }

}
