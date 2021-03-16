using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUseManager : MonoBehaviour
{
    Movements MovementsControls;
    public PlayerMovement PM;
    public AnimationManager AM;
    public ScreenShake screenShakeScript;
    public HandManager HM;

    public float AttackCD_failedCombo;
    public float AttackCD_doingCombo;
    public float ComboMinTimingWindows;
    private float AttackLastDate;
    private float ComboLastDate;
    private bool isAttacking;
    private int comboIndex;
    private int CurrentAttackID;

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
        isAttacking = false;
        CurrentAttackID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (ComboLastDate + AttackCD_failedCombo <= Time.time)
        {
            isAttacking = false;
        }
    }

    private void CheckStart()
    {
        if(HM.CurrentHands == HandManager.Holding.SwordShield || HM.CurrentHands == HandManager.Holding.Empty)
        {
            Debug.Log("Attack1");
            HM.CurrentHands = HandManager.Holding.SwordShield;
            HM.UpdateHands();

            if (!PM.IsAlmostGrounded() && !PM.GetDiving())
            {
                AM.LaunchAirAttack();
                CurrentAttackID = (int)Random.RandomRange(1, 10000000);
                isAttacking = true;
            }

            if (!isAttacking && PM.IsAlmostGrounded() && ComboLastDate+AttackCD_failedCombo<=Time.time)
            {
                Debug.Log("Attack1A1");
                ComboLastDate = Time.time;
                AttackLastDate = Time.time;
                CurrentAttackID = (int)Random.RandomRange(1, 10000000);
                isAttacking = true;
                comboIndex = 1;
                AM.LaunchAttack();
            }
            else if (isAttacking && PM.IsAlmostGrounded() && ComboLastDate + AttackCD_doingCombo >= Time.time && ComboLastDate + ComboMinTimingWindows <= Time.time && comboIndex>=1 && comboIndex <=3)
            {
                Debug.Log("Attack1B1");
                ComboLastDate = Time.time;
                CurrentAttackID = (int)Random.RandomRange(1, 10000000);
                isAttacking = true;

                if(comboIndex == 1)
                {
                    comboIndex = 2;
                    AM.LaunchAttackCombo2();
                }
                else if (comboIndex == 2)
                {
                    comboIndex = 3;
                    AM.LaunchAttackCombo3();
                }
                else if (comboIndex == 3)
                {
                    comboIndex = 4;
                    ComboLastDate = Time.time+0.7f;
                    AM.LaunchAttackCombo4();
                }
            }
        }
        
        
    }

    private void CheckEnd()
    {

    }

    public bool GetAttacking()
    {
        return isAttacking;
    }
}
