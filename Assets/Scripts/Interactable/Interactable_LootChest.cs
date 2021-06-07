using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Interactable_LootChest : Interactable
{
    private ElliotSoundSystem ESS;
    

    public RelevantEntity RE;

    public GameObject Loot;
    public int Quantity;

    public GameObject SPOT_Spawn;
    public GameObject SPOT_Destination;

    public Transform Joint;

    public AudioSource AsourceSimple;
    public AudioSource AsourceEffect;


    public sealed override void CustomStart()
    {
        ESS = GameObject.FindGameObjectWithTag("ElliotSoundSystem").GetComponent<ElliotSoundSystem>();
        used = false;
    }

    public sealed override void CustomUpdate()
    {
        
    }

    public sealed override void Interact()
    {
        if (!used)
        {
            used = true;
            RE.NotRelevantAnymore();
            AsourceSimple.Play();
            AsourceEffect.Play();
            Joint.DOLocalRotate(new Vector3(-70, 0, 0), 1.6f).OnComplete(() => { OpenningPart2(); });
        }
    }

    private void OpenningPart2()
    {
        for(int i = 0; i< Quantity; i++)
        {
            GameObject loot = GameObject.Instantiate(Loot, SPOT_Spawn.transform.position , Quaternion.identity);
            loot.transform.DOJump(SPOT_Destination.transform.position,2,1,0.7f);
        }
    }

    
}
