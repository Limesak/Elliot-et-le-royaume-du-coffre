using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    public AttackUseManager AM;
    public HandManager HM;
    public bool canDamage;

    public GameObject Debug_CanDamageSign;

    void Start()
    {
        Debug_CanDamageSign.SetActive(false);
    }

    void Update()
    {
        Debug_CanDamageSign.SetActive(canDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other);
    }

    private void OnTriggerExit(Collider other)
    {
        DealDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        DealDamage(other);
    }

    private void DealDamage(Collider other)
    {
        //Debug.Log("Enter DD");
        if (canDamage && other.gameObject.GetComponent<DamageReceiver>())
        {
            //Debug.Log("DD");
            other.gameObject.GetComponent<DamageReceiver>().Receive(new Damage(CurrentDamage(), AM.GetCurrentKey()));
        }
    }

    private int CurrentDamage()
    {
        int res = 0;

        if(HM.CurrentSword == HandManager.SwordType.Wood)
        {
            res = 1;
        }
        else if (HM.CurrentSword == HandManager.SwordType.Pin)
        {
            res = 1;
        }
        else if (HM.CurrentSword == HandManager.SwordType.Bone)
        {
            res = 1;
        }
        else if (HM.CurrentSword == HandManager.SwordType.Butter)
        {
            res = 1;
        }

        return res;
    }

    
}
