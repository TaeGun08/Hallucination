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
    /// 키를 변경했을 때 무슨 키인지 보내주기 위한 함수
    /// </summary>
    /// <returns></returns>
    public KeyCode getKey()
    {
        return currentKeycode;
    }
}
