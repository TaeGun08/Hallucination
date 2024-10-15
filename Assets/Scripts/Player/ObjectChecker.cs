using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    private Inventory inventory;

    private CharacterController characterController;

    [Header("전장의 오브젝트를 확인하기 위한 거리")]
    [SerializeField] private float checkDistance;

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
        objectCheck();
    }

    /// <summary>
    ///  전방의 오브젝트를 확인하기 위한 함수
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
