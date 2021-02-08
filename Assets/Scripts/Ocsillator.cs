using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocsillator : OnOffMachine
{
    public float speedIncrement;
    public float limitAngle;
    public Transform pivot;
    private float currentSpeed;
    private bool GoingPositive;


    void Start()
    {
        GoingPositive = true;
    }

    void Update()
    {
        if (isOn)
        {
            float buffer = 0 ;
            if (pivot.localEulerAngles.x < 180)
            {
                buffer = speedIncrement * (limitAngle - pivot.localEulerAngles.x) * Time.deltaTime;
            }
            else
            {
                buffer = speedIncrement * Mathf.Abs(360-limitAngle - pivot.localEulerAngles.x) * Time.deltaTime;
            }
            
            //Debug.Log("Buffer=" + buffer +"  /  current angle="+ pivot.localEulerAngles.x);
            if (GoingPositive)
            {
                if (pivot.localEulerAngles.x >= limitAngle - (limitAngle*0.05f) && pivot.localEulerAngles.x <= limitAngle + (limitAngle * 0.5f))
                {
                    GoingPositive = false;
                    //Debug.Log("Going negative");
                }
                else
                {
                    pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x + buffer , pivot.localEulerAngles.y, pivot.localEulerAngles.z);
                }
            }
            else
            {
                if (pivot.localEulerAngles.x <= 360-limitAngle + (limitAngle * 0.05f) && pivot.localEulerAngles.x >= 360 - limitAngle - (limitAngle * 0.5f))
                {
                    GoingPositive = true;
                    //Debug.Log("Going positive");
                }
                else
                {
                    pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x - buffer, pivot.localEulerAngles.y, pivot.localEulerAngles.z);
                }
            }
        }
       
    }
}
