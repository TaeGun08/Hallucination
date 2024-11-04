using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [Header("인벤토리")]
    [SerializeField] private int slotIndex;
    [SerializeField] private List<int> slot;

    private void Awake()
    {
        for (int iNum = 0; iNum < slotIndex; iNum++)
        {
            slot.Add(-1);
        }
    }

    /// <summary>
    /// 플레이어의 인벤토리에서 아이템이 사용되었다면 캔버스로 보이는 인벤토리에 아이템을 한 칸씩 당겨주기 위한 함수
    /// </summary>
    public int UseItem(int _index)
    {
        for (int iNum = 0; iNum < slot.Count; iNum++)
        {
            if (slot[iNum] == _index)
            {
                return slot[iNum];
            }
        }

        return -1;
    }

    /// <summary>
    /// 아이템 인덱스를 인벤토리에 넣기 위한 함수
    /// </summary>
    /// <param name="_index"></param>
    public void SetItemIndex(int _index)
    {
        for (int iNum = 0; iNum < slot.Count; iNum++)
        {
            if (slot[iNum] == -1)
            {
                slot[iNum] = _index;
                break;
            }
        }
    }
}
