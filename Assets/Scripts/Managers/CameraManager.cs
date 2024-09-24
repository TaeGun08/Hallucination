using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("카메라")]
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
    /// 시네머신 카메라를 가져오기 위한 함수
    /// </summary>
    /// <param name="_cameraNumber"></param>
    /// <returns></returns>
    public CinemachineVirtualCamera GetVirtualCamera(int _cameraNumber)
    {
        return cameras[_cameraNumber];
    }
}
