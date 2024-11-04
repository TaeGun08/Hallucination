using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [Header("�κ��丮")]
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
    /// �÷��̾��� �κ��丮���� �������� ���Ǿ��ٸ� ĵ������ ���̴� �κ��丮�� �������� �� ĭ�� ����ֱ� ���� �Լ�
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
    /// ������ �ε����� �κ��丮�� �ֱ� ���� �Լ�
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
