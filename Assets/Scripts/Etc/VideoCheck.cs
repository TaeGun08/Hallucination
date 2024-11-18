using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoCheck : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.Play();

        StartCoroutine(videosCheck());
    }

    private IEnumerator videosCheck()
    {
        yield return new WaitForSeconds((float)videoPlayer.clip.length);

        FadeInOut.Instance.SetActive(false, () =>
        {
            SceneManager.LoadSceneAsync("LoadingScene");

            PlayerPrefs.SetInt("SaveScene", 1);

            GameManager.Instance.CutSceneLoad = false;

            FadeInOut.Instance.SetActive(true);
        });
    }
}
