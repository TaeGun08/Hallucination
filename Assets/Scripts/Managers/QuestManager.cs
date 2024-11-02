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
    /// ����Ʈ �����Ͱ� ���ٸ� �߰��ϴ� �Լ�
    /// </summary>
    private void questAdd()
    {
        quest.Add(100, new QuestData(1000, 100, "�� ����", false));
        quest.Add(101, new QuestData(1000, 101, "�� ���� Ŭ����", false));
        quest.Add(105, new QuestData(1000, 105, "�� ���� ����", false));
    }

    /// <summary>
    /// ����Ʈ�� �����ϴ� �Լ�
    /// </summary>
    private void questSave()
    {
        string questSave = JsonConvert.SerializeObject(quest);
        PlayerPrefs.SetString("questSaveKey", questSave);
    }

    /// <summary>
    /// ����Ʈ�� �����͸� �ҷ����� �Լ�
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
    /// ����Ʈ�� �������� ���� �Լ�
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
    /// ����Ʈ�� Ŭ�����Ű�� �Լ�
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
    /// ����Ʈ�� Ŭ���� �Ǿ����� Ȯ���ϱ� ���� �Լ�
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
    /// ����Ʈ ������ �����ϴ� ��ũ��Ʈ�� �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public QuestGames GetQuestGames()
    {
        return questGames;
    }
}
