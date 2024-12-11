using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectChecker : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager cameraManager;
    private DialogueManager dialogueManager;
    private QuestManager questManager;

    private Inventory inventory;

    [Header("������ ������Ʈ�� Ȯ���ϱ� ���� �Ÿ�")]
    [SerializeField] private float checkDistance;

    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            checkDistance = 2;
        }

        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);
    }

    private void Update()
    {
        objectCheck();

        if (dialogueManager.IsDialogue == true)
        {
            gameManager.EKeyText.SetActive(false);
        }
    }

    /// <summary>
    ///  ������ ������Ʈ�� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    private void objectCheck()
    {
        if (dialogueManager.IsDialogue == false && gameManager.GoDemoScene == false)
        {
            Ray pickUpRay = Camera.main.ScreenPointToRay(new Vector3(gameManager.RenderTexture.width * 0.5f, gameManager.RenderTexture.height * 0.5f));

            if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(pickUpRay, out RaycastHit hit, checkDistance, layerMask))
            {
                hitObejct(hit);
            }

            if (Physics.Raycast(pickUpRay, out RaycastHit hitCheck, checkDistance, layerMask))
            {
                if ((PlayerPrefs.GetInt("SaveScene") == 1 && hitCheck.collider.gameObject.layer == LayerMask.NameToLayer("Sleep")) ||
                    !(questManager.QuestCheck(100) && questManager.QuestCheck(110) &&
                        questManager.QuestCheck(200) && questManager.QuestCheck(210)) && hitCheck.collider.gameObject.layer == LayerMask.NameToLayer("Sleep"))
                {
                    gameManager.EKeyText.SetActive(false);
                }
                else
                {
                    gameManager.EKeyText.SetActive(true);
                }
            }
            else
            {
                gameManager.EKeyText.SetActive(false);
            }
        }
    }

    private void hitObejct(RaycastHit _hit)
    {
        if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Item") && inventory != null)
        {
            inventory.SetItemIndex(_hit.collider.gameObject.GetComponent<Item>().ItemIndex);
            Destroy(_hit.collider.gameObject);
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Npc npcSc = _hit.collider.gameObject.GetComponent<Npc>();
            ItemQuestNpc itemQuestNpcSc = _hit.collider.gameObject.GetComponent<ItemQuestNpc>();

            Vector3 playerPos = gameObject.transform.position;
            Vector3 npcPos = _hit.collider.gameObject.transform.position;

            if (dialogueManager.IsDialogue == false && itemQuestNpcSc != null)
            {
                cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
                itemQuestNpcSc.getInven(inventory);
                playerNpcDistance(playerPos, npcPos);
                gameObject.SetActive(false);
            }

            if (dialogueManager.IsDialogue == false && npcSc != null)
            {
                cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
                dialogueManager.StartDialogue(npcSc.GetNpcId(), npcSc.GetNpcQuestId());
                playerNpcDistance(playerPos, npcPos);
                gameObject.SetActive(false);
            }
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            Door doorSc = _hit.collider.GetComponent<Door>();

            if (doorSc.TeacherRoomOpen == true && inventory.EscapeKeyCount == 4)
            {
                doorSc.TeacherRoomOpen = false;
                doorSc.Open = true;
            }
            else
            {
                doorSc.Open = true;
            }
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Closet"))
        {
            Closet closetSc = _hit.collider.GetComponent<Closet>();
            if (closetSc != null)
            {
                closetSc.Open = true;
            }
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("ExitDoor"))
        {
            EscapeDoor escapeDoorSc = _hit.collider.GetComponent<EscapeDoor>();
            if (inventory.EscapeKeyCount == 4)
            {
                escapeDoorSc.Open = true;
                gameObject.SetActive(false);
            }
        }
        else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer("Sleep"))
        {
            Sleep sleepSc = _hit.collider.GetComponent<Sleep>();
            sleepSc.IsSleep();
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
