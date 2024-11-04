using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : ItemQuestNpc
{
    public override void getInven(Inventory _inven)
    {
        base.getInven(_inven);
        base.questItemCheck(base.npcId, base.questId[0], 201);
    }
}
