using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance;

    private Image imgFade;
    [SerializeField] float fadeTime = 1.0f;//���̵� �Ǵ� �ð� �� �����Ǵ� �ð�

    [SerializeField] int targetFrm = 34;

    private float timer;

    private UnityAction actionFadeOut;//���̵� �ƿ� �Ǿ����� ������ ���
    private UnityAction actionFadeIn;//���̵� �� �Ǿ����� ������ ���

    bool fade = true;//true�� In false�� Out;

    private void Awake()
    {
        imgFade = GetComponent<Image>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(functionFade());
    }

    //private void Update()
    //{
    //    int currentFrm = (int)(1 / Time.deltaTime); //���� �����Ӱ� ���� �������� �ð�
    //    if (currentFrm < targetFrm) return;

    //    if (fade && imgFade.color.a > 0)
    //    {
    //        Color color = imgFade.color;
    //        color.a -= Time.unscaledDeltaTime / fadeTime;
    //        if (color.a < 0)
    //        {
    //            color.a = 0;
    //            invokeAction(fade);
    //        }
    //        imgFade.color = color;
    //    }
    //    else if (!fade && imgFade.color.a < 1)
    //    {
    //        Color color = imgFade.color;
    //        color.a += Time.unscaledDeltaTime / fadeTime;
    //        if (color.a > 1)
    //        {
    //            color.a = 1;
    //            invokeAction(fade);
    //        }
    //        imgFade.color = color;
    //    }

    //    imgFade.raycastTarget = imgFade.color.a != 0;
    //}

    public IEnumerator functionFade()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        Time.timeScale = 0;

        int currentFrm = 0;
        while (currentFrm < targetFrm)
        {
            currentFrm = (int)(1 / Time.unscaledDeltaTime); //���� �����Ӱ� ���� �������� �ð�
            yield return null;
        }

        Color curColor = imgFade.color;
        Color targetColor = imgFade.color;
        targetColor.a = fade == true ? 0 : 1;

        float ratio = 0;

        float fakeLoadingTime = 1.5f;
        timer = 0;

        imgFade.raycastTarget = true;

        //yield return new WaitUntil(() =>
        //{
        //    if (timer > fakeLoadingTime && curColor.a != 0)
        //    {
        //        return true;
        //    }
        //    timer += Time.unscaledDeltaTime;
        //    return false;
        //});

        while (timer < fakeLoadingTime && curColor.a != 0)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        while (ratio < 1)
        {
            ratio += Time.unscaledDeltaTime / fadeTime;
            if (ratio > 1f)
            {
                Time.timeScale = 1.0f;
                ratio = 1;
            }

            imgFade.color = Color.Lerp(curColor, targetColor, ratio);
            yield return null;
        }

        invokeAction(fade);

        imgFade.raycastTarget = imgFade.color.a != 0;
    }

    private void invokeAction(bool _fade)
    {
        switch (_fade)
        {
            case true:
                {
                    if (actionFadeIn != null)
                    {
                        actionFadeIn.Invoke();
                        actionFadeIn = null;
                    }
                }
                break;
            case false:
                {
                    if (actionFadeOut != null)
                    {
                        actionFadeOut.Invoke();
                        actionFadeOut = null;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// ���̵� ����� ����
    /// </summary>
    /// <param name="_fade">true�� In false �� Out</param>
    /// <param name="_action">ture�Ͻ� In�� �ɶ� �����, false�϶� Out����� ����մϴ�</param>
    public void SetActive(bool _fade, UnityAction _action = null)
    {
        fade = _fade;
        switch (_fade)
        {
            case true: actionFadeIn = _action; break;
            case false: actionFadeOut = _action; break;
        }
        StartCoroutine(functionFade());
    }

    /// <summary>
    /// ���İ��� �������ֱ� ���� �Լ�
    /// </summary>
    /// <param name="_a"></param>
    public void SetImageAlpha(float _a)
    {
        Color color = imgFade.color;
        color.a = _a;
        imgFade.color = color;
    }

    public float DontEsc()
    {
        Color color = imgFade.color;
        return color.a;
    }
}
