using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    private ElliotSoundSystem ESS;
    public AttackUseManager AM;
    public HandManager HM;
    public bool canDamage;

    public ScreenShake screenShakeScript;
    public ScreenShake screenShakeScriptLock;

    public float HitShakeForce;
    public float HitShakeDuration;

    public GameObject Debug_CanDamageSign;

    private float lastHitDate;

    void Start()
    {
        Debug_CanDamageSign.SetActive(false);
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
    }

    void Update()
    {
        //Debug_CanDamageSign.SetActive(canDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        DealWithCollider(other);
    }

    private void OnTriggerExit(Collider other)
    {
        DealWithCollider(other);
    }

    private void OnTriggerStay(Collider other)
    {
        //DealWithCollider(other);
    }

    private void DealWithCollider(Collider other)
    {
        //Debug.Log("Enter DD");
        if (canDamage && other.gameObject.GetComponent<DamageReceiver>())
        {
            other.gameObject.GetComponent<DamageReceiver>().Receive(new Damage(CurrentDamage(), AM.GetCurrentKey(),AM.gameObject,other.ClosestPointOnBounds(transform.position)));
            screenShakeScript.setShake(HitShakeForce,HitShakeDuration, true);
            screenShakeScriptLock.setShake(HitShakeForce, HitShakeDuration, true);
            if (lastHitDate + 0.1f < Time.time)
            {
                lastHitDate = Time.time;
                ESS.PlaySound(ESS.OneOf(ESS.COMBAT_TapeEnnemiAvecEpee), ESS.Asource_Effects, 0.7f, false);
            }
            
        }

        if (canDamage && other.gameObject.GetComponent<Trigger_Poutch>())
        {
            other.gameObject.GetComponent<Trigger_Poutch>().TriggerCD();
            screenShakeScript.setShake(HitShakeForce, HitShakeDuration, false);
            screenShakeScriptLock.setShake(HitShakeForce, HitShakeDuration, false);
            if (lastHitDate + 0.1f < Time.time)
            {
                lastHitDate = Time.time;
                ESS.PlaySound(ESS.COMBAT_TapeTriggerAvecEpee, ESS.Asource_Effects, 0.15f, false);
            }
            
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
