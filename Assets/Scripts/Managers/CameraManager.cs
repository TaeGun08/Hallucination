using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    [Header("버추얼 카메라")]
    [SerializeField] private List<CinemachineVirtualCamera> virtualCamera;

    private CinemachinePOV cinemachinePov;

    private void Start()
    {
        cinemachinePov = virtualCamera[0].GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        SetMouse();

        if (SceneManager.GetActiveScene().name == "TeacherCutScene")
        {
            if (virtualCamera[1].gameObject.activeSelf == false)
            {
                virtualCamera[1].gameObject.SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name == "MapScene")
        {
            if (virtualCamera[1].gameObject.activeSelf == true)
            {
                virtualCamera[1].gameObject.SetActive(false);
            }
        }
    }

    private void SetMouse()
    {
        cinemachinePov.m_HorizontalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 500;
        cinemachinePov.m_VerticalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 500;
    }

    /// <summary>
    /// 원하는 버추얼 카메라를 가져오는 함수
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    public CinemachineVirtualCamera GetVirtualCamera(int _index)
    {
        return virtualCamera[_index];
    }

    /// <summary>
    /// SettingManger에서 마우스 민감도를 넣어주기 위한 함수
    /// </summary>
    /// <param name="_mouseSensitivity"></param>
    /// <param name="_mouseSensitivity"></param>
    public void SetMouseSensitivity(float _mouseSensitivity)
    {
        cinemachinePov.m_HorizontalAxis.m_MaxSpeed = _mouseSensitivity * 500;
        cinemachinePov.m_VerticalAxis.m_MaxSpeed = _mouseSensitivity * 500;
    }
}
