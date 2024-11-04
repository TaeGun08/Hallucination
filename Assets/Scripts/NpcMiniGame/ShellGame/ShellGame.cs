using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellGame : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    [Header("컵 게임 설정")]
    [SerializeField] private List<Shuffle> shuffleSc;
    [SerializeField] private List<GameObject> cupTrs;
    [SerializeField] private GameObject ball;
    [SerializeField] private List<Transform> ballTrs;
    [SerializeField] private GameObject shellGameCamera;
    [Space]
    [SerializeField] private bool gameStart = false;
    [SerializeField] private bool gameOver;
    [SerializeField] private bool gameClear;
    private bool gameEnd;
    private float startDelay;
    [SerializeField] private float shuffleTime;
    private float timer;
    private bool shuffleCheck = false;
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

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);
        questManager = gameManager.GetManagers<QuestManager>(3);

        shellGameCamera.SetActive(false);
    }

    private void Update()
    {
        if (gameClear == true && gameEnd == false)
        {
            shellGameCamera.SetActive(false);
            shellGameClear();
            gameEnd = true;
            return;
        }

        if (gameOver == true)
        {
            for (int iNum = 0; iNum < shuffleSc.Count; iNum++)
            {
                ShuffleCup cupSc = cupTrs[iNum].GetComponent<ShuffleCup>();
                cupSc.Choice = false;

                if (cupSc.transform.childCount > 0)
                {
                    GameObject ballObj = cupSc.transform.GetChild(0).gameObject;
                    Destroy(ballObj);
                }
            }

            shellGameOver();
            timer = 0;
            gameOver = false;
        }

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

        if (timer >= shuffleTime && Input.GetMouseButtonDown(0))
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

                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private IEnumerator shuffleGame()
    {
        int ballRandomTrs = Random.Range(0, ballTrs.Count);

        Transform shellCupTrs = ballTrs[ballRandomTrs];

        GameObject ballObj = Instantiate(ball, new Vector3(shellCupTrs.position.x, shellCupTrs.position.y - 0.2f, shellCupTrs.position.z), Quaternion.identity, transform);
        ballObj.transform.SetParent(shellCupTrs);

        ShuffleCup cupSc = cupTrs[ballRandomTrs].GetComponent<ShuffleCup>();
        cupSc.Choice = true;

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
    }

    /// <summary>
    /// 게임이 클리어 되었을 때 실행되는 함수
    /// </summary>
    private void shellGameClear()
    {
        if (gameClear == true && questManager.QuestCheck(100) == false)
        {
            questManager.CompleteQuest(100);
            dialogueManager.StartDialogue(1000, new List<int> { 101 });
            questManager.CompleteQuest(101);
        }
    }

    /// <summary>
    /// 게임을 실패했을 때 실행되는 함수
    /// </summary>
    private void shellGameOver()
    {
        if (gameOver == true && questManager.QuestCheck(100) == false)
        {
            dialogueManager.StartDialogue(1000, new List<int> { 105 });
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
