using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;
    public AudioSource AudioManagerSource
    {
        get
        {
            return audioSource;
        }
        set
        {
            audioSource = value;
        }
    }
    [SerializeField] private List<AudioClip> bgmClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (EscapeDoor.Instance != null)
        {
            if (EscapeDoor.Instance.Open == true)
            {
                audioSource.Pause();
                return;
            }
        }

        audioSource.volume = gameManager.Option.GetSlidersValue(0);

        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            audioSource.Pause();
            return;
        }

        if (audioSource.isPlaying == false)
        {
            if (SceneManager.GetActiveScene().name == "GameOverScene")
            {
                audioSource.clip = bgmClip[4];
                audioSource.Play();
            }
            else if (SceneManager.GetActiveScene().name == "MainScene")
            {
                audioSource.clip = bgmClip[0];
                audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("SaveScene") == 0 && SceneManager.GetActiveScene().name == "MapScene")
            {
                audioSource.clip = bgmClip[1];
                audioSource.Play();
            }
            else if (PlayerPrefs.GetInt("SaveScene") == 1 && SceneManager.GetActiveScene().name == "MapScene")
            {
                audioSource.clip = bgmClip[2];
                audioSource.Play();
            }
            else if (SceneManager.GetActiveScene().name == "TeacherCutScene")
            {
                audioSource.clip = bgmClip[3];
                audioSource.Play();
            }
        }
    } 
}
