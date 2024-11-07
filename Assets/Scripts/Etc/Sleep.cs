using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        questManager = gameManager.GetManagers<QuestManager>(3);

        gameManager.EyesUISc.EyesCheck = true;
        gameManager.EyesUISc.OpenOrClose = false;
    }

    public void IsSleep()
    {
        if ((questManager.QuestCheck(100) && questManager.QuestCheck(110)) ||
            (questManager.QuestCheck(200) && questManager.QuestCheck(210)))
        {
            gameManager.EyesUISc.EyesCheck = true;
            gameManager.EyesUISc.OpenOrClose = true;
        }
    }
}
