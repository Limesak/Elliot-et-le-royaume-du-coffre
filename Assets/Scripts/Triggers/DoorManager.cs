using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : TriggerReceptor
{
    public DoorEntity[] Doors;
    public bool isOpen;
    public Interactable_Cadenas[] PossibleLock;
    // Start is called before the first frame update
    void Start()
    {
        OpenClose(isOpen);
    }

    public sealed override void Recept()
    {
        if (CanInteract())
        {
            isOpen = !isOpen;
            OpenClose(isOpen);
        }
        else if (!isOpen)
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].TryButFailOpen();
            }
        }
        
    }

    public void OpenClose(bool b)
    {
        if (b)
        {
            for(int i = 0; i < Doors.Length; i++)
            {
                Doors[i].OpenTheDoor();
            }
            Debug.Log("Opening");
        }
        else
        {
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].CloseTheDoor();
            }
            Debug.Log("Closing");
        }
    }

    public bool CanInteract()
    {
        bool res = true;

        if (PossibleLock != null)
        {
            for (int i = 0; i < PossibleLock.Length; i++)
            {
                if (PossibleLock[i] != null)
                {
                    res = false;
                }
            }
        }

        return res;
    }
}
