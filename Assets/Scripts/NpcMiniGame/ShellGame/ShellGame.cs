using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellGame : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    [Header("컵 게임 설정")]
    [SerializeField] private List<Shuffle> shuffleSc; //컵을 섞기 위한 
    [SerializeField] private List<GameObject> cupTrs; //컵의 위치
    [SerializeField] private GameObject ball; //컵에 숨길 공
    [SerializeField] private List<Transform> ballTrs; //공을 숨길 위치
    [SerializeField] private GameObject shellGameCamera; //Shell게임을 위한 카메라
    [Space]
    private bool gameStart ; //게임 시작 체크
    private bool gameOver; //게임 실패 체크
    private bool gameClear; //게임 성공 체크
    private bool gameEnd; //게임 종료 체크,
    private float startDelay; //게임 딜레이
    [SerializeField] private float shuffleTime; //컵을 섞을 시간
    public float ShuffleTime
    {
        get
        {
            return shuffleTime;
        }
    }
    private float timer; //섞는 시간
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    private bool shuffleCheck = false; //섞고 있는지 체크
    public bool ShuffleCheck
    {
        get
        {
            return shuffleCheck;
        }
        set
        {
            shuffleCheck = value;
        }
    }

    private float choiceTimer;
    public float ChooseTimer
    {
        get
        {
            return choiceTimer;
        }
    }
    private GameObject ballObj;
    private Transform shellCupTrs;

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);

        shellGameCamera.SetActive(false);

        ballCreate();
    }

    private void Update()
    {
        if (gameClear == true && gameEnd == false)
        {
            shellGameClear();
            Destroy(gameObject);
        }

        if (gameOver == true)
        {
            shellGameOver();
            Destroy(gameObject);
        }

        gameStartCheck();
        choiceCup();
    }

    /// <summary>
    /// 게임이 시작되었는지 체크 후 5초의 딜레이를 주기 위한 함수
    /// </summary>
    private void gameStartCheck()
    {
        if (gameStart)
        {
            shellGameCamera.SetActive(true);

            startDelay += Time.deltaTime;

            if (startDelay >= 5)
            {
                Cursor.lockState = CursorLockMode.None;
                StartCoroutine(shuffleGame());
                startDelay = 0;
                gameStart = false;
            }
        }
    }

    /// <summary>
    /// 일정시간이 지나면 컵을 고르기 위한 함수
    /// </summary>
    private void choiceCup()
    {
        if (timer >= shuffleTime)
        {
            if (choiceTimer <= 1)
            {
                choiceTimer += Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0) && choiceTimer >= 1)
            {
                Ray choiceRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(choiceRay, out RaycastHit hit, 5, LayerMask.GetMask("ShuffleCup")))
                {
                    ShuffleCup cupSc = hit.collider.GetComponent<ShuffleCup>();

                    if (cupSc != null)
                    {
                        if (cupSc.Choice)
                        {
                            gameClear = true;
                        }
                        else
                        {
                            gameOver = true;
                        }
                    }

                    gameManager.PlayerQuestGame = false;

                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    /// <summary>
    /// shell 게임에 필요한 공 생성 함수
    /// </summary>
    private void ballCreate()
    {
        int ballRandomTrs = Random.Range(0, ballTrs.Count);

        shellCupTrs = ballTrs[ballRandomTrs];

        ballObj = Instantiate(ball, new Vector3(shellCupTrs.position.x, shellCupTrs.position.y - 0.2f, shellCupTrs.position.z), Quaternion.identity, transform);

        ShuffleCup cupSc = cupTrs[ballRandomTrs].GetComponent<ShuffleCup>();
        cupSc.Choice = true;
    }

    /// <summary>
    /// 게임 시작을 위한 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator shuffleGame()
    {
        ballObj.transform.SetParent(shellCupTrs);

        while (timer <= shuffleTime)
        {
            timer += Time.deltaTime;

            if (!shuffleCheck)
            {
                cupShuffle();
                shuffleCheck = true;
                yield return null;
            }

            yield return null;
        }
    }

    private void setCupTrs(int _cupA, int _cupB, Transform _trs)
    {
        GameObject temp = null;

        cupTrs[_cupA].transform.SetParent(_trs);
        cupTrs[_cupB].transform.SetParent(_trs);

        temp = cupTrs[_cupA];
        cupTrs[_cupA] = cupTrs[_cupB];
        cupTrs[_cupB] = temp;
    }

    private void cupShuffle()
    {
        int pickUpNumber = Random.Range(0, shuffleSc.Count);

        Shuffle shuffleScript = shuffleSc[pickUpNumber];

        switch (pickUpNumber)
        {
            case 0:
                setCupTrs(0, 1, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
            case 1:
                setCupTrs(0, 2, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
            case 2:
                setCupTrs(1, 2, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
        }

        for (int iNum = 0; iNum < shuffleSc.Count; iNum++)
        {
            Shuffle shuSc = shuffleSc[iNum];
            shuSc.ShuffleSpeed += 0.15f;
        }
    }

    /// <summary>
    /// 게임이 클리어 되었을 때 실행되는 함수
    /// </summary>
    private void shellGameClear()
    {
        if (gameClear == true && questManager.QuestCheck(110) == false)
        {
            questManager.CompleteQuest(110);
            dialogueManager.StartDialogue(2000, new List<int> { 111 });
            questManager.CompleteQuest(111);
        }
    }

    /// <summary>
    /// 게임을 실패했을 때 실행되는 함수
    /// </summary>
    private void shellGameOver()
    {
        if (gameOver == true && questManager.QuestCheck(110) == false)
        {
            dialogueManager.StartDialogue(2000, new List<int> { 115 });
        }
    }

    /// <summary>
    /// 컵의 위치를 원상태로 돌려주는 함수
    /// </summary>
    public void ResetParentTrs()
    {
        for (int iNum = 0; iNum < cupTrs.Count; iNum++)
        {
            cupTrs[iNum].transform.SetParent(transform);
        }
    }

    /// <summary>
    /// 쉘 게임을 실행시키기 위한 함수
    /// </summary>
    public void ShellGameStart()
    {
        gameStart = true;
    }
}
