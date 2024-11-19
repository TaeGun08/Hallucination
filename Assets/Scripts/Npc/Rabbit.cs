using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : ItemQuestNpc
{
    [SerializeField] private List<Transform> trs;

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 260;
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
            base.anim.SetBool("isHead", true);
            transform.position = trs[1].position;
        }
        else
        {
            base.questId[0] = 210;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
            transform.position = trs[0].position;
        }
    }

    public override void getInven(Inventory _inven)
    {
        base.getInven(_inven);
        base.questItemCheck(base.npcId, base.questId[0], 211);
    }

    public override int GetNpcId()
    {
        base.gameManager.CatRabbitQuestCheck(2);
        base.GetNpcId();
        return base.npcId;
    }
}
