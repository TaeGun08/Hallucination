using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager cameraManager;
    private DialogueManager dialogueManager;

    private CharacterController characterController; //ĳ������Ʈ�ѷ�

    private PlayerBehaviorCheck playerBehaviorCheck; //�÷��̾� �ൿ�� üũ���ִ� ��ũ��Ʈ

    [Header("�÷��̾� ������ ����")]
    [SerializeField] private Transform headTrs; //ĳ������ �Ӹ� Transform
    public Transform HeadTrs
    {
        get
        {
            return headTrs;
        }
    }
    [SerializeField] private float moveSpeed; //ĳ���� �⺻ �̵��ӵ�
    [SerializeField] private float runSpeed; //ĳ���� �޸��� �ӵ�
    [SerializeField] private float gravity; //ĳ������ �߷�
    private Vector3 moveDir; //ĳ���Ͱ� �����̱� ���� vec3��

    private float hasMoveTimer;
    private bool moveOn;
    public bool MoveOn
    {
        get
        {
            return moveOn;
        }
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();

        playerBehaviorCheck = GetComponent<PlayerBehaviorCheck>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
    }

    private void Update()
    {
        if (moveOn == false)
        {
            hasMoveTimer += Time.deltaTime;
            if (hasMoveTimer >= 3)
            {
                moveOn = true;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "TeacherCutScene")
            {
                if (dialogueManager.IsDialogue == false && gameManager.PlayerQuestGame == false)
                {
                    rotate();
                    move();
                    gravityVelocity();
                }
            }
            else
            {
                headTrs.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z - 90f);
            }
        }
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

    private void gravityVelocity()
    {
        if (characterController.isGrounded == false)
        {
            characterController.Move(new Vector3(0f, -gravity, 0f) * Time.deltaTime);
        }
        else
        {
            characterController.Move(new Vector3(0f, 0f, 0f));
        }
    }
}
