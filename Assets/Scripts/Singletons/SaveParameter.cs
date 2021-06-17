using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveParameter
{
    private static SaveParameter _current;
    public static SaveParameter current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveParameter();
                _current.ResetValueToDefault();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    public bool createdByManager;
    public int InputMode;
    public float Xsensibility;
    public bool canUseInputs;
    public bool canUseOnglets;
    public bool canUseRotation;
    public MenuMemoryTMP MMTMP;

    //Audio
    public float MixerVolume_Master;
    public float MixerVolume_Musique;
    public float MixerVolume_Interface;
    public float MixerVolume_Effets;

    public void ResetValueToDefault()
    {
        InputMode = 0;
        canUseInputs = true;
        canUseOnglets = true;
        canUseRotation = true;
        Xsensibility = 160;
        MMTMP = new MenuMemoryTMP();

        MixerVolume_Master = 1;
        MixerVolume_Musique = 1;
        MixerVolume_Interface = 0.7f;
        MixerVolume_Effets = 1;
    }
}
