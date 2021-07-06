using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pousserin_Anim_Script : MonoBehaviour
{
    Animator anim;
    public bool spawn = false;
    public bool walk = false;
    public bool fast_w = false;
    public bool death = false;
    public bool attack_v = false;
    public bool attack_h = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn) anim.SetBool("spawn", true); else anim.SetBool("spawn", false);
        if (walk) anim.SetBool("walk", true); else anim.SetBool("walk", false);
        if (fast_w) anim.SetBool("fast_walk", true); else anim.SetBool("fast_walk", false);
        if (death) anim.SetBool("death", true); else anim.SetBool("death", false);
        if (attack_v) anim.SetBool("attack_v", true); else anim.SetBool("attack_v", false);
        if (attack_h) anim.SetBool("attack_h", true); else anim.SetBool("attack_h", false);
    }
}
