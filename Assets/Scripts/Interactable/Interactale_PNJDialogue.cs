using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactale_PNJDialogue : Interactable
{
    public LecheCuillerePermanent LCP;

    public sealed override void Interact()
    {
        LCP.TryToInteract(MM);
    }
}
