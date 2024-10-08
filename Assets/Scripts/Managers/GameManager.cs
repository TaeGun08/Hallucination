using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���� ����")]
    [SerializeField] private bool gamePause;

    private bool inventoryCheck = false; //�κ��丮 ������ �Ѿ���� üũ�ϱ� ���� ����

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
    /// ������ ���� �Ǵ� Ǯ�� ���� �Լ�
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
    /// ���� ���� �� Ȱ��ȭ
    /// </summary>
    public void GamePause(bool _gamePause)
    {
        gamePause = _gamePause;
    }
}
