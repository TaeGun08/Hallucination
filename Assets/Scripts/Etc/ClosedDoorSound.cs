using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoorSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> doorClips;
    private AudioSource doorAuido;

    private void Awake()
    {
        doorAuido = GetComponent<AudioSource>();
    }

    public void OpenDoorPlay()
    {
        doorAuido.clip = doorClips[0];
        doorAuido.Play();
    }

    public void ClosedDoorPlay()
    {
        doorAuido.clip = doorClips[1];
        doorAuido.Play();
    }
}
