using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("매니저오브젝트")]
    [SerializeField] private List<GameObject> managers;
    private QuestManager questManager;

    [Header("게임 정지")]
    [SerializeField] private bool gamePause;

    [Header("생성할 플레이어 오브젝트")]
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
    }

    private void Update()
    {
        if (eyesUI.EyesCheck == true)
        {
            eyesUI.gameObject.SetActive(true);
        }
        else
        {
            eyesUI.gameObject.SetActive(false);
        }

        if ((questManager.QuestCheck(100) && questManager.QuestCheck(110) && PlayerPrefs.GetInt("SvaeScene") == 0) ||
            (questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SvaeScene") == 1))
        {
            if (SceneManager.GetActiveScene().name == "MapScene")
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

        if (Input.GetKeyDown(KeyCode.Escape))
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
    /// 게임을 정지 또는 풀기 위한 함수
    /// </summary>
    private void gamePauseCheck()
    {
        Time.timeScale = gamePause == true ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    /// <summary>
    /// 게임 정지 및 활성화
    /// </summary>
    public void GamePause(bool _gamePause)
    {
        gamePause = _gamePause;
    }

    /// <summary>
    /// 플레이어 캐릭터를 생성하기 위한 함수
    /// </summary>
    public GameObject CreateBear()
    {
        return bearPrefab;
    }

    /// <summary>
    /// 필요한 매니저를 가져오기 위한 함수
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
