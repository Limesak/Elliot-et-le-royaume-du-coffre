using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventTransmitter : MonoBehaviour
{
    public SwordTrigger ST;
    public AttackUseManager AM;
    private ElliotSoundSystem ESS;
    private float lastStepDate;
    public PlayerMovement PM;

    private void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

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

    public void SOUND_StepMarche()
    {
        if (lastStepDate + 0.1f <= Time.time && PM.IsAlmostGrounded())
        {
            lastStepDate = Time.time;
            ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_BruitDePasMarche), ESS.Asource_Effects, 0.2f, false);
        }
        
    }

    public void SOUND_StepSprint()
    {
        if (PM.IsAlmostGrounded())
        {
            ESS.PlaySound(ESS.OneOf(ESS.MOUVEMENT_BruitDePasSprint), ESS.Asource_Effects, 0.3f, false);
        }
        
    }

    public void SOUND_SwingAttack1()
    {
        ESS.PlaySound(ESS.COMBAT_Attaque1, ESS.Asource_Effects, 0.8f, false);
    }

    public void SOUND_SwingAttack2()
    {
        ESS.PlaySound(ESS.COMBAT_Attaque2, ESS.Asource_Effects, 0.8f, false);
    }

    public void SOUND_SwingAttack3()
    {
        ESS.PlaySound(ESS.COMBAT_Attaque3, ESS.Asource_Effects, 0.8f, false);
    }

    public void SOUND_SwingAttack4()
    {
        ESS.PlaySound(ESS.COMBAT_Attaque4, ESS.Asource_Effects, 0.8f, false);
    }

    public void SOUND_SwingAttackAir()
    {
        ESS.PlaySound(ESS.COMBAT_AttaqueAerienne, ESS.Asource_Effects, 0.8f, false);
    }

    public void HOP_1_forward()
    {
        PM.SetAerianDir(transform.forward*30f + new Vector3(0,4,0));
    }
}
