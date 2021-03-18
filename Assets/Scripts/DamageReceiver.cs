using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    private int lastKey;

    void Start()
    {
        lastKey = 0;
    }

    void Update()
    {
        
    }

    public void Receive(Damage d)
    {
        if (d._key != lastKey)
        {
            lastKey = d._key;
            Debug.Log("Hit power: "+d._power);
        }
    }
}
