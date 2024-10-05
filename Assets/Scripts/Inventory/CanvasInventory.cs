using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInventory : MonoBehaviour
{
    public static CanvasInventory Instance;

    [Header("캔버스 인벤토리")]
    [SerializeField] private List<GameObject> slot;

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
}
