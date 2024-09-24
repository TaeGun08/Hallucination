using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("ī�޶�")]
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

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
    }


    /// <summary>
    /// �ó׸ӽ� ī�޶� �������� ���� �Լ�
    /// </summary>
    /// <param name="_cameraNumber"></param>
    /// <returns></returns>
    public CinemachineVirtualCamera GetVirtualCamera(int _cameraNumber)
    {
        return cameras[_cameraNumber];
    }
}
