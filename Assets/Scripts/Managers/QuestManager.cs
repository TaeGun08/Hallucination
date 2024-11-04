using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    private int npcId;
    private int questId;
    public int QuestId
    {
        get
        {
            return questId;
        }
        set
        {
            questId = value;
        }
    }
    private string questName;
    private bool questClear;
    public bool QuestClear
    {
        get
        {
            return questClear;
        }
        set
        {
            questClear = value;
        }
    }

    public QuestData(int _npcId, int _questId, string _questName, bool _questClear)
    {
        npcId = _npcId;
        questId = _questId;
        questName = _questName;
        questClear = _questClear;
    }
}

public class QuestManager : MonoBehaviour
{
    private QuestGames questGames;

    private Dictionary<int, QuestData> quest = new Dictionary<int, QuestData>();

    private void Awake()
    {
        questGames = GetComponent<QuestGames>();

        questAdd();
        qusetLoad();
    }

    /// <summary>
    /// 퀘스트 데이터가 없다면 추가하는 함수
    /// </summary>
    private void questAdd()
    {
        addQuest(1000, 100, "퍼즐 게임",  false);
        addQuest(1000, 101, "퍼즐 게임 클리어", false);

        addQuest(2000, 110, "컵 게임", false);
        addQuest(2000, 111, "컵 게임 클리어", false);
        addQuest(2000, 115, "컵 게임 실패", false);

        addQuest(3000, 200, "생선 장난감 찾기", false);
        addQuest(3000, 201, "생선 장난감 주기", false);

        addQuest(4000, 210, "당근 장난감 찾기", false);
        addQuest(4000, 211, "당근 장난감 주기", false);
    }

    /// <summary>
    /// 퀘스트 추가를 간결하게 하기 위한 함수
    /// </summary>
    /// <param name="_npcId"></param>
    /// <param name="_questId"></param>
    /// <param name="_questName"></param>
    /// <param name="_questClear"></param>
    private void addQuest(int _npcId, int _questId, string _questName, bool _questClear)
    {
        quest.Add(_questId, new QuestData(_npcId, _questId, _questName, _questClear));
    }

    /// <summary>
    /// 퀘스트를 저장하는 함수
    /// </summary>
    private void questSave()
    {
        string questSave = JsonConvert.SerializeObject(quest);
        PlayerPrefs.SetString("questSaveKey", questSave);
    }

    /// <summary>
    /// 퀘스트의 데이터를 불러오는 함수
    /// </summary>
    private void qusetLoad()
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("questSaveKey")))
        {
            string qeustLoad = PlayerPrefs.GetString("questSaveKey");
            quest = JsonConvert.DeserializeObject<Dictionary<int, QuestData>>(qeustLoad);
        }
    }

    /// <summary>
    /// 퀘스트를 가져오기 위한 함수
    /// </summary>
    /// <param name="_questId"></param>
    /// <returns></returns>
    public int GetQuestId(List<int> _questId)
    {
        foreach (int q_id in _questId)
        {
            if (quest.ContainsKey(q_id))
            {
                return quest[q_id].QuestId;
            }
        }


        return 0;
    }

    /// <summary>
    /// 퀘스트를 클리어시키는 함수
    /// </summary>
    /// <param name="_questId"></param>
    public void CompleteQuest(int _questId)
    {
        if (quest.ContainsKey(_questId) && quest[_questId].QuestClear == false)
        {
            quest[_questId].QuestClear = true;
            questSave();
        }
    }

    /// <summary>
    /// 퀘스트가 클리어 되었는지 확인하기 위한 함수
    /// </summary>
    /// <param name="_questId"></param>
    /// <returns></returns>
    public bool QuestCheck(int _questId)
    {
        if (quest.ContainsKey(_questId))
        {
            return quest[_questId].QuestClear;
        }

        return false;
    }

    /// <summary>
    /// 퀘스트 게임을 관리하는 스크립트를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public QuestGames GetQuestGames()
    {
        return questGames;
    }
}
