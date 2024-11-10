using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : ItemQuestNpc
{
    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 260;
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
        }
        else
        {
            base.questId[0] = 210;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
        }
    }


    public override void getInven(Inventory _inven)
    {
        base.getInven(_inven);
        base.questItemCheck(base.npcId, base.questId[0], 211);
    }
}
