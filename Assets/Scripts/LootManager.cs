using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public enum LootType { None, Piece, Bonbon, CleJaune, WoodSword, WoodShield, Cape, Bucket, Maillet };

    public DiaryManager DM;
    public BonbonUseManager BM;
    public NotifManager NM;

    private ElliotSoundSystem ESS;

    void Start()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        NM = GameObject.FindGameObjectWithTag("CanvasUI").GetComponent<NotifManager>();
    }

    public void Loot(LootType LT)
    {
        switch (LT)
        {
            case LootType.Piece:
                //Debug.Log("Piece");
                DM.AddMoney(1);
                SaveData.current.haveDiscoveredMoney = true;
                SaveData.current.CPT_Money += 1;
                ESS.PlaySound(ESS.OneOf(ESS.UI_LOOT_RamassePiece), ESS.Asource_Effects, 0.8f, false);
                NM.NewNotif("Ramassé: Ecu du père Leprechaun x1");
                break;
            case LootType.Bonbon:
                //Debug.Log("Bonbon");
                DM.AddCandy(1);
                SaveData.current.haveDiscoveredCandy = true;
                SaveData.current.CPT_Candy += 1;
                ESS.PlaySound(ESS.UI_LOOT_RamasseBonbon, ESS.Asource_Effects, 0.8f, false);
                BM.UpdateValues();
                NM.NewNotif("Ramassé: Bonbon santé ensorcelé x1");
                break;
            case LootType.CleJaune:
                //Debug.Log("CleJaune");
                DM.AddYellowKey(1);
                SaveData.current.haveDiscoveredYellowKey = true;
                SaveData.current.CPT_YellowKey += 1;
                ESS.PlaySound(ESS.UI_LOOT_RamasseCle, ESS.Asource_Effects, 0.8f, false);
                NM.NewNotif("Ramassé: Clé de gobelin jaune x1");
                break;
            case LootType.Bucket:
                //Debug.Log("Bucket");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de BUCKET dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[0] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Sceau casque");
                break;
            case LootType.Cape:
                //Debug.Log("Cape");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de CAPE dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[1] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Cape");
                break;
            case LootType.WoodSword:
                //Debug.Log("WoodSword");
                //DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de WOODSWORD dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[2] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Epée en bois");
                break;
            case LootType.WoodShield:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de WOODSHIELD dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[3] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Bouclier en bois");
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
    }
}
