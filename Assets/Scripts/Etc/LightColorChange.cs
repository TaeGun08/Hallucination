using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    private Light lit;
    private Color color;

    private void Awake()
    {
        lit = GetComponent<Light>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {

            color.r = 0.5f;
            color.g = 0.2f;
            color.b = 0.2f;
            lit.color = color;
        }
        else
        {
            color.r = 1f;
            color.g = 1f;
            color.b = 1f;
            lit.color = color;
        }
    }
}
