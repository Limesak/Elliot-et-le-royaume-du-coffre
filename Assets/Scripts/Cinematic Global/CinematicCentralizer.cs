using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCentralizer : TriggerReceptor
{
    public CinematicStep[] Steps;
    public CinematicManager CM;

    public sealed override void Recept()
    {
        
        CM.StartNewCinematic(Steps);
    }
}
