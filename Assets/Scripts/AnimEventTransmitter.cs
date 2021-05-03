﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTransmitter : MonoBehaviour
{
    public SwordTrigger ST;
    public AttackUseManager AM;

    public void isDangerous()
    {
        ST.canDamage = true;
        SaveParameter.current.canUseRotation = false;
        //Debug.Log("isDangerous");
    }

    public void isntDangerous()
    {
        ST.canDamage = false;
        SaveParameter.current.canUseRotation = true;
        //Debug.Log("isntDangerous");
    }

    public void canWalk()
    {
        AM.SetAttacking(false);
        SaveParameter.current.canUseRotation = true;
        //Debug.Log("canWalk");
    }

    public void cantWalk()
    {
        AM.SetAttacking(true);
        
        //Debug.Log("cantWalk");

    }

    public void canCombo()
    {
        AM.SetComboing(true);
        //Debug.Log("canCombo");
    }

    public void cantCombo()
    {
        AM.SetComboing(false);
        //Debug.Log("cantCombo");
    }
}
