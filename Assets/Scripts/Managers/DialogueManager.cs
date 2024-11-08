using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager cameraManager;
    private QuestManager questManager;

    private DialogueData dialogueData;

    [Header("다이얼로그 설정")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float dialogueTime;
    [SerializeField] private float textSpeed;
    private List<string> dialogueLine = new List<string>();
    private int index = 0;
    private bool isDialogue = false;
    public bool IsDialogue
    {
        get
        {
            return isDialogue;
        }
        set
        {
            isDialogue = value;
        }
    }
    private List<int> questId = new List<int>();

    private float nextSceneTimer;
    private bool nextSceneCheck;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);
        questManager = gameManager.GetManagers<QuestManager>(3);

        dialogueData = GetComponent<DialogueData>();

        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (nextSceneCheck)
        {
            nextSceneTimer += Time.deltaTime;
            if (nextSceneTimer >= 2f)
            {
                nextSceneTimer = 0;
                if (questManager.QuestCheck(100) && questManager.QuestCheck(110) && PlayerPrefs.GetInt("SaveScene") == 0)
                {
                    PlayerPrefs.SetInt("SaveScene", 1);
                }
                else if (questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SaveScene") == 1)
                {
                    PlayerPrefs.SetInt("SaveScene", 2);
                }
                gameManager.EyesUISc.OpenOrClose = true;
                gameManager.EyesUISc.EyesCheck = true;
                gameManager.EyesUISc.gameObject.SetActive(true);
                gameManager.CutSceneLoad = false;
                nextSceneCheck = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isDialogue == true && dialogueTime >= 0.3f)
        {
            if (dialogueText.text == dialogueLine[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLine[index];
            }
        }
    }

    private IEnumerator dialogueTimer()
    {
        while (dialogueTime < 0.3f)
        {
            dialogueTime += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 다음 줄로 넘어가기 위한 함수
    /// </summary>
    private void nextLine()
    {
        if (index < dialogueLine.Count - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(FuntionDialogue());
        }
        else
        {
            index = 0;
            dialogueText.text = string.Empty;
            dialogueBox.SetActive(false);
            isDialogue = false;
            dialogueLine = null;
            StartCoroutine(dialogueTimer());

            if (SceneManager.GetActiveScene().name == "TeacherCutScene" &&
                ((questManager.QuestCheck(100) && questManager.QuestCheck(110) && PlayerPrefs.GetInt("SaveScene") == 0) ||
            (questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SaveScene") == 1)))
            {
                nextSceneCheck = true;
            }
            else
            {
                cameraManager.GetVirtualCamera(0).gameObject.SetActive(true);
            }

            questManager.GetQuestGames().GameStart(questId);
        }
    }

    /// <summary>
    /// 다이얼로그 기능
    /// </summary>
    /// <returns></returns>
    public IEnumerator FuntionDialogue()
    {
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(true);
        string fullText = dialogueLine[index];

        foreach (var text in fullText.ToCharArray())
        {
            dialogueText.text += text;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// 코루틴의 시작과 다이얼로그에 들어갈 string을 받아오기 위한 함수
    /// </summary>
    /// <param name="_string"></param>
    public void StartDialogue(int _npcId, List<int> _questId)
    {
        dialogueLine = dialogueData.GetDialogue(_npcId, questManager.GetQuestId(_questId));
        questId = _questId;

        if (dialogueLine != null)
        {
            index = 0;
            isDialogue = true;
            dialogueTime = 0;
            cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
            StartCoroutine(dialogueTimer());
            StartCoroutine(FuntionDialogue());
        }
    }

    public void StartCutSceneDialogue(int _cutScene)
    {
        dialogueLine = dialogueData.GetCutSceneDialogue(_cutScene);

        if (dialogueLine != null)
        {
            index = 0;
            isDialogue = true;
            dialogueTime = 0;
            cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
            StartCoroutine(dialogueTimer());
            StartCoroutine(FuntionDialogue());
        }
    }
}
