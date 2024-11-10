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
            "안녕! 내가 준비한 퍼즐이 있어! 같이 해보지 않을래?",
            "하나씩 맞춰보면 재미있을 거야!"),
            setDialogueTalk(1000, 101,
            "와, 난 한 조각 맞췄네! 너 진짜 똑똑하다!",
            "나도 너 처럼 금방 맞출 수 있게 노력할게! 잘가!"),
            setDialogueTalk(2000, 0,
            "더는 이길 기회를 줄 수 없어!",
            "너무 잘하잖아!"),
            setDialogueTalk(2000, 110,
            "헤헤, 내가 준비한 게임에 도전해 볼래?",
            "눈 깜빡이면 놓칠지도 몰라!"),
            setDialogueTalk(2000, 111,
            "이젠 내 차례가 아니야! 네가 더 잘하니까",
            "난 이제 다른 걸 찾아봐야겠다!"),
            setDialogueTalk(2000, 115,
            "봐봐, 내가 이겼지!",
            "역시 내 눈속임을 따라오려면 멀었어!"),
            setDialogueTalk(3000, 0,
            "품에 꼭 안고 다닐 거야... 찾아줘서 고마워..!",
            "이제 더 이상 잃어버리지 않을게"),
            setDialogueTalk(3000, 200,
            "내 장난감... 찾을 수 있을까...?",
            "혹시라도 못 찾으면... 너무 슬퍼... 제발, 도와줘..."),
            setDialogueTalk(3000, 201,
            "정말... 정말 고마워... 내 장난감 찾았어!",
            "정말 고마워... 다음엔 잃어버리지 않을게... 잘 가, 또 봐!"),
            setDialogueTalk(4000, 0,
            "앞으로는 잃어버리지 않을 거야",
            "고마워, 그냥 고마워"),
            setDialogueTalk(4000, 210,
            "어, 내 장난감이 어디 갔지?",
            "뛰어놀다 보니까... 어디 갔는지 모르겠네..."),
            setDialogueTalk(4000, 211,
            "덕분에 이제 다시 놀 수 있어",
            "찾아줘서 고마워"),

            setDialogueTalk(1000, 150,
            "선생님은 우리를 아주... 아주 잘 돌봐 줘. 그렇지? 너도 느끼지?",
            "선생님 손이 항상 우리 주위에 있어... 머리 속에도 있어..."),
            setDialogueTalk(2000, 160,
            "선생님은 다 알아. 우리 안에 뭐가 있는지도, 우리가 어떤 존재인지도...",
            "난 선생님이 원하는 대로 될 거야. 이게... 맞는 거야."),
            setDialogueTalk(3000, 250,
            "선생님은 나를 정말... 좋아해. 나는 선생님이 필요해...",
            "네가 그걸 이해 못해도, 상관없어. 너는 모를 테니까..."),
            setDialogueTalk(4000, 260,
            "선생님이 나를 항상 보고 있어. 내 안의 모든 걸 알고 있어.",
            "그러니까 내가 뭘 할 필요도 없어... 이미 다 정해졌으니까..."),  
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
