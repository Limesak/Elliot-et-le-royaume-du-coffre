using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CustomSliderCentralizer : MonoBehaviour
{
    public enum SlideType { None, Master, Musique, Interface, Effets };
    public SlideType Target;

    public float CurrentValue;
    public CustomSliderButton[] CSB_list;
    public Color ColorIn;
    public Color ColorOut;
    public AudioMixer mixer;

    private ElliotSoundSystem ESS;

    void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        GetCurrentValue();
    }

    void Update()
    {
        UpdateUI();
        UpdateMixers();
    }

    public void SetNewValue(float value)
    {
        CurrentValue = value;
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Effects, 0.8f, false);
    }

    public void UpdateUI()
    {
        for(int i = 0; i < CSB_list.Length; i++)
        {
            if (CSB_list[i].ValueOfButton <= CurrentValue)
            {
                CSB_list[i].ImageOfButton.color = ColorIn;
            }
            else
            {
                CSB_list[i].ImageOfButton.color = ColorOut;
            }
        }
    }

    public void UpdateMixers()
    {
        switch (Target)
        {
            case SlideType.Master:
                mixer.SetFloat("MasterVol", Mathf.Log10(CurrentValue) * 20);
                break;
            case SlideType.Musique:
                mixer.SetFloat("MusiqueVol", Mathf.Log10(CurrentValue) * 20);
                break;
            case SlideType.Interface:
                mixer.SetFloat("InterfaceVol", Mathf.Log10(CurrentValue) * 20);
                break;
            case SlideType.Effets:
                mixer.SetFloat("EffetVol", Mathf.Log10(CurrentValue) * 20);
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
    }

    public void GetCurrentValue()
    {
        switch (Target)
        {
            case SlideType.Master:
                mixer.GetFloat("MasterVol", out CurrentValue);
                CurrentValue = Mathf.Pow(10,CurrentValue / 20);
                break;
            case SlideType.Musique:
                mixer.GetFloat("MusiqueVol", out CurrentValue);
                CurrentValue = Mathf.Pow(10, CurrentValue / 20);
                break;
            case SlideType.Interface:
                mixer.GetFloat("InterfaceVol", out CurrentValue);
                CurrentValue = Mathf.Pow(10, CurrentValue / 20);
                break;
            case SlideType.Effets:
                mixer.GetFloat("EffetVol", out CurrentValue);
                CurrentValue = Mathf.Pow(10, CurrentValue / 20);
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
    }
}
