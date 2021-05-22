using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LecheCuillerePermanent : MonoBehaviour
{
    [Header("Inspector Info")]
    public Animator AnimLutin;
    public NavMeshAgent NavAgentLutin;
    public LookAt LA;
    public SoundManager_LecheCuillere SM;
    public GameObject[] Spots;
    private bool isWalking;

    void Start()
    {
        isWalking = false;
        if (SaveData.current.LecheCuillereTutoSpot == -1)
        {
            gameObject.SetActive(false);
        }
        else if (SaveData.current.LecheCuillereTutoSpot == 0)
        {
            SaveData.current.LecheCuillereTutoSpot = 1;
            //GoToSpot();
        }
        else
        {
            gameObject.transform.position = Spots[SaveData.current.LecheCuillereTutoSpot].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking && Vector3.Distance(transform.position, Spots[SaveData.current.LecheCuillereTutoSpot].transform.position) <=1f)
        {
            AnimLutin.SetTrigger("Idle");
            NavAgentLutin.SetDestination(transform.position);
            isWalking = false;
        }

        if(!isWalking && Vector3.Distance(transform.position, Spots[SaveData.current.LecheCuillereTutoSpot].transform.position) > 1f)
        {
            GoToSpot();
        }
    }

    void GoToSpot()
    {
        LA.dontLook = true;
        AnimLutin.SetTrigger("Walk");
        NavAgentLutin.SetDestination(Spots[SaveData.current.LecheCuillereTutoSpot].transform.position);
        isWalking = true;
    }
}
