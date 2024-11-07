using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject gameCanvas; //게임 캔버스
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
    /// 게임 캔버스를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameCanvas()
    {
        return gameCanvas;
    }
}
