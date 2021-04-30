using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoussierinAnimTransmitter : MonoBehaviour
{
    public Poussierin Entity;
    public PoussierinDamageZone[] DZ;

    public void CanWalk()
    {
        //Debug.Log("Attack done transmitter");
        Entity.CanWalk();
    }

    public void DoingVerticalAttack()
    {
        foreach(PoussierinDamageZone pdz in DZ)
        {
            pdz.AT = PoussierinDamageZone.AttackType.vertical;
            pdz.canDamage = true;
        }
        
    }

    public void DoingHorizontalAttack()
    {
        DZ[0].AT = PoussierinDamageZone.AttackType.horizontal;
        DZ[0].canDamage = true;
    }

    public void AttackFinished()
    {
        foreach (PoussierinDamageZone pdz in DZ)
        {
            pdz.canDamage = false;
        }
        
    }

    public void GoSlow()
    {
        Entity.Anim.SetFloat("attackSpeed", 0.25f);
    }

    public void GoNormalSpeed()
    {
        Entity.Anim.SetFloat("attackSpeed", 1f);
    }
}
