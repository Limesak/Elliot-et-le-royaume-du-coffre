using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    public enum CheckerType { AreneTutoPop, AreneTutoDepop };
    public CheckerType Type;
    public GameObject Object;

    void Start()
    {
        if(Type == CheckerType.AreneTutoPop && !SaveData.current.Achievements_AreneTuto)
        {
            Object.SetActive(false);
            Debug.Log("ArenePop");
        }

        if (Type == CheckerType.AreneTutoDepop && SaveData.current.Achievements_AreneTuto)
        {
            //Object.SetActive(false);
            Destroy(Object);
            Debug.Log("AreneDepop");
        }
    }
}
