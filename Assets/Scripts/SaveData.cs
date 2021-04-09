using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
                _current.ResetValueToDefault();
            }
            return _current;
        }
        set
        {
            if (value != null)
            {
                _current = value;
            }
        }
    }

    //Geo Position infos
    public int spawnInt;
    public int currentScene;

    //Life infos
    public float LifeCellule_1;
    public float LifeCellule_2;
    public float LifeCellule_3;
    public float LifeCellule_4;

    //Inventory
    public bool[] UnlockList;
    public int CurrentItemHEAD;
    public int CurrentItemBACK;
    public int CurrentItemSHIELD;
    public int CurrentItemSWORD;

    public void ResetValueToDefault()
    {
        spawnInt = 0;
        currentScene = 0;
        LifeCellule_1 = 25;
        LifeCellule_2 = 25;
        LifeCellule_3 = 25;
        LifeCellule_4 = 25;

        CurrentItemHEAD = -1;
        CurrentItemBACK = -1;
        CurrentItemSHIELD = -1;
        CurrentItemSWORD = -1;

        UnlockList = new bool[4];
        for(int i = 0; i< UnlockList.Length; i++)
        {
            UnlockList[i] = true;//True pour debug   &  False for build
        }

        Debug.Log("SaveData reseted");
    }
}
