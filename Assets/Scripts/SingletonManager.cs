using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonManager : MonoBehaviour
{
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            SaveData.current.ResetValueToDefault();
        }
    }
}
