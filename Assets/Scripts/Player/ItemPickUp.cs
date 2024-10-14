using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Inventory inventory;

    private CharacterController characterController;

    [Header("������ �ݱ� ���")]
    [SerializeField, Tooltip("�������� �ֿ� �� �ִ� �Ÿ�")] private float pickUpDistance;

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
        pickUpItemCheck();
    }

    /// <summary>
    /// ȹ���� �� �ִ� ���������� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    private void pickUpItemCheck()
    {
        Ray pickUpRay = Camera.main.ScreenPointToRay(new Vector3(screenWidth * 0.5f, screenHeight * 0.5f));

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(pickUpRay, out RaycastHit hittem, pickUpDistance, LayerMask.GetMask("Item")) && inventory != null)
            {
                inventory.SetItemIndex(hittem.collider.gameObject.GetComponent<Item>().ItemIndex);
                Destroy(hittem.collider.gameObject);
            }
            else if (Physics.Raycast(pickUpRay, out RaycastHit hitHide, pickUpDistance, LayerMask.GetMask("HideObject")))
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
        }
    }
}
