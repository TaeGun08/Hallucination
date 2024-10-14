using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private bool hide; //지금 이 오브젝트를 통해 숨어있는지 체크하기 위한 변수
    public bool Hide
    {
        get
        {
            return hide;
        }
        set
        {
            hide = value;
        }
    }
    [SerializeField] private Transform hideTrs; //숨어있을 위치
    [SerializeField] private Transform hideOnTrs; //밖으로 나와있을 위치

    private GameObject player;

    private void Update()
    {
        hideOnCheck();
    }

    private void hideOnCheck()
    {
        if (Input.GetKeyDown(KeyCode.E) && hide == true)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            characterController.height = 2;
            characterController.enabled = false;
            player.transform.position = hideOnTrs.position;
            characterController.enabled = true;
            hide = false;
        }
    }

    public Transform HideTransform()
    {
        return hideTrs;
    }

    public Transform HideOnTransform()
    {
        return hideOnTrs;
    }

    public void PlayerObject(GameObject _player)
    {
        player = _player;
    }
}
