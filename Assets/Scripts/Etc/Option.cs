using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public static Option Instance;

    [Header("¹öÆ°")]
    [SerializeField] private List<Button> buttons;
    [SerializeField] private GameObject change;
    [SerializeField] private GameObject setting;

    private bool changeSetting;

    public bool ChangeSetting
    {
        get
        {
            return changeSetting;
        }
        set
        {
            changeSetting = value;
        }
    }

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

        change.SetActive(false);
        setting.SetActive(false);

        buttons[0].onClick.AddListener(() => 
        {
            changeSetting = false;
            Inventory.Instance.Components().SetActive(true);
            setting.SetActive(false);
        });

        buttons[1].onClick.AddListener(() =>
        {
            changeSetting = true;
            Inventory.Instance.Components().SetActive(false);
            setting.SetActive(true);
        });
    }

    private void Update()
    {
        optionOnOff();
    }

    private void optionOnOff()
    {
        if (SceneManager.GetActiveScene().name == "Inventory" && change.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            change.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name != "Inventory" && change.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            change.SetActive(false);
        }
    }
}
