using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{
    public static bool Save( object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/Save.save";

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();
        return true;
    }

    public static bool alreadySavedGame()
    {
        Debug.Log("Looking for save at : " + Application.persistentDataPath + "/saves/Save.save");
        return File.Exists(Application.persistentDataPath + "/saves/Save.save");
    }

    public static object Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/saves/Save.save"))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(Application.persistentDataPath + "/saves/Save.save", FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.Log("Failed to load, path is wrong :" + Application.persistentDataPath + "/saves/Save.save");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
