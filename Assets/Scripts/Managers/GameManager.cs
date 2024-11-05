using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("�Ŵ���������Ʈ")]
    [SerializeField] private List<GameObject> managers;

    [Header("���� ����")]
    [SerializeField] private bool gamePause;

    [Header("������ �÷��̾� ������Ʈ")]
    [SerializeField] private GameObject bearPrefab;

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
        //StartCoroutine(FadeInOut.Instance.functionFade());
    }

    private void Update()
    {
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
