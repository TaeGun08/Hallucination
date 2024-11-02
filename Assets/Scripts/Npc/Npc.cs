using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private QuestManager questManager;

    [Header("NPC����")]
    [SerializeField] private int npcId;
    [SerializeField] private List<int> questId;
    [SerializeField] private GameObject npcCamera;

    private void Start()
    {
        questManager = QuestManager.Instance;
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

        npcCamera.SetActive(DialogueManager.Instance.IsDialogue == false ? false : true);
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
