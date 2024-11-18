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

    private NavMeshAgent agent; //������ agent
    private Animator anim;

    [Header("���� ����")]
    [SerializeField] private LayerMask obstacleMask; // ��ֹ� ���̾�
    [SerializeField] private Collider checkColl; //�ݶ��̴��� ���� ������Ʈ�� üũ�ϴ� �뵵
    [SerializeField, Range(0f, 360f)] private float angle; // �þ� ����
    [SerializeField] private float distance; // �ν� �Ÿ�
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
    [SerializeField] private bool chaseCheck;
    [SerializeField] private float chaseTime;
    [SerializeField] private float chaseTimer;

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
                    chaseCheck = false;
                    chaseTimer = 0;
                }
            }

            playerChase();
        }
    }

    /// <summary>
    /// �÷��̾ �߰��ϱ� ���� �Լ�
    /// </summary>
    private void playerChase()
    {
        if (currentSceneName == "MapScene" && PlayerPrefs.GetInt("SaveScene") == 1)
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

                    if (checkDistance <= 3)
                    {
                        cameraObejct.SetActive(true);
                        cameraOn = true;
                        anim.SetBool("isWalk", false);
                        anim.SetBool("isFastWalk", false);
                    }
                    else if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2 || checkDistance <= 8)
                    {
                        if (!Physics.Raycast(teacherHeadRayPos, directionToPlayer, checkDistance, obstacleMask))
                        {
                            if (chaseCheck == false)
                            {
                                chaseTimer = chaseTime;
                                StartCoroutine(chaseAudioPlaying());
                                chaseCheck = true;
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

            setRandomPos();

            if (playerDetected == false && chaseCheck == false)
            {
                StartCoroutine(walkAudioPlaying());
                anim.SetBool("isFastWalk", false);
                anim.SetBool("isWalk", true);
            }
        }
    }

    /// <summary>
    /// ������ ��ġ�� �־��ֱ� ���� �Լ�
    /// </summary>
    private void setRandomPos()
    {
        if (chaseCheck == false)
        {
            if (randomPosCheck == true)
            {
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
                agent.speed = 3.5f;
                randomNumber = Random.Range(0, teacherPos.TeacherTrs.Count);
                anim.SetBool("isWalk", true);
                randomPosCheck = true;
            }
        }
    }

    /// <summary>
    /// �߰��� ���带 �����ŰŰ ���� �Լ�
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
    /// ��Ȳ�� �´� ���带 �����Ű�� ���� �Լ�
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
}
