using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
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
