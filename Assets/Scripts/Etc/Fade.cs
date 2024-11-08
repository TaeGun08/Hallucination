using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FadeInOut.Instance.functionFade());
    }
}
