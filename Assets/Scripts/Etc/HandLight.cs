using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : MonoBehaviour
{
    private void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        Vector3 direction = (worldPoint - transform.position).normalized;
        transform.LookAt(worldPoint);
    }
}
