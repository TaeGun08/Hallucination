using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Inventory inventory;

    [Header("������ �̹���")]
    [SerializeField] private List<Sprite> sprite;
    private int curIndex;
    private int checkIndex;

    [SerializeField] private Image image;

    private void Awake()
    {
        curIndex = -1;
    }

    private void Start()
    {
        inventory = Inventory.Instance;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        inventory.InstructiononOff().SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        inventory.InstructiononOff().SetActive(false);
    }

    private void Update()
    {
        if (curIndex != checkIndex)
        {
            image.sprite = sprite[checkIndex];
            curIndex = checkIndex;
        }
    }

    /// <summary>
    /// �̹����� �����ϱ� ���� �ε����� �־��ִ� �Լ�
    /// </summary>
    /// <param name="_index"></param>
    public void SetIndex(int _index)
    {
        checkIndex = _index;
        image.sprite = sprite[checkIndex];
    }
}
