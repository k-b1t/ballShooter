using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;

    [SerializeField] private int ballCountInRow = 10;
    public float spaceBetweenBalls = 0.1f;
    public float sizeOfBall = 0.9f;
    public Transform ballContainer;
    [SerializeField] private Transform ballSpawnStartPosition;
    private List<Ball> _spawnedBalls = new();
    private bool _isOffsetLine;
    private float _xOffset;
    public List<GameObject> ballVariants;
    public Dictionary<GridId, Ball> grid = new();
    private int rowCounter;

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
    }

    void Start()
    {
        _xOffset = (sizeOfBall + spaceBetweenBalls) / 2;

        SpawnNewLevelOfBalls(9);
    }

    //Public methods

    public void SpawnNewLineOfBalls()
    {
        MoveBallContainer();
        SpawnLineOfBalls();
    }

    public void SpawnNewLevelOfBalls(int ballCountInColum)
    {
        ClearBallContainer();
        for (int i = 0; i < ballCountInColum; i++)
        {
            if (i != 0) MoveBallContainer();
            SpawnLineOfBalls();
        }
    }


    //Private methods

    private void SpawnLineOfBalls()
    {
        _isOffsetLine = !_isOffsetLine;
        var offset = _isOffsetLine ? _xOffset : 0;
        var startPosition = ballSpawnStartPosition.position;


        for (int i = 0; i < ballCountInRow; i++)
        {
            var xPosition = startPosition.x + (i * (spaceBetweenBalls + sizeOfBall)) + offset;
            var spawnPosition = new Vector3(xPosition, startPosition.y, startPosition.z);
            var prefab = ballVariants[UnityEngine.Random.Range(0, ballVariants.Count)];

            var ball = Instantiate(prefab, spawnPosition, Quaternion.identity);
            var ballClass = ball.GetComponent<Ball>();
            _spawnedBalls.Add(ballClass);
            GridId gridId = new GridId( new int[] { i, rowCounter });
            ballClass.gridId = gridId;
            grid.Add(gridId, ballClass);
            ball.transform.SetParent(ballContainer);
            ball.GetComponent<Ball>().RemoveRigidbody();
        }

        rowCounter++;
    }

    private void MoveBallContainer()
    {
        //TODO add smooth movement here for better feel
        ballContainer.transform.position += Vector3.back * (sizeOfBall + spaceBetweenBalls);
    }

    private void ClearBallContainer()
    {
        ballContainer.transform.position = Vector3.zero;

        foreach (var ball in _spawnedBalls)
        {
            Destroy(ball?.gameObject);
        }

        _spawnedBalls.Clear();
    }

    private void Reset()
    {
        ClearBallContainer();
        rowCounter = 0;
    }
}
