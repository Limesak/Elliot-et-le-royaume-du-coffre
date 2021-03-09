using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] Spawn;
    private GameObject PlayerFollower;
    private GameObject Cam;

    void Start()
    {
        transform.position = Spawn[SaveData.current.spawnInt].position;
        //Debug.Log("with player at  " + transform.position);
        PlayerFollower = GameObject.FindGameObjectWithTag("PlayerFollower");
        PlayerFollower.transform.position = transform.position;
        Cam = GameObject.FindGameObjectWithTag("CineMainCam");
        Cam.transform.position = transform.position;
        StartCoroutine(DelayedPositionChange());
    }

    IEnumerator DelayedPositionChange()
    {
        yield return null;
        //Debug.Log("Spawn = " + SaveData.current.spawnInt + " at " + Spawn[SaveData.current.spawnInt].position);
        transform.position = Spawn[SaveData.current.spawnInt].position;
        //Debug.Log("with player at  " + transform.position);
        PlayerFollower = GameObject.FindGameObjectWithTag("PlayerFollower");
        PlayerFollower.transform.position = transform.position;
        Cam = GameObject.FindGameObjectWithTag("CineMainCam");
        Cam.transform.position = transform.position;
    }
}
