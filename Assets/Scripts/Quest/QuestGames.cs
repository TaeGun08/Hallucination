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
                        Instantiate(puzzleGame, new Vector3(57.5f, 26.5f, -7f), Quaternion.Euler(0, 180, 0), transform);
                        gameManager.PlayerObject.SetActive(false);
                        break;
                    case 110:
                        gameManager.PlayerQuestGame = true;
                        GameObject shell = Instantiate(shellGame, new Vector3(41.6f, 26.5f, -18f), Quaternion.identity, transform);
                        ShellGame shellSc = shell.GetComponent<ShellGame>();
                        shellSc.ShellGameStart();
                        gameManager.PlayerObject.SetActive(false);
                        break;
                }
            }
        }
    }
}
