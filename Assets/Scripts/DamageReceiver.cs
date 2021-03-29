using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    private int lastKey;
    public GameObject Debug_HitSign;

    void Start()
    {
        lastKey = 0;
        Debug_HitSign.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Receive(Damage d)
    {
        Debug_HitSign.SetActive(!Debug_HitSign.activeSelf);
        if (d._key != lastKey)
        {
            lastKey = d._key;
            Debug.Log("Hit power: "+d._power);
            
        }
    }
}
