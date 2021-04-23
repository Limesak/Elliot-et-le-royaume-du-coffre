using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public DamageCentralizer DC;

    public void Receive(Damage d)
    {
        DC.Receive(d);
    }


}
