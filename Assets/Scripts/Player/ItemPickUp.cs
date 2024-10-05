using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Inventory inventory;

    [Header("������ �ݱ� ���")]
    [SerializeField, Tooltip("�������� �ֿ� �� �ִ� �Ÿ�")] private float pickUpDistance;

    private float screenHeight; //ȭ�� ���� ũ��
    private float screenWidth; //ȭ�� ���� ũ��

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
    /// ȹ���� �� �ִ� ���������� Ȯ���ϱ� ���� �Լ�
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
