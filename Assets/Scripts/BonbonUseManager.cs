using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;

public class BonbonUseManager : MonoBehaviour
{
    Movements MovementsControls;

    private ElliotSoundSystem ESS;
    public AttackUseManager AM;
    public LifeManager LM;
    public PlayerMovement PM;
    public ParticleSystem UsePS;
    public GameObject UI;
    public GameObject UI_CanUse;
    public GameObject UI_CantUse;
    public Text UItext;
    public Image UIjaugeCD;
    public float Cooldown;
    private float lastUseDate;
    public float healPower;
    private bool showingUI;
    private bool reloading;

    void Awake()
    {
        MovementsControls = new Movements();
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.ConsumeCandy.started += ctx => TryToEatCandy();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.ConsumeCandy.started += ctx => TryToEatCandy();
        }
    }

    public void OnEnable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.ConsumeCandy.Enable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.ConsumeCandy.Enable();
        }

    }

    public void OnDisable()
    {
        if (SaveParameter.current.InputMode == 0)
        {
            MovementsControls.Player.ConsumeCandy.Disable();
        }
        else if (SaveParameter.current.InputMode == 1)
        {
            MovementsControls.Player1.ConsumeCandy.Disable();
        }

    }

    void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        UpdateValues();
    }

    void Update()
    {
        if(reloading && showingUI)
        {
            //Debug.Log("("+ Cooldown+ "-(" + (Time.time - lastUseDate) + ")) /" + Cooldown + "=" + (Cooldown - (Time.time - lastUseDate)) / Cooldown);
            UIjaugeCD.fillAmount = (Cooldown-(Time.time-lastUseDate)) / Cooldown;
            if(lastUseDate + Cooldown <= Time.time)
            {
                UIjaugeCD.gameObject.SetActive(false);

                reloading = false;
                
                if (SaveData.current.CPT_Candy > 0)
                {
                    UI_CantUse.SetActive(false);
                    UI_CanUse.SetActive(true);
                }
                else
                {
                    UI_CantUse.SetActive(true);
                    UI_CanUse.SetActive(false);
                }
            }
        }
    }

    void TryToEatCandy()
    {
        if (SaveParameter.current.canUseInputs && LM.isAlive() && !PM.GetSprinting() && PM.IsAlmostGrounded() && !AM.GetAttacking() && lastUseDate + Cooldown <= Time.time && SaveData.current.CPT_Candy>0)
        {
            lastUseDate = Time.time;
            LM.GetHeal(healPower);
            UsePS.Play();
            SaveData.current.CPT_Candy--;

            if (showingUI)
            {
                reloading = true;
                UIjaugeCD.gameObject.SetActive(true);
                UI_CanUse.SetActive(false);
                UI_CantUse.SetActive(true);
                UI_CantUse.transform.DOPunchScale(UI_CantUse.transform.localScale*1.2f, 0.4f, 0,0).OnComplete(() => { UI_CantUse.SetActive(false); });
            }

            UpdateValues();
            ESS.PlaySound(ESS.COMBAT_MangerBonbon, ESS.Asource_Effects, 0.6f, false);
        }
    }

    public void UpdateValues()
    {
        if(SaveData.current.CurrentDifficulty != 2 && SaveData.current.CurrentDifficulty != 4 && SaveData.current.haveDiscoveredCandy)
        {
            showingUI = true;
            if (!reloading)
            {
                if (SaveData.current.CPT_Candy > 0)
                {
                    UIjaugeCD.gameObject.SetActive(false);
                    UI_CantUse.SetActive(false);
                    UI_CanUse.SetActive(true);
                }
                else
                {
                    UIjaugeCD.gameObject.SetActive(false);
                    UI_CantUse.SetActive(true);
                    UI_CanUse.SetActive(false);
                }
            }
            else
            {
                UI_CanUse.SetActive(false);
            }
        }
        else
        {
            showingUI = false;
        }
        UI.SetActive(showingUI);
        UItext.text = SaveData.current.CPT_Candy + "";
    }

}
