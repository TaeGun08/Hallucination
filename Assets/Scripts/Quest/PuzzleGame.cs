using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueManager dialogueManager;
    private QuestManager questManager;

    [SerializeField] private bool puzzleClear;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);
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
