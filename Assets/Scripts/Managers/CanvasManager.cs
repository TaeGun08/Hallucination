using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject gameCanvas; //���� ĵ����
    [SerializeField] private GameObject mainSceneCanvas;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            mainSceneCanvas.SetActive(true);
        }
        else
        {
            mainSceneCanvas.SetActive(false);
        }
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
