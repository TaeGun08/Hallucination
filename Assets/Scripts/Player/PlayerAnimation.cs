using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; //플레이어 캐릭터에 있는 애니메이터

    private PlayerBehaviorCheck playerBehaviorCheck; //플레이어 행동을 체크해주는 스크립트
    private MoveController moveController; //플레이어를 행동할 수 있게 하는 스크립트

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
