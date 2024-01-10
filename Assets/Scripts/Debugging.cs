using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Debug New Line of balls");
           BallSpawner.instance.SpawnNewLineOfBalls();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Debug New Level of balls");
            BallSpawner.instance.SpawnNewLevelOfBalls(10);
        }
    }
}
