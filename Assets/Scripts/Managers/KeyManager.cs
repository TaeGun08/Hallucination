using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    [SerializeField] KeyCode currentKeycode;

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            //debug:
            Debug.Log("key read: " + e.keyCode);
            currentKeycode = e.keyCode;
        }
    }

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
    }

    /// <summary>
    /// Ű�� �������� �� ���� Ű���� �����ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public KeyCode getKey()
    {
        return currentKeycode;
    }
}
