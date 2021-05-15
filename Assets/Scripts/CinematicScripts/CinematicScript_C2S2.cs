using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicScript_C2S2 : CinematicScript
{
    [Header("Inspector Info")]
    public Animator AnimLutin;
    public NavMeshAgent NavAgentLutin;
    public LookAt LA;
    public SoundManager_LecheCuillere SM;
    public GameObject PREFAB_WoodSword;
    public GameObject PosForDrop;
    public GameObject PosToGo;
    public GameObject PosToGo2;

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0.5f));
        //StartCoroutine(Step2_AferWaitingX(1.3f));
        StartCoroutine(Step3_AferWaitingX(2.1f));
        StartCoroutine(Step4_AferWaitingX(2.5f));
        StartCoroutine(Step5_AferWaitingX(3.0f));
        StartCoroutine(Step6_AferWaitingX(5.0f));
        StartCoroutine(Step7_AferWaitingX(8.0f));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        SM.PlaySound(SM.INTERACTION_FouilleInventaire, SM.Asource_Effects, 0.8f, false);
    }

    IEnumerator Step2_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        //Coroutine pour animation de fouille de Lechecuillere
    }

    IEnumerator Step3_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimLutin.SetTrigger("Give");
    }

    IEnumerator Step4_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        SM.PlaySound(SM.INTERACTION_DropEpeeEnBois, SM.Asource_Effects, 0.8f, false);
        Instantiate(PREFAB_WoodSword, PosForDrop.transform.position, Quaternion.identity);
    }

    IEnumerator Step5_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        LA.dontLook = true;
        AnimLutin.SetTrigger("Walk");
        NavAgentLutin.SetDestination(PosToGo.transform.position);
    }

    IEnumerator Step6_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        NavAgentLutin.SetDestination(PosToGo2.transform.position);
    }

    IEnumerator Step7_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        //Do stuffs
        AnimLutin.SetTrigger("Idle");
    }
}
