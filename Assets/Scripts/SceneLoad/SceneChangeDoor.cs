using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeDoor : MonoBehaviour
{

    // �̱��� ����
    private static SceneChangeDoor instance;
    public static SceneChangeDoor Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    [SerializeField]
    // �� �ִϸ�����
    private Animator animator;
    [SerializeField]
    // �� ������Ʈ
    private GameObject door;

    // �ε��� �� �̸�
    private string loadSceneName;
    // �� �ִϸ��̼� �Ķ����
    private int openHash = Animator.StringToHash("Open");
    private int closeHash = Animator.StringToHash("Close");

    private void Awake()
    {
        // �̱��� ����
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// ������ �ִϸ��̼� ����
    /// </summary>
    public void PlayOpenAnimation()
    {
        animator.SetTrigger(openHash);
    }

    /// <summary>
    /// ������ �ִϸ��̼�, �� �ε� ����
    /// </summary>
    /// <param name="loadScene">�ε��� �� �̸�</param>
    public void PlayCloseAnimation(string loadScene)
    {
        // �ε��� �� �̸� ����
        loadSceneName = loadScene;
        // �� ������Ʈ Ȱ��ȭ
        door.SetActive(true);
        // ������ �ִϸ��̼� ����
        animator.SetTrigger(closeHash);
        // �� �ε� �Ϸ� �ݹ� �޼��� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
        // �� �ε� �ڷ�ƾ ����
        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// �� �ε� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        // �񵿱� �ε����� �� �ε�
        var op = SceneManager.LoadSceneAsync(loadSceneName);
        // �� �ε� �Ϸ�ǵ� ��� ���
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            // ������ �ִϸ��̼� ������ ���� ���
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // ���� ������ �ε�
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    /// <summary>
    /// �� �ε� �Ϸ� �ݺ� �޼���
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // �ε� ���� �ҷ����ߴ� ���̸� ��
        if (arg0.name == loadSceneName)
        {
            // ������ �ִϸ��̼� ����
            PlayOpenAnimation();
            // �ݹ� �޼��� ����
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

}
