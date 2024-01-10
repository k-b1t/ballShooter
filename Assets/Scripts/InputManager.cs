using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 previousTouchPosition;
    private bool isTouching;
    [SerializeField] private GameObject ballShooter;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {

                var rotation = MapValue((touch.position.x / Screen.width), 0, 1, 110, 240);
                ballShooter.transform.eulerAngles = new Vector3(0, rotation, 0);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
    }
    float MapValue(float input, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (input - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }
}
