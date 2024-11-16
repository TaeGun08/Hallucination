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
    private bool stopTeacher;
    private bool animChange;
    private float changeAnimTimer;

    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    private bool chaseCheck;
    private bool isChase;
    [SerializeField] private float chaseTimer;
    private bool dontStopChase;
    private float dontStopChaseTimer;

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
            if (sceneChange >= 2)
            {
                cameraOn = false;
                sceneChange = 0;
                FadeInOut.Instance.SetActive(false, () =>
                {
                    SceneManager.LoadSceneAsync("LoadingScene");

                    GameManager.Instance.GameOver = true;

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
                    chaseTimer = 0;
                    chaseCheck = false;
                }
            }

            if (dontStopChase == true)
            {
                dontStopChaseTimer += Time.deltaTime;
                if (dontStopChaseTimer > 10)
                {
                    dontStopChase = false;
                    dontStopChaseTimer = 0;
                }
            }

            playerChase();
        }

        cutScene();
    }

    /// <summary>
    /// 플레이어를 추격하기 위한 함수
    /// </summary>
    private void playerChase()
    {
        if (currentSceneName == "MapScene" && PlayerPrefs.GetInt("SaveScene") == 1)
        {
            bool playerDetected = false;
            bool playerChaseCheck = false;

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
                    playerChaseCheck = true;

                    Vector3 directionToPlayer = (coll.transform.position - transform.position).normalized;
                    float checkDistance = Vector3.Distance(coll.transform.position, transform.position);
                    Vector3 teacherHeadRayPos = new Vector3(transform.position.x, headTrs.position.y, transform.position.z);

                    if (checkDistance <= 3)
                    {
                        cameraObejct.SetActive(true);
                        cameraOn = true;
                        anim.SetBool("isWalk", false);
                        anim.SetBool("isFastWalk", false);
                    }
                    else if (dontStopChaseTimer <= 5 && dontStopChase == true)
                    {
                        agent.SetDestination(coll.transform.position);
                        anim.SetBool("isWalk", false);
                        anim.SetBool("isFastWalk", true);
                    }
                    else if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2 || checkDistance <= 8)
                    {
                        if (!Physics.Raycast(teacherHeadRayPos, directionToPlayer, checkDistance, obstacleMask))
                        {
                            if (chaseCheck == false && isChase == false && dontStopChase == false)
                            {
                                chaseTimer = 15;
                                StartCoroutine(chaseAudioPlaying());
                                chaseCheck = true;
                                isChase = true;
                                dontStopChase = true;
                            }

                            agent.speed = 4.5f;
                            anim.SetBool("isWalk", false);
                            anim.SetBool("isFastWalk", true);
                            agent.SetDestination(coll.transform.position);
                            playerDetected = true;
                        }
                        else
                        {
                            isChase = false;
                            anim.SetBool("isFastWalk", false);
                            anim.SetBool("isWalk", false);
                        }

                        if (isChase == true)
                        {
                            agent.SetDestination(coll.transform.position);
                        }
                    }
                }
            }

            if (playerChaseCheck == false)
            {
                isChase = false;
            }

            setRandomPos();

            if (playerDetected == false && chaseCheck == false && chaseTimer <= 0 && isChase == false && dontStopChase == false)
            {
                StartCoroutine(walkAudioPlaying());
                chaseCheck = false;
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
        if (chaseCheck == false && isChase == false)
        {
            if (randomPosCheck == true)
            {
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
                agent.speed = 4;
                randomNumber = Random.Range(0, teacherPos.TeacherTrs.Count);
                anim.SetBool("isWalk", true);
                randomPosCheck = true;
            }
        }
    }

    /// <summary>
    /// 발검으 사운드를 실행시키키 위한 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator walkAudioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[0];
        audioSource.minDistance = 1;
        audioSource.maxDistance = 3;
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
        audioSource.minDistance = 5;
        audioSource.maxDistance = 10;
        audioSource.Play();

        yield return new WaitForSeconds(2);

        if (chaseCheck == true)
        {
            audioSource.clip = audioClips[2];
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /// <summary>
    /// 컷씬에서 재생할 애니메이션
    /// </summary>
    private void cutScene()
    {
        if (currentSceneName == "TeacherCutScene" && stopTeacher == false)
        {
            Collider[] checkColl = Physics.OverlapSphere(transform.position, 10, LayerMask.GetMask("Door"));

            if (checkColl != null)
            {
                foreach (var door in checkColl)
                {
                    float distan = Vector3.Distance(door.transform.position, transform.position);
                    if (distan <= 4)
                    {
                        Door doorSc = door.GetComponent<Door>();
                        if (doorSc.TeacherRoomOpen == false)
                        {
                            doorSc.Open = true;
                        }
                    }
                }
            }

            Vector3 myPos = transform.position;
            myPos.y = 0;
            Vector3 teaPos = gameManager.PlayerObject.transform.position;
            teaPos.y = 0;

            float distanceCheck = Vector3.Distance(teaPos, myPos);

            if (distanceCheck > 2.7f)
            {
                anim.SetBool("isWalk", true);
                agent.SetDestination(gameManager.PlayerObject.transform.position);
            }
            else
            {
                audioSource.Pause();
                anim.SetBool("isWalk", false);
                Vector3 rot = transform.eulerAngles;
                rot.y = 90;
                transform.eulerAngles = rot;
                agent.SetDestination(new Vector3(0, 0, 0));
                dialogueManager.StartCutSceneDialogue(10);
                anim.SetBool("isGiveCandy", true);
                stopTeacher = true;
            }
        }
        else if (currentSceneName == "TeacherCutScene" && stopTeacher == true && animChange == false)
        {
            changeAnimTimer += Time.deltaTime;
            if (changeAnimTimer >= 2)
            {
                anim.SetBool("isGiveCandy", false);
                animChange = true;
            }
        }
    }
}
