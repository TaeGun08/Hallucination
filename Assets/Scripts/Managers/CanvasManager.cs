using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private GameObject canvas; //캔버스

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
    /// 캔버스를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject GetCanvas()
    {
        return canvas;
    }
}
