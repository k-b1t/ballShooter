using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        Application.targetFrameRate = 60;
    }

    private int ballsShot;

    public void BallShot()
    {
        ballsShot++;
        if (ballsShot == 5)
        {
            ballsShot = 0;
            BallSpawner.instance.SpawnNewLineOfBalls();
        }
        //TODO add reset method here
    }



    private bool useTouchInput = true;

    public bool UseTouchInput
    {
        get { return useTouchInput; }
    }



}
