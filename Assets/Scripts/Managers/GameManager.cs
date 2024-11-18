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

    [SerializeField] private GameObject clearUI;

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
        }
        else
        {
            eyesUI.gameObject.SetActive(false);
        }

        if (questManager.QuestCheck(100) && questManager.QuestCheck(110) &&
                    questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SaveScene") == 0)
        {
            if (SceneManager.GetActiveScene().name == "MapScene" && dialogueManager.IsDialogue == false)
            {
                clearUI.SetActive(true);
            }
            else
            {
                clearUI.SetActive(false);
            }
        }
        else
        {
            clearUI.SetActive(false);
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
}
