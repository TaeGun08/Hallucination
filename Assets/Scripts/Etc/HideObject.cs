using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private bool hide; //���� �� ������Ʈ�� ���� �����ִ��� üũ�ϱ� ���� ����
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
    [SerializeField] private Transform hideTrs; //�������� ��ġ
    [SerializeField] private Transform hideOnTrs; //������ �������� ��ġ

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
