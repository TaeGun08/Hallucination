using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitTextCheck : MonoBehaviour
{
    public static ExitTextCheck Instance;

    private GameObject childObject;

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
    }

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            childObject.SetActive(true);
        }
        else
        {
            childObject.SetActive(false);
        }

        childObject.SetActive(false);
    }

    public void SetActiveTrue()
    {
        childObject.SetActive(true);
    }

    public void SetActiveFalse()
    {
        childObject.SetActive(false);
    }
}
