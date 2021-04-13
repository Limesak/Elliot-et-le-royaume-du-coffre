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
    public GameObject Debug_ComboTimeSign;
    public GameObject Debug_AttrackingSign;

    public float AttackCD_failedCombo;
    public float AttackCD_doingCombo;
    public float ComboMinTimingWindows;
    private float AttackLastDate;
    private float ComboLastDate;
    private bool isAttacking;
    private int comboIndex;
    private int CurrentAttackID;
    private bool isComboing;

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
        isComboing = false;
        CurrentAttackID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (ComboLastDate + AttackCD_failedCombo <= Time.time)
        {
            isAttacking = false;
            isComboing = false;
        }
        */

        if (isComboing && PM.IsAlmostGrounded() && ComboLastDate + AttackCD_doingCombo >= Time.time && ComboLastDate + ComboMinTimingWindows <= Time.time && comboIndex >= 1 && comboIndex <= 3)
        {
            Debug_ComboTimeSign.SetActive(true);
        }
        else
        {
            Debug_ComboTimeSign.SetActive(false);
        }

        Debug_AttrackingSign.SetActive(isAttacking);
    }

    private void CheckStart()
    {
        if((HM.CurrentHands == HandManager.Holding.SwordShield || HM.CurrentHands == HandManager.Holding.Empty) && SaveData.current.CurrentItemSWORD != -1)
        {
            //Debug.Log("Attack1");
            HM.CurrentHands = HandManager.Holding.SwordShield;
            HM.UpdateHands();

            if (!PM.IsAlmostGrounded() && !PM.GetDiving())
            {
                AM.LaunchAirAttack();
                CurrentAttackID = (int)Random.Range(1, 10000000);
                //isAttacking = true;
            }

            if (!isAttacking && PM.IsAlmostGrounded() && ComboLastDate+AttackCD_failedCombo<=Time.time)
            {
                //Debug.Log("Attack1A1");
                ComboLastDate = Time.time;
                AttackLastDate = Time.time;
                CurrentAttackID = (int)Random.Range(1, 10000000);
                isAttacking = true;
                comboIndex = 1;
                AM.LaunchAttack();
            }
            else if (isComboing && PM.IsAlmostGrounded() && ComboLastDate + AttackCD_doingCombo >= Time.time && ComboLastDate + ComboMinTimingWindows <= Time.time && comboIndex>=1 && comboIndex <=3)
            {
                //Debug.Log("Attack1B1");
                ComboLastDate = Time.time;
                CurrentAttackID = (int)Random.Range(1, 10000000);
                isAttacking = true;
                isComboing = false;

                if (comboIndex == 1)
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

    public void SetAttacking(bool b)
    {
        isAttacking = b;
    }

    public bool GetComboing()
    {
        return isComboing;
    }

    public void SetComboing(bool b)
    {
        isComboing = b;
    }

    public int GetCurrentKey()
    {
        return CurrentAttackID;
    }
}
