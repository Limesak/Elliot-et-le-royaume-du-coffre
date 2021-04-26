using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoussierinAnimTransmitter : MonoBehaviour
{
    public Poussierin Entity;
    public PoussierinDamageZone[] DZ;

    public void CanWalk()
    {
        Debug.Log("Attack done transmitter");
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
        foreach (PoussierinDamageZone pdz in DZ)
        {
            pdz.AT = PoussierinDamageZone.AttackType.horizontal;
            pdz.canDamage = true;
        }
        
    }

    public void AttackFinished()
    {
        foreach (PoussierinDamageZone pdz in DZ)
        {
            pdz.canDamage = false;
        }
        
    }
}
