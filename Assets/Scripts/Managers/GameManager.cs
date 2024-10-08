using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 정지")]
    [SerializeField] private bool gamePause;

    private bool inventoryCheck = false; //인벤토리 씬으로 넘어갔는지 체크하기 위한 변수

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
            inventoryCheck = inventoryCheck == false ? true : false;

            if (inventoryCheck == true)
            {
                SceneManager.LoadSceneAsync("Inventory");
            }
            else
            {
                SceneManager.LoadSceneAsync("TestScene");
            }
        }
    }

    /// <summary>
    /// 게임 정지 및 활성화
    /// </summary>
    public void GamePause(bool _gamePause)
    {
        gamePause = _gamePause;
    }
}
