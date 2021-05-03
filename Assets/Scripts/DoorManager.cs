using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : TriggerReceptor
{
    public DoorEntity[] Doors;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        OpenClose(isOpen);
    }

    public sealed override void Recept()
    {
        isOpen = !isOpen;
        OpenClose(isOpen);
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
}
