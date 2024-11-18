using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandLight : MonoBehaviour
{
    private void Update()
    {
        Vector3 lookPos = Camera.main.transform.position + Camera.main.transform.forward * 5f;
        transform.LookAt(lookPos);
    }
}
