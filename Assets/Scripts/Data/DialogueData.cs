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
    /// 다이얼로그 대화를 관리하는 함수
    /// </summary>
    private void dialogueTalkManage()
    {
        dialogueTalk = new List<DialogueTalk>
        {
            setDialogueTalk(1000, 0,
            "일어났구나!",
            "기다리고 있었어"),
            setDialogueTalk(1000, 100,
            "같이 퍼즐 맞추기 놀이 할래?",
            "엄청 재미있을 거야!"),
            setDialogueTalk(1000, 101,
            "역시 같이 맞추니깐 빨리 맞추는 것 같아!",
            "재미있었어 다음에도 또 하자!"),
            setDialogueTalk(2000, 0,
            "왔어?",
            "심심하면 나랑 놀래?"),
            setDialogueTalk(2000, 110,
            "도토리가 어디에 있는 지 맞혀봐!",
            "쉽지 않을 거 라구~"),
            setDialogueTalk(2000, 111,
            "잘하는데?",
            "다음에는 맞추지 못할 걸~"),
            setDialogueTalk(2000, 115,
            "쉽지 않다고 했잖아~",
            "연습해서 오라구~"),
            setDialogueTalk(3000, 0,
            "나는 개똥벌레",
            "친구가 없네"),
            setDialogueTalk(3000, 200,
            "내 생성 장남감을 찾아줘",
            "부탁할게"),
            setDialogueTalk(3000, 201,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(4000, 0,
            "나는 개똥벌레",
            "친구가 없네"),
            setDialogueTalk(4000, 210,
            "내 생성 장남감을 찾아줘",
            "부탁할게"),
            setDialogueTalk(4000, 211,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(1000, 150,
            "퍼즐 놀이..?",
            "싫은데",
            "그림 그릴 거야"),
            setDialogueTalk(1000, 151,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(2000, 160,
            "나랑 놀고 싶다고?",
            "싫은데"),
            setDialogueTalk(2000, 161,
            ""),
            setDialogueTalk(3000, 250,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(3000, 251,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(4000, 260,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
            setDialogueTalk(4000, 261,
            "고마워", 
            "다음에 도움이 필요하면 도와줄게"),      
        };

        dialogueCutSceneTalk = new List<DialogueTalk>
        {
            setDialogueCutSceneTalk(10,
            "사탕 먹을 시간이에요",
            "맛있게 먹고 꿈나라로 갑시다")
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
    /// 다이얼로그 대화를 가져오기 위한 함수
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
