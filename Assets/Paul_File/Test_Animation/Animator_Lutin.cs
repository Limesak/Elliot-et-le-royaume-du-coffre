using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Lutin : MonoBehaviour
{
    Animator anim;
    public bool panic = false;
    public bool walk = false;
    public bool run = false;
    public bool surprise = false;
    public bool give = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panic) anim.SetBool("panic", true); else anim.SetBool("panic", false);
        if (walk) anim.SetBool("walk", true); else anim.SetBool("walk", false);
        if (run) anim.SetBool("run", true); else anim.SetBool("run", false);
        if (surprise) anim.SetBool("surprise", true); else anim.SetBool("surprise", false);
        if (give) anim.SetBool("surprise", true); else anim.SetBool("give", false);

    }
}
