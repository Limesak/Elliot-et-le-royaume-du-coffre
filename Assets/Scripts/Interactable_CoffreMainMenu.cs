using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_CoffreMainMenu : Interactable
{

    public sealed override void Interact()
    {
        if (!this.MM.Menu_ContinueOuNouvelle.activeSelf && !this.MM.Menu_EcraserOuAnnuler.activeSelf)
        {
            MM.PopMenuCoffre();
        }
    }
}
