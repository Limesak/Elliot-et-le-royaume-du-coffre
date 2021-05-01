using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerReceptor[] TR;
    public float Cooldown;
    private float lastUse;

    void Start()
    {
        lastUse = -9999999;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerCD()
    {
        if(lastUse + Cooldown<= Time.time)
        {
            lastUse = Time.time;
            Triggered();
        }
    }

    public virtual void Triggered()
    {
        //custom on each son
        Debug.Log("This is a motherScript, please replace it with the adequat son.");
    }
}
