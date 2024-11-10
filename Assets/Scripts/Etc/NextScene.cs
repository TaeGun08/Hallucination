using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] int targetFrm = 34;
    private float timer = 0;

    private void Start()
    {
        gameManager = GameManager.Instance;
        StartCoroutine(lodingTime());
    }

    private IEnumerator lodingTime()
    {
        int currentFrm = 0;
        while (currentFrm < targetFrm)
        {
            currentFrm = (int)(1 / Time.unscaledDeltaTime); //���� �����Ӱ� ���� �������� �ð�
            yield return null;
        }

        timer = 0;

        while (timer <= 1f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        if (timer > 1f)
        {
            if (gameManager.GoMain == true)
            {
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("MainScene");

                    gameManager.GoMain = false;

                    FadeInOut.Instance.SetActive(true);
                });
            }
            else if (gameManager.CutSceneLoad == true)
            {
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("TeacherCutScene");

                    gameManager.CutSceneLoad = false;

                    FadeInOut.Instance.SetActive(true);
                });
            }
            else
            {
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("MapScene");

                    FadeInOut.Instance.SetActive(true);
                });
            }
            yield return null;
        }
    }
}
