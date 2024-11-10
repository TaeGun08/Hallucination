using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    private Light lit;

    private void Awake()
    {
        lit = GetComponent<Light>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            lit.color = Color.red;
        }
        else
        {
            lit.color = Color.white;
        }
    }
}
