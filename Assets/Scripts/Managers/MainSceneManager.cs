using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<Button> buttons;

    [SerializeField] private GameObject choicePanel;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        buttons[0].onClick.AddListener(() => 
        {
            audioSource.Play();

            FadeInOut.Instance.SetActive(false, () =>
            {
                SceneManager.LoadSceneAsync("LoadingScene");

                FadeInOut.Instance.SetActive(true);
            });
        });

        buttons[1].onClick.AddListener(() =>
        {
            audioSource.Play();
            GameManager.Instance.Option.SettingComponent.SetActive(true);
        });

        buttons[2].onClick.AddListener(() => 
        {
            audioSource.Play();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        buttons[3].onClick.AddListener(() =>
        {
            choicePanel.SetActive(true);
        });

        buttons[4].onClick.AddListener(() =>
        {
            GameManager.Instance.ResetBool();
            QuestManager questManager = GameManager.Instance.GetManagers<QuestManager>(3);
            questManager.ResetQuestData();
            PlayerPrefs.DeleteAll();
            choicePanel.SetActive(false);
        });

        buttons[5].onClick.AddListener(() =>
        {
            choicePanel.SetActive(false);
        });
    }
}
