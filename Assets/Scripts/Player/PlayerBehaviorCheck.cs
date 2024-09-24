using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorCheck : MonoBehaviour
{
    [Header("�÷��̾��� �ൿ")]
    [SerializeField, Tooltip("�ൿ�� �ϰ� �ִ���")] private bool isBehavior;
    public bool IsBehavior
    {
        get
        {
            return isBehavior;
        }
        set
        {
            isBehavior = value;
        }
    }

    private float walkRunCheck; //�ȴ� ������ �ٴ� ������ üũ
    public float WalkRunCheck
    {
        get
        {
            return walkRunCheck;
        }
        set
        {
            walkRunCheck = value;
        }
    }

    private float hideCheck; //�������� üũ
    public float HideCheck
    {
        get
        {
            return hideCheck;
        }
        set
        {
            hideCheck = value;
        }
    }

    private float isHorizontal; //�������� �����̰� �ִ��� üũ
    public float IsHorizontal
    {
        get
        {
            return isHorizontal;
        }
        set
        {
            isHorizontal = value;
        }
    }

    private float isVertical; //�������� �����̰� �ִ��� üũ
    public float IsVertical
    {
        get
        {
            return isVertical;
        }
        set
        {
            isVertical = value;
        }
    }
}
