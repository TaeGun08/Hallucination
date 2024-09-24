using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField, Tooltip("기본 속도")] private float moveSpeed;

    [SerializeField] private Transform headTrs;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        headTrs.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z - 90f);
    }
}
