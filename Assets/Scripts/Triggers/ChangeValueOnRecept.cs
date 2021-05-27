using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeValueOnRecept : TriggerReceptor
{
    public enum ValueType { LecheCuillereOut };
    public ValueType Type;

    public sealed override void Recept()
    {
        switch (Type)
        {
            case ValueType.LecheCuillereOut:
                SaveData.current.Achievements_OutMarmite=true;
                Debug.Log("OutMarmite");
                break;
        }
    }
}
