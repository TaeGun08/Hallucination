using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

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
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isDialogue == true)
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
        while (isDialogue == false)
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
            StartCoroutine(funtionDialogue());
        }
        else
        {
            index = 0;
            dialogueText.text = string.Empty;
            dialogueBox.SetActive(false);
            isDialogue = false;
            dialogueLine = null;
            StartCoroutine(dialogueTimer());
        }
    }

    /// <summary>
    /// ���̾�α� ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator funtionDialogue()
    {
        dialogueText.text = string.Empty;
        isDialogue = true;
        dialogueTime = 0;
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
        StartCoroutine(funtionDialogue());
    }
}
