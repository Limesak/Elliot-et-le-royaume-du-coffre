using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicScript_C2S1 : CinematicScript
{
    [Header("Inspector Info")]
    public Animator AnimLutin;
    public NavMeshAgent NavAgentLutin;
    public LookAt LA;

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0.5f));
        StartCoroutine(Step2_AferWaitingX(1.3f));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        //GameObject PLAYER = GameObject.FindGameObjectWithTag("Player");
        //AnimLutin.gameObject.transform.LookAt(new Vector3(PLAYER.transform.position.x, AnimLutin.gameObject.transform.position.y, PLAYER.transform.position.z));
        LA.dontLook = false;
    }

    IEnumerator Step2_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimLutin.SetBool("isInPanic", false);
    }
}
