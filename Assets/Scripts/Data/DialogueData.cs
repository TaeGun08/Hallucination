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
    /// 다이얼로그 대화를 관리하는 함수
    /// </summary>
    private void dialogueTalkManage()
    {
        dialogueTalk = new List<DialogueTalk>
        {
            setDialogueTalk(1000, 0, 
            "안녕!", 
            "나한테 무슨 볼 일 있니?"),
            setDialogueTalk(1000, 100, 
            "안녕, 나랑 같이 퍼즐 게임할래?", 
            "엄청 재미있을 거야!"),
            setDialogueTalk(1000, 101, 
            "역시 같이 맞추니깐 빨리 맞추는 것 같아!", 
            "재미있었어 다음에도 또 하자!"),
            setDialogueTalk(2000, 0,
            "볼 일 있음?",
            "없음 가셈"),
            setDialogueTalk(2000, 110,
            "나랑 야바위 ㄱ?",
            "걍 발라드림"),
            setDialogueTalk(2000, 111,
            "ㄷㄷ 왜이리 잘하심",
            "자존심 상하네 ㅊ.."),
            setDialogueTalk(2000, 115,
            "ㅋㅋㅋㅋ ez 하죠",
            "한 번 더 덤벼보셈ㅋㅋ"),
            setDialogueTalk(3000, 0,
            "나는 개똥벌레",
            "친구가 없네"),
            setDialogueTalk(3000, 200,
            "내 생성 장남감을 찾아줘",
            "부탁할게"),
            setDialogueTalk(3000, 201,
            "고마워",
            "다음에 도움이 필요하면 도와줄게"),
        };
    }

    private DialogueTalk setDialogueTalk(int _npcId, int questId, params string[] dialogues)
    {
        DialogueTalk newDialogue = new DialogueTalk(_npcId, questId, dialogues);
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
}
