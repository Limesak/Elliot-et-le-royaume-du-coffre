using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerReceptor[] TR;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Triggered()
    {
        //custom on each son
        Debug.Log("This is a motherScript, please replace it with the adequat son.");
    }
}
