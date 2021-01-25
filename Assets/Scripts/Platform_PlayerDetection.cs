using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_PlayerDetection : MonoBehaviour
{
    public Platform_movement PlatScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlatScript.SetDetection(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlatScript.SetDetection(false);
        }
    }
}
