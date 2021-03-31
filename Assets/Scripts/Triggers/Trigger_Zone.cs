using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Zone : Trigger
{
    public bool DestroyOnUse;

    public float coolDown;
    private float lastUse;


    void Start()
    {
        
    }

    public sealed override void Triggered()
    {
        if (lastUse + coolDown <= Time.time)
        {
            foreach (TriggerReceptor tr in this.TR)
            {
                tr.Recept();
            }


            if (DestroyOnUse)
            {
                Destroy(gameObject);
            }
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Triggered();
        }
    }
}
