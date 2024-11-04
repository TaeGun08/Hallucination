using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestNpc : Npc
{
    [Header("퀘스트 아이템")]
    [SerializeField] protected int itemIndex;
    protected bool questCheck;
    protected Inventory inven;

    protected virtual void Update()
    {
        base.npc();
    }

    protected virtual void questItemCheck(int _npcId, int _questId, int _questClearId)
    {
        if (inven != null)
        {
            if (base.questManager.QuestCheck(_questId) == false && inven.InveItemCheck(itemIndex) == true)
            {
                base.npcCamera.SetActive(true);
                cameraManager.GetVirtualCamera(0).gameObject.SetActive(false);
                inven.UseItem(itemIndex);
                base.questManager.CompleteQuest(_questId);
                base.dialogueManager.StartDialogue(_npcId, new List<int> { _questClearId });
                base.questManager.CompleteQuest(_questClearId);
                inven = null;
            }
        }
    }

    public virtual void getInven(Inventory _inven)
    {
        inven = _inven;
    }
}
