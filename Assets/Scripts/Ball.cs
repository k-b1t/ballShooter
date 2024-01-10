using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public float speed = 5.0f;
    [FormerlySerializedAs("_isShooting")] public bool isShooting;
    [FormerlySerializedAs("_ballId")] public int _ballTypeId;
    [SerializeField] public GridId gridId;
    [FormerlySerializedAs("test")] public bool DEBUGGING;
    private bool _moveForward;

    private void Update()
    {
        if (DEBUGGING)
        {
            GameMechanics.instance.StartCheckAndDestroy(this);
            DEBUGGING = false;
        }
    }


    public void OnBallShoot()
    {
        _moveForward = true;
        isShooting = true;
        StartCoroutine(MoveForwardRoutine());
    }

    public void RemoveRigidbody()
    {
        if (GetComponent<Rigidbody>())
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }

    private void StickToBall(Transform neighborBall)
    {
        isShooting = false;
        _moveForward = false;
        RemoveRigidbody();
        transform.parent = BallSpawner.instance.ballContainer;
        transform.position = neighborBall.position;
        transform.rotation = neighborBall.rotation;

        var _xOffset = (BallSpawner.instance.sizeOfBall + BallSpawner.instance.spaceBetweenBalls) / 2;
        _xOffset = neighborBall.position.x > 0 ? -_xOffset : _xOffset;

        var _zOffset = BallSpawner.instance.sizeOfBall + BallSpawner.instance.spaceBetweenBalls;

        transform.position += new Vector3(_xOffset, 0, -_zOffset);

        GameMechanics.instance.StartCheckAndDestroy(this);
    }

    public IEnumerator ExpandColliderAndFindNeighbors(List<Ball> matchingBalls)
    {
        FindMatchingNeighbors(matchingBalls);
        yield return null;
    }


    public void FindMatchingNeighbors(List<Ball> matchingBalls)
    {
        if (!matchingBalls.Contains(this))
        {
            matchingBalls.Add(this);

            var sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = sphereCollider.radius * 2;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereCollider.radius);
            foreach (var hitCollider in hitColliders)
            {
                var neighborBall = hitCollider.GetComponent<Ball>();
                if (neighborBall != null && neighborBall._ballTypeId == this._ballTypeId && !matchingBalls.Contains(neighborBall))
                {
                    neighborBall.FindMatchingNeighbors(matchingBalls);
                }
            }

            sphereCollider.radius = sphereCollider.radius / 2;
        }
    }


    private IEnumerator MoveForwardRoutine()
    {
        while (_moveForward)
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            var currentRotation = gameObject.transform.eulerAngles;
            gameObject.transform.eulerAngles =
                new Vector3(currentRotation.x, 180 + (180 - currentRotation.y), currentRotation.z);
        }
        else if (other.CompareTag("Wall_top"))
        {
            var currentRotation = gameObject.transform.eulerAngles;
            gameObject.transform.eulerAngles =
                new Vector3(currentRotation.x, 180 - currentRotation.y, currentRotation.z);
        }
        else if (other.CompareTag("Ball"))
        {
            if (isShooting)
            {
                StickToBall(other.transform);
            }
        }
    }

    private void OnDestroy()
    {
        BallSpawner.instance.grid.Remove(gridId);
    }
}
