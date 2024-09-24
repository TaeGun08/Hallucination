using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator; //플레이어 캐릭터에 있는 애니메이터

    private PlayerBehaviorCheck playerBehaviorCheck; //플레이어 행동을 체크해주는 스크립트

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
