using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTransmitter_ForLecheCuillere : MonoBehaviour
{
    public SoundManager_LecheCuillere SM;

    public void MakeWALKstepSound()
    {
        SM.PlaySound(SM.OneOf(SM.MOUVEMENT_StepsWalk), SM.Asource_Effects, 0.15f, false);
    }

    public void MakeSPRINTstepSound()
    {
        SM.PlaySound(SM.OneOf(SM.MOUVEMENT_StepsSprint), SM.Asource_Effects, 0.2f, false);
    }

    public void MakeSURPRISESound()
    {
        SM.PlaySound(SM.INTERACTION_CriSurprise, SM.Asource_Effects, 0.8f, false);
    }
}
