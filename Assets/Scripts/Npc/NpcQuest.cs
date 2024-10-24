using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcQuest : MonoBehaviour
{
    [Header("NpcÄù½ºÆ®")]
    [SerializeField] private bool questCheck = false;
    public bool QuestCheck
    {
        get
        {
            return questCheck;
        }
        set
        {
            questCheck = value;
        }
    }
}
