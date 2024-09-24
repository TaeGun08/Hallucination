using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ���콺�� ����Ұ��� �� �Ұ��� ���ϱ� ���� �Լ�
    /// </summary>
    public void SetMouseLockOrNone(bool _lockOn)
    {
        if (_lockOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
