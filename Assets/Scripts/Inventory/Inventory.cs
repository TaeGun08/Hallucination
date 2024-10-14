using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("인벤토리")]
    [SerializeField] private List<int> slot;
    private GameObject components; //인벤토리의 구성들을 자식으로 담아 둘 오브젝트
    private bool inventoyOnOffCheck = false; //인벤토리가 열려 있으면 다른 UI창을 못 열도록 만들기 위한 변수

    [Header("캔버스 인벤토리")]
    [SerializeField] private List<GameObject> slotObject;
    [SerializeField] private GameObject itemImage;

    [Header("아이템 설명")]
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
    /// 플레이어의 인벤토리에서 아이템을 얻었을 떄 캔버스로 보이는 인벤토리에 아이템을 생성해주기 위한 함수
    /// </summary>
    private void setItem(int _index)
    {
        GameObject item = Instantiate(itemImage, slotObject[_index].transform);
        item.transform.SetParent(slotObject[_index].transform);
        ItemImage itemImg = item.GetComponent<ItemImage>();
        itemImg.SetIndex(_index);
    }

    /// <summary>
    /// 플레이어의 인벤토리에서 아이템이 사용되었다면 캔버스로 보이는 인벤토리에 아이템을 한 칸씩 당겨주기 위한 함수
    /// </summary>
    private void UseItem()
    {

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
                setItem(iNum);
                break;
            }
        }
    }

    /// <summary>
    /// 인벤토리 구성요소들
    /// </summary>
    /// <returns></returns>
    public GameObject Components()
    {
        return components;
    }

    /// <summary>
    /// 아이템 설명 UI를 끄고 켜기 위한 함수
    /// </summary>
    /// <param name="_onOff"></param>
    public GameObject InstructiononOff()
    {
        return Instruction;
    }

    /// <summary>
    /// 인벤토리가 열려있는지 닫혀있는지 체크하기 위한 함수
    /// </summary>
    /// <returns></returns>
    public bool GetInventoyOnOffCheck()
    {
        return inventoyOnOffCheck;
    }
}
