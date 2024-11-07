using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueManager dialogueManager;
    private QuestManager questManager;

    [SerializeField] private List<PuzzleChecker> puzzleCheckers;
    private bool gameClear;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);
    }

    private void Update()
    {
        if (gameClear == true && questManager.QuestCheck(100) == false)
        {
            questManager.CompleteQuest(100);
            dialogueManager.StartDialogue(1000, new List<int> { 101 });
            questManager.CompleteQuest(101);
            Destroy(gameObject);
        }

        if (puzzleCheckers[0].Check == true &&
            puzzleCheckers[1].Check == true &&
            puzzleCheckers[2].Check == true &&
            puzzleCheckers[3].Check == true &&
            puzzleCheckers[4].Check == true &&
            puzzleCheckers[5].Check == true &&
            puzzleCheckers[6].Check == true)
        {
            gameClear = true;
        }
    }
}
