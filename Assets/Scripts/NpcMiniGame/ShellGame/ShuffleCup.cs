using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCup : MonoBehaviour
{
    private Material material;

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
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        material = Instantiate(renderer.material);
        renderer.material = material;
    }

    private void Update()
    {
        Ray choiceRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(choiceRay, out RaycastHit hit, 5))
        {
            if (hit.collider.gameObject == gameObject)
            {
                material.color = Color.green;
            }
        }
        else if (material.color != Color.white)
        {
            material.color = Color.white;
        }
    }
}