using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Npc
{
    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 150;
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
        }
        else
        {
            base.questId[0] = 100;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
        }
    }

    private void Update()
    {
        base.npc();
    }
}
