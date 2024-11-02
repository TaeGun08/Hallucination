using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public static QuestManager Instance;

    private QuestGames questGames;

    private Dictionary<int, QuestData> quest = new Dictionary<int, QuestData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        questGames = GetComponent<QuestGames>();

        qusetLoad();
    }

    /// <summary>
    /// 퀘스트 데이터가 없다면 추가하는 함수
    /// </summary>
    private void questAdd()
    {
        quest.Add(100, new QuestData(1000, 100, "컵 게임", false));
        quest.Add(101, new QuestData(1000, 101, "컵 게임 클리어", false));
        quest.Add(105, new QuestData(1000, 105, "컵 게임 실패", false));
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
        else
        {
            questAdd();
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
