using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeDoor : MonoBehaviour
{

    // 싱글톤 패턴
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
    // 문 애니메이터
    private Animator animator;
    [SerializeField]
    // 문 오브젝트
    private GameObject door;

    // 로딩할 씬 이름
    private string loadSceneName;
    // 문 애니메이션 파라미터
    private int openHash = Animator.StringToHash("Open");
    private int closeHash = Animator.StringToHash("Close");

    private void Awake()
    {
        // 싱글톤 생성
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// 열리는 애니메이션 실행
    /// </summary>
    public void PlayOpenAnimation()
    {
        animator.SetTrigger(openHash);
    }

    /// <summary>
    /// 닫히는 애니메이션, 씬 로딩 실행
    /// </summary>
    /// <param name="loadScene">로딩할 씬 이름</param>
    public void PlayCloseAnimation(string loadScene)
    {
        // 로딩할 씬 이름 저장
        loadSceneName = loadScene;
        // 문 오브젝트 활성화
        door.SetActive(true);
        // 닫히는 애니메이션 실행
        animator.SetTrigger(closeHash);
        // 씬 로딩 완료 콜백 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 씬 로딩 코루틴 실행
        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// 씬 로딩 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        // 비동기 로딩으로 씬 로드
        var op = SceneManager.LoadSceneAsync(loadSceneName);
        // 씬 로딩 완료되도 잠시 대기
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            // 닫히는 애니메이션 끝날때 까지 대기
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // 씬을 완전히 로딩
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    /// <summary>
    /// 씬 로딩 완료 콜벡 메서드
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        // 로딩 씬과 할려고했던 씬이름 비교
        if (arg0.name == loadSceneName)
        {
            // 열리는 애니메이션 실행
            PlayOpenAnimation();
            // 콜백 메서드 제거
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

}
