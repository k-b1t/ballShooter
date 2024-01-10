using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RessetGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!other.GetComponent<Ball>().isShooting)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
