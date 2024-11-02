using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame : MonoBehaviour
{
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    [SerializeField] private bool puzzleClear;

    private void Start()
    {
        questManager = QuestManager.Instance;
        dialogueManager = DialogueManager.Instance;
    }

    private void Update()
    {
        if (puzzleClear == true && questManager.QuestCheck(100) == false)
        {
            questManager.CompleteQuest(100);
            dialogueManager.StartDialogue(1000, new List<int> { 101 });
            questManager.CompleteQuest(101);
        }       
    }
}
