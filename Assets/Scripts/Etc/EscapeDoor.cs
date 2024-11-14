using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeDoor : MonoBehaviour
{
    public static EscapeDoor Instance;

    private Animator anim;
    private Collider coll;

    [SerializeField] private Animator teacherAnim;
    [SerializeField] private AudioSource teacherAudio;
    [SerializeField] private GameObject teacher;

    private bool open;
    public bool Open
    {
        get
        {
            return open;
        }
        set
        {
            open = value;
        }
    }
    private float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {
        if (open)
        {
            anim.SetBool("isOpen", true);
            teacherAnim.SetBool("isGiveCandy", true);

            timer += Time.deltaTime;
            coll.isTrigger = true;
            teacher.SetActive(false);

            if (timer <= 0.3f && teacherAudio.isPlaying == false)
            {
                teacherAudio.Play();
            }

            if (timer >= 3)
            {
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("LoadingScene");

                    GameManager.Instance.GoDemoScene = true;

                    FadeInOut.Instance.SetActive(true);
                });
            }
        }
    }
}
