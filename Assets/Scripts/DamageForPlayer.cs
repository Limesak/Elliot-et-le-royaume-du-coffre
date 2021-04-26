using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForPlayer
{
    public float _power;
    public int _key;
    public GameObject _source;

    public DamageForPlayer(float power, int key, GameObject src)
    {
        _power = power;
        _key = key;
        _source = src;
    }
}
