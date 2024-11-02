using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGames : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField] private GameObject shellGame;
    [SerializeField] private GameObject puzzleGame;

    private void Start()
    {
        questManager = QuestManager.Instance;
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
                        GameObject shell = Instantiate(shellGame, new Vector3(5f, 1.5f, -9f), Quaternion.identity, transform);
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
