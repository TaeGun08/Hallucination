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
                    case 100:
                        gameManager.PlayerQuestGame = true;
                        Instantiate(puzzleGame, new Vector3(49.187f, 27f, -14.247f), Quaternion.Euler(0, 180, 0), transform);
                        break;
                    case 110:
                        gameManager.PlayerQuestGame = true;
                        GameObject shell = Instantiate(shellGame, new Vector3(52f, 26.5f, -13.5f), Quaternion.identity, transform);
                        ShellGame shellSc = shell.GetComponent<ShellGame>();
                        shellSc.ShellGameStart();
                        break;
                }
            }
        }
    }
}
