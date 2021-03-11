using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations_Testing : MonoBehaviour
{
    Animator anim;
    Movements MovementsControls;

    [Header("A1")]
     bool canA1;
     bool A1;
    
    [Header("A1")]
     bool canA2;
     bool A2;    
    
    [Header("A3")]
     bool canA3;
     bool A3;    
    
    [Header("A3")]
     bool canA4;
     bool A4;



    // Start is called before the first frame update
    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.LAT.performed += ctx => Ctrlanim();
        }
        anim = transform.GetComponent<Animator>();
    }

    private void Start()
    {
        canA1 = true;
    }
    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.LAT.Enable();
        }

    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.LAT.Disable();
        }
    }

    void Ctrlanim()
    {
        /*        if (canA1)
                {
                    A1 = true;
                    anim.SetTrigger("A1");
                    canA2 = true;
                    canA1 = false;
                }

                else if (canA2) 
                {
                    A2 = true;
                    anim.SetTrigger("A2");
                    canA3 = true;
                    canA2 = false;
                }        

                else if (canA3) 
                {
                    A3 = true;
                    anim.SetTrigger("A3");
                    canA4 = true;
                    canA3 = false;
                }

                else if (canA4)
                {
                    A4 = true;
                    anim.SetTrigger("A4");

                    A1 = false;
                    A2 = false;
                    A3 = false;
                    A4 = false;

                    canA1 = true;
                    canA4 = false;
                }*/
        anim.SetTrigger("A1");
    }
}
