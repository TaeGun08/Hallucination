using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("�κ��丮")]
    [SerializeField] private List<int> slot;
    private GameObject components; //�κ��丮�� �������� �ڽ����� ��� �� ������Ʈ
    private bool inventoyOnOffCheck = false; //�κ��丮�� ���� ������ �ٸ� UIâ�� �� ������ ����� ���� ����

    [Header("ĵ���� �κ��丮")]
    [SerializeField] private List<GameObject> slotObject;
    [SerializeField] private GameObject itemImage;

    [Header("������ ����")]
    [SerializeField] private GameObject Instruction;

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

        for (int iNum = 0; iNum < 9; iNum++)
        {
            slot.Add(-1);
        }

        components = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        inventoryOnOff();
    }

    private void inventoryOnOff()
    {
        if (SceneManager.GetActiveScene().name == "Inventory" && components.activeSelf == false && Option.Instance.ChangeSetting == false)
        {
            Cursor.lockState = CursorLockMode.None;
            components.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name != "Inventory" && components.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            components.SetActive(false);
        }
    }

    /// <summary>
    /// �÷��̾��� �κ��丮���� �������� ����� �� ĵ������ ���̴� �κ��丮�� �������� �������ֱ� ���� �Լ�
    /// </summary>
    private void setItem(int _index)
    {
        GameObject item = Instantiate(itemImage, slotObject[_index].transform);
        item.transform.SetParent(slotObject[_index].transform);
        ItemImage itemImg = item.GetComponent<ItemImage>();
        itemImg.SetIndex(_index);
    }

    /// <summary>
    /// �÷��̾��� �κ��丮���� �������� ���Ǿ��ٸ� ĵ������ ���̴� �κ��丮�� �������� �� ĭ�� ����ֱ� ���� �Լ�
    /// </summary>
    private void UseItem()
    {

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
                setItem(iNum);
                break;
            }
        }
    }

    /// <summary>
    /// �κ��丮 ������ҵ�
    /// </summary>
    /// <returns></returns>
    public GameObject Components()
    {
        return components;
    }

    /// <summary>
    /// ������ ���� UI�� ���� �ѱ� ���� �Լ�
    /// </summary>
    /// <param name="_onOff"></param>
    public GameObject InstructiononOff()
    {
        return Instruction;
    }

    /// <summary>
    /// �κ��丮�� �����ִ��� �����ִ��� üũ�ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool GetInventoyOnOffCheck()
    {
        return inventoyOnOffCheck;
    }
}
