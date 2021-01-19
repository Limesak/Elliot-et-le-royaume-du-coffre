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

    public bool createdByManager;
    public int spawnInt;

    public void InitMainMenu()
    {
        createdByManager = true;
        spawnInt = 0;
    }

    public void ResetValueToDefault()
    {
        spawnInt = 0;
    }
}
