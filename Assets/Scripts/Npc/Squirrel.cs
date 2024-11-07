using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Npc
{
    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 0)
        {
            hasQuest = false;
        }
        else
        {
            hasQuest = true;
        }
    }


    private void Update()
    {
        base.npc();
    }
}
