using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    private Animator anim;
    private BoxCollider boxColl;

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
    private float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (open == true)
        {
            timer += Time.deltaTime;
            boxColl.isTrigger = true;
            if (timer >= 3)
            {
                boxColl.isTrigger = false;
                open = false;
                timer = 0;
            }
        }

        anim.SetBool("isOpen", open);
    }
}
