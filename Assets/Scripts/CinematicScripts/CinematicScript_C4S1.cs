using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicScript_C4S1 : CinematicScript
{
    [Header("Inspector Info")]
    public Animator AnimLutin;
    public NavMeshAgent NavAgentLutin;
    public LookAt LA;
    public SoundManager_LecheCuillere SM;

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0f));

    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        LA.dontLook = false;
    }
}
