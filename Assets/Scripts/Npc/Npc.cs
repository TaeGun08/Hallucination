using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("NPC����")]
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
        npcCamera.SetActive(DialogueManager.Instance.IsDialogue == true ? true : false);
    }

    /// <summary>
    /// Npc�� ������ �ִ� string�� �־��ֱ� ���� �Լ�
    /// </summary>
    public List<string> DialogueCheck()
    {
        return dialogues;
    }
}
