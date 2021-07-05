using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Trigger_Poutch : Trigger
{
    public Material[] colorOfTrigger;
    [SerializeField] private Renderer triggerRenderer;
    public int currentSwitchPosition;
    public bool DestroyOnUse;
    public GameObject Lever;

    void Start()
    {
        triggerRenderer.material.color = colorOfTrigger[currentSwitchPosition].color;
        Lever.transform.localEulerAngles = new Vector3(0,0,0);
    }

    public sealed override void Triggered()
    {
        currentSwitchPosition++;

        if(Lever.transform.localEulerAngles.z == 0)
        {
            Lever.transform.DOLocalRotate(new Vector3(0, 0, 100), 1);
        }
        else
        {
            Lever.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
        }
        
        //Lever.transform.localEulerAngles = new Vector3(0, 0, -Lever.transform.localEulerAngles.z);
        if (currentSwitchPosition >= colorOfTrigger.Length)
        {
            currentSwitchPosition = 0;
        }
        triggerRenderer.material.color = colorOfTrigger[currentSwitchPosition].color;

        foreach (TriggerReceptor tr in this.TR)
        {
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

    }
}
