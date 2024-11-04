using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGames : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;

    [SerializeField] private GameObject shellGame;
    [SerializeField] private GameObject puzzleGame;
    private bool playerQuestGame;
    public bool PlayerQuestGame
    {
        get
        {
            return playerQuestGame;
        }
        set
        {
            playerQuestGame = value;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        questManager = gameManager.GetManagers<QuestManager>(3);
    }

    public void GameStart(List<int> _questId)
    {
        for (int iNum = 0; iNum < _questId.Count; iNum++)
        {
            if (questManager.QuestCheck(_questId[iNum]) == false)
            {
                switch (_questId[iNum])
                {
                    case 110:
                        gameManager.PlayerQuestGame = true;
                        GameObject shell = Instantiate(shellGame, new Vector3(5f, 1f, -7f), Quaternion.identity, transform);
                        ShellGame shellSc = shell.GetComponent<ShellGame>();
                        shellSc.ShellGameStart();
                        break;
                    case 200:
                        break;
                }
            }
        }
    }
}
