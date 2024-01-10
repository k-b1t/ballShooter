using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameMechanics : MonoBehaviour
{
    public static GameMechanics instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("There is more than one GameMechanics instance");
        }
    }


    public void StartCheckAndDestroy(Ball initialBall)
    {
        StartCoroutine(CheckAndDestroyMatchingBalls(initialBall));
    }

    private IEnumerator CheckAndDestroyMatchingBalls(Ball initialBall)
    {
        List<Ball> matchingBalls = new List<Ball>();
        yield return StartCoroutine(initialBall.ExpandColliderAndFindNeighbors(matchingBalls));

        if (matchingBalls.Count >= 3)
        {
            foreach (var ball in matchingBalls)
            {
                Destroy(ball.gameObject);
            }
        }
    }
}

[Serializable]
public struct GridId
{
    public int[] gridId;

    public GridId(int[] gridId)
    {
        this.gridId = gridId;
    }
}

// Neighbour finding by using array instead of physics collider
//TODO to increase performance, use array instead of physics collider
/*public struct Neighbors
{
    private GridId GetValidNeighbor(GridId id, int neighborIndex)
    {
        int[] neighborId = new int[2];

        switch (neighborIndex)
        {
            case 0:
                neighborId[0] = id.x;
                neighborId[1] = id.y + 1;
                break;
        }
        return new GridId(neighborId);
    }
}*/
