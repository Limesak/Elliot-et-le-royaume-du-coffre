using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveIf : MonoBehaviour
{
    public enum ValueType { None, AreneTuto };
    public ValueType value;
    // Start is called before the first frame update
    void Start()
    {
        switch (value)
        {
            case ValueType.None:
                break;
            case ValueType.AreneTuto:
                if (SaveData.current.Achievements_AreneTuto)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
