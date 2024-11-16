using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<Button> buttons;

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
    }
}
