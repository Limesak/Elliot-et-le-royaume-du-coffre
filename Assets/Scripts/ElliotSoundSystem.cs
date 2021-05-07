using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElliotSoundSystem : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource Asource_Effects;
    public AudioSource Asource_Interface;

    [Header("Banque de son")]
    public AudioClip COMBAT_Attaque1;
    public AudioClip COMBAT_Attaque2;
    public AudioClip COMBAT_Attaque3;
    public AudioClip COMBAT_Attaque4;
    public AudioClip COMBAT_AttaqueAerienne;
    public AudioClip[] COMBAT_PrendDegat;
    public AudioClip COMBAT_Meurt;
    public AudioClip[] COMBAT_TapeEnnemiAvecEpee;
    public AudioClip COMBAT_TapeTriggerAvecEpee;
    public AudioClip COMBAT_Soin;
    public AudioClip COMBAT_MangerBonbon;
    public AudioClip[] COMBAT_HitBouclier;

    public AudioClip[] MOUVEMENT_BruitDePasMarche;
    public AudioClip[] MOUVEMENT_BruitDePasSprint;
    public AudioClip[] MOUVEMENT_Saut;
    public AudioClip MOUVEMENT_DoubleSaut;
    public AudioClip MOUVEMENT_Dash;

    public AudioClip UI_Valider;
    public AudioClip UI_Annuler;
    public AudioClip UI_GameOver;

    public AudioClip[] UI_CARNET_PageTournee;
    public AudioClip UI_CARNET_ScotchPose;
    public AudioClip UI_CARNET_ScotchArrache;
    public AudioClip[] UI_CARNET_CrayonEcrit;
    public AudioClip UI_CARNET_OuvertureCarnet;
    public AudioClip UI_CARNET_FermetureCarnet;

    public AudioClip[] UI_DIALOGUE_ParcheminDeroule;
    public AudioClip[] UI_DIALOGUE_ParcheminEnroule;
    public AudioClip[] UI_DIALOGUE_BlipBloup;

    public AudioClip[] UI_LOOT_RamassePiece;
    public AudioClip UI_LOOT_RamasseBonbon;
    public AudioClip UI_LOOT_RamasseCle;
    public AudioClip UI_LOOT_RamasseEquipement;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip, AudioSource source, float power, bool ResetBefore)
    {
        if (power < 0)
        {
            power = 0;
        }
        if(power > 1)
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
            Debug.Log("MISSING SOUND MOTHERFUCKER   "+clip.ToString());
        }
    }

    public AudioClip OneOf(AudioClip[] list)
    {
        return list[(int)Random.Range(0,list.Length)];
    }
}
