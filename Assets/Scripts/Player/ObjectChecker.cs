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

    [Header("전장의 오브젝트를 확인하기 위한 거리")]
    [SerializeField] private float checkDistance;

    private float screenHeight; //화면 세로 크기
    private float screenWidth; //화면 가로 크기

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
    ///  전방의 오브젝트를 확인하기 위한 함수
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

            if (dialogueManager.IsDialogue == false)
            {          
                dialogueManager.StartDialogue(npcSc.GetNpcId(), npcSc.GetNpcQuestId());
            }
        }
    }
}
