using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Inventory inventory;

    [Header("아이템 줍기 기능")]
    [SerializeField, Tooltip("아이템을 주울 수 있는 거리")] private float pickUpDistance;

    private float screenHeight; //화면 세로 크기
    private float screenWidth; //화면 가로 크기

    private void Awake()
    {
        inventory = Inventory.Instance;
    }

    private void Start()
    {
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

        if (Physics.Raycast(pickUpRay, out RaycastHit hit, pickUpDistance, LayerMask.GetMask("Item")))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.SetItemIndex(hit.collider.gameObject.GetComponent<Item>().ItemIndex);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
