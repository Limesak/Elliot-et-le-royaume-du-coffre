using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomBar : MonoBehaviour
{
    public enum BarType { None, Resolution, Qualite };
    public BarType Target;

    public int CurrentIndex;
    public Image TextBackZone;
    public Text TextDisplay;
    public Color ColorIn;
    public Color ColorOut;

    public string[] QualityTable;
    Resolution[] resolutions;

    private ElliotSoundSystem ESS;

    void Start()
    {
        resolutions = Screen.resolutions;
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Target)
        {
            case BarType.Resolution:
                TextDisplay.text = resolutions[CurrentIndex].width + "x" + resolutions[CurrentIndex].height;
                if (resolutions[CurrentIndex].width == Screen.currentResolution.width && resolutions[CurrentIndex].height == Screen.currentResolution.height)
                {
                    TextBackZone.color = ColorIn;
                }
                else
                {
                    TextBackZone.color = ColorOut;
                }
                break;
            case BarType.Qualite:
                TextDisplay.text = QualityTable[CurrentIndex];
                if (QualitySettings.GetQualityLevel() == CurrentIndex)
                {
                    TextBackZone.color = ColorIn;
                }
                else
                {
                    TextBackZone.color = ColorOut;
                }
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
    }

    public void GoBefore()
    {
        CurrentIndex--;
        switch (Target)
        {
            case BarType.Resolution:
                if (CurrentIndex < 0)
                {
                    CurrentIndex = resolutions.Length - 1;
                }
                break;
            case BarType.Qualite:
                if (CurrentIndex < 0)
                {
                    CurrentIndex = QualityTable.Length - 1;
                }
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
    }

    public void GoAfter()
    {
        CurrentIndex++;
        switch (Target)
        {
            case BarType.Resolution:
                if (CurrentIndex > resolutions.Length - 1)
                {
                    CurrentIndex = 0;
                }
                break;
            case BarType.Qualite:
                if (CurrentIndex > QualityTable.Length - 1)
                {
                    CurrentIndex = 0;
                }
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
    }

    public void Appliquer()
    {
        switch (Target)
        {
            case BarType.Resolution:
                Screen.SetResolution(resolutions[CurrentIndex].width, resolutions[CurrentIndex].height,Screen.fullScreen);
                break;
            case BarType.Qualite:
                QualitySettings.SetQualityLevel(CurrentIndex);
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
        ESS.PlaySound(ESS.UI_Valider, ESS.Asource_Interface, 0.8f, false);
    }
}
