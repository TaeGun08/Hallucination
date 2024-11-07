using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EyesUI : MonoBehaviour
{
    private VerticalLayoutGroup layout;
    [SerializeField] private float spacingSpeed;
    private bool openOrClose;
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
    private bool eyesCheck;
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
    private int chapterCount;

    private void Awake()
    {
        layout = GetComponent<VerticalLayoutGroup>();
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
                }
                else
                {
                    EyesCheck = false;
                }
            }
            else
            {
                if (layout.spacing >= 0)
                {
                    layout.spacing -= spacingSpeed * Time.deltaTime;
                }
                else
                {
                    EyesCheck = false;
                    if (SceneManager.GetActiveScene().name != "TeacherCutScene")
                    {
                        GameManager.Instance.CutSceneLoad = true;
                        ++chapterCount;
                        PlayerPrefs.SetInt("SaveScene", chapterCount);
                        PlayerPrefs.Save();
                    }

                    SceneManager.LoadSceneAsync("LoadingScene");
                }
            }
        }
    }
}
