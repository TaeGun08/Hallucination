using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private GameObject gameCanvas; //���� ĵ����

    private void Awake()
    {
        gameCanvas = transform.GetChild(0).GetComponent<GameObject>();
    }

    /// <summary>
    /// ���� ĵ������ �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameCanvas()
    {
        return gameCanvas;
    }
}
