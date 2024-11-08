using System.Collections;
using System.Collections.Generic;
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
    private Transform playerTrs; //플레이어의 위치
    [SerializeField, Range(0f, 360f)] private float angle = 45f; // 시야 각도
    [SerializeField] private float distance = 10f; // 인식 거리
    private string currentSceneName;
    [SerializeField] private TeacherPos teacherTrs;
    private bool stopTeacher;
    private bool animChange;
    private float changeAnimTimer;

    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    private bool chaseCheck;
    [SerializeField] private float chaseTimer;

    // 애니메이션 체크
    private bool isWalk;

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
    }

    private void Update()
    {
        if (chaseCheck == true)
        {
            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                chaseTimer = 0;
            }
        }
        //playerTestChase();
        playerChase();
        cutScene();
    }
    /// <summary>
    /// 테스트용
    /// </summary>
    private void playerTestChase()
    {
        if (currentSceneName != "TeacherCutScene")
        {
            bool playerDetected = false;

            Collider[] checkColl = Physics.OverlapSphere(transform.position, 20);

            if (checkColl != null)
            {
                foreach (Collider coll in checkColl)
                {
                    if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        Vector3 directionToPlayer = (coll.transform.position - transform.position).normalized;

                        if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
                        {
                            if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask))
                            {
                                if (chaseCheck == false)
                                {
                                    chaseTimer = 15;
                                    StartCoroutine(audioPlaying());
                                    chaseCheck = true;
                                }
                                anim.SetBool("isFastWalk", true);
                                agent.SetDestination(coll.transform.position);
                                playerDetected = true;
                            }
                        }
                    }

                    if (coll.gameObject.layer == LayerMask.NameToLayer("Door"))
                    {
                        float distan = Vector3.Distance(transform.position, coll.transform.position);
                        if (distan <= 5)
                        {
                            Door doorSc = coll.GetComponent<Door>();
                            doorSc.Open = true;
                        }
                    }
                }
            }

            if (!playerDetected && chaseCheck && chaseTimer <= 0)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                chaseCheck = false;
                anim.SetBool("isFastWalk", false);
            }
        }
    }

    /// <summary>
    /// 플레이어를 추격하기 위한 함수
    /// </summary>
    private void playerChase()
    {
        if (currentSceneName == "MapScene" && PlayerPrefs.GetInt("SaveScene") >= 3)
        {
            bool playerDetected = false;

            Collider[] checkColl = Physics.OverlapSphere(transform.position, 20);

            if (checkColl != null)
            {
                foreach (Collider coll in checkColl)
                {
                    if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        Vector3 directionToPlayer = (coll.transform.position - transform.position).normalized;

                        if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
                        {
                            if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask))
                            {
                                if (chaseCheck == false)
                                {
                                    chaseTimer = 15;
                                    StartCoroutine(audioPlaying());
                                    chaseCheck = true;
                                }
                                anim.SetBool("isFastWalk", true);
                                agent.SetDestination(coll.transform.position);
                                playerDetected = true;
                            }
                        }
                    }

                    if (coll.gameObject.layer == LayerMask.NameToLayer("Door"))
                    {
                        float distan = Vector3.Distance(transform.position, coll.transform.position);
                        if (distan <= 5)
                        {
                            Door doorSc = coll.GetComponent<Door>();
                            doorSc.Open = true;
                        }
                    }
                }
            }

            if (!playerDetected && chaseCheck && chaseTimer <= 0)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                chaseCheck = false;
                anim.SetBool("isFastWalk", false);
            }
        }
    }

    private IEnumerator audioPlaying()
    {
        audioSource.Pause();

        audioSource.clip = audioClips[1];
        audioSource.loop = false;
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
                    float distan = Vector3.Distance(transform.position, door.transform.position);
                    if (distan <= 5)
                    {
                        Door doorSc = door.GetComponent<Door>();
                        doorSc.Open = true;
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
