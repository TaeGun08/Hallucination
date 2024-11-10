using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemActive : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
