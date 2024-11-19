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

        public int cutScene;

        public DialogueTalk(int _talkIndex, int _questId, string[] _dialogueLines)
        {
            npcId = _talkIndex;
            questId = _questId;
            dialogueLines = _dialogueLines;
        }

        public DialogueTalk(int _cutScene, string[] _dialogueLines)
        {
            cutScene = _cutScene;
            dialogueLines = _dialogueLines;
        }
    }

    private List<DialogueTalk> dialogueTalk;
    private List<DialogueTalk> dialogueCutSceneTalk;

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
            "�������� �� ����!",
            "�� ��ٸ���!"),
            setDialogueTalk(1000, 100,
            "�Ͼ����!",
            "��ٸ��� �־���.",
            "���� ���� ���߱� ���� �ҷ�?"),
            setDialogueTalk(1000, 101,
            "��վ���?",
            "�츮 ���� �԰� �� ����."),
            setDialogueTalk(2000, 0,
            "������ �������",
            "������ ���ڱ�~"),
            setDialogueTalk(2000, 110,
            "�ɽ��ϸ� ���� �?",
            "���丮�� ��� �ִ� �� ������!",
            "���� ���� �� ��~"),
            setDialogueTalk(2000, 111,
            "���ϴµ�?",
            "�������� ���߱� ����� ��~"),
            setDialogueTalk(2000, 115,
            "������ �� ����غ�",
            "�׷� �߰�~"),
            setDialogueTalk(3000, 0,
            "ǰ�� �� �Ȱ� �ٴ� �ž�... ã���༭ ����..!",
            "���� �� �̻� �Ҿ������ ������"),
            setDialogueTalk(3000, 200,
            "�� ���� ������ �������.....",
            "���������� 2�� ū�濡�� ��Ű�����....."),
            setDialogueTalk(3000, 201,
            "�� ���� ����..!",
            "����!!!"),
            setDialogueTalk(4000, 0,
            "�����δ� �Ҿ������ ������ ����",
            "����"),
            setDialogueTalk(4000, 210,
            "�� ��� �峭���� �ֹ濡�� �Ҿ���� �� ������", 
            "ã���� ���ؼ� ���ƿԾ�.."),
            setDialogueTalk(4000, 211,
            "ã���༭ ����"),
         
            setDialogueTalk(1000, 150,
            "�̰Ŷ�~ ���Ŷ�~",
            "��� �׷�, ����~ ����~"),
            setDialogueTalk(2000, 160,
            "��.�����丮��",
            "���丮..����..����"),
            setDialogueTalk(3000, 250,
            "�� ���� ����..",
            "���������.. ����!!!!!"),
            setDialogueTalk(4000, 260,
            "�����R�R��������",
            "���R�������������R��"),  
        };

        dialogueCutSceneTalk = new List<DialogueTalk>
        {
            setDialogueCutSceneTalk(10,
            "��~ ������, ���� �ð��̿���",
            "������ ������ �ٰԿ�~")
        };
    }

    private DialogueTalk setDialogueTalk(int _npcId, int questId, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_npcId, questId, dialogues);
        return newDialogue;
    }

    private DialogueTalk setDialogueCutSceneTalk(int _cutScene, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_cutScene, dialogues);
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

    public List<string> GetCutSceneDialogue(int _cutScene)
    {
        for (int iNum = 0; iNum < dialogueCutSceneTalk.Count; iNum++)
        {
            if (dialogueCutSceneTalk[iNum].cutScene == _cutScene)
            {
                List<string> dialogueList = new List<string>(dialogueCutSceneTalk[iNum].dialogueLines);
                return dialogueList;
            }
        }

        return null;
    }
}
