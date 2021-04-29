using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public enum LootType { None, Piece, Bonbon, CleJaune, WoodSword, WoodShield, Cape, Bucket, Maillet };

    public DiaryManager DM;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Loot(LootType LT)
    {
        switch (LT)
        {
            case LootType.Piece:
                Debug.Log("Piece");
                DM.AddMoney(1);
                SaveData.current.haveDiscoveredMoney = true;
                SaveData.current.CPT_Money += 1; 
                break;
            case LootType.Bonbon:
                Debug.Log("Bonbon");
                DM.AddCandy(1);
                SaveData.current.haveDiscoveredCandy = true;
                SaveData.current.CPT_Candy += 1;
                break;
            case LootType.CleJaune:
                Debug.Log("CleJaune");
                DM.AddYellowKey(1);
                SaveData.current.haveDiscoveredYellowKey = true;
                SaveData.current.CPT_YellowKey += 1;
                break;
            case LootType.Bucket:
                Debug.Log("Bucket");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de BUCKET dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[0] = true;
                break;
            case LootType.Cape:
                Debug.Log("Cape");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de CAPE dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[1] = true;
                break;
            case LootType.WoodSword:
                Debug.Log("WoodSword");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de WOODSWORD dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[2] = true;
                break;
            case LootType.WoodShield:
                Debug.Log("WoodShield");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de WOODSHIELD dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[3] = true;
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
    }
}
