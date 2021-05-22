using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicScript_C4S2 : CinematicScript
{
    [Header("Inspector Info")]
    public GameObject Rampe;
    public GameObject NewRampe;
    public GameObject LUTIN_PERMANENT;
    public GameObject LUTIN_CINEMATIC;
    private ElliotSoundSystem ESS;

    private void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

    public sealed override void ExecuteScript()
    {
        StartCoroutine(Step1_AferWaitingX(0.2f));
        StartCoroutine(Step2_AferWaitingX(ESS.STORY_RampeCasse.length+0.2f));
    }

    IEnumerator Step1_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        LUTIN_CINEMATIC.SetActive(false);
        Rampe.SetActive(false);
        NewRampe.SetActive(true);
        ESS.PlaySound(ESS.STORY_RampeCasse, ESS.Asource_Effects, 1f, false);
    }

    IEnumerator Step2_AferWaitingX(float X)
    {
        yield return new WaitForSeconds(X);
        LUTIN_PERMANENT.SetActive(true);
    }
}
