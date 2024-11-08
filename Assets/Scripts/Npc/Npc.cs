using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    protected GameManager gameManager;
    protected CameraManager cameraManager;
    protected DialogueManager dialogueManager;
    protected QuestManager questManager;

    [Header("NPC����")]
    [SerializeField] protected int npcId;
    [SerializeField] protected List<int> questId;
    [SerializeField] protected GameObject npcCamera;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);
    }

    protected virtual void npc()
    {
        for (int iNum = 0; iNum < questId.Count; iNum++)
        {
            if (questManager.QuestCheck(questId[iNum]) == true)
            {
                questId[iNum]++;
            }
        }


        if (npcCamera.activeSelf == true && dialogueManager.IsDialogue == false)
        {
            npcCamera.SetActive(false);
        }
    }

    /// <summary>
    /// Npc�� ������ �ִ� string�� �־��ֱ� ���� �Լ�
    /// </summary>
    public virtual int GetNpcId()
    {
        npcCamera.SetActive(true);
        cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
        return npcId;
    }

    public virtual List<int> GetNpcQuestId()
    {
        return questId;
    }
}
