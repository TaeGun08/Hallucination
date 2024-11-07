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

    [Header("NPC설정")]
    [SerializeField] protected int npcId;
    [SerializeField] protected List<int> questId;
    [SerializeField] protected GameObject npcCamera;
    [SerializeField] protected bool hasQuest;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);

        if (PlayerPrefs.GetInt("SaveScene") == 3)
        {
            gameObject.SetActive(false);
        }
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
    /// Npc가 가지고 있는 string을 넣어주기 위한 함수
    /// </summary>
    public virtual int GetNpcId()
    {
        npcCamera.SetActive(true);
        cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
        return npcId;
    }

    public virtual List<int> GetNpcQuestId()
    {
        if (hasQuest == false)
        {
            return questId;
        }

        return new List<int>() { 0 };
    }
}
