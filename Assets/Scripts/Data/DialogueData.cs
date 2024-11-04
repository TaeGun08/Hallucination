using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public class DialogueTalk
    {
        public int npcId;
        public int questId;
        public string[] dialogueLines;

        public DialogueTalk(int _talkIndex, int _questId, string[] _dialogueLines)
        {
            npcId = _talkIndex;
            questId = _questId;
            dialogueLines = _dialogueLines;
        }
    }

    private List<DialogueTalk> dialogueTalk;

    private void Awake()
    {
        dialogueTalkManage();
    }

    /// <summary>
    /// ���̾�α� ��ȭ�� �����ϴ� �Լ�
    /// </summary>
    private void dialogueTalkManage()
    {
        dialogueTalk = new List<DialogueTalk>
        {
            setDialogueTalk(1000, 0, 
            "�ȳ�!", 
            "������ ���� �� �� �ִ�?"),
            setDialogueTalk(1000, 100, 
            "�ȳ�, ���� ���� ���� �����ҷ�?", 
            "��û ������� �ž�!"),
            setDialogueTalk(1000, 101, 
            "���� ���� ���ߴϱ� ���� ���ߴ� �� ����!", 
            "����־��� �������� �� ����!"),
            setDialogueTalk(2000, 0,
            "�� �� ����?",
            "���� ����"),
            setDialogueTalk(2000, 110,
            "���� �߹��� ��?",
            "�� �߶�帲"),
            setDialogueTalk(2000, 111,
            "���� ���̸� ���Ͻ�",
            "������ ���ϳ� ��.."),
            setDialogueTalk(2000, 115,
            "�������� ez ����",
            "�� �� �� ������������"),
            setDialogueTalk(3000, 0,
            "���� ���˹���",
            "ģ���� ����"),
            setDialogueTalk(3000, 200,
            "�� ���� �峲���� ã����",
            "��Ź�Ұ�"),
            setDialogueTalk(3000, 201,
            "����",
            "������ ������ �ʿ��ϸ� �����ٰ�"),
        };
    }

    private DialogueTalk setDialogueTalk(int _npcId, int questId, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_npcId, questId, dialogues);
        return newDialogue;
    }

    /// <summary>
    /// ���̾�α� ��ȭ�� �������� ���� �Լ�
    /// </summary>
    /// <param name="_npcId"></param>
    /// <param name="_questId"></param>
    /// <returns></returns>
    public List<string> GetDialogue(int _npcId, int _questId)
    {
        for (int iNum = 0; iNum < dialogueTalk.Count; iNum++)
        {
            if (dialogueTalk[iNum].npcId == _npcId
                && dialogueTalk[iNum].questId == _questId)
            {
                List<string> dialogueList = new List<string>(dialogueTalk[iNum].dialogueLines);
                return dialogueList;
            }
        }

        return null;
    }
}
