using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoussierinAnimTransmitter : MonoBehaviour
{
    public Poussierin Entity;

    public void CanWalk()
    {
        Debug.Log("Attack done transmitter");
        Entity.CanWalk();
    }
}
