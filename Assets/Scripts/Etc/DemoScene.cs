using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoScene : MonoBehaviour
{
    [SerializeField] private Button mainGoButton;
    private QuestManager questManager;

    private void Start()
    {
        GameManager.Instance.ResetBool();
        questManager = GameManager.Instance.GetManagers<QuestManager>(3);
        questManager.ResetQuestData();
        PlayerPrefs.DeleteAll();

        Cursor.lockState = CursorLockMode.None;

        mainGoButton.onClick.AddListener(() =>
        {
            FadeInOut.Instance.SetActive(false, () =>
            {
                SceneManager.LoadSceneAsync("LoadingScene");

                GameManager.Instance.GoMain = true;

                FadeInOut.Instance.SetActive(true);
            });
        });
    }
}
