using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public enum LootType { None, Piece, Bonbon, CleJaune, WoodSword, WoodShield, Cape, Bucket, Maillet , Amu_Contre, Amu_Envol, Amu_Fendoir, CleConfiture, PotDeConfiture};

    public DiaryManager DM;
    public BonbonUseManager BM;
    public NotifManager NM;

    private ElliotSoundSystem ESS;

    private float lastLootDate;

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
                NM.NewNotif("Ecu du père Leprechaun");
                break;
            case LootType.Bonbon:
                //Debug.Log("Bonbon");
                DM.AddCandy(1);
                SaveData.current.haveDiscoveredCandy = true;
                SaveData.current.CPT_Candy += 1;
                ESS.PlaySound(ESS.UI_LOOT_RamasseBonbon, ESS.Asource_Effects, 0.8f, false);
                BM.UpdateValues();
                NM.NewNotif("Bonbon santé ensorcelé");
                break;
            case LootType.CleJaune:
                //Debug.Log("CleJaune");
                DM.AddYellowKey(1);
                SaveData.current.haveDiscoveredYellowKey = true;
                SaveData.current.CPT_YellowKey += 1;
                ESS.PlaySound(ESS.UI_LOOT_RamasseCle, ESS.Asource_Effects, 0.8f, false);
                NM.NewNotif("Clé de gobelin jaune");
                break;
            case LootType.Bucket:
                //Debug.Log("Bucket");
                DM.AddBufferEntry("Je sais que ce sceau n'est pas un casque, mais c'est toujours mieux que rien!");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[0] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Sceau casque");
                break;
            case LootType.Cape:
                //Debug.Log("Cape");
                DM.AddBufferEntry("Cette cape à l'aire de se prendre au vent quand je saute.");// Changer la ligne de buffer ici  <--
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
                DM.AddBufferEntry("En ouvrant un coffre j'ai découvert un bouclier en bois, il devrai pouvoir me proteger des coups si je l'équipe.");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[3] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Bouclier en bois");
                break;
            case LootType.Amu_Contre:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de Amu_Contre dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[4] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Amulette Contre");
                break;
            case LootType.Amu_Envol:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de Amu_Envol dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[5] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Amulette Envol");
                break;
            case LootType.Amu_Fendoir:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("***Ajouter ligne de buffer pour déblocage de Amu_Fendoir dans script LootManager.cs***");// Changer la ligne de buffer ici  <--
                SaveData.current.UnlockList[6] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Amulette Fendoir");
                break;
            case LootType.CleConfiture:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("J'ai trouvé une clé qui sent la confiture ! Elle devrait me permettre de sortir de cette marmite.");// Changer la ligne de buffer ici  <--
                SaveData.current.ItemSacocheUnique[0] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseCle, ESS.Asource_Effects, 0.8f, false);
                NM.NewNotif("Ramassé: Clé cuillère");
                break;
            case LootType.PotDeConfiture:
                //Debug.Log("WoodShield");
                DM.AddBufferEntry("J'ai trouver un pot de confiture, Lechecuièllere en voudra surement !");// Changer la ligne de buffer ici  <--
                SaveData.current.ItemSacocheUnique[1] = true;
                ESS.PlaySound(ESS.UI_LOOT_RamasseEquipement, ESS.Asource_Effects, 0.7f, false);
                NM.NewNotif("Ramassé: Pot de confiture");
                SaveData.current.Codex_Souvenirs_unlockList[0] = true;
                NM.NewNotif("Nouvelle fiche codex!");
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }
        lastLootDate = Time.time;
    }

    public float lastLootedDate()
    {
        return lastLootDate;
    }
}
