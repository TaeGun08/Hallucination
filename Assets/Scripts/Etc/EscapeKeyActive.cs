using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeKeyActive : MonoBehaviour
{
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
    }
}
