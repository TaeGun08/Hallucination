using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�Ŵ���������Ʈ")]
    [SerializeField] private List<GameObject> managers;
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    [Header("���� ����")]
    [SerializeField] private bool gamePause;

    [Header("������ �÷��̾� ������Ʈ")]
    [SerializeField] private GameObject bearPrefab;
    private GameObject playerObject;
    public GameObject PlayerObject
    {
        get
        {
            return playerObject;
        }
        set
        {
            playerObject = value;
        }
    }

    [SerializeField] private RenderTexture renderTexture;
    public RenderTexture RenderTexture
    {
        get
        {
            return renderTexture;
        }
        set
        {
            renderTexture = value;
        }
    }

    private bool playerQuestGame;
    public bool PlayerQuestGame
    {
        get
        {
            return playerQuestGame;
        }
        set
        {
            playerQuestGame = value;
        }
    }

    [SerializeField] private EyesUI eyesUI;
    public EyesUI EyesUISc
    {
        get
        {
            return eyesUI;
        }
        set
        {
            eyesUI = value;
        }
    }
    private bool cutSceneLoad;
    public bool CutSceneLoad
    {
        get
        {
            return cutSceneLoad;
        }
        set
        {
            cutSceneLoad = value;
        }
    }

    [SerializeField] private List<GameObject> clearUI;
    private bool catCheck;
    private bool rabbitCheck;
    private bool wakeUpCheck;

    [SerializeField] private SettingManager option;
    public SettingManager Option
    {
        get
        {
            return option;
        }
        set
        {
            option = value;
        }
    }
    [SerializeField] private GameObject eKeyText;
    public GameObject EKeyText
    {
        get
        {
            return eKeyText;
        }
        set
        {
            eKeyText = value;
        }
    }
    private bool goMain;
    public bool GoMain
    {
        get
        {
            return goMain;
        }
        set
        {
            goMain = value;
        }
    }
    private bool goDemoScene;
    public bool GoDemoScene
    {
        get
        {
            return goDemoScene;
        }
        set
        {
            goDemoScene = value;
        }
    }
    private bool gameOver;
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
        set
        {
            gameOver = value;
        }
    }

    [SerializeField] private GameObject tutorialImage;
    private float tutorialTimer;
    private bool tutoCheck;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        questManager = transform.Find("QuestManager").GetComponent<QuestManager>();
        dialogueManager = managers[2].GetComponent<DialogueManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "DemoEndingScene" ||
            SceneManager.GetActiveScene().name == "GameOverScene")
        {
            return;
        }

        if (goDemoScene == true)
        {
            eKeyText.SetActive(false);
        }

        if (eyesUI.EyesCheck == true)
        {
            eyesUI.gameObject.SetActive(true);
            eKeyText.SetActive(false);
            for (int iNum = 0; iNum < clearUI.Count; iNum++)
            {
                clearUI[iNum].SetActive(false);
            }
            return;
        }
        else
        {
            eyesUI.gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "MapScene" 
            && dialogueManager.IsDialogue == false && PlayerPrefs.GetInt("SaveScene") == 0)
        {
            if (tutoCheck == false)
            {
                if (tutorialImage.activeSelf == false)
                {
                    tutorialImage.SetActive(true);
                }
                tutorialTimer += Time.deltaTime;
                if (tutorialTimer >= 5f)
                {
                    tutoCheck = true;
                    tutorialTimer = 0;
                }
            }
            else
            {
                if (tutorialImage.activeSelf == true)
                {
                    tutorialImage.SetActive(false);
                }
            }

            if (wakeUpCheck == false)
            {
                clearUI[0].SetActive(true);
            }

            if (catCheck == true && rabbitCheck == true && !questManager.QuestCheck(200) && !questManager.QuestCheck(210))
            {
                clearUI[1].SetActive(false);
                clearUI[2].SetActive(false);
                clearUI[3].SetActive(true);
            }
            else if (catCheck == true && !questManager.QuestCheck(200))
            {
                clearUI[1].SetActive(true);
                clearUI[3].SetActive(false);
            }
            else if (rabbitCheck == true && !questManager.QuestCheck(210))
            {
                clearUI[2].SetActive(true);
                clearUI[3].SetActive(false);
            }

            else if (catCheck == true && questManager.QuestCheck(200))
            {
                clearUI[1].SetActive(false);
                clearUI[3].SetActive(false);
            }
            else if (rabbitCheck == true && questManager.QuestCheck(210))
            {
                clearUI[2].SetActive(false);
                clearUI[3].SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "MapScene"
                 && dialogueManager.IsDialogue == true && PlayerPrefs.GetInt("SaveScene") == 0)
        {
            for (int iNum = 0; iNum < clearUI.Count; iNum++)
            {
                clearUI[iNum].SetActive(false);
            }
        }

        if (questManager.QuestCheck(100) && questManager.QuestCheck(110) &&
                    questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SaveScene") == 0)
        {
            if (SceneManager.GetActiveScene().name == "MapScene" && dialogueManager.IsDialogue == false)
            {
                for (int iNum = 0; iNum < clearUI.Count; iNum++)
                {
                    clearUI[iNum].SetActive(true);
                }
                clearUI[4].SetActive(true);
            }
            else
            {
                for (int iNum = 0; iNum < clearUI.Count; iNum++)
                {
                    clearUI[iNum].SetActive(false);
                }
            }
        }

        if (EscapeDoor.Instance != null)
        {
            if (EscapeDoor.Instance.Open == true)
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MapScene")
        {
            if (option.SettingComponent.activeSelf == true)
            {
                gamePause = false;
                option.SettingComponent.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                gamePause = true;
                option.SettingComponent.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
        gamePauseCheck();
    }

    /// <summary>
    /// ������ ���� �Ǵ� Ǯ�� ���� �Լ�
    /// </summary>
    private void gamePauseCheck()
    {
        Time.timeScale = gamePause == true ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    /// <summary>
    /// ���� ���� �� Ȱ��ȭ
    /// </summary>
    public void GamePause(bool _gamePause)
    {
        gamePause = _gamePause;
    }

    /// <summary>
    /// �÷��̾� ĳ���͸� �����ϱ� ���� �Լ�
    /// </summary>
    public GameObject CreateBear()
    {
        return bearPrefab;
    }

    /// <summary>
    /// �ʿ��� �Ŵ����� �������� ���� �Լ�
    /// 0 - CameraManager, 1 - CanvasManager, 2 - DialogueManager, 3 - QuestManager
    /// </summary>
    /// <param name="_index"></param>
    public T GetManagers<T>(uint _index) where T : Component
    {
        if (_index < managers.Count)
        {
            T manager = managers[(int)_index].GetComponent<T>();
            return manager;
        }

        return null;
    }

    public void CatRabbitQuestCheck(int _number)
    {
        switch (_number)
        {
            case 1:
                catCheck = true;
                break;
            case 2:
                rabbitCheck = true;
                break;
        }
    }

    public void TextActiveFalse()
    {
        if (wakeUpCheck == false)
        {
            clearUI[0].SetActive(false);
            wakeUpCheck = true;
        }
    }

    public void ResetBool()
    {
        for (int iNum = 0; iNum < clearUI.Count; iNum++)
        {
            clearUI[iNum].SetActive(false);
        }
        tutorialImage.SetActive(false);
        tutoCheck = false;
        catCheck = false;
        rabbitCheck = false;
        wakeUpCheck = false;
    }
}
