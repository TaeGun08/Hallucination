using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellGame : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;
    private DialogueManager dialogueManager;

    [Header("�� ���� ����")]
    [SerializeField] private List<Shuffle> shuffleSc; //���� ���� ���� 
    [SerializeField] private List<GameObject> cupTrs; //���� ��ġ
    [SerializeField] private GameObject ball; //�ſ� ���� ��
    [SerializeField] private List<Transform> ballTrs; //���� ���� ��ġ
    [SerializeField] private GameObject shellGameCamera; //Shell������ ���� ī�޶�
    [Space]
    private bool gameStart ; //���� ���� üũ
    private bool gameOver; //���� ���� üũ
    private bool gameClear; //���� ���� üũ
    private bool gameEnd; //���� ���� üũ
    private float startDelay; //���� ������
    [SerializeField] private float shuffleTime; //���� ���� �ð�
    public float ShuffleTime
    {
        get
        {
            return shuffleTime;
        }
    }
    private float timer; //���� �ð�
    public float Timer
    {
        get
        {
            return timer;
        }
    }
    private bool shuffleCheck = false; //���� �ִ��� üũ
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
            gameManager.PlayerQuestGame = false;
            shellGameClear();
            Destroy(gameObject);
            return;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
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
    /// ������ ���۵Ǿ����� üũ �� 5���� �����̸� �ֱ� ���� �Լ�
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
    /// �����ð��� ������ ���� ���� ���� �Լ�
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
                Vector3 mousePosition = Input.mousePosition;

                float xRatio = (float)GameManager.Instance.RenderTexture.width / Screen.width;
                float yRatio = (float)GameManager.Instance.RenderTexture.height / Screen.height;

                mousePosition.x *= xRatio;
                mousePosition.y *= yRatio;
                mousePosition.z = 10f;

                Ray choiceRay = Camera.main.ScreenPointToRay(mousePosition);

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
    /// shell ���ӿ� �ʿ��� �� ���� �Լ�
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
    /// ���� ������ ���� �ڷ�ƾ
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
    /// ������ Ŭ���� �Ǿ��� �� ����Ǵ� �Լ�
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
    /// ������ �������� �� ����Ǵ� �Լ�
    /// </summary>
    private void shellGameOver()
    {
        if (gameOver == true && questManager.QuestCheck(110) == false)
        {
            dialogueManager.StartDialogue(2000, new List<int> { 115 });
        }
    }

    /// <summary>
    /// ���� ��ġ�� �����·� �����ִ� �Լ�
    /// </summary>
    public void ResetParentTrs()
    {
        for (int iNum = 0; iNum < cupTrs.Count; iNum++)
        {
            cupTrs[iNum].transform.SetParent(transform);
        }
    }

    /// <summary>
    /// �� ������ �����Ű�� ���� �Լ�
    /// </summary>
    public void ShellGameStart()
    {
        gameStart = true;
    }
}
