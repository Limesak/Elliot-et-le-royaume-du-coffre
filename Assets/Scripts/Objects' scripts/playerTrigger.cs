using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    [Header("Insert the object to trigger")]
    public GameObject objectToActivate;

    [Header("Make sure these settings are correct if this trigger is a slab")]
    public Animator animTrigger;
    public bool iAmSlab, canBeUnset;

    // Start is called before the first frame update
    void Start()
    {
        if (iAmSlab)
        {
            animTrigger = GetComponentInParent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (iAmSlab && other.GetComponent<PlayerMovement>().IsGrounded())
            {
                animTrigger.SetBool("Pushed", true);
            }
            objectToActivate.GetComponent<MonoBehaviour>().SendMessage("TriggerMe");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (iAmSlab && canBeUnset)
        {
            animTrigger.SetBool("Pushed", false);
            objectToActivate.GetComponent<MonoBehaviour>().SendMessage("ResetMe");
        }
    }
}
