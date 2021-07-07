using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Poussierin : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource Asource_Effects;

    [Header("Banque de son")]
    public AudioClip VOCAL_Scream;
    public AudioClip VOCAL_Pop;
    public AudioClip VOCAL_AttaqueHorizontale;
    public AudioClip VOCAL_AttaqueVerticale;
    public AudioClip[] VOCAL_Die;
    public AudioClip[] VOCAL_Hit;
    public AudioClip[] MOUVEMENT_StepsWalk;
    public AudioClip[] MOUVEMENT_StepsSprint;

    public void PlaySound(AudioClip clip, AudioSource source, float power, bool ResetBefore)
    {
        if (power < 0)
        {
            power = 0;
        }
        if (power > 1)
        {
            power = 1;
        }

        if (ResetBefore)
        {
            source.Stop();
        }

        if (clip != null)
        {
            source.PlayOneShot(clip, power);
        }
        else
        {
            Debug.Log("MISSING SOUND MOTHERFUCKER   " + clip.ToString());
        }
    }

    public AudioClip OneOf(AudioClip[] list)
    {
        return list[(int)Random.Range(0, list.Length)];
    }
}
