using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private GameObject gameCanvas; //게임 캔버스

    private GameObject inventoryCanvas; //인벤토리 캔버스

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
    /// 게임 캔버스를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameCanvas()
    {
        return gameCanvas;
    }

    /// <summary>
    ///  인벤토리 캔버스를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject GetInventoryCanvas()
    {
        return inventoryCanvas;
    }
}
