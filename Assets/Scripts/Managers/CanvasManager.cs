using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private GameObject canvas; //ĵ����

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

        canvas = transform.GetChild(0).GetComponent<GameObject>();
    }

    /// <summary>
    /// ĵ������ �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetCanvas()
    {
        return canvas;
    }
}
