using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EyesUI : MonoBehaviour
{
    private QuestManager questManager;

    private VerticalLayoutGroup layout;
    [SerializeField] private float spacingSpeed;
    [SerializeField] private bool openOrClose;
    public bool OpenOrClose
    {
        get
        {
            return openOrClose;
        }
        set
        {
            openOrClose = value;
        }
    }
    [SerializeField] private bool eyesCheck;
    public bool EyesCheck
    {
        get
        {
            return eyesCheck;
        }
        set
        {
            eyesCheck = value;
        }
    }

    private void Awake()
    {
        layout = GetComponent<VerticalLayoutGroup>();
    }

    private void Start()
    {
        questManager = GameManager.Instance.GetManagers<QuestManager>(3);
    }

    private void Update()
    {
        if (EyesCheck == true)
        {
            if (openOrClose == false)
            {
                if (layout.spacing <= 1700)
                {
                    layout.spacing += spacingSpeed * Time.deltaTime;

                    if (layout.spacing >= 1700)
                    {
                        layout.spacing = 1700;
                        openOrClose = true;
                        eyesCheck = false;
                    }
                }
            }
            else
            {
                if (layout.spacing >= 0)
                {
                    layout.spacing -= spacingSpeed * Time.deltaTime;

                    if (layout.spacing <= 0)
                    {
                        layout.spacing = 0;

                        if ((questManager.QuestCheck(100) && questManager.QuestCheck(110) && PlayerPrefs.GetInt("SaveScene") == 0) ||
                           (questManager.QuestCheck(200) && questManager.QuestCheck(210) && PlayerPrefs.GetInt("SaveScene") == 1))
                        {
                            GameManager.Instance.CutSceneLoad = true;
                        }

                        FadeInOut.Instance.SetActive(false, () =>
                        {
                            SceneManager.LoadSceneAsync("LoadingScene");

                            FadeInOut.Instance.SetActive(true);

                            openOrClose = false;
                            eyesCheck = false;
                        });
                    }
                }
            }
        }
    }
}
