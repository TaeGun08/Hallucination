using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("���߾� ī�޶�")]
    [SerializeField] private List<CinemachineVirtualCamera> virtualCamera;

    private CinemachinePOV cinemachinePov;
    private CinemachinePOV cinemachinePOVB;

    private void Start()
    {
        cinemachinePov = virtualCamera[0].GetCinemachineComponent<CinemachinePOV>();
        cinemachinePOVB = virtualCamera[1].GetCinemachineComponent<CinemachinePOV>();
    }

    private void Update()
    {
        SetMouse();
    }

    private void SetMouse()
    {
        cinemachinePov.m_HorizontalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 300;
        cinemachinePov.m_VerticalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 300;

        cinemachinePOVB.m_HorizontalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 300;
        cinemachinePOVB.m_VerticalAxis.m_MaxSpeed = GameManager.Instance.Option.GetSlidersValue(2) * 300;
    }

    /// <summary>
    /// ���ϴ� ���߾� ī�޶� �������� �Լ�
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    public CinemachineVirtualCamera GetVirtualCamera(int _index)
    {
        return virtualCamera[_index];
    }

    /// <summary>
    /// SettingManger���� ���콺 �ΰ����� �־��ֱ� ���� �Լ�
    /// </summary>
    /// <param name="_mouseSensitivity"></param>
    /// <param name="_mouseSensitivity"></param>
    public void SetMouseSensitivity(float _mouseSensitivity)
    {
        cinemachinePov.m_HorizontalAxis.m_MaxSpeed = _mouseSensitivity * 300;
        cinemachinePov.m_VerticalAxis.m_MaxSpeed = _mouseSensitivity * 300;
    }
}
