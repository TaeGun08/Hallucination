using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private Collider coll;

    [SerializeField] private bool open;
    public bool Open
    {
        get
        {
            return open;
        }
        set
        {
            timer = 0;
            open = value;
        }
    }
    [SerializeField] private bool teacherRoomOpen;
    public bool TeacherRoomOpen
    {
        get
        {
            return teacherRoomOpen;
        }
        set
        {
            teacherRoomOpen = value;
        }
    }

    private float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {
        if (teacherRoomOpen == false)
        {
            if (open == true)
            {
                timer += Time.deltaTime;
                coll.isTrigger = true;
                if (timer >= 3)
                {
                    coll.isTrigger = false;
                    open = false;
                    timer = 0;
                }
            }

            anim.SetBool("isOpen", open);
        }
    }
}
