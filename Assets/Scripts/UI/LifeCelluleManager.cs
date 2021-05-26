using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCelluleManager : MonoBehaviour
{
    public int index;
    public GameObject Bar;
    public Image LIFE_Instant_BarImageFilled;
    public Image LIFE_Progressive_BarImageFilled;
    public GameObject UnusableBar;

    private float hp;
    private float hpSlow;
    private float maxHp;

    public float DecreaseSpeed;

    public float restDMG;
    public  float restHEAL;

    void Start()
    {
        maxHp = 25;
        switch (index)
        {
            case 1:
                hp = SaveData.current.LifeCellule_1;
                break;
            case 2:
                hp = SaveData.current.LifeCellule_2;
                break;
            case 3:
                hp = SaveData.current.LifeCellule_3;
                break;
            case 4:
                hp = SaveData.current.LifeCellule_4;
                break;
            default:
                hp = maxHp;
                Debug.Log("No data found");
                break;
        }
        hpSlow = hp;

        if (hp >= 0)
        {
            UnusableBar.SetActive(false);
        }
        else
        {
            UnusableBar.SetActive(true);
        }
    }

    void Update()
    {
        if(hp == -1)
        {
            LIFE_Instant_BarImageFilled.fillAmount = 0;
            LIFE_Progressive_BarImageFilled.fillAmount = 0;
            UnusableBar.SetActive(true);
        }
        else
        {
            UnusableBar.SetActive(false);
            if (hp < hpSlow)
            {
                hpSlow = hpSlow - DecreaseSpeed * Time.deltaTime;
            }
            else
            {
                hpSlow = hp;
            }
            LIFE_Instant_BarImageFilled.fillAmount = hp/maxHp;
            LIFE_Progressive_BarImageFilled.fillAmount = hpSlow/maxHp;
        }
    }

    public void GetDamage(float dmg)
    {
        if (hp - dmg < 0)
        {
            restDMG = restDMG + (dmg - hp);
            hp = 0;
        }
        else
        {
            hp = hp - dmg;
        }
    }

    public void GetHeal(float heal)
    {
        if (hp + heal > maxHp)
        {
            restHEAL = restHEAL + (hp + heal - maxHp);
            hp = maxHp;
            hpSlow = maxHp;
        }
        else
        {
            hp = hp + heal;
            if (hp > hpSlow)
            {
                hpSlow = hp;
            }
        }
    }

    public void GetHealOnlyThisBar(float heal)
    {
        hp = hp + heal;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp > hpSlow)
        {
            hpSlow = hp;
        }
    }

    public bool Dead()
    {
        bool res = true;
        if (hp > 0)
        {
            res = false;
        }
        return res;
    }

    public bool Full()
    {
        bool res = false;
        if (hp == maxHp)
        {
            res = true;
        }
        return res;
    }

    public void SetUsability(bool b)
    {
        if (b)
        {
            hp = 0;
            UnusableBar.SetActive(true);
        }
        else
        {
            hp = -1;
            UnusableBar.SetActive(false);
        }
    }

    public float GetMaxHP()
    {
        return maxHp;
    }

    public float GetHP()
    {
        return hp;
    }

    public void SetHP(float h)
    {
        hp = h;
        hpSlow = h;
    }
}
