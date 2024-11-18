using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Teacher : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueManager dialogueManager;

    private NavMeshAgent agent; //몬스터의 agent
    private Animator anim;

    [Header("몬스터 설정")]
    [SerializeField] private LayerMask obstacleMask; // 장애물 레이어
    [SerializeField] private Collider checkColl; //콜라이더에 들어온 오브젝트를 체크하는 용도
    [SerializeField, Range(0f, 360f)] private float angle; // 시야 각도
    [SerializeField] private float distance; // 인식 거리
    private string currentSceneName;
    private TeacherPos teacherPos;
    public TeacherPos TeacherPos
    {
        set
        {
            teacherPos = value;
        }
    }
    private bool randomPosCheck;
    private int randomNumber;

    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private bool chaseCheck;
    [SerializeField] private float chaseTime;
    private float chaseTimer;
    private bool isChaseAudio;

    [SerializeField] private Transform headTrs;

    [SerializeField] private GameObject cameraObejct;
    private bool cameraOn;
    private float sceneChange;

    private void OnDrawGizmos()
    {
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * transform.forward;

        if (angle <= 180f)
        {
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * distance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * distance);
        }
        else
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * distance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * distance);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueManager = gameManager.GetManagers<DialogueManager>(2);

        currentSceneName = SceneManager.GetActiveScene().name;

        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    private void Update()
    {
        if (cameraOn == true)
        {
            float shakeValue = Random.Range(-5f, 5f);
            cameraObejct.transform.rotation = Quaternion.Euler(cameraObejct.transform.eulerAngles.x, cameraObejct.transform.eulerAngles.y, shakeValue);
            sceneChange += Time.deltaTime;

            if (sceneChange <= 0.1f)
            {
                agent.speed = 0;
                StartCoroutine(overAudioPlaying());
                anim.SetBool("isWalk", false);
                anim.SetBool("isFastWalk", false);
            }

            if (sceneChange >= 2)
            {
                sceneChange = 0;
                FadeInOut.Instance.SetActive(false, () =>
                {
                    GameManager.Instance.GameOver = true;

                    SceneManager.LoadSceneAsync("LoadingScene");

                    FadeInOut.Instance.SetActive(true);
                });
            }
        }

        if (dialogueManager.IsDialogue == false && cameraOn == false)
        {
            if (chaseCheck == true)
            {
                chaseTimer -= Time.deltaTime;
                if (chaseTimer <= 0)
                {
                    chaseCheck = false;
                    chaseTimer = 0;
                }
            }

            playerChase();
        }
    }

    /// <summary>
    /// 플레이어를 추격하기 위한 함수
    /// </summary>
    private void playerChase()
    {
        if (currentSceneName == "MapScene" && PlayerPrefs.GetInt("SaveScene") == 1 && cameraOn == false)
        {
            bool playerDetected = false;

            Collider[] checkColl = Physics.OverlapSphere(transform.position, distance);

            foreach (Collider coll in checkColl)
            {
                if (coll.gameObject.layer == LayerMask.NameToLayer("Door"))
                {
                    float distan = Vector3.Distance(coll.transform.position, transform.position);
                    if (distan <= 4)
                    {
                        Door doorSc = coll.GetComponent<Door>();
                        if (doorSc.TeacherRoomOpen == false)
                        {
                            doorSc.Open = true;
                        }
                    }
                }

                if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Vector3 directionToPlayer = (coll.transform.position - transform.position).normalized;
                    float checkDistance = Vector3.Distance(coll.transform.position, transform.position);
                    Vector3 teacherHeadRayPos = new Vector3(transform.position.x, headTrs.position.y, transform.position.z);

                    if (checkDistance <= 2)
                    {
                        cameraObejct.SetActive(true);
                        cameraOn = true;
                    }
                    else if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2 || checkDistance <= 8)
                    {
                        if (!Physics.Raycast(teacherHeadRayPos, directionToPlayer, checkDistance, obstacleMask))
                        {
                            if (chaseCheck == false)
                            {
                                chaseTimer = chaseTime;
                                chaseCheck = true;

                                if (isChaseAudio == false)
                                {
                                    StartCoroutine(chaseAudioPlaying());
                                }
                            }

                            agent.speed = 4f;
                            anim.SetBool("isFastWalk", true);
                            agent.SetDestination(coll.transform.position);
                            playerDetected = true;
                        }
                        else
                        {
                            if (chaseCheck == false)
                            {
                                audioSource.Pause();
                                agent.speed = 0;
                                anim.SetBool("isFastWalk", false);
                                anim.SetBool("isWalk", false);
                            }
                        }
                    }
                }
            }

            if (chaseCheck == true)
            {
                agent.SetDestination(GameManager.Instance.PlayerObject.transform.position);
            }

            if (isChaseAudio == true)
            {
                StartCoroutine(isChaseAudioPlaying());
            }

            setRandomPos();

            if (playerDetected == false && chaseCheck == false && isChaseAudio == false)
            {
                StartCoroutine(walkAudioPlaying());
                anim.SetBool("isFastWalk", false);
                anim.SetBool("isWalk", true);
            }
        }
    }

    /// <summary>
    /// 랜덤한 위치를 넣어주기 위한 함수
    /// </summary>
    private void setRandomPos()
    {
        if (chaseCheck == false)
        {
            if (randomPosCheck == true)
            {
                isChaseAudio = false;
                audioSource.Play();
                agent.speed = 3.5f;
                Vector3 myPos = transform.position;
                myPos.y = 0;
                Vector3 arrivalPos = teacherPos.TeacherTrs[randomNumber].position;
                arrivalPos.y = 0;

                float distan = Vector3.Distance(arrivalPos, myPos);

                if (distan >= 1f)
                {
                    agent.SetDestination(teacherPos.TeacherTrs[randomNumber].position);
                }
                else
                {
                    randomPosCheck = false;
                }
            }
            else
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }

                isChaseAudio = false;
                agent.speed = 3.5f;
                randomNumber = Random.Range(0, teacherPos.TeacherTrs.Count);
                anim.SetBool("isWalk", true);
                randomPosCheck = true;
            }
        }
    }

    private IEnumerator overAudioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[3];
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
    }

    /// <summary>
    /// 발검으 사운드를 실행시키키 위한 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator walkAudioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[0];
        audioSource.spatialBlend = 1f;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
    }

    /// <summary>
    /// 상황에 맞는 사운드를 실행시키기 위한 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator chaseAudioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[1];
        audioSource.loop = false;
        audioSource.spatialBlend = 0.5f;
        audioSource.Play();

        yield return new WaitForSeconds(2);

        isChaseAudio = true;
    }

    private IEnumerator isChaseAudioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[2];
        audioSource.loop = true;
        audioSource.spatialBlend = 0.5f;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
    }
}
