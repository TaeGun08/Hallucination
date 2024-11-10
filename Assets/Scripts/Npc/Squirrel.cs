using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Npc
{
    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 160;
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
        }
        else
        {
            base.questId[0] = 110;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
        }
    }


    private void Update()
    {
        base.npc();
    }
}
