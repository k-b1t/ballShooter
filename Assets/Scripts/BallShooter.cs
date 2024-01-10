using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallShooter : MonoBehaviour
{
    [SerializeField] Transform ballSpawnPosition;
    [SerializeField] Transform shooterBase;

    private GameObject ballToShoot;


    private void Start()
    {
        LoadBall();
    }

    public void LoadBall()
    {
        ballToShoot = Instantiate(BallSpawner.instance.ballVariants[Random.Range(0, BallSpawner.instance.ballVariants.Count)], shooterBase.position , Quaternion.identity);
        ballToShoot.GetComponent<SphereCollider>().enabled = false;
    }


    public void ShootBall()
    {

        ballToShoot.transform.rotation = ballSpawnPosition.rotation;
        ballToShoot.transform.position = ballSpawnPosition.position;
        ballToShoot.GetComponent<Ball>().OnBallShoot();
        ballToShoot.GetComponent<SphereCollider>().enabled = true;
        ballToShoot = null;
        LoadBall();
    }
}
