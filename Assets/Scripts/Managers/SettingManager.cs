using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [Header("���� ĵ����")]
    [SerializeField] private GameObject settingCanvas;
    private GameObject settingObject; // ���� ĵ������ ������Ʈ
    private Button closeButton; // ����â�� �ݴ� ��ư
    private List<Slider> sliders = new List<Slider>(); // �����, ȿ����, �ΰ��� ������ ���� �����̴�
    private List<TMP_Text> valueText = new List<TMP_Text>(); // ���� ���� ǥ������ �ؽ�Ʈ
    private Toggle windowToggle; // â��带 ���� ���
    private Button settingSaveButton; // ���� ���� ��ư

    private bool saveCheck = false; // �������� ���� ���� �־��ֱ� ���� �Լ�
    public bool SaveCheck
    {
        get { return saveCheck; }
        set { saveCheck = value; }
    }

    private void Awake()
    {
        settingObject = Instantiate(settingCanvas, transform);
        settingObject.SetActive(false);

        closeButton = settingObject.transform.GetChild(0).Find("X").GetComponent<Button>();

        for (int iNum = 0; iNum < 3; iNum++)
        {
            sliders.Add(settingObject.transform.GetChild(0).Find("SliderLayout").GetChild(iNum).GetComponent<Slider>());
            valueText.Add(settingObject.transform.GetChild(0).Find("ValueTextLayout").GetChild(iNum).GetComponent<TMP_Text>());
        }

        windowToggle = settingObject.transform.GetChild(0).Find("WindowMode").GetComponent<Toggle>();
        settingSaveButton = settingObject.transform.GetChild(0).Find("Save").GetComponent<Button>();

        closedButton();
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
        textChange();
    }

    /// <summary>
    /// ����â�� �ݴ� ��ư
    /// </summary>
    private void closedButton()
    {
        closeButton.onClick.AddListener(() =>
        {
            settingObject.SetActive(false);
        });
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

        Screen.SetResolution(1920, 1080, !windowToggle.isOn);
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

    /// <summary>
    /// ��ġ�� ������ �����ֱ� ���� �ؽ�Ʈ�� �ǽð����� ��ġ�� �����ϱ� ���� �Լ�
    /// </summary>
    private void textChange()
    {
        valueText[0].text = $"{(sliders[0].value * 100).ToString("F0")}";
        valueText[1].text = $"{(sliders[1].value * 100).ToString("F0")}";
        valueText[2].text = $"{sliders[2].value.ToString("F1")}";
    }


    /// <summary>
    /// ���� ĵ���� ������Ʈ�� �ٸ� ��ũ��Ʈ�� �����ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject SettingObject()
    {
        return settingObject;
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
