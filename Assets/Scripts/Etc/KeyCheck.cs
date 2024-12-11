using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    public static KeyCheck Instance;

    [SerializeField] private TMP_Text text;

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
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTextKey(int _key)
    {
        text.text = $"{_key} / 4";
    }
}
