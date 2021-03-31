using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Poutch : Trigger
{
    public Material[] colorOfTrigger;
    [SerializeField] private Renderer triggerRenderer;
    public int currentSwitchPosition;
    public bool DestroyOnUse;

    public float coolDown;
    private float lastUse;

    void Start()
    {
        triggerRenderer.material.color = colorOfTrigger[currentSwitchPosition].color;
    }

    public sealed override void Triggered()
    {
        if (lastUse + coolDown <= Time.time)
        {
            currentSwitchPosition++;
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
                Destroy(gameObject);
            }
        }
        
    }
}
