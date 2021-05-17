using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicScript_C2S0 : CinematicScript
{
    [Header("Inspector Info")]
    public Animator AnimLutin;

    void Start()
    {
        AnimLutin.SetBool("isInPanic", true);
    }
}
