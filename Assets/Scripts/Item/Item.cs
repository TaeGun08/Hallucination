using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("아이템 번호")]
    [SerializeField] private int itemIndex;

    public int ItemIndex
    {
        get
        {
            return itemIndex;
        }
        set
        {
            itemIndex = value;
        }
    }
}
