using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_PressurePlate : Trigger
{
    public Animator animTrigger;
    public bool canBeUnset;

    void Start()
    {
        
    }

    public sealed override void Triggered()
    {

        foreach (TriggerReceptor tr in this.TR)
        {
            tr.Recept();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PlayerMovement>().IsGrounded() && !animTrigger.GetBool("Pushed"))
            {
                animTrigger.SetBool("Pushed", true);
                Triggered();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            if (canBeUnset && animTrigger.GetBool("Pushed"))
            {
                animTrigger.SetBool("Pushed", false);
                Triggered();
            }

        }
    }
}
