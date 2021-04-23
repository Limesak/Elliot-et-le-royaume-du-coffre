using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public MenuManager MM;
    public LifeCelluleManager[] Cells;
    private bool isDead;

    private int blockBar;
    private float lastBlockDate;
    private float durationOfBlock;

    private float lastHit;
    public float AutoHeal_Cooldown;
    public float AutoHeal_Power;
    public float InvunerableFramesDuration;

    public float TESTvalue;
    public bool TESTgetDmg;
    public bool TESTgetHeal;
    public bool TESTgetBlock;

    private string KeyMemory;

    void Start()
    {
        isDead = false;
        KeyMemory = "";
    }

    void Update()
    {
        if(GetCurrentIndex() < 0 && !isDead)
        {
            Die();
            return;
        }
        if (!isDead)
        {
            if (blockBar > 0)
            {
                CheckUsability();
            }

            if ( AutoHeal_Cooldown + lastHit <= Time.time && !Cells[GetCurrentIndex()].Full())
            {
                AutoHeal(AutoHeal_Power * Time.deltaTime);
                Debug.Log("AutoHeal");
            }

            if (TESTgetDmg)
            {
                TESTgetDmg = false;
                GetDamage(new DamageForPlayer(TESTvalue, Random.Range(0, 10000), gameObject));
                Debug.Log("Test DMG");
            }

            if (TESTgetHeal)
            {
                TESTgetHeal = false;
                GetHeal(TESTvalue);
                Debug.Log("Test HEAL");
            }

            if (TESTgetBlock)
            {
                TESTgetBlock = false;
                TryToBlockBar((int)TESTvalue, 10);
                Debug.Log("Test BLOCK");
            }

            CheckRest();
        }
       
    }

    public void GetDamage(DamageForPlayer dmg)
    {
        if (!isDead && InvunerableFramesDuration + lastHit <= Time.time && !KeyMemory.Contains(dmg._key+""))
        {
            int index = GetCurrentIndex();
            if (index < Cells.Length && index != -1)
            {
                lastHit = Time.time;
                Cells[index].GetDamage(dmg._power);
            }
            else
            {
                Die();
            }
            KeyMemory = KeyMemory + dmg._key;
        }
        
    }

    public void GetHeal(float heal)
    {
        if (!isDead)
        {
            int index = GetCurrentIndex();
            if (index < Cells.Length && index != -1)
            {
                Cells[index].GetHeal(heal);
            }
        }
    }

    public void AutoHeal(float heal)
    {
        if (!isDead)
        {
            int index = GetCurrentIndex();
            if (index < Cells.Length && index != -1)
            {
                Cells[index].GetHealOnlyThisBar(heal);
            }
        }
    }

    private void CheckRest()
    {
        for(int i = 0; i < Cells.Length; i++)
        {
            if (Cells[i].restDMG > 0 && i!= Cells.Length-1)
            {
                Debug.Log("Rest founded for cell n°" + i + " with restDMG=" + Cells[i].restDMG);
                float rest = Cells[i].restDMG;
                Cells[i + 1].GetDamage(rest);
                Cells[i].restDMG = 0;
            }
            
            if (Cells[i].restHEAL > 0 && i != 0)
            {
                
                if (Cells[i - 1].GetHP() != -1)
                {
                    Debug.Log("Rest founded for cell n°" + i + " with restHEAL=" + Cells[i].restHEAL);
                    float rest = Cells[i].restHEAL;
                    Cells[i - 1].GetHeal(rest);
                    Cells[i].restHEAL = 0;
                }
                else
                {
                    Cells[i].restHEAL = 0;
                }
            }
        }
    }

    public void TryToBlockBar(int nbOfBars, float duration)
    {
        if (nbOfBars >= blockBar)
        {
            for (int i = 0; i < nbOfBars; i++)
            {
                Cells[i].SetUsability(false);
            }
            durationOfBlock = duration;
            lastBlockDate = Time.time;
            lastHit = Time.time;
            blockBar = nbOfBars;
        }
    }

    private void CheckUsability()
    {
        if(lastBlockDate + durationOfBlock <= Time.time)
        {
            /*
            float hp = Cells[GetCurrentIndex()].GetHP();
            Cells[GetCurrentIndex()].SetHP(Cells[GetCurrentIndex()].GetMaxHP());
            Cells[0].SetUsability(true);
            Cells[0].SetHP(hp);
            */
            for (int i = 0; i < blockBar; i++)
            {
                Cells[i].SetUsability(true);
            }
            blockBar = 0;
        }
    }

    public int GetCurrentIndex()
    {
        int res = -1;
        for(int i =0; i <Cells.Length; i++)
        {
            if(res == -1 && !Cells[i].Dead())
            {
                res = i;
            }
        }
        return res;
    }

    public bool Full()
    {
        bool res = false;
        if (GetCurrentIndex() == 0 && Cells[0].Full())
        {
            res = true;
        }
        
        return res;
    }

    public void Die()
    {
        if (!isDead)
        {
            SaveData.current.ResetValueToDefault();
            isDead = true;
            MM.BlackScreen.gameObject.SetActive(true);
            MM.BlackScreen.color = new Color(0, 0, 0, 0);
            StartCoroutine(MM.FadeNLoad());
        }
        
    }
}
