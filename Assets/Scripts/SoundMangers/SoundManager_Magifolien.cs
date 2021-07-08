using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Magifolien : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource Asource_Effects;

    [Header("Banque de son")]
    public AudioClip VOCAL_Attaque1;
    public AudioClip VOCAL_Attaque2;
    public AudioClip VOCAL_BlablaCalm;
    public AudioClip VOCAL_BlablaAgro;
    public AudioClip[] VOCAL_Die;
    public AudioClip[] VOCAL_Hit;
    public AudioClip[] MOUVEMENT_Steps;

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
