using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SavePoint : Interactable
{
    public int spawnIndex;
    public Renderer ColorIndicator;
    public Material CanUseColor;
    public Material CantUseColor;
    public float cooldown;
    private float lastUse;
    public NotifManager NM;
    public ParticleSystem PS;
    public GameObject Lights;

    public sealed override void CustomStart()
    {
        lastUse = -9999;
        NM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<NotifManager>();
        Lights.SetActive(false);
    }

    public sealed override void Interact()
    {
        if(lastUse + cooldown <= Time.time)
        {
            lastUse = Time.time;
            GetPlayer().GetComponent<DiaryManager>().SaveInSaveData();
            foreach (LifeCelluleManager lcm in GetPlayer().GetComponent<LifeManager>().Cells)
            {
                if (lcm.index == 1)
                {
                    SaveData.current.LifeCellule_1 = lcm.GetHP();
                }
                else if (lcm.index == 2)
                {
                    SaveData.current.LifeCellule_2 = lcm.GetHP();
                }
                else if (lcm.index == 3)
                {
                    SaveData.current.LifeCellule_3 = lcm.GetHP();
                }
                else if (lcm.index == 4)
                {
                    SaveData.current.LifeCellule_4 = lcm.GetHP();
                }
            }
            SaveData.current.spawnInt = spawnIndex;
            SaveData.current.currentScene = SceneManager.GetActiveScene().buildIndex;
            SaveLoad.Save(SaveData.current);
            PS.Play();
            NM.NewNotif("Progression sauvegardée!");
        }
        
    }

    public sealed override void CustomUpdate()
    {
        if (lastUse + cooldown <= Time.time)
        {
            Lights.SetActive(false);
        }
        else
        {
            Lights.SetActive(true);
        }
    }
}
