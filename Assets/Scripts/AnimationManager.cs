﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    public PlayerMovement PM;
    public Transform ModelParent;
    public Animator anim;

    public float FlipDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("pressingJump", PM.GetJumping());
        anim.SetBool("isGrounded", PM.IsGroundedAnim());
        anim.SetBool("sprinting", PM.GetSprinting());
        anim.SetFloat("walkCoef", PM.GetDirectionInputs().magnitude);
        if (PM.IsGrounded())
        {
            anim.ResetTrigger("startAirAttack");
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
        Debug.Log("jumpAnim");
    }

    public void LaunchAttack()
    {
        anim.SetTrigger("startAttack");
        anim.SetBool("combo2", false);
        anim.SetBool("combo3", false);
        anim.SetBool("combo4", false);
        Debug.Log("startAttack");
    }

    public void LaunchAttackCombo2()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", false);
        anim.SetBool("combo4", false);
        Debug.Log("combo2");
    }
    public void LaunchAttackCombo3()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", true);
        anim.SetBool("combo4", false);
        Debug.Log("combo3");
    }
    public void LaunchAttackCombo4()
    {
        anim.SetBool("combo2", true);
        anim.SetBool("combo3", true);
        anim.SetBool("combo4", true);
        Debug.Log("combo4");
    }

}
