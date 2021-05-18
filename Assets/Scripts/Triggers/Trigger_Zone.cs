using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Zone : Trigger
{
    public bool DestroyOnUse;

    public float coolDownSpecialZone;
    private float lastUseSpecialZone;


    void Start()
    {
        
    }

    public sealed override void Triggered()
    {
        if (lastUseSpecialZone + coolDownSpecialZone <= Time.time)
        {
            foreach (TriggerReceptor tr in this.TR)
            {
                tr.Recept();
                //Debug.Log("Activated "+ tr.gameObject.name);
            }


            if (DestroyOnUse)
            {
                if (RE != null)
                {
                    RE.NotRelevantAnymore();
                }
                Destroy(gameObject);
            }
            lastUseSpecialZone = Time.time;
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            LocalTriggered();
        }
    }

    private void LocalTriggered()
    {
        if (lastUseSpecialZone + coolDownSpecialZone <= Time.time)
        {
            foreach (TriggerReceptor tr in this.TR)
            {
                Debug.Log("Activated " + tr.gameObject.name);
                tr.Recept();
                
            }


            if (DestroyOnUse)
            {
                if (RE != null)
                {
                    RE.NotRelevantAnymore();
                }
                Destroy(gameObject);
            }
            lastUseSpecialZone = Time.time;
        }
    }
}
