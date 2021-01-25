using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] Spawn;
    void Start()
    {
        if(Spawn.Length> SaveData.current.spawnInt)
        {
            transform.position = Spawn[SaveData.current.spawnInt].position;
        }
       
    }
}
