using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; //�÷��̾� ĳ���Ϳ� �ִ� �ִϸ�����

    private PlayerBehaviorCheck playerBehaviorCheck; //�÷��̾� �ൿ�� üũ���ִ� ��ũ��Ʈ

    private void Awake()
    {
        animator = GetComponent<Animator>();

        playerBehaviorCheck = GetComponent<PlayerBehaviorCheck>();
    }

    private void Update()
    {
        animCheck();
    }

    private void animCheck()
    {
        animator.SetBool("isRun", playerBehaviorCheck.WalkRunCheck == 1 ? true : false);
    }
}
