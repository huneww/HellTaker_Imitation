using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePlosionController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle_Heart;
    [SerializeField]
    private ParticleSystem particle_Star;

    private Animator animator;
    private bool isPlay = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ReleaseLovePlosion"))
        {

            if (!isPlay)
            {
                particle_Heart.Play();
                particle_Star.Play();
                isPlay = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                StartCoroutine(StartStageChange());
            }
        }
    }

    private IEnumerator StartStageChange()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GameManager.Instance.StageChange(false));
        Destroy(this.gameObject);
    }

}
