using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elliot_Anim_Script : MonoBehaviour
{
    Animator anim;
    public bool walking = false;
    public bool trotting = false;
    public bool running = false;
    public bool death = false;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (walking) anim.SetBool("Walk", true); else anim.SetBool("Walk", false);
        if (trotting) anim.SetBool("Trotting", true); else anim.SetBool("Trotting", false);
        if (running) anim.SetBool("Sprint", true); else anim.SetBool("Sprint", false);
        if (death) anim.SetBool("Death", true); else anim.SetBool("Death", false);
        if (attack) anim.SetBool("Attack", true); else anim.SetBool("Attack", false);
    }
}
