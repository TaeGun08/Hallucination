using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button gameGoButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        gameGoButton.onClick.AddListener(() =>
        {
            FadeInOut.Instance.SetActive(false, () =>
            {
                SceneManager.LoadSceneAsync("MapScene");

                FadeInOut.Instance.SetActive(true);
            });
        });
    }
}
