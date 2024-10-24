using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellGame : MonoBehaviour
{
    [SerializeField] private List<Shuffle> shuffleSc;
    [SerializeField] private List<GameObject> cupTrs;
    [SerializeField] private Transform ballTrs;
    [Header("ƒ≈ ∞‘¿” º≥¡§")]
    [SerializeField] private bool gameStart = false;
    [SerializeField] private float shuffleTime;
    private float timer;
    private bool shuffleCheck = false;
    public bool ShuffleCheck
    {
        get
        {
            return shuffleCheck;
        }
        set
        {
            shuffleCheck = value;
        }
    }

    private void Update()
    {
        if (gameStart)
        {
            StartCoroutine(shuffleGame());
            gameStart = false;
        }
    }

    private IEnumerator shuffleGame()
    {
        while (timer <= shuffleTime)
        {
            timer += Time.deltaTime;

            if (!shuffleCheck)
            {
                cupShuffle();
                shuffleCheck = true;
                yield return null;
            }

            yield return null;
        }
    }

    private void setCupTrs(int _cupA, int _cupB, Transform _trs)
    {
        GameObject temp = null;

        cupTrs[_cupA].transform.SetParent(_trs);
        cupTrs[_cupB].transform.SetParent(_trs);

        temp = cupTrs[_cupA];
        cupTrs[_cupA] = cupTrs[_cupB];
        cupTrs[_cupB] = temp;
    }

    private void cupShuffle()
    {
        int pickUpNumber = Random.Range(0, shuffleSc.Count);
        Debug.Log(pickUpNumber);
        Shuffle shuffleScript = shuffleSc[pickUpNumber];

        switch (pickUpNumber)
        {
            case 0:
                setCupTrs(0, 1, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
            case 1:
                setCupTrs(0, 2, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
            case 2:
                setCupTrs(1, 2, shuffleScript.transform);
                shuffleScript.IsShuffle = true;
                break;
        }
    }

    public void ResetParentTrs()
    {
        for (int iNum = 0; iNum < cupTrs.Count; iNum++)
        {
            cupTrs[iNum].transform.SetParent(transform);
        }
    }
}
