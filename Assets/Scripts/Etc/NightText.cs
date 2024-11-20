using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightText : MonoBehaviour
{
    private bool check;
    private float timer;

    private void Update()
    {
        if (check == false)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                timer = 0;
                check = true;
            }
        }
    }
}
