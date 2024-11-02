using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public class DialogueTalk
    {
        public int npcId;
        public int questId;
        public List<string> dialogueLines;

        public DialogueTalk(int _talkIndex, int _questId, List<string> _dialogueLines)
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
            new DialogueTalk(1000, 0, new List<string>
            {
                "�ȳ�, ���� �۹��̾�",
                "������ ���� ���� �ִ�?"
            }),
             new DialogueTalk(1000, 100, new List<string>
            {
                 "�ȳ�, ���� �۹��̾�",
                 "���� ���� ���� ��������",
                 "���� ���� ����"
            }),
            new DialogueTalk(1000, 101, new List<string>
            {
                 "�� ���� ����ϱ���!",
                 "ȥ�� �� ���ߴٴ�, ¯�̴�!",
                 "�������� �� ����, �߰�!"
            }),
            new DialogueTalk(1000, 105, new List<string>
            {
                 "���� �Ʊ���!",
                 "�ɽ��� �� �ٽ� �� �� ����",
            }),
        };
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
                return dialogueTalk[iNum].dialogueLines;
            }
        }

        return null;
    }
}
