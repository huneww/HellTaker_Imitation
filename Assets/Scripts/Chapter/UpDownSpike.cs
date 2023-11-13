using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownSpike : MonoBehaviour
{
    [SerializeField]
    private bool isUp;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = isUp;
        animator.SetBool("isUp", isUp);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            SpikeChange();
    }

    public void SpikeChange()
    {
        isUp = !isUp;
        boxCollider.enabled = isUp;
        animator.SetBool("isUp", isUp);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Down"))
        {
            animator.Play(isUp ? "Spike_Up" : "Spike_Down", -1, 1f - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
        {
            animator.SetTrigger(isUp ? "Up" : "Down");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (isUp)
            {
                GameManager.Instance.SpawnBlood(transform.position);
            }
        }
    }


}
