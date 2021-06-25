using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagifolienAnimTransmitter : MonoBehaviour
{
    public Magifolien Entity;
    public ParticleSystem PS_Sort1;
    public GameObject PrefabSort1;
    public Transform BaguetteBarrel;

    public void CanWalk()
    {
        //Debug.Log("Attack done transmitter");
        Entity.CanWalk();
    }

    public void PopSort1()
    {
        PS_Sort1.Play();
        Instantiate(PrefabSort1, BaguetteBarrel.position, Quaternion.identity);
    }
}
