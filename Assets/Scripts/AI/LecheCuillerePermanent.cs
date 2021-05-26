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
    private GameObject PLAYER;

    [Header("Conv")]
    public ConversationInfo ConvCureDent;
    public ConversationInfo ConvPorteSortieEnPoche;
    public ConversationInfo ConvPorteSortiePasTrouvee;

    void Start()
    {
        PLAYER = GameObject.FindGameObjectWithTag("Player");
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
            //gameObject.transform.position = Spots[SaveData.current.LecheCuillereTutoSpot].transform.position;
            NavAgentLutin.Warp(Spots[SaveData.current.LecheCuillereTutoSpot].transform.position);
        }
    }

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

        if (!isWalking && !LA.dontLook &&Vector3.Distance(transform.position, PLAYER.transform.position) > 7f)
        {
            LA.dontLook = true;
        }

        if(SaveData.current.Achievements_CureDentTuto && SaveData.current.LecheCuillereTutoSpot != 2)
        {
            SaveData.current.LecheCuillereTutoSpot = 2;
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

    public void TryToInteract( MenuManager MM)
    {
        if (!isWalking && !MM.isInDialogue())
        {
            if(SaveData.current.LecheCuillereTutoSpot == 1)
            {
                MM.DIALOGUE_OpenDialogue(ConvCureDent);
                LA.dontLook = false;
            }
            else if (SaveData.current.LecheCuillereTutoSpot == 2 && SaveData.current.ItemSacocheUnique[0])
            {
                MM.DIALOGUE_OpenDialogue(ConvPorteSortieEnPoche);
                LA.dontLook = false;
            }
            else if (SaveData.current.LecheCuillereTutoSpot == 2 && !SaveData.current.ItemSacocheUnique[0])
            {
                MM.DIALOGUE_OpenDialogue(ConvPorteSortiePasTrouvee);
                LA.dontLook = false;
            }
        }
    }
}
