using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueManager dialogueManager;

    private CharacterController characterController;
    private Inventory inventory;

    [Header("������ ������Ʈ�� Ȯ���ϱ� ���� �Ÿ�")]
    [SerializeField] private float checkDistance;

    private float screenHeight; //ȭ�� ���� ũ��
    private float screenWidth; //ȭ�� ���� ũ��

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);

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
            ItemQuestNpc itemQuestNpcSc = _hit.collider.gameObject.GetComponent<ItemQuestNpc>();

            Vector3 playerPos = gameObject.transform.position;
            Vector3 npcPos = _hit.collider.gameObject.transform.position;

            if (dialogueManager.IsDialogue == false && itemQuestNpcSc != null)
            {
                itemQuestNpcSc.getInven(inventory);
                playerNpcDistance(playerPos, npcPos);
            }

            if (dialogueManager.IsDialogue == false && npcSc != null)
            {
                dialogueManager.StartDialogue(npcSc.GetNpcId(), npcSc.GetNpcQuestId());
                playerNpcDistance(playerPos, npcPos);
            }
        }
    }

    /// <summary>
    /// ��ȭ�� �����ϱ��� Npc�� ����Ʈ�⸮ ���� �Լ�
    /// </summary>
    /// <param name="_playerPos"></param>
    /// <param name="_npcPos"></param>
    private void playerNpcDistance(Vector3 _playerPos, Vector3 _npcPos)
    {
        float distance = Vector3.Distance(_playerPos, _npcPos);

        if (distance <= 5)
        {
            Vector3 targetPos = _npcPos - (_npcPos - _playerPos).normalized * 3.8f;

            gameObject.transform.position = targetPos;
        }
    }
}
