using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 정지")]
    [SerializeField] private bool gamePause;

    [Header("옵션창")]
    [SerializeField] private GameObject optionWindow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gamePauseCheck();
        optionWindowOnOff();
    }

    /// <summary>
    /// 게임을 정지 또는 풀기 위한 함수
    /// </summary>
    private void gamePauseCheck()
    {
        Time.timeScale = gamePause == true ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    private void optionWindowOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionWindow.SetActive(optionWindow.activeSelf == false ? true : false);
            gamePause = optionWindow.activeSelf == false ? false : true;
        }
    }
}
