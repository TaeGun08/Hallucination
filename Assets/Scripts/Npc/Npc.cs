using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("NPC설정")]
    [SerializeField] private List<string> dialogues;
    [SerializeField] private GameObject npcCamera;

    //private bool cameraOnOff = false;

    //public bool CameraOnOff
    //{
    //    get
    //    {
    //        return cameraOnOff;
    //    }
    //    set
    //    {
    //        cameraOnOff = value;
    //    }
    //}

    private void Update()
    {
        npcCamera.SetActive(DialogueManager.Instance.IsDialogue == false ? false : true);
    }

    /// <summary>
    /// Npc가 가지고 있는 string을 넣어주기 위한 함수
    /// </summary>
    public List<string> DialogueCheck()
    {
        CameraManager.Instance.GetVirtualCamera(0).gameObject.SetActive(false);
        npcCamera.SetActive(true);
        return dialogues;
    }
}
