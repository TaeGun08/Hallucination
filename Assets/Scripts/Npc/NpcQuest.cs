using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcQuest : MonoBehaviour
{
    [Header("Npc����Ʈ")]
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
