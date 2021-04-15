﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SceneTransitionZone : Interactable
{
    public bool NeedInteraction;
    public int SceneIndex;
    public int NextSpawnInt;

    private bool used;

    void Start()
    {
        InitVariables();
        used = false;
        string path = SceneUtility.GetScenePathByBuildIndex(SceneIndex);
        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
        TextDescription = TextDescription +" \""+ sceneName + "\"" ;
    }

    public sealed override void Interact()
    {
        if (NeedInteraction && !MM.isUnfading)
        {
            InternInteract();
        }
    }

    private void InternInteract()
    {
        if (!used)
        {
            used = true;
            GetPlayer().GetComponent<DiaryManager>().SaveTMPinSaveData();
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
            SaveData.current.currentScene = SceneIndex;
            SaveData.current.spawnInt = NextSpawnInt;
            MM.BlackScreen.gameObject.SetActive(true);
            MM.BlackScreen.color = new Color(0, 0, 0, 0);
            StartCoroutine(MM.FadeNLoad());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !NeedInteraction && !MM.isUnfading)
        {
            
            InternInteract();
        }
    }
}
