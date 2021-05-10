using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    public PlayerMovement PM;
    public Transform ModelParent;
    public Animator anim;
    public GameObject CAPE;

    public float FlipDuration;

    void Start()
    {
        
    }

    void Update()
    {
        anim.SetBool("pressingJump", PM.GetJumping());
        anim.SetBool("isGrounded", PM.IsGroundedAnim());
        anim.SetBool("sprinting", PM.GetSprinting());
        anim.SetBool("canUseInput", SaveParameter.current.canUseInputs);
        anim.SetFloat("walkCoef", PM.GetDirectionInputs().magnitude);
        if (PM.IsGrounded())
        {
            anim.ResetTrigger("startAirAttack");
        }
        anim.SetBool("floatingAA", !PM.GetGravityFloating());
        //anim.SetFloat("AirAttackSpeed", ((PM.DistanceFromGround() / 45) / (0.167f * Time.deltaTime)) * 0.7f);
        if (!CAPE.GetComponent<Cloth>().enabled && !PM.isPlayerJumping())
        {
            CAPE.GetComponent<Cloth>().enabled = true;
        }
    }

    public void LaunchAirAttack()
    {
        //SaveParameter.current.canUseInputs = false;
        PM.SetGravityFloating(true);
        PM.SetDiving(true);
        PM.GravityPower = 0;
        anim.SetTrigger("startAirAttack");
        StartCoroutine(EndAirAttack());
    }

    IEnumerator EndAirAttack()
    {
        yield return new WaitForSeconds(FlipDuration);
        PM.SetGravityFloating(false);
        PM.GravityPower = PM.DivingGravityForce;
        anim.ResetTrigger("startAirAttack");
    }

    public void StartJump()
    {
        anim.SetTrigger("jump");
        //Debug.Log("jumpAnim");
    }

    public void StartAirJump()
    {
        //anim.SetTrigger("airJump");
        //Debug.Log("jumpAnim");
        CAPE.GetComponent<Cloth>().enabled = false;
    }

    public void LaunchAttack()
    {
        anim.SetTrigger("startAttack");
        anim.SetBool("combo2", false);
        anim.SetBool("combo3", false);
        anim.SetBool("combo4", false);
        //Debug.Log("startAttack");
    }

    public void LaunchAttackCombo2()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", false);
        anim.SetBool("combo4", false);
        //Debug.Log("combo2");
    }
    public void LaunchAttackCombo3()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", true);
        anim.SetBool("combo4", false);
        //Debug.Log("combo3");
    }
    public void LaunchAttackCombo4()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", true);
        anim.SetBool("combo4", true);
        //Debug.Log("combo4");
    }

    public void Dash()
    {
        anim.SetTrigger("dash");
    }

    public void SetBlocking(bool b)
    {
        anim.SetBool("blocking",b);
    }

    public void SetXYwalkVelues(float x, float y)
    {
        anim.SetFloat("XvalueWalk", x);
        anim.SetFloat("YvalueWalk", y);
    }
}
