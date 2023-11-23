using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpDownSpike : MonoBehaviour
{
    // ���� �ö���ִ��� Ȯ�� ����
    [SerializeField]
    private bool isUp;

    private BoxCollider2D boxCollider;
    private Animator animator;

    private void Start()
    {
        // �� ������Ʈ ȹ��
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // �ݶ��̴� isUp���� ���� ����
        boxCollider.enabled = isUp;
        // �ִϸ��̼� ����
        animator.SetBool("isUp", isUp);
    }

    public void Update()
    {

        // Ȱ��ǥ �Է½�
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)||
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            // ������ũ ���� ����
            SpikeChange();
    }

    public void SpikeChange()
    {
        isUp = !isUp;
        boxCollider.enabled = isUp;
        animator.SetBool("isUp", isUp);

        // ���� �ִϸ��̼��� �ö���ų�, �������� �ִϸ��̼��̸�
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Up") || animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Down"))
        {
            // 1���� ���� �ִϸ��̼� ���൵�� �� ���� ���� �ð����� �����Ͽ� �ִϸ��̼� ����
            animator.Play(isUp ? "Spike_Up" : "Spike_Down", -1, 1f - animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else
        {
            // �ö����, �������� �ִϸ��̼� ����
            animator.SetTrigger(isUp ? "Up" : "Down");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ������ ���´ٸ�
        if (collision.transform.CompareTag("Player"))
        {
            // ���� �ö���ִ��� Ȯ��
            if (isUp)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Boss"))
                {
                    Debug.Log("Blood");
                }
                else
                {
                    // �÷��̾� ���� ����Ʈ ����
                    GameManager.Instance.SpawnBlood(transform.position);
                }
                
            }
        }
    }


}
