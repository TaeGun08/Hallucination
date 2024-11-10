using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXM : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;

    private void Start()
    {
        gameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource != null)
        {
            audioSource.volume = gameManager.Option.GetSlidersValue(1);
        }
    }
}
