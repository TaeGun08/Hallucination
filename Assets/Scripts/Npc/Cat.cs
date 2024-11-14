using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : ItemQuestNpc
{
    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 250;
            base.material.SetTexture("_BaseMap", base.matTrxture[2]);
            base.anim.SetBool("isAngry", true);
        }
        else
        {
            base.questId[0] = 200;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (base.questId[0] == 201)
        {
            base.anim.SetBool("isCry", false);
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
        }
        else
        {
            base.anim.SetBool("isCry", true);
        }
    }


    public override void getInven(Inventory _inven)
    {
        base.getInven(_inven);
        base.questItemCheck(base.npcId, base.questId[0], 201);
    }
}
