using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoussierinDamageZone : MonoBehaviour
{
    public enum AttackType { horizontal, vertical};
    public bool canDamage;
    public Poussierin Entity;
    public AttackType AT;

    private void OnTriggerEnter(Collider other)
    {
        DealWithCollider(other);
    }

    private void OnTriggerExit(Collider other)
    {
        //DealWithCollider(other);
    }

    private void OnTriggerStay(Collider other)
    {
        //DealWithCollider(other);
    }

    private void DealWithCollider(Collider other)
    {
        //Debug.Log("Enter DD");
        if (canDamage && other.gameObject.GetComponent<LifeManager>())
        {
            other.gameObject.GetComponent<LifeManager>().GetDamage(new DamageForPlayer(CurrentDamage(),Entity.GetAttackKey(),Entity.gameObject, other.ClosestPointOnBounds(transform.position)));
        }
    }

    private float CurrentDamage()
    {
        float res = 0;

        if (AT == AttackType.horizontal)
        {
            res=Entity.DamageHorizontalAttack;
        }
        else
        {
            res = Entity.DamageVerticalAttack;
        }


        return res;
    }
}
