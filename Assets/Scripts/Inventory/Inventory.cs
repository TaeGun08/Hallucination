using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("�κ��丮")]
    [SerializeField] private int slotIndex;
    [SerializeField] private List<int> slot;
    private int escapeKeyCount;
    public int EscapeKeyCount
    {
        get
        {
            return escapeKeyCount;
        }
        set
        {
            escapeKeyCount = value;
        }
    }

    [SerializeField] private GameObject flashLight;

    private void Awake()
    {
        for (int iNum = 0; iNum < slotIndex; iNum++)
        {
            slot.Add(-1);
        }
    }

    private void Update()
    {
        if (flashLight.activeSelf == false && InveItemCheck(50) == true)
        {
            flashLight.SetActive(true);
        }
    }

    /// <summary>
    /// �÷��̾��� �κ��丮�� �ִ� �������� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    public bool InveItemCheck(int _index)
    {
        for (int iNum = 0; iNum < slot.Count; iNum++)
        {
            if (slot[iNum] == _index)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// �������� ����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="_index"></param>
    public void UseItem(int _index)
    {
        for (int iNum = 0; iNum < slot.Count; iNum++)
        {
            if (slot[iNum] == _index)
            {
                slot[iNum] = -1;
            }
        }
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
                if (_index == 10)
                {
                    ++escapeKeyCount;
                    KeyCheck.Instance.SetTextKey(escapeKeyCount);

                    if (escapeKeyCount >= 4)
                    {
                        ExitTextCheck.Instance.SetActiveTrue();
                    }
                }
                slot[iNum] = _index;
                break;
            }
        }
    }
}
