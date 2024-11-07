using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shuffle : MonoBehaviour
{
    private ShellGame shellGame;

    [Header("ÄÅ ¼¯±â")]
    private bool isShuffle = false;
    public bool IsShuffle
    {
        get
        {
            return isShuffle;
        }
        set
        {
            isShuffle = value;
        }
    }
    [SerializeField] private float shuffleSpeed;
    public float ShuffleSpeed
    {
        get
        {
            return shuffleSpeed;
        }
        set
        {
            shuffleSpeed = value;
        }
    }
    private int count;

    private Vector3 eulerAng;

    private void Start()
    {
        shellGame = transform.GetComponentInParent<ShellGame>();
    }

    private void Update()
    {
        if (shuffleSpeed > 2)
        {
            shuffleSpeed = 2;
        }

        if (isShuffle)
        {
            shuffleRot();
        }
    }

    /// <summary>
    /// ÄÅÀ» Á¶°Ç¿¡ ¸Â°Ô ¼¯¾îÁÖ±â À§ÇÑ ÇÔ¼ö
    /// </summary>
    private void shuffleRot()
    {
        eulerAng = transform.eulerAngles;

        eulerAng.y += 180f * (Time.deltaTime * shuffleSpeed);

        if (count == 0 && eulerAng.y >= 180)
        {
            count = 1;
            eulerAng.y = 180;
            isShuffle = false;
            shellGame.ResetParentTrs();
            shellGame.ShuffleCheck = false;
        }
        else if (count == 1 && eulerAng.y >= 360)
        {
            count = 0;
            eulerAng.y = 0;
            isShuffle = false;
            shellGame.ResetParentTrs();
            shellGame.ShuffleCheck = false;
        }

        transform.eulerAngles = eulerAng;
    }
}
