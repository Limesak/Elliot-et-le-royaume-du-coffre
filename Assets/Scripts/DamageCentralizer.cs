using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCentralizer : MonoBehaviour
{
    private int lastKey;
    //public GameObject Debug_HitSign;
    private List<Damage> DamageInMemory;

    void Start()
    {
        lastKey = 0;
        DamageInMemory = new List<Damage>();
        //Debug_HitSign.SetActive(false);
    }

    public void Receive(Damage d)
    {
        //Debug_HitSign.SetActive(!Debug_HitSign.activeSelf);
        if (d._key != lastKey)
        {
            lastKey = d._key;
            Debug.Log("Hit power: " + d._power);
            DamageInMemory.Add(d);
        }
    }

    public List<Damage> AnalyseCache()
    {
        List<Damage> res = DamageInMemory;
        DamageInMemory = new List<Damage>();
        return res;
    }
}
