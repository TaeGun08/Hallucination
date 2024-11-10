using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherCreater : MonoBehaviour
{
    [SerializeField] private GameObject teacherPrefab;
    [SerializeField] private Transform teacherTrs;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SaveScene") == 1)
        {
            GameObject obj = Instantiate(teacherPrefab, teacherTrs.position, Quaternion.identity, transform);
            Teacher teacher = obj.GetComponent<Teacher>();
            teacher.TeacherPos = transform.GetComponent<TeacherPos>();
        }
    }
}
