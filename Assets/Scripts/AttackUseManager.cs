using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUseManager : MonoBehaviour
{
    Movements MovementsControls;
    public PlayerMovement PM;
    public AnimationManager AM;
    public ScreenShake screenShakeScript;

    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.AttackUse.started += ctx => CheckStart();
            MovementsControls.Player.AttackUse.canceled += ctx => CheckEnd();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.AttackUse.started += ctx => CheckStart();
            MovementsControls.Player1.AttackUse.canceled += ctx => CheckEnd();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.AttackUse.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.AttackUse.Enable();
        }

    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.AttackUse.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.AttackUse.Disable();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckStart()
    {
        if (!PM.IsAlmostGrounded())
        {
            AM.LaunchAirAttack();
        }
        
    }

    private void CheckEnd()
    {

    }
}
