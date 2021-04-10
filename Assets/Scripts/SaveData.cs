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

    //Diary
    public string[] Diary;
    public string[] TMP_Buffer;
    public int TMP_CPTmoney;
    public int[] TMP_CPTkills;
    public int TMP_CPTcurrentIntro;
    public string TMP_MISSIONcontent;
    public string[] TMP_MISSIONhint;
    public bool hasBeenTMP;

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

        Diary = new string[0];
        TMP_Buffer = new string[0];
        TMP_CPTmoney = 0;
        TMP_CPTkills = new int[1];//A changer si nombre de mobs est modifié
        for (int i = 0; i < TMP_CPTkills.Length; i++)
        {
            TMP_CPTkills[i] = 0;
        }
        TMP_CPTcurrentIntro = 0;
        TMP_MISSIONcontent = "";
        TMP_MISSIONhint = new string[0];
        hasBeenTMP = false;


        Debug.Log("SaveData reseted");
    }
}
