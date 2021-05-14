using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicScript_C1S1 : CinematicScript
{
    [Header("Inspector Info")]
    public Animator AnimLutin;
    public Animator AnimPoupou;
    public NavMeshAgent NavAgentLutin;
    public NavMeshAgent NavAgentPoupou;
    public GameObject Destination;

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0.2f));
        StartCoroutine(Step2_AferWaitingX(0.6f));
        StartCoroutine(Step3_AferWaitingX(2f));
        StartCoroutine(Step4_AferWaitingX(2.5f));
        StartCoroutine(Step5_AferWaitingX(5f));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimPoupou.SetTrigger("Surprise");
    }

    IEnumerator Step2_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimLutin.SetTrigger("Surprise");
        AnimLutin.SetBool("isInPanic", true);
    }

    IEnumerator Step3_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimLutin.SetTrigger("Run");
        NavAgentLutin.SetDestination(Destination.transform.position);
    }

    IEnumerator Step4_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimPoupou.SetTrigger("Walk");
        NavAgentPoupou.SetDestination(Destination.transform.position);
    }

    IEnumerator Step5_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        Destroy(AnimPoupou.gameObject);
        Destroy(AnimLutin.gameObject);
    }
}
