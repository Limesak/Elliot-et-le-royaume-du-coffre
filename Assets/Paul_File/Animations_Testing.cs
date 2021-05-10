using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations_Testing : MonoBehaviour
{
    Animator anim;
    Movements MovementsControls;

    public float speed;



    // Start is called before the first frame update
    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            //MovementsControls.Player.LAT.performed += ctx => Ctrlanim();
        }
        anim = transform.GetComponent<Animator>();
    }

    private void Start()
    {

    }
    public void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}
