using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueManager dialogueManager;
    private QuestManager questManager;

    [Header("NPC����")]
    [SerializeField] private int npcId;
    [SerializeField] private List<int> questId;
    [SerializeField] private GameObject npcCamera;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);
    }

    private void Update()
    {
        for (int iNum = 0; iNum < questId.Count; iNum++)
        {
            if (questManager.QuestCheck(questId[iNum]) == true)
            {
                questId[iNum]++;
            }
        }

        npcCamera.SetActive(dialogueManager.IsDialogue == false ? false : true);
    }

    /// <summary>
    /// Npc�� ������ �ִ� string�� �־��ֱ� ���� �Լ�
    /// </summary>
    public int GetNpcId()
    {     
        return npcId;
    }

    public List<int> GetNpcQuestId()
    {
        return questId;
    }
}
