using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private CameraManager cameraManager;

    [Header("���̾�α� ����")]
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cameraManager = CameraManager.Instance;

        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
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
    /// ���� �ٷ� �Ѿ�� ���� �Լ�
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
            cameraManager.GetVirtualCamera(0).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ���̾�α� ���
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
    /// �ڷ�ƾ�� ���۰� ���̾�α׿� �� string�� �޾ƿ��� ���� �Լ�
    /// </summary>
    /// <param name="_string"></param>
    public void StartDialogue(List<string> _string)
    {
        dialogueLine = _string;
        index = 0;
        isDialogue = true;
        dialogueTime = 0;
        StartCoroutine(dialogueTimer());
        StartCoroutine(FuntionDialogue());
    }
}
