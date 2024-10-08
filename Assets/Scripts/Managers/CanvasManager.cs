using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private GameObject gameCanvas; //���� ĵ����

    private GameObject inventoryCanvas; //�κ��丮 ĵ����

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

        gameCanvas = transform.GetChild(0).GetComponent<GameObject>();

        inventoryCanvas = transform.GetChild(1).GetComponent<GameObject>();
    }

    /// <summary>
    /// ���� ĵ������ �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameCanvas()
    {
        return gameCanvas;
    }

    /// <summary>
    ///  �κ��丮 ĵ������ �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetInventoryCanvas()
    {
        return inventoryCanvas;
    }
}
