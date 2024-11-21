using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightText : MonoBehaviour
{
    private bool check;
    private float timer;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        check = false;
        timer = 0;
    }

    private void Update()
    {
        if (check == false)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                gameObject.SetActive(false);
                timer = 0;
                check = true;
            }
        }
    }
}
