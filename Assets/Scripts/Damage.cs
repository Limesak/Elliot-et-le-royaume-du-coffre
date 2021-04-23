using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public int _power;
    public int _key;
    public GameObject _source;

    public Damage (int power, int key, GameObject src)
    {
        _power = power;
        _key = key;
        _source = src;
    }
}
