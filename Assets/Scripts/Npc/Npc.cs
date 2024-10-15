using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject npcCamera;

    private bool cameraOnOff = false;

    public bool CameraOnOff
    {
        get
        {
            return cameraOnOff;
        }
        set
        {
            cameraOnOff = value;
        }
    }

    private void Update()
    {
        npcCamera.SetActive(cameraOnOff == true ? true : false);
    }
}
