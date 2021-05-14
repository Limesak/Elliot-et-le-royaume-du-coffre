using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTransmitter_ForPoussierin : MonoBehaviour
{
    public SoundManager_Poussierin SM;

    public void MakeWALKstepSound()
    {
        SM.PlaySound(SM.OneOf(SM.MOUVEMENT_StepsWalk), SM.Asource_Effects, 0.15f, false);
    }

    public void MakeSPRINTstepSound()
    {
        SM.PlaySound(SM.OneOf(SM.MOUVEMENT_StepsSprint), SM.Asource_Effects, 0.2f, false);
    }

    public void MakeSCREAMSound()
    {
        SM.PlaySound(SM.VOCAL_Scream, SM.Asource_Effects, 0.5f, false);
    }

    public void MakeVERTICALATTACKSound()
    {
        SM.PlaySound(SM.VOCAL_AttaqueVerticale, SM.Asource_Effects, 0.5f, false);
    }

    public void MakeHORIZONTALATTACKSound()
    {
        SM.PlaySound(SM.VOCAL_AttaqueHorizontale, SM.Asource_Effects, 0.7f, false);
    }
}
