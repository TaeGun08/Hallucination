using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    [SerializeField] private int checkerIndex;
    private bool check;
    public bool Check
    {
        get
        {
            return check;
        }
        set
        {
            check = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PuzzleMove puzzle = collision.GetComponent<PuzzleMove>();

        if (puzzle != null)
        {
            if (checkerIndex == puzzle.Index)
            {
                check = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PuzzleMove puzzle = collision.GetComponent<PuzzleMove>();

        if (puzzle != null)
        {
            if (checkerIndex == puzzle.Index)
            {
                check = false;
            }
        }
    }
}
