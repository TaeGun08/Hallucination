using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    private Inventory inventory;

    private CharacterController characterController;

    [Header("������ ������Ʈ�� Ȯ���ϱ� ���� �Ÿ�")]
    [SerializeField] private float checkDistance;

    private float screenHeight; //ȭ�� ���� ũ��
    private float screenWidth; //ȭ�� ���� ũ��

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        inventory = Inventory.Instance;

        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    private void Update()
    {
        objectCheck();
    }

    /// <summary>
    ///  ������ ������Ʈ�� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    private void objectCheck()
    {
        Ray pickUpRay = Camera.main.ScreenPointToRay(new Vector3(screenWidth * 0.5f, screenHeight * 0.5f));

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(pickUpRay, out RaycastHit hittem, checkDistance, LayerMask.GetMask("Item")) && inventory != null)
            {
                inventory.SetItemIndex(hittem.collider.gameObject.GetComponent<Item>().ItemIndex);
                Destroy(hittem.collider.gameObject);
            }
            else if (Physics.Raycast(pickUpRay, out RaycastHit hitHide, checkDistance, LayerMask.GetMask("HideObject")))
            {
                HideObject hideSc = hitHide.collider.gameObject.GetComponent<HideObject>();

                if (hideSc.Hide == false)
                {
                    characterController.height = 1;
                    characterController.enabled = false;
                    gameObject.transform.position = hideSc.HideTransform().position;
                    characterController.enabled = true;
                    hideSc.Hide = true;
                    hideSc.PlayerObject(gameObject);
                }
            }
            else if (Physics.Raycast(pickUpRay, out RaycastHit hitNpc, checkDistance, LayerMask.GetMask("Npc")))
            {
                Npc npcSc = hitNpc.collider.gameObject.GetComponent<Npc>();
                npcSc.CameraOnOff = true;
            }
        }
    }
}
