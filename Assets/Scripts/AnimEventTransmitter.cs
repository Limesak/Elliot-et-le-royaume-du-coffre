using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTransmitter : MonoBehaviour
{
    public SwordTrigger ST;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isDangerous()
    {
        ST.canDamage = true;
        Debug.Log("isDangerous");
    }

    public void isntDangerous()
    {
        ST.canDamage = false;
        Debug.Log("isntDangerous");
    }
}
