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
            "�ȳ�! ���� �غ��� ������ �־�! ���� �غ��� ������?",
            "�ϳ��� ���纸�� ������� �ž�!"),
            setDialogueTalk(1000, 101,
            "��, �� �� ���� �����! �� ��¥ �ȶ��ϴ�!",
            "���� �� ó�� �ݹ� ���� �� �ְ� ����Ұ�! �߰�!"),
            setDialogueTalk(2000, 0,
            "���� �̱� ��ȸ�� �� �� ����!",
            "�ʹ� �����ݾ�!"),
            setDialogueTalk(2000, 110,
            "����, ���� �غ��� ���ӿ� ������ ����?",
            "�� �����̸� ��ĥ���� ����!"),
            setDialogueTalk(2000, 111,
            "���� �� ���ʰ� �ƴϾ�! �װ� �� ���ϴϱ�",
            "�� ���� �ٸ� �� ã�ƺ��߰ڴ�!"),
            setDialogueTalk(2000, 115,
            "����, ���� �̰���!",
            "���� �� �������� ��������� �־���!"),
            setDialogueTalk(3000, 0,
            "ǰ�� �� �Ȱ� �ٴ� �ž�... ã���༭ ����..!",
            "���� �� �̻� �Ҿ������ ������"),
            setDialogueTalk(3000, 200,
            "�� �峭��... ã�� �� ������...?",
            "Ȥ�ö� �� ã����... �ʹ� ����... ����, ������..."),
            setDialogueTalk(3000, 201,
            "����... ���� ����... �� �峭�� ã�Ҿ�!",
            "���� ����... ������ �Ҿ������ ������... �� ��, �� ��!"),
            setDialogueTalk(4000, 0,
            "�����δ� �Ҿ������ ���� �ž�",
            "����, �׳� ����"),
            setDialogueTalk(4000, 210,
            "��, �� �峭���� ��� ����?",
            "�پ��� ���ϱ�... ��� ������ �𸣰ڳ�..."),
            setDialogueTalk(4000, 211,
            "���п� ���� �ٽ� �� �� �־�",
            "ã���༭ ����"),

            setDialogueTalk(1000, 150,
            "�������� �츮�� ����... ���� �� ���� ��. �׷���? �ʵ� ������?",
            "������ ���� �׻� �츮 ������ �־�... �Ӹ� �ӿ��� �־�..."),
            setDialogueTalk(2000, 160,
            "�������� �� �˾�. �츮 �ȿ� ���� �ִ�����, �츮�� � ����������...",
            "�� �������� ���ϴ� ��� �� �ž�. �̰�... �´� �ž�."),
            setDialogueTalk(3000, 250,
            "�������� ���� ����... ������. ���� �������� �ʿ���...",
            "�װ� �װ� ���� ���ص�, �������. �ʴ� �� �״ϱ�..."),
            setDialogueTalk(4000, 260,
            "�������� ���� �׻� ���� �־�. �� ���� ��� �� �˰� �־�.",
            "�׷��ϱ� ���� �� �� �ʿ䵵 ����... �̹� �� ���������ϱ�..."),  
        };

        dialogueCutSceneTalk = new List<DialogueTalk>
        {
            setDialogueCutSceneTalk(10,
            "���� ���� �ð��̿���",
            "���ְ� �԰� �޳���� ���ô�")
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
