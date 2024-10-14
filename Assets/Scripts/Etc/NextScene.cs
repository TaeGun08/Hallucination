using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] int targetFrm = 34;
    private float timer = 0;

    private void Start()
    {
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

        while (timer <= 2f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        if (timer > 2f)
        {
            SceneManager.LoadSceneAsync("TestScene");
            yield return null;
        }
    }
}
