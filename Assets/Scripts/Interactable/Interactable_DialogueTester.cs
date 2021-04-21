using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_DialogueTester : Interactable
{
    public ConversationInfo Conv;

    public sealed override void Interact()
    {
        if (!this.MM.isInDialogue())
        {
            MM.DIALOGUE_OpenDialogue(Conv);
        }
    }
}
