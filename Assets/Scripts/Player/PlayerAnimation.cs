using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; //�÷��̾� ĳ���Ϳ� �ִ� �ִϸ�����

    private PlayerBehaviorCheck playerBehaviorCheck; //�÷��̾� �ൿ�� üũ���ִ� ��ũ��Ʈ
    private MoveController moveController; //�÷��̾ �ൿ�� �� �ְ� �ϴ� ��ũ��Ʈ

    private void Awake()
    {
        animator = GetComponent<Animator>();

        playerBehaviorCheck = GetComponent<PlayerBehaviorCheck>();
        moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "TeacherCutScene")
        {
            animator.SetBool("isWakeUp", !moveController.MoveOn);
            animCheck();
        }
        else
        {
            animator.SetBool("isWakeUp", true);
        }
    }

    private void animCheck()
    {
        if (playerBehaviorCheck.IsBehavior)
        {
            animator.SetBool("isWalk", playerBehaviorCheck.WalkRunCheck == 0 ? true : false);
            animator.SetBool("isRun", playerBehaviorCheck.WalkRunCheck == 1 ? true : false);
        }
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }
    }
}
