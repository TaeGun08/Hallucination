using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���� ����")]
    [SerializeField] private bool gamePause;

    [Header("�ɼ�â")]
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
            optionWindow.SetActive(optionWindow.activeSelf == false ? true : false);
            gamePause = optionWindow.activeSelf == false ? false : true;
        }
    }
}
