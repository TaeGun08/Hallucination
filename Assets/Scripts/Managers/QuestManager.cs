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
    private QuestGames questGames;

    private Dictionary<int, QuestData> quest = new Dictionary<int, QuestData>();

    private void Awake()
    {
        questGames = GetComponent<QuestGames>();

        questAdd();
        qusetLoad();
    }

    /// <summary>
    /// ����Ʈ �߰��� �����ϰ� �ϱ� ���� �Լ�
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
    /// ����Ʈ �����Ͱ� ���ٸ� �߰��ϴ� �Լ�
    /// </summary>
    private void questAdd()
    {
        addQuest(1000, 100, "���� ����", false);
        addQuest(1000, 101, "���� ���� Ŭ����", false);

        addQuest(2000, 110, "�� ����", false);
        addQuest(2000, 111, "�� ���� Ŭ����", false);
        addQuest(2000, 115, "�� ���� ����", false);

        addQuest(3000, 200, "���� �峭�� ã��", false);
        addQuest(3000, 201, "���� �峭�� �ֱ�", false);

        addQuest(4000, 210, "��� �峭�� ã��", false);
        addQuest(4000, 211, "��� �峭�� �ֱ�", false);

        addQuest(1000, 150, "2���� ���", false);
        addQuest(2000, 160, "2���� ���", false);
        addQuest(3000, 250, "1���� ���", false);
        addQuest(4000, 260, "1���� ���", false);
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

    /// <summary>
    /// ����Ʈ �����͸� �ʱ�ȭ�ϱ� ���� �Լ�
    /// </summary>
    public void ResetQuestData()
    {
        quest = new Dictionary<int, QuestData>();
        questAdd();
    }
}
