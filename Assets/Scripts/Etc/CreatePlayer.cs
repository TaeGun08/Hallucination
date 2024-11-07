using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatePlayer : MonoBehaviour
{
    private GameManager gameManager;
    private CameraManager cameraManager;

    [Header("생성할 위치")]
    [SerializeField] private Transform createTrs;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraManager = gameManager.GetManagers<CameraManager>(0);

        createPlayerObject();
    }

    private void createPlayerObject()
    {
        if (SceneManager.GetActiveScene().name != "TeacherCutScene")
        {
            GameObject playerObject = Instantiate(gameManager.CreateBear(), createTrs.position, createTrs.rotation, transform);
            MoveController moveController = playerObject.GetComponent<MoveController>();
            cameraManager.GetVirtualCamera(0).Follow = moveController.HeadTrs;
            cameraManager.GetVirtualCamera(0).gameObject.SetActive(true);

            gameManager.PlayerObject = playerObject;
        }
        else
        {
            GameObject playerObject = Instantiate(gameManager.CreateBear(), createTrs.position, createTrs.rotation, transform);
            MoveController moveController = playerObject.GetComponent<MoveController>();
            cameraManager.GetVirtualCamera(1).Follow = moveController.HeadTrs;
            cameraManager.GetVirtualCamera(1).gameObject.SetActive(true);

            gameManager.PlayerObject = playerObject;
        }
    }
}
