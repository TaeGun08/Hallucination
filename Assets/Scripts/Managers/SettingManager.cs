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
        public List<float> sliders = new List<float>(); // 저장된 배경음, 효과음, 민감도 설정을 위한 슬라이더
        public bool windowToggle; // 저장된 창모드를 위한 토글
    }

    private SaveSetting saveSetting = new SaveSetting();

    [Header("설정")]
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
    [SerializeField] private Button closeButton; // 설정창을 닫는 버튼
    [SerializeField] private List<Slider> sliders = new List<Slider>(); // 배경음, 효과음, 민감도 설정을 위한 슬라이더
   // private List<TMP_Text> valueText = new List<TMP_Text>(); // 설정 값을 표기해줄 텍스트
    [SerializeField] private Toggle windowToggle; // 창모드를 위한 토글
    [SerializeField] private Button settingSaveButton; // 설정 저장 버튼
    [SerializeField] private Button main;
    private bool check;

    private bool saveCheck = false; // 저장했을 때만 값을 넣어주기 위한 함수
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
            SceneManager.LoadSceneAsync("MainScene");
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
    /// 설정 저장이 되어있는지 확인을 하는 함수
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
    /// 설정 저장 버튼
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
    ///// 수치를 눈으로 보여주기 위한 텍스트를 실시간으로 수치를 변경하기 위한 함수
    ///// </summary>
    //private void textChange()
    //{
    //    valueText[0].text = $"{(sliders[0].value * 100).ToString("F0")}";
    //    valueText[1].text = $"{(sliders[1].value * 100).ToString("F0")}";
    //    valueText[2].text = $"{sliders[2].value.ToString("F1")}";
    //}


    /// <summary>
    /// 설정 캔버스 오브젝트를 다른 스크립트에 보내주기 위한 함수
    /// </summary>
    /// <returns></returns>
    public GameObject SettingObject()
    {
        return setting;
    }

    /// <summary>
    /// 설정된 슬라이더의 값을 가져오기 위한 함수
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
