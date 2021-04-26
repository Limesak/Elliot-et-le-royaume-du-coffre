using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public int _power;
    public int _key;
    public GameObject _source;
    public Vector3 _impactPoint;

    public Damage(int power, int key, GameObject src, Vector3 Ipoint)
    {
        _power = power;
        _key = key;
        _source = src;
        _impactPoint = Ipoint;
}
}
