using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_Magifolien : MonoBehaviour
{
    Animator anim;
    public bool run = false;
    public bool death = false;
    public bool a1 = false;
    public bool a2 = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (run) anim.SetBool("run", true); else anim.SetBool("run", false);
        if (death) anim.SetBool("death", true); else anim.SetBool("death", false);
        if (a1) anim.SetBool("a1", true); else anim.SetBool("a1", false);
        if (a2) anim.SetBool("a2", true); else anim.SetBool("a2", false);
    }
}
