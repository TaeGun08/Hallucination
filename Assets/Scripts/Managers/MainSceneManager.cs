using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;

    private void Awake()
    {
        buttons[0].onClick.AddListener(() => 
        {
            FadeInOut.Instance.SetActive(false, () =>
            {
                SceneManager.LoadSceneAsync("LoadingScene");

                FadeInOut.Instance.SetActive(true);
            });
        });
    }
}
