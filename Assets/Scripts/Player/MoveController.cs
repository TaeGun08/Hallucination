using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private CharacterController characterController; //ĳ������Ʈ�ѷ�

    private PlayerBehaviorCheck playerBehaviorCheck; //�÷��̾� �ൿ�� üũ���ִ� ��ũ��Ʈ

    [Header("�÷��̾� ������ ����")]
    [SerializeField] private Transform headTrs; //ĳ������ �Ӹ� Transform
    [SerializeField] private float moveSpeed; //ĳ���� �⺻ �̵��ӵ�
    [SerializeField] private float runSpeed; //ĳ���� �޸��� �ӵ�
    private Vector3 moveDir; //ĳ���Ͱ� �����̱� ���� vec3��

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
