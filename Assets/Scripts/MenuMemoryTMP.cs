using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMemoryTMP 
{
    public enum CancelState { Main, InOpenMap, OnStuffDesciption, OnCodexDesciption };

    public int index;
    public CancelState CS;

    public MenuMemoryTMP()
    {
        index = 0;
        CS = CancelState.Main;
    }
}
