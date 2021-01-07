using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public float Stamina;
    private float MaxStamina;
    public float StaminaRegen;

    public float RegeneCD;
    private float lastStaminaUse;

    void Start()
    {
        MaxStamina = Stamina;
    }

    void Update()
    {
        if(lastStaminaUse + RegeneCD <= Time.time)
        {
            if (Stamina < MaxStamina)
            {
                Stamina = Stamina + (StaminaRegen * Time.deltaTime);
            }
            else
            {
                Stamina = MaxStamina;
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
        }

        return res;
    }
}
