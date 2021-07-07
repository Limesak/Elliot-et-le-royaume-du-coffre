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

    public void PopSort2()
    {
        PS_Sort1.Play();
        GameObject Poupou = Instantiate(Entity.InvocPoupou, Entity.transform.position, Quaternion.identity);
        Poupou.GetComponent<Poussierin>().WanderingPoints = Entity.Spots[Entity.OldSpot].WSpots;
        Instantiate(Entity.TeleportPS, Entity.transform.position, Quaternion.identity);
        Entity.NavAgent.Warp(Entity.Spots[Entity.CurrentSpot].Spawn.transform.position);
        Entity.WanderingPoints = Entity.Spots[Entity.CurrentSpot].WSpots;
        Instantiate(Entity.TeleportPS, Entity.Spots[Entity.CurrentSpot].Spawn.transform.position, Quaternion.identity);
    }
}
