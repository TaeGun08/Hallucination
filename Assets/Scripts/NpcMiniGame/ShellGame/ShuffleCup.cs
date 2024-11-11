using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCup : MonoBehaviour
{
    private ShellGame shellGame;

    private Animator anim;
    private Material material;

    private bool timerOn;
    private float timer;

    private bool choice;
    public bool Choice
    {
        get
        {
            return choice;
        }
        set
        {
            choice = value;
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        material = Instantiate(renderer.material);
        renderer.material = material;
    }

    private void Start()
    {
        shellGame = transform.GetComponentInParent<ShellGame>();
    }

    private void Update()
    {
        if (timerOn == false)
        {
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                anim.SetBool("isShuffle", true);
                timerOn = false;
            }
        }

        if (shellGame != null && shellGame.ChooseTimer >= 1)
        {
            Vector3 mousePosition = Input.mousePosition;

            float xRatio = (float)GameManager.Instance.RenderTexture.width / Screen.width;
            float yRatio = (float)GameManager.Instance.RenderTexture.height / Screen.height;

            mousePosition.x *= xRatio;
            mousePosition.y *= yRatio;
            mousePosition.z = 10f;

            Ray choiceRay = Camera.main.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(choiceRay, out RaycastHit hit, 5))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    material.color = Color.green;
                    return;
                }
            }

            if (material.color != Color.white)
            {
                material.color = Color.white;
            }
        }
    }
}