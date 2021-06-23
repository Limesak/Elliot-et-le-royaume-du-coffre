using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagifolienAnimTransmitter : MonoBehaviour
{
    public Magifolien Entity;

    public void CanWalk()
    {
        //Debug.Log("Attack done transmitter");
        Entity.CanWalk();
    }
}
