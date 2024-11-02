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
    /// 다이얼로그 대화를 관리하는 함수
    /// </summary>
    private void dialogueTalkManage()
    {
        dialogueTalk = new List<DialogueTalk>
        {
            new DialogueTalk(1000, 0, new List<string>
            {
                "안녕, 나는 멍뭉이야",
                "나한테 무슨 볼일 있니?"
            }),
             new DialogueTalk(1000, 100, new List<string>
            {
                 "안녕, 나는 멍뭉이야",
                 "나랑 같이 퍼즐 게임하자",
                 "같이 맞춰 보자"
            }),
            new DialogueTalk(1000, 101, new List<string>
            {
                 "너 정말 대단하구나!",
                 "혼자 다 맞추다니, 짱이다!",
                 "다음에도 또 놀자, 잘가!"
            }),
            new DialogueTalk(1000, 105, new List<string>
            {
                 "정말 아깝다!",
                 "심심할 때 다시 와 또 하자",
            }),
        };
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
                return dialogueTalk[iNum].dialogueLines;
            }
        }

        return null;
    }
}
