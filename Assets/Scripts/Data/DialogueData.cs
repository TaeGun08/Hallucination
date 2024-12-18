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
            "언제든지 또 놀자!",
            "널 기다릴게!"),
            setDialogueTalk(1000, 100,
            "일어났구나!",
            "기다리고 있었어.",
            "같이 퍼즐 맞추기 놀이 할래?"),
            setDialogueTalk(1000, 101,
            "재밌었지?",
            "우리 간식 먹고 또 하자."),
            setDialogueTalk(2000, 0,
            "오늘은 여기까지",
            "다음에 놀자구~"),
            setDialogueTalk(2000, 110,
            "심심하면 나랑 놀래?",
            "도토리가 어디에 있는 지 맞혀봐!",
            "쉽지 않을 거 라구~"),
            setDialogueTalk(2000, 111,
            "잘하는데?",
            "다음에는 맞추기 어려울 걸~"),
            setDialogueTalk(2000, 115,
            "다음에 더 노력해봐",
            "그럼 잘가~"),
            setDialogueTalk(3000, 0,
            "품에 꼭 안고 다닐 거야... 찾아줘서 고마워..!",
            "이제 더 이상 잃어버리지 않을게"),
            setDialogueTalk(3000, 200,
            "내 생선 인형이 사라졌어.....",
            "마지막으로 2층 큰방에서 논거같은데....."),
            setDialogueTalk(3000, 201,
            "내 생선 인형..!",
            "고마워!!!"),
            setDialogueTalk(4000, 0,
            "앞으로는 잃어버리지 않을게 고마워",
            "고마워"),
            setDialogueTalk(4000, 210,
            "내 당근 장난감을 주방에서 잃어버린 것 같은데", 
            "찾지를 못해서 돌아왔어.."),
            setDialogueTalk(4000, 211,
            "찾아줘서 고마워"),
         
            setDialogueTalk(1000, 150,
            "이거랑~ 저거랑~",
            "모두 그려, 가득~ 가득~"),
            setDialogueTalk(2000, 160,
            "….도…토리…",
            "도토리..ㅇㄹ..ㅇㄴ"),
            setDialogueTalk(3000, 250,
            "내 인형 내놔..",
            "내놓으라고.. 내놔!!!!!"),
            setDialogueTalk(4000, 260,
            "히히힣힣히히히ㅣ",
            "히힣히히히히히히힣ㅣ"),  
        };

        dialogueCutSceneTalk = new List<DialogueTalk>
        {
            setDialogueCutSceneTalk(10,
            "자~ 여러분, 간식 시간이에요",
            "달콤한 사탕을 줄게요~")
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
