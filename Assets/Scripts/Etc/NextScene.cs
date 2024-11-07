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
            currentFrm = (int)(1 / Time.unscaledDeltaTime); //이전 프레임과 현재 프레임의 시간
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
            if (gameManager.CutSceneLoad == true)
            {
                SceneManager.LoadSceneAsync("TeacherCutScene");
            }
            else
            {
                SceneManager.LoadSceneAsync("MapScene");
            }
            yield return null;
        }
    }
}
