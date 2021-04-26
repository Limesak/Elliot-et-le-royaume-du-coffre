using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForPlayer
{
    public float _power;
    public int _key;
    public GameObject _source;
    public Vector3 _impactPoint;

    public DamageForPlayer(float power, int key, GameObject src, Vector3 Ipoint)
    {
        _power = power;
        _key = key;
        _source = src;
        _impactPoint = Ipoint;
    }
}
