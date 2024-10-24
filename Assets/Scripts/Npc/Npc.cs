using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("NPC����")]
    [SerializeField] private List<string> dialogues;
    [SerializeField] private GameObject npcCamera;
    [SerializeField] private bool questCheck = false;
    public bool QuestCheck
    {
        get
        {
            return questCheck;
        }
        set
        {
            questCheck = value;
        }
    }

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
    /// Npc�� ������ �ִ� string�� �־��ֱ� ���� �Լ�
    /// </summary>
    public List<string> DialogueCheck()
    {
        CameraManager.Instance.GetVirtualCamera(0).gameObject.SetActive(false);
        npcCamera.SetActive(true);
        return dialogues;
    }
}
