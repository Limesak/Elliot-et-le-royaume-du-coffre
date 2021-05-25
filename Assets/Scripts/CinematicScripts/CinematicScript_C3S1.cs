using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicScript_C3S1 : CinematicScript
{
    [Header("Inspector Info")]
    public GameObject Poussierin;
    public GameObject Barrière;
    public ParticleSystem PS;

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0.2f));
        StartCoroutine(Step2_AferWaitingX(6f));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        Barrière.SetActive(true);
        PS.Play();
        Poussierin.SetActive(true);
        Poussierin.GetComponent<SoundManager_Poussierin>().PlaySound(Poussierin.GetComponent<SoundManager_Poussierin>().VOCAL_Scream, Poussierin.GetComponent<SoundManager_Poussierin>().Asource_Effects, 1f, true);
    }

    IEnumerator Step2_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        Barrière.SetActive(false);
    }
}
