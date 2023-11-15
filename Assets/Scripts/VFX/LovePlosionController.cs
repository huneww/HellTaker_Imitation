using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePlosionController : MonoBehaviour
{
    // ��Ʈ ��ƼŬ
    [SerializeField]
    private ParticleSystem particle_Heart;
    // �� ��ƼŬ
    [SerializeField]
    private ParticleSystem particle_Star;
    // �ִϸ����� ������Ʈ
    private Animator animator;
    // �ߺ� ���� ���� ����
    private bool isPlay = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �ִϸ��̼��� ���� �ִϸ��̼��̶��
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ReleaseLovePlosion"))
        {
            // ���� ��ƼŬ�� ������ ���� �ʾ�����
            if (!isPlay)
            {
                // ��ƼŬ ����
                particle_Heart.Play();
                particle_Star.Play();
                // ������ ����
                isPlay = true;
            }
            // �ִϸ��̼��� �����ٸ�
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                // ���� ���������� �Ѿ�� �ڷ�ƾ ����
                StartCoroutine(StartStageChange());
            }
        }
    }

    private IEnumerator StartStageChange()
    {
        // �����ð� ���
        yield return new WaitForSeconds(0.5f);
        // ���� �Ŵ����� �������� ���� �޼��� ����
        StartCoroutine(GameManager.Instance.StageChange(false));
        // ������Ʈ ����
        Destroy(this.gameObject);
    }

}
