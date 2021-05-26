using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMachine : MonoBehaviour
{
    public bool isOn;
    
    public virtual void OnOff(bool b)
    {
        isOn = b;
    }
}
