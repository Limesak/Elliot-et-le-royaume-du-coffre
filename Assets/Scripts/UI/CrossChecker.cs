using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossChecker : MonoBehaviour
{
    public enum CheckType { None, InputMode1, InputMode2, FullScreen, Windows};
    public CheckType Target;
    public GameObject Cross;

    void Update()
    {
        switch (Target)
        {
            case CheckType.InputMode1:
                if (SaveParameter.current.InputMode == 0)
                {
                    Cross.SetActive(true);
                }
                else
                {
                    Cross.SetActive(false);
                }
                break;
            case CheckType.InputMode2:
                if (SaveParameter.current.InputMode == 1)
                {
                    Cross.SetActive(true);
                }
                else
                {
                    Cross.SetActive(false);
                }
                break;
            case CheckType.FullScreen:
                if (Screen.fullScreen)
                {
                    Cross.SetActive(true);
                }
                else
                {
                    Cross.SetActive(false);
                }
                break;
            case CheckType.Windows:
                if (!Screen.fullScreen)
                {
                    Cross.SetActive(true);
                }
                else
                {
                    Cross.SetActive(false);
                }
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
    }
}
