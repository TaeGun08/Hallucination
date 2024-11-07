using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPos : MonoBehaviour
{
    [SerializeField] private List<Transform> teacherTrs;
    public List<Transform> TeacherTrs
    {
        get
        {
            return teacherTrs;
        }
        set
        {
            teacherTrs = value;
        }
    }
}
