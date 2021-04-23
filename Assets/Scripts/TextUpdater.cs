using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    public enum Value { None, Money, Candy, YellowKey };
    public Value choice;
    public Text TextToUpdate;

    void Update()
    {
        switch (choice)
        {
            case Value.Money:
                TextToUpdate.text = SaveData.current.TMP_CPTmoney + "";
                break;
            case Value.Candy:
                TextToUpdate.text = SaveData.current.TMP_CPTcandy + "";
                break;
            case Value.YellowKey:
                TextToUpdate.text = SaveData.current.TMP_CPTYellowKey + "";
                break;
            default:
                TextToUpdate.text = "x";
                break;
        }
    }
}
