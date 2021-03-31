using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_SavePoint : Interactable
{
    public int spawnIndex;

    public sealed override void Interact()
    {
        SaveData.current.spawnInt = spawnIndex;
        SaveData.current.currentScene = SceneManager.GetActiveScene().buildIndex;
        SaveLoad.Save(SaveData.current);
    }
}
