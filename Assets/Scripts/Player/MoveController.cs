using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private CharacterController characterController; //캐릭터컨트롤러

    private PlayerBehaviorCheck playerBehaviorCheck; //플레이어 행동을 체크해주는 스크립트

    [Header("플레이어 움직임 설정")]
    [SerializeField] private Transform headTrs; //캐릭터의 머리 Transform
    [SerializeField] private float moveSpeed; //캐릭터 기본 이동속도
    [SerializeField] private float runSpeed; //캐릭터 달리기 속도
    private Vector3 moveDir; //캐릭터가 움직이기 위한 vec3값

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();

        playerBehaviorCheck = GetComponent<PlayerBehaviorCheck>();
    }

    private void Update()
    {
        rotate();
        move();
    }

    private void rotate()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        headTrs.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z - 90f);
    }

    private void move()
    {
        moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W) == true)
        {
            moveDir.z = 1;
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            moveDir.z = -1;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            moveDir.x = -1;
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            moveDir.x = 1;
        }

        playerBehaviorCheck.IsBehavior = moveDir.x == 0 && moveDir.z == 0 ? false : true;

        playerBehaviorCheck.IsHorizontal = moveDir.x;
        playerBehaviorCheck.IsVertical = moveDir.z;

        playerBehaviorCheck.WalkRunCheck = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;

        if (playerBehaviorCheck.WalkRunCheck == 0)
        {
            characterController.Move(transform.rotation * moveDir.normalized * moveSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(transform.rotation * moveDir.normalized * runSpeed * Time.deltaTime);
        }
    }
}
