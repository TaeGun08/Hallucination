using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private GameObject gameCanvas; //게임 캔버스

    private void Awake()
    {
        gameCanvas = transform.GetChild(0).GetComponent<GameObject>();
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
