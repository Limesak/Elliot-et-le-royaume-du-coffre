using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFlow : OnOffMachine
{
    private Vector3 Direction;
    public float speedXZ;
    public float speedY;
    private PlayerMovement PM;
    private bool PlayerInFlow;
    public GameObject[] ParticleSys;

    void Start()
    {
        Direction = transform.forward;
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (PlayerInFlow && isOn)
        {
            PM.SetAerianDir(new Vector3(Direction.x * speedXZ, Direction.y*speedY, Direction.z * speedXZ));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInFlow = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInFlow = false;
        }
    }

    void SetPS(bool b)
    {
        for(int i = 0; i < ParticleSys.Length; i++)
        {
            ParticleSys[i].SetActive(b);
        }
    }

    public sealed override void OnOff(bool b)
    {
        isOn = b;
        SetPS(b);
    }
}
