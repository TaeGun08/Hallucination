using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Inventory inventory;

    private CharacterController characterController;

    [Header("아이템 줍기 기능")]
    [SerializeField, Tooltip("아이템을 주울 수 있는 거리")] private float pickUpDistance;

    private float screenHeight; //화면 세로 크기
    private float screenWidth; //화면 가로 크기

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
    /// 획득할 수 있는 아이템인지 확인하기 위한 함수
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
