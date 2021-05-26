using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_PlayerDetection : MonoBehaviour
{
    public bool PlayerDetected;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerDetected = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerDetected = false;
        }
    }
}
