using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelevantEntity : MonoBehaviour
{
    public string Key;

    void Start()
    {
        if (SaveData.current.RELEVANT_KeyList.Contains(Key) && Key != "")
        {
            Destroy(gameObject);
        }
    }

    public void NotRelevantAnymore()
    {
        SaveData.current.RELEVANT_KeyList = SaveData.current.RELEVANT_KeyList + Key+"|";
    }
}
