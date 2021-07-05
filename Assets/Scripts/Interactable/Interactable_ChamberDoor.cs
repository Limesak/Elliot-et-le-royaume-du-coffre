using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_ChamberDoor : Interactable
{
    public sealed override void Interact()
    {
        Application.Quit();
    }
}
