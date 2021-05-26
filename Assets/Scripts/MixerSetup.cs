using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerSetup : MonoBehaviour
{
    public AudioMixer mixer;

    void Start()
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(SaveParameter.current.MixerVolume_Master) * 20);
        mixer.SetFloat("MusiqueVol", Mathf.Log10(SaveParameter.current.MixerVolume_Musique) * 20);
        mixer.SetFloat("InterfaceVol", Mathf.Log10(SaveParameter.current.MixerVolume_Interface) * 20);
        mixer.SetFloat("EffetVol", Mathf.Log10(SaveParameter.current.MixerVolume_Effets) * 20);
    }
}
