using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Animator))]
public class SceneChangeDoor : MonoBehaviour
{
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
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;

    private AudioSource audioSource;
    private Animator animator;

    private int openHash = Animator.StringToHash("Open");
    private int closeHash = Animator.StringToHash("Close");

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        StartCoroutine(StartDoorClose());
    }

    public IEnumerator StartDoorClose()
    {
        animator.SetTrigger(closeHash);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(closeSound);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetTrigger(openHash);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(openSound);
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }
    
    public void SetOnable()
    {
        gameObject.SetActive(true);
    }

}
