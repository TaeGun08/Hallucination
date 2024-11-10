using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager cameraManager;

    public class SaveSetting
    {
        public List<float> sliders = new List<float>(); // ����� �����, ȿ����, �ΰ��� ������ ���� �����̴�
        public bool windowToggle; // ����� â��带 ���� ���
    }

    private SaveSetting saveSetting = new SaveSetting();

    [Header("����")]
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject settingComponent;
    public GameObject SettingComponent
    {
        get
        {
            return settingComponent;
        }
        set
        {
            settingComponent = value;
        }
    }
    [SerializeField] private Button closeButton; // ����â�� �ݴ� ��ư
    [SerializeField] private List<Slider> sliders = new List<Slider>(); // �����, ȿ����, �ΰ��� ������ ���� �����̴�
   // private List<TMP_Text> valueText = new List<TMP_Text>(); // ���� ���� ǥ������ �ؽ�Ʈ
    [SerializeField] private Toggle windowToggle; // â��带 ���� ���
    [SerializeField] private Button settingSaveButton; // ���� ���� ��ư
    [SerializeField] private Button main;
    private bool check;

    private bool saveCheck = false; // �������� ���� ���� �־��ֱ� ���� �Լ�
    public bool SaveCheck
    {
        get { return saveCheck; }
        set { saveCheck = value; }
    }

    private void Awake()
    {
        settingComponent.SetActive(false);

        closeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GamePause(false);
            if (SceneManager.GetActiveScene().name != "MainScene")
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            settingComponent.SetActive(false);
        });

        main.onClick.AddListener(() =>
        {
            FadeInOut.Instance.SetActive(false, () =>
            {
                SceneManager.LoadSceneAsync("LoadingScene");

                settingComponent.SetActive(false);
                gameManager.GoMain = true;

                FadeInOut.Instance.SetActive(true);
            });
        });

        saveSettingCheck();
        saveButton();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);
    }

    private void Update()
    {
        //textChange();
        if (SceneManager.GetActiveScene().name == "MainScene" ||
            SceneManager.GetActiveScene().name == "LodingScene")
        {
            main.gameObject.SetActive(false);
        }
        else
        {
            main.gameObject.SetActive(true);
        }

        if (settingComponent.activeSelf == false && check == true)
        {
            int count = sliders.Count;

            string getSaveSetting = PlayerPrefs.GetString("saveSetting");
            saveSetting = JsonConvert.DeserializeObject<SaveSetting>(getSaveSetting);
            for (int iNum = 0; iNum < count; iNum++)
            {
                sliders[iNum].value = saveSetting.sliders[iNum];
            }

            windowToggle.isOn = saveSetting.windowToggle;
            check = false;
        }
        else if (settingComponent.activeSelf == true)
        {
            check = true;
        }
    }

    /// <summary>
    /// ���� ������ �Ǿ��ִ��� Ȯ���� �ϴ� �Լ�
    /// </summary>
    private void saveSettingCheck()
    {
        int count = sliders.Count;

        if (PlayerPrefs.GetString("saveSetting") == string.Empty)
        {
            for (int iNum = 0; iNum < count; iNum++)
            {
                saveSetting.sliders.Add(0.5f);
                saveSetting.windowToggle = false;
                sliders[iNum].value = 0.5f;
                windowToggle.isOn = false;
            }

            string setSaveSetting = JsonConvert.SerializeObject(saveSetting);
            PlayerPrefs.SetString("saveSetting", setSaveSetting);
        }
        else
        {
            string getSaveSetting = PlayerPrefs.GetString("saveSetting");
            saveSetting = JsonConvert.DeserializeObject<SaveSetting>(getSaveSetting);

            for (int iNum = 0; iNum < count; iNum++)
            {
                sliders[iNum].value = saveSetting.sliders[iNum];
            }

            windowToggle.isOn = saveSetting.windowToggle;
        }

        Screen.SetResolution(2560, 1440, !windowToggle.isOn);
    }

    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    private void saveButton()
    {
        settingSaveButton.onClick.AddListener(() =>
        {
            int count = sliders.Count;

            for (int iNum = 0; iNum < count; iNum++)
            {
                saveSetting.sliders[iNum] = sliders[iNum].value;
            }

            saveSetting.windowToggle = windowToggle.isOn;

            string setSaveSetting = JsonConvert.SerializeObject(saveSetting);
            PlayerPrefs.SetString("saveSetting", setSaveSetting);

            Screen.SetResolution(2560, 1440, !windowToggle.isOn);

            if (cameraManager != null)
            {
                cameraManager.SetMouseSensitivity(sliders[2].value);
            }

            saveCheck = true;
        });
    }

    ///// <summary>
    ///// ��ġ�� ������ �����ֱ� ���� �ؽ�Ʈ�� �ǽð����� ��ġ�� �����ϱ� ���� �Լ�
    ///// </summary>
    //private void textChange()
    //{
    //    valueText[0].text = $"{(sliders[0].value * 100).ToString("F0")}";
    //    valueText[1].text = $"{(sliders[1].value * 100).ToString("F0")}";
    //    valueText[2].text = $"{sliders[2].value.ToString("F1")}";
    //}


    /// <summary>
    /// ���� ĵ���� ������Ʈ�� �ٸ� ��ũ��Ʈ�� �����ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject SettingObject()
    {
        return setting;
    }

    /// <summary>
    /// ������ �����̴��� ���� �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public float GetSlidersValue(int _number)
    {
        switch (_number)
        {
            case 0:
                return sliders[0].value;
            case 1:
                return sliders[1].value;
            case 2:
                return sliders[2].value;
        }

        return 0;
    }
}
