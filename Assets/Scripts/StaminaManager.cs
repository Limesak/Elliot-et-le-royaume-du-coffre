using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public PlayerMovement PM;

    public float Stamina;
    private float MaxStamina;
    public float StaminaRegen;

    public float RegeneCD;
    private float lastStaminaUse;

    public GameObject StaminaBar;
    public Image StaminaImageFilled;

    void Start()
    {
        MaxStamina = Stamina;
    }

    void Update()
    {
        if(lastStaminaUse + RegeneCD <= Time.time && !PM.isCurrentlyBlocking())
        {
            if (Stamina < MaxStamina)
            {
                Stamina = Stamina + (StaminaRegen * Time.deltaTime);
                StaminaBar.SetActive(true);
                StaminaImageFilled.fillAmount = Stamina / MaxStamina;
            }
            else
            {
                Stamina = MaxStamina;
                StaminaBar.SetActive(false);
            }
        }
    }

    public bool UseXamount(float cost)
    {
        bool res = true;

        if (Stamina - cost <= 0)
        {
            res = false ;
        }
        else
        {
            Stamina = Stamina - cost;
            lastStaminaUse = Time.time;
            StaminaBar.SetActive(true);
            StaminaImageFilled.fillAmount = Stamina / MaxStamina;
        }

        return res;
    }
}
