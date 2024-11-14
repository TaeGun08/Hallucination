using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Npc
{
    [SerializeField] private GameObject crayon;

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            base.questId[0] = 150;
            base.material.SetTexture("_BaseMap", base.matTrxture[1]);
            base.anim.SetBool("isDraw", true);
            crayon.SetActive(true);
        }
        else
        {
            base.questId[0] = 100;
            base.material.SetTexture("_BaseMap", base.matTrxture[0]);
            crayon.SetActive(false);
        }
    }

    private void Update()
    {
        base.npc();
    }
}
