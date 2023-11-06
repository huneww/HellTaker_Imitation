using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{
    // 오디오 소스
    private AudioSource audioSource;
    [SerializeField]
    // 닫히는 사운드
    private AudioClip closeSound;
    [SerializeField]
    // 열리는 사운드
    private AudioClip openSound;

    private void Start()
    {
        // 오디오 소스 컴포넌트 획득
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 닫히는 사운드 재생
    /// 애니메이터 이벤트로 실행
    /// </summary>
    public void CloseSound()
    {
        audioSource.PlayOneShot(closeSound);
    }

    /// <summary>
    /// 열리는 사운드 재생
    /// 애니메이터 이벤트로 실행
    /// </summary>
    public void OpenSound()
    {
        audioSource.PlayOneShot(openSound);
    }

    /// <summary>
    /// 문 오브젝트 비활성화
    /// 애니메이터 이벤트로 실행
    /// </summary>
    public void DoorDisalbe()
    {
        gameObject.SetActive(false);
    }

}
