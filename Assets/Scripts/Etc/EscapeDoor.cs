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
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private GameObject teacher;
    [SerializeField] private GameObject teacherCamera;

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
            teacherCamera.SetActive(true);

            teacherCamera.transform.position += teacherCamera.transform.forward * 0.5f * Time.deltaTime;

            if (timer <= 0.3f && teacherAudio.isPlaying == false)
            {
                StartCoroutine(audioPlaying());
                GameManager.Instance.GoDemoScene = true;
                ExitTextCheck.Instance.SetActiveFalse();
            }

            if (timer >= 3)
            {
                float shakeValue = Random.Range(-2f, 2f);
                teacherCamera.transform.rotation = Quaternion.Euler(teacherCamera.transform.eulerAngles.x, teacherCamera.transform.eulerAngles.y, shakeValue);
            }

            if (timer >= 6)
            {
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("LoadingScene");

                    FadeInOut.Instance.SetActive(true);
                });
            }
        }
    }

    private IEnumerator audioPlaying()
    {
        teacherAudio.Pause();

        teacherAudio.clip = clips[0];
        teacherAudio.Play();

        yield return new WaitForSeconds(2);

        teacherAudio.clip = clips[1];
        teacherAudio.Play();
    }
}
