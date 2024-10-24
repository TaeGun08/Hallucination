using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    private Inventory inventory;
    private DialogueManager dialogueManager;

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
        dialogueManager = DialogueManager.Instance;

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

        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(pickUpRay, out RaycastHit hit, checkDistance))
        {
            hitObejct(hit);
        }
    }

    private void hitObejct(RaycastHit _hit)
    {
        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Item") && inventory != null)
        {
            inventory.SetItemIndex(_hit.collider.gameObject.GetComponent<Item>().ItemIndex);
            Destroy(_hit.collider.gameObject);
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("HideObject"))
        {
            HideObject hideSc = _hit.collider.gameObject.GetComponent<HideObject>();

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
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Npc npcSc = _hit.collider.gameObject.GetComponent<Npc>();

            if (dialogueManager.IsDialogue == false && npcSc.QuestCheck == false)
            {
                CameraManager.Instance.GetVirtualCamera(0).gameObject.SetActive(false);
                dialogueManager.StartDialogue(npcSc.DialogueCheck());
            }
        }
    }
}
