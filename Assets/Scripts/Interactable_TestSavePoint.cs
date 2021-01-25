using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_TestSavePoint : Interactable
{
    public int spawnIndex;

    public sealed override void Interact()
    {
        SaveData.current.spawnInt = spawnIndex;
        SaveLoad.Save(SaveData.current);
    }
}
